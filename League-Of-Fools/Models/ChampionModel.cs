namespace League_Of_Fools.Models
{
    public class ChampionModel
    {
        public string Version { get; set; }
        public string Id { get; set; }
        public int Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public string Lore { get; set; }
        public ChampIconModel Image { get; set; }
        public List<SkinModel> Skins { get; set; }


    }
}
