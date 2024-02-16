namespace League_Of_Fools.Models
{
    public class ChampionMasteryEntry
    {
        public string Puuid { get; set; } // Player Universal Unique Identifier. Exact length of 78 characters. (Encrypted)
        public long ChampionPointsUntilNextLevel { get; set; } // Number of points needed to achieve next level. Zero if player reached maximum champion level for this champion.
        public bool ChestGranted { get; set; } // Is chest granted for this champion or not in the current season.
        public long ChampionId { get; set; } // Champion ID for this entry.
        public long LastPlayTime { get; set; } // Last time this champion was played by this player - in Unix milliseconds time format.
        public int ChampionLevel { get; set; } // Champion level for the specified player and champion combination.
        public string SummonerId { get; set; } // Summoner ID for this entry. (Encrypted)
        public int ChampionPoints { get; set; } // Total number of champion points for this player and champion combination - they are used to determine championLevel.
        public long ChampionPointsSinceLastLevel { get; set; } // Number of points earned since the current level has been achieved.
        public int TokensEarned { get; set; } // The token earned for this champion at the current championLevel. When the championLevel is advanced, the tokensEarned resets to 0.
    }
}
