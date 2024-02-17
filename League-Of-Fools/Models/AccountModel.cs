namespace League_Of_Fools.Models
{
    public class AccountModel
    {
        public string ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<SummonerModel> FollowedUsers { get; set; }

        public AccountModel(string iD, string username, string password, List<SummonerModel> followedUsers)
        {
            ID = iD;
            Username = username;
            Password = password;
            FollowedUsers = followedUsers;
        }
        public AccountModel(string username, string password)
        {
            ID = "";
            Username = username;
            Password = password;
            FollowedUsers = new List<SummonerModel>();
        }

        public override string? ToString()
        {
            return "id: " + ID + " username: " + Username + " password: " + Password + " followedUsers Count: " + FollowedUsers.Count;
        }
    }
}
