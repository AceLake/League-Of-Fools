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
        public bool AddAccountAsync(AccountModel newAccount)
        {
            _accountDAO.Create(newAccount);
            return true;
        }

        public void AddUserToList(SummonerModel userToAdd, AccountModel account)
        {
            SummonerModel copy = account.FollowedUsers.Find(a => a.GameName == userToAdd.GameName);
            if (copy == null)
            {
                account.FollowedUsers.Add(userToAdd);
                _accountDAO.Update(account.ID, account);
            }
        }
        public void RemoveUserFromList(SummonerModel userToRemove, AccountModel account)
        {
            account.FollowedUsers.RemoveAll(u => u.GameName == userToRemove.GameName);
            _accountDAO.Update(account.ID, account);
        }

        public AccountModel getUserByID(string userID)
        {
            AccountModel user =_accountDAO.GetById(userID);
            return user;
        }

        public AccountModel LoginAccount(string username, string password)
        {
            AccountModel account = new AccountModel(username, password);
            account = _accountDAO.GetByNameAndPassword(account);
            return account;
        }
    }
}
