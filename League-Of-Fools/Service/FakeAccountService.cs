using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public class FakeAccountService : IAccountService
    {
        private List<AccountModel> _accounts;
        public FakeAccountService() 
        {
            _accounts = new List<AccountModel>();
            _accounts.Add(new AccountModel(0, "josh", "peck", new List<string>
            {
                "1", "2", "3"
            }));
            _accounts.Add(new AccountModel(1, "zach", "lake", new List<string>
            {
                "1", "2", "3"
            }));
            _accounts.Add(new AccountModel(2, "ace", "peck", new List<string>
            {
                "1" , "2" , "3"
            }));
        }
        public bool AddAccount(AccountModel newAccount)
        {
            _accounts.Add(newAccount);
            return true;
        }

        public void addUserToList(UserModel userToAdd, AccountModel account)
        {
            _accounts.FirstOrDefault(a => a.ID == account.ID).FollowedUsers.Add(userToAdd.Id);
        }

        public AccountModel getUserByID(int userID)
        {
            AccountModel user = _accounts.FirstOrDefault(a => a.ID == userID);
            return user;
        }

        public AccountModel LoginAccount(string username, string password)
        {
            return _accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
        }
    }
}
