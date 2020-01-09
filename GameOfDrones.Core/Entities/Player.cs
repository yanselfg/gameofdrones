using System;
using System.Collections.Generic;

namespace GameOfDrones.Core.Entities
{
    /// <summary>
    /// Player class
    /// </summary>
    public partial class Player : Entity
    {
        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }

        ///// <summary>
        ///// Gender of the player
        ///// </summary>
        //public Gender Gender { get; set; }

        ///// <summary>
        ///// Name of the Gender of the Player
        ///// </summary>
        //public string GenderName
        //{
        //    get
        //    {
        //        return Enum.GetName(typeof(Gender), Gender);
        //    }
        //}

        /// <summary>
        /// Games won by the Player
        /// </summary>
        public virtual ICollection<Game> GamesWon { get; set; }
    }
}
