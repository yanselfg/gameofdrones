using GameOfDrones.Core.Entities;
using System.Collections.Generic;

namespace GameOfDrones.Core.Interfaces
{
    /// <summary>
    /// IRoundService interface
    /// </summary>
    public partial interface IRoundService
    {
        /// <summary>
        /// Inserts an Round
        /// </summary>
        /// <param name="round">Round</param>
        void InsertRound(Round round);

        /// <summary>
        /// Updates the Round
        /// </summary>
        /// <param name="round">Round</param>
        void UpdateRound(Round round);

        /// <summary>
        /// Deletes a Round
        /// </summary>
        /// <param name="round">Round</param>
        void DeleteRound(Round round);

        /// <summary>
        /// Gets a Round
        /// </summary>
        /// <param name="roundId">Round identifier</param>
        /// <returns>Round</returns>
        Round GetRoundById(int moveId);

        /// <summary>
        /// List all Rounds
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Rounds</returns>
        IList<Round> GetAllRounds(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Amount of rounds won by a player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        int RoundsWonByPlayer(int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        int GameRoundsWonByPlayer(int gameId, int playerId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moveOne"></param>
        /// <param name="moveTwo"></param>
        /// <returns></returns>
        int GetWinnerMoveIndex(string moveOne, string moveTwo);
    }
}
