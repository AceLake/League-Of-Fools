namespace League_Of_Fools.Models
{
    public class SummonerModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Puuid { get; set; }
        public string Name { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public int ProfileIconId { get; set; }
        public long RevisionDate { get; set; }
        public int SummonerLevel { get; set; }
        public int MyProperty { get; set; }
        public Task<List<ChampionModel>> Champions { get; set; }
    }
}
