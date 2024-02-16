namespace League_Of_Fools.Models
{
    public class ChampionMasteryEntry
    {
        /// <summary>
        /// Player Universal Unique Identifier. Exact length of 78 characters. (Encrypted)
        /// </summary>
        public string Puuid { get; set; }
        /// <summary>
        /// Number of points needed to achieve next level. Zero if player reached maximum champion level for this champion.
        /// </summary>
        public long ChampionPointsUntilNextLevel { get; set; }
        /// <summary>
        /// Is chest granted for this champion or not in the current season.
        /// </summary>
        public bool ChestGranted { get; set; }
        /// <summary>
        /// Champion ID for this entry.
        /// </summary>
        public long ChampionId { get; set; }
        /// <summary>
        /// Last time this champion was played by this player - in Unix milliseconds time format.
        /// </summary>
        public long LastPlayTime { get; set; }
        /// <summary>
        /// Champion level for the specified player and champion combination.
        /// </summary>
        public int ChampionLevel { get; set; }
        /// <summary>
        /// Summoner ID for this entry. (Encrypted)
        /// </summary>
        public string SummonerId { get; set; }
        /// <summary>
        /// Total number of champion points for this player and champion combination - they are used to determine championLevel.
        /// </summary>
        public int ChampionPoints { get; set; }
        /// <summary>
        /// Number of points earned since the current level has been achieved.
        /// </summary>
        public long ChampionPointsSinceLastLevel { get; set; }
        /// <summary>
        /// The token earned for this champion at the current championLevel. When the championLevel is advanced, the tokensEarned resets to 0.
        /// </summary>
        public int TokensEarned { get; set; }
    }
}
