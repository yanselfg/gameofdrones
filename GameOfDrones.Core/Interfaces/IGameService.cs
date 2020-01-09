using GameOfDrones.Core.Entities;
using System.Collections.Generic;

namespace GameOfDrones.Core.Interfaces
{
    /// <summary>
    /// IGameService interface
    /// </summary>
    public partial interface IGameService
    {
        /// <summary>
        /// Inserts an Game
        /// </summary>
        /// <param name="game">Game</param>
        void InsertGame(Game game);

        /// <summary>
        /// Updates the Game
        /// </summary>
        /// <param name="game">Game</param>
        void UpdateGame(Game game);

        /// <summary>
        /// Deletes a Game
        /// </summary>
        /// <param name="game">Game</param>
        void DeleteGame(Game game);

        /// <summary>
        /// Gets a Game
        /// </summary>
        /// <param name="gameId">Game identifier</param>
        /// <returns>Game</returns>
        Game GetGameById(int gameId);

        /// <summary>
        /// List all Games
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Games</returns>
        IList<Game> GetAllGames(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Get amount of games won by a player
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        int GamesWonByPlayer(int playerId);

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
        /// <param name="gameId"></param>
        /// <returns></returns>
        int GetNextRoundNumber(int gameId);
    }
}
