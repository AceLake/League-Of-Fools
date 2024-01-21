namespace League_Of_Fools.Models
{
    public class ChampRootObject
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string Version { get; set; }
        public Dictionary<string, ChampionModel> Data { get; set; }
    }
}
