namespace League_Of_Fools.Models
{
    public class ChampionMasteryEntry
    {
        public string Puuid { get; set; }
        public int ChampionId { get; set; }
        public int ChampionLevel { get; set; }
        public long ChampionPoints { get; set; }
        public long LastPlayTime { get; set; }
        public long ChampionPointsSinceLastLevel { get; set; }
        public long ChampionPointsUntilNextLevel { get; set; }
        public bool ChestGranted { get; set; }
        public int TokensEarned { get; set; }
        public string SummonerId { get; set; }
    }
}
