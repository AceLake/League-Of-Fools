using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public class FakeAccountService : IAccountService
    {
        private List<AccountModel> _accounts;
        public FakeAccountService() 
        {
            _accounts = new List<AccountModel>();
            _accounts.Add(new AccountModel("1", "josh", "peck", new List<SummonerModel>
            {
                new SummonerModel("Oneshot369", "NA1","AMERICAS", "NA1")
            }));
            _accounts.Add(new AccountModel("2", "zach", "lake", new List<SummonerModel>
            {
                new SummonerModel("Oneshot369", "NA1","AMERICAS", "NA1")
            }));
            _accounts.Add(new AccountModel("3", "ace", "peck", new List<SummonerModel>
            {
                new SummonerModel("Oneshot369", "NA1","AMERICAS", "NA1")
            }));
        }
        public bool AddAccountAsync(AccountModel newAccount)
        {
            _accounts.Add(newAccount);
            return true;
        }

        public void addUserToList(SummonerModel userToAdd, AccountModel account)
        {
            account.FollowedUsers.Add(userToAdd);
        }

        public void AddUserToList(SummonerModel userToAdd, AccountModel account)
        {
            throw new NotImplementedException();
        }

        public AccountModel getUserByID(string userID)
        {
            AccountModel user = _accounts.FirstOrDefault(a => a.ID == userID);
            return user;
        }

        public AccountModel LoginAccount(string username, string password)
        {
            return _accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        }

        public void RemoveUserFromList(SummonerModel userToAdd, AccountModel account)
        {
            throw new NotImplementedException();
        }
    }
}
