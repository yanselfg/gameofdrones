namespace GameOfDrones.Web.ViewModels
{
    /// <summary>
    /// RankItemViewModel class
    /// </summary>
    public class RankItemViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Amount of Games won
        /// </summary>
        public int WonGames { get; set; }

        /// <summary>
        /// Amount of Rounds won
        /// </summary>
        public int WonRounds { get; set; }
    }
}
