using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public class AccountService : IAccountService
    {
        private AccountDAO _accountDAO;
        public AccountService(AccountDAO accountDAO) 
        {
            _accountDAO = accountDAO;
        }
        public bool AddAccount(AccountModel newAccount)
        {
            return false;
        }

        public void addUserToList(SummonerModel userToAdd, AccountModel account)
        {
            
        }

        public AccountModel getUserByID(string userID)
        {
            return null;
        }

        public AccountModel LoginAccount(string username, string password)
        {
            return null;
        }
    }
}
