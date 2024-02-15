﻿namespace League_Of_Fools.Models
{
    public class AccountModel
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> FollowedUsers { get; set; }

        public AccountModel(int iD, string username, string password, List<string> followedUsers)
        {
            ID = iD;
            Username = username;
            Password = password;
            FollowedUsers = followedUsers;
        }
        public AccountModel(string username, string password)
        {
            ID = -1;
            Username = username;
            Password = password;
            FollowedUsers = new List<string>();
        }

        public override string? ToString()
        {
            return "id: " + ID + " username: " + Username + " password: " + Password + " followedUsers Count: " + FollowedUsers.Count;
        }
    }
}