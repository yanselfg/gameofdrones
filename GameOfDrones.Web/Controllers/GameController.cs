using GameOfDrones.Core.Entities;
using GameOfDrones.Core.Interfaces;
using GameOfDrones.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfDrones.Web.Controllers
{
    [Route("api/[controller]")]
    public class GameController : BaseController
    {
        private readonly IGameService _gameService;
        private readonly IRoundService _roundService;
        private readonly IPlayerService _playerService;
        private readonly ILogService _logService;

        public GameController(
            IGameService gameService,
            IRoundService roundService,
            IPlayerService playerService,
            ILogService logService)
        {
            _gameService = gameService;
            _roundService = roundService;
            _playerService = playerService;
            _logService = logService;
        }

        [HttpPost("{action}")]
        public IActionResult Create(GameViewModel model)
        {
            try
            {
                if (!ModelState.IsValid || model == null)
                {
                    return ErrorResponse("Invalid model");
                }

                var playerOne = _playerService.GetPlayerByName(model.Players[0].Name);

                if (playerOne == null)
                {
                    playerOne = new Player
                    {
                        Name = model.Players[0].Name
                    };

                    _playerService.InsertPlayer(playerOne);
                }

                var playerTwo = _playerService.GetPlayerByName(model.Players[1].Name);

                if (playerTwo == null)
                {
                    playerTwo = new Player
                    {
                        Name = model.Players[1].Name
                    };

                    _playerService.InsertPlayer(playerTwo);
                }

                var game = new Game();
                _gameService.InsertGame(game);
                // Prepare model        
                var newModel = PrepareGameModel(game.Id, model, new[] 
                {
                    playerOne, playerTwo
                });

                return Json(newModel);
            }
            catch(Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                SaveActivity("Create Game", "ERROR", message);

                return ErrorResponse(message);
            }
        }

        [HttpPost("{action}")]
        public IActionResult AddRound(GameViewModel model)
        {
            try
            {
                if (!ModelState.IsValid || model == null)
                {
                    return ErrorResponse("Invalid model");
                }

                var round = new Round
                {
                    Id = 0,
                    GameId = model.Id
                };

                var index = _roundService.GetWinnerMoveIndex(model.CurrentRound.PlayersMoves[0].MoveName, model.CurrentRound.PlayersMoves[1].MoveName);
                var lastRound = model.CurrentRound;

                if(index == 0)
                {
                    lastRound.WinnerMove = lastRound.PlayersMoves[0].MoveName; // Tied
                }
                else
                {
                    round.WinnerId = model.Players[index - 1].Id;
                    _roundService.InsertRound(round);
                    lastRound.WinnerName = lastRound.PlayersMoves[index - 1].PlayerName;
                    lastRound.WinnerMove = lastRound.PlayersMoves[index - 1].MoveName;
                }

                model.Rounds.Add(lastRound);
                // Prepare another round
                var nextRoundNumber = lastRound.Number + 1;

                if (nextRoundNumber > 3)
                {
                    var winnerId = 0;

                    if (_roundService.GameRoundsWonByPlayer(model.Id, model.Players[0].Id) == 3)
                    {
                        winnerId = model.Players[0].Id;
                        model.WinnerName = model.Players[0].Name;
                    }
                    else if (_roundService.GameRoundsWonByPlayer(model.Id, model.Players[1].Id) == 3)
                    {
                        winnerId = model.Players[1].Id;
                        model.WinnerName = model.Players[1].Name;
                    }

                    if (!string.IsNullOrEmpty(model.WinnerName))
                    {
                        model.ShowWinner = true;
                        var game = _gameService.GetGameById(model.Id);

                        if(game != null)
                        {
                            game.WinnerId = winnerId;
                            _gameService.UpdateGame(game);
                        }
                    }
                }

                if(!model.ShowWinner)
                {
                    model.CurrentRound = new RoundViewModel
                    {
                        Id = 0,
                        GameId = lastRound.GameId,
                        Number = nextRoundNumber,
                        WinnerName = "",
                        WinnerMove = "",
                        ShowScore = nextRoundNumber > 1,
                        PlayersMoves = new List<PlayerMoveViewModel>(new[]
                        {
                            new PlayerMoveViewModel
                            {
                                MoveName = "",
                                PlayerName = model.Players[0].Name,
                                Selected = false
                            },
                            new PlayerMoveViewModel
                            {
                                MoveName = "",
                                PlayerName = model.Players[1].Name,
                                Selected = false
                            }
                        })
                    };
                }

                return Json(model);
            }
            catch(Exception ex)
            {
                var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
                SaveActivity("Add Round", "ERROR", message);

                return ErrorResponse(message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Ranking()
        {
            try
            {
                var viewModel = new List<RankItemViewModel>();
                var players = _playerService.GetAllPlayers().Select(p => p);
                var count = 0;

                foreach (var p in players)
                {
                    viewModel.Add(new RankItemViewModel
                    {
                        PlayerName = p.Name,
                        WonGames = _gameService.GamesWonByPlayer(p.Id),
                        WonRounds = _roundService.RoundsWonByPlayer(p.Id)
                    });

                    count++;
                }

                var position = 0;
                var orderedList = viewModel.OrderByDescending(vm => vm.WonGames).ThenByDescending(vm => vm.WonRounds);

                foreach (var item in orderedList)
                {
                    item.Position = position + 1;
                    position++;
                }

                return Json(orderedList.ToList());
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Logs()
        {
            try
            {
                var viewModel = new List<LogItemViewModel>();
                var logs = _logService.GetAllLogs().Select(l => new LogItemViewModel
                {
                    Date = l.Date.ToShortDateString(),
                    Level = l.Level,
                    Message = l.Message
                });

                return Json(logs.ToList());
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        private GameViewModel PrepareGameModel(int gameId, GameViewModel model, Player[] players)
        {
            var gameModel = new GameViewModel
            {
                Id = gameId                
            };

            var nextRoundNumber = _gameService.GetNextRoundNumber(gameId);

            if (nextRoundNumber >= 3)
            {
                if (_gameService.GamesWonByPlayer(players[0].Id) == 3)
                {
                    gameModel.WinnerName = players[0].Name;
                }
                else if (_gameService.GamesWonByPlayer(players[0].Id) == 3)
                {
                    gameModel.WinnerName = players[1].Name;
                }

                if (!string.IsNullOrEmpty(gameModel.WinnerName))
                {
                    gameModel.ShowWinner = true;
                }
            }
            else
            {
                gameModel.Players = new List<PlayerViewModel>(new[]
                {
                    new PlayerViewModel
                    {
                        Id = players[0].Id,
                        Name = players[0].Name
                    },
                    new PlayerViewModel
                    {
                        Id = players[1].Id,
                        Name = players[1].Name
                    }
                });

                gameModel.CurrentRound = new RoundViewModel
                {
                    Id = 0,
                    GameId = gameId,
                    Number = _gameService.GetNextRoundNumber(gameId),
                    WinnerName = "",
                    WinnerMove = "",
                    ShowScore = false,
                    PlayersMoves = new List<PlayerMoveViewModel>(new[] 
                    {
                        new PlayerMoveViewModel
                        {
                            MoveName = "",
                            PlayerName = players[0].Name
                        },
                        new PlayerMoveViewModel
                        {
                            MoveName = "",
                            PlayerName = players[1].Name
                        }
                    })
                };

                gameModel.Rounds = new List<RoundViewModel>();
            }

            return gameModel;
        }

        private void SaveActivity(string action, string level, string message)
        {
            try
            {
                _logService.InsertLog(new Log
                {
                    Date = DateTime.UtcNow,
                    Level = level,
                    Message = message
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}