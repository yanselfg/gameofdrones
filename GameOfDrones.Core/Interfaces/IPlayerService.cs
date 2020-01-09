using GameOfDrones.Core.Entities;
using System.Collections.Generic;

namespace GameOfDrones.Core.Interfaces
{
    /// <summary>
    /// IPlayerService interface
    /// </summary>
    public partial interface IPlayerService
    {
        /// <summary>
        /// Inserts an Player
        /// </summary>
        /// <param name="move">Player</param>
        void InsertPlayer(Player player);

        /// <summary>
        /// Updates the Player
        /// </summary>
        /// <param name="player">Player</param>
        void UpdatePlayer(Player player);

        /// <summary>
        /// Deletes a Player
        /// </summary>
        /// <param name="player">Player</param>
        void DeletePlayer(Player player);

        /// <summary>
        /// Gets a Player
        /// </summary>
        /// <param name="playerId">Player identifier</param>
        /// <returns>Player</returns>
        Player GetPlayerById(int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerIds"></param>
        /// <returns></returns>
        Player[] GetPlayersByIds(int[] playerIds);

        /// <summary>
        /// Gets a Player
        /// </summary>
        /// <param name="playerId">Player identifier</param>
        /// <returns>Player</returns>
        Player GetPlayerByName(string playerName);

        /// <summary>
        /// List all Players
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IList<Player> GetAllPlayers(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
