using GameOfDrones.Core.Entities;
using GameOfDrones.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfDrones.Core.Services
{
    public partial class GameService : IGameService
    {
        #region Fields

        private readonly ILogService _logService;
        private readonly IRepository<Game> _gameRepository;

        #endregion

        #region Ctor

        public GameService(
            ILogService logService, 
            IRepository<Game> gameRepository)
        {
            _logService = logService;
            _gameRepository = gameRepository;
        }

        #endregion

        #region Methods

        public void InsertGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            _gameRepository.Insert(game);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Game Created"
            });
        }

        public void UpdateGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            _gameRepository.Update(game);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Game Updated"
            });
        }

        public void DeleteGame(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            _gameRepository.Delete(game);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Game Deleted"
            });
        }

        public Game GetGameById(int gameId)
        {
            if (gameId == 0)
            {
                return null;
            }

            return _gameRepository.GetById(gameId);
        }

        public IList<Game> GetAllGames(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _gameRepository.Table;
            var games = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return games;
        }

        public int GamesWonByPlayer(int playerId)
        {
            if (playerId <= 0)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            var query = _gameRepository.Table;

            return query.Count(g => g.WinnerId.HasValue && g.WinnerId.Value == playerId);
        }

        public int GetNextRoundNumber(int gameId)
        {
            if (gameId <= 0)
            {
                throw new ArgumentNullException(nameof(gameId));
            }

            var game = _gameRepository.GetById(gameId);

            if (game == null)
            {
                return 0;
            }

            return game.Rounds == null ? 1 : game.Rounds.Count + 1;
        }

        public int GameRoundsWonByPlayer(int gameId, int playerId)
        {
            if (gameId <= 0)
            {
                throw new ArgumentNullException(nameof(gameId));
            }
            else if (playerId <= 0)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            var game = _gameRepository.Table.Where(g => g.Id == gameId).FirstOrDefault();

            if(game == null)
            {
                return 0;
            }

            return game.Rounds.Count(r => r.WinnerId == playerId);
        }

        #endregion
    }
}
