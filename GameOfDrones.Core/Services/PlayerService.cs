using GameOfDrones.Core.Entities;
using GameOfDrones.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfDrones.Core.Services
{
    /// <summary>
    /// PlayerService
    /// </summary>
    public partial class PlayerService : IPlayerService
    {
        #region Fields

        private readonly ILogService _logService;
        private readonly IRepository<Player> _playerRepository;

        #endregion

        #region Ctor

        public PlayerService(
            ILogService logService, 
            IRepository<Player> playerRepository)
        {
            _logService = logService;
            _playerRepository = playerRepository;
        }

        #endregion

        #region Methods

        public void InsertPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _playerRepository.Insert(player);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Player Created"
            });
        }

        public void UpdatePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _playerRepository.Update(player);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Player Updated"
            });
        }

        public void DeletePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            _playerRepository.Delete(player);
            _logService.InsertLog(new Log
            {
                Date = DateTime.UtcNow,
                Level = "INFORMATION",
                Message = "Player Deleted"
            });
        }

        public Player GetPlayerById(int playerId)
        {
            if (playerId == 0)
            {
                return null;
            }

            return _playerRepository.GetById(playerId);
        }

        public Player[] GetPlayersByIds(int[] playerIds)
        {
            if(playerIds == null)
            {
                throw new ArgumentNullException(nameof(playerIds));
            }
            else if (playerIds.Count() == 0)
            {
                return null;
            }

            var players = new List<Player>();

            foreach(var id in playerIds)
            {
                var player = _playerRepository.GetById(id);

                if(player != null)
                {
                    players.Add(player);
                }
            }

            return players.ToArray();
        }

        public Player GetPlayerByName(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
            {
                return null;
            }

            return _playerRepository.Table
                .Where(p => p.Name == playerName)
                .FirstOrDefault();
        }

        public IList<Player> GetAllPlayers(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _playerRepository.Table;
            var players = query.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return players;
        }

        #endregion
    }
}
