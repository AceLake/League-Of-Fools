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
        /// <summary>
        /// example PLATFORM:NA1 HOST: na1.api.riotgames.com
        /// </summary>
        public string PlatformRoutingValue { get; set; }
        /// <summary>
        /// example REGION:AMERICAS HOST: americas.api.riotgames.com
        /// </summary>
        public string RegionalRoutingValue { get; set; }
        public Task<List<ChampionModel>> Champions { get; set; }
        public Task<List<ChampionMasteryEntry>> CMEs { get; set; }

        public SummonerModel(string id, string accountId, string puuid, string name, string gameName, string tagLine, int profileIconId, long revisionDate, int summonerLevel, string platformRoutingValue, string regionalRoutingValue, Task<List<ChampionModel>> champions, Task<List<ChampionMasteryEntry>> cMEs)
        {
            Id = id;
            AccountId = accountId;
            Puuid = puuid;
            Name = name;
            GameName = gameName;
            TagLine = tagLine;
            ProfileIconId = profileIconId;
            RevisionDate = revisionDate;
            SummonerLevel = summonerLevel;
            PlatformRoutingValue = platformRoutingValue;
            RegionalRoutingValue = regionalRoutingValue;
            Champions = champions;
            CMEs = cMEs;
        }
        public SummonerModel()
        {
            
        }
        public SummonerModel(string gameName, string tagLine, string regionalRoutingValue, string platformRoutingValue)
        {
            GameName = gameName;
            TagLine = tagLine;
            PlatformRoutingValue = platformRoutingValue;
            RegionalRoutingValue = regionalRoutingValue;
            
        }
    }
}
