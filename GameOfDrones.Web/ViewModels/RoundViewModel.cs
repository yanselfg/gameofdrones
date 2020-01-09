using System.Collections.Generic;

namespace GameOfDrones.Web.ViewModels
{
    public class RoundViewModel
    {      
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WinnerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WinnerMove { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<PlayerMoveViewModel> PlayersMoves { get; set; }
    }
}
