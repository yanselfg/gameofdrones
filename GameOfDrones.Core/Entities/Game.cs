using System.Collections.Generic;

namespace GameOfDrones.Core.Entities
{
    /// <summary>
    /// Game class
    /// </summary>
    public partial class Game : Entity
    {
        /// <summary>
        /// Id of the Game winner
        /// </summary>
        public int? WinnerId { get; set; }

        /// <summary>
        /// Game winner
        /// </summary>
        public virtual Player Winner { get; set; }

        /// <summary>
        /// All rounds of the Game
        /// </summary>
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
