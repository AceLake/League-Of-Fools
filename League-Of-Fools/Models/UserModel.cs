using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace League_Of_Fools.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public SummonerModel MySommoner { get; set; }
        public List<SummonerModel> FollowedSommoners { get; set; }
    }
}
