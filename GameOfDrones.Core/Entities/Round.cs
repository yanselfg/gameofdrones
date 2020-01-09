namespace GameOfDrones.Core.Entities
{
    /// <summary>
    /// Round class
    /// </summary>
    public partial class Round : Entity
    {
        /// <summary>
        /// Id of the Game where the round belongs
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Game where the round belongs
        /// </summary>
        public virtual Game Game { get; set; }

        /// <summary>
        /// Id of the Round winner
        /// </summary>
        public int WinnerId { get; set; }

        /// <summary>
        /// Round winner
        /// </summary>
        public virtual Player Winner { get; set; }
    }
}
