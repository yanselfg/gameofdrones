using GameOfDrones.Core.Entities;
using GameOfDrones.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfDrones.Core.Services
{
    public partial class RoundService : IRoundService
    {
        #region Fields

        private readonly ILogService _logService;
        private readonly IRepository<Round> _roundRepository;

        #endregion

        #region Ctor

        public RoundService(
            ILogService logService, 
            IRepository<Round> roundRepository)
        {
            _logService = logService;
            _roundRepository = roundRepository;
        }

        #endregion

        #region Methods

        public void InsertRound(Round round)
        {
            if (round == null)
            {
                throw new ArgumentNullException(nameof(round));
            }

            _roundRepository.Insert(round);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Round Added"
            });
        }

        public void UpdateRound(Round round)
        {
            if (round == null)
            {
                throw new ArgumentNullException(nameof(round));
            }

            _roundRepository.Update(round);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Round Updated"
            });
        }

        public void DeleteRound(Round round)
        {
            if (round == null)
            {
                throw new ArgumentNullException(nameof(round));
            }

            _roundRepository.Delete(round);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Round Deleted"
            });
        }

        public Round GetRoundById(int roundId)
        {
            if (roundId == 0)
            {
                return null;
            }

            return _roundRepository.GetById(roundId);
        }

        public IList<Round> GetAllRounds(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _roundRepository.Table;
            var rounds = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return rounds;
        }

        public int RoundsWonByPlayer(int playerId)
        {
            var query = _roundRepository.Table;

            return query.Where(r => r.WinnerId == playerId).Count();
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

            return _roundRepository
                .Table
                .Count(r => r.GameId == gameId && r.WinnerId == playerId);
        }

        public int GetWinnerMoveIndex(string moveOne, string moveTwo)
        {
            if(string.IsNullOrEmpty(moveOne) || string.IsNullOrEmpty(moveTwo))
            {
                throw new ArgumentNullException(string.IsNullOrEmpty(moveOne) ? nameof(moveOne) : nameof(moveTwo));
            }

            var result = 0;

            if(moveOne != moveTwo)
            {
                if (moveOne == "Rock" && moveTwo == "Paper")
                {
                    result = 2;
                }
                else if (moveOne == "Rock" && moveTwo == "Scissors")
                {
                    result = 1;
                }
                else if (moveOne == "Paper" && moveTwo == "Rock")
                {
                    result = 1;
                }
                else if (moveOne == "Paper" && moveTwo == "Scissors")
                {
                    result = 2;
                }
                else if (moveOne == "Scissors" && moveTwo == "Rock")
                {
                    result = 2;
                }
                else if (moveOne == "Scissors" && moveTwo == "Paper")
                {
                    result = 1;
                }
                else
                {
                    // do nothing because moveOne == moveTwo then return 0
                }
            }

            return result;
        }

        #endregion


    }
}
