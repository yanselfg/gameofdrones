using System.Collections.Generic;

namespace GameOfDrones.Web.ViewModels
{
    public partial class GameViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WinnerName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowWinner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RoundViewModel CurrentRound { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<PlayerViewModel> Players { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<RoundViewModel> Rounds { get; set; }
    }
}
