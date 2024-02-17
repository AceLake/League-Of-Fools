using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public class AccountService : IAccountService
    {
        private AccountDAO _accountDAO;
        //inject the Account DAO
        public AccountService(AccountDAO accountDAO) 
        {
            _accountDAO = accountDAO;
        }
        /// <summary>
        /// adds and account to the DB
        /// </summary>
        /// <param name="newAccount"></param>
        /// <returns></returns>
        public bool AddAccountAsync(AccountModel newAccount)
        {
            //create an account
            _accountDAO.Create(newAccount);
            return true;
        }
        /// <summary>
        /// adda a user to an accounts list
        /// </summary>
        /// <param name="userToAdd"></param>
        /// <param name="account"></param>
        public void AddUserToList(SummonerModel userToAdd, AccountModel account)
        {
            //see if there the account is already in the list
            SummonerModel copy = account.FollowedUsers.Find(a => a.GameName == userToAdd.GameName);
            if (copy == null)
            {
                // if the account is not in the list then add it
                account.FollowedUsers.Add(userToAdd);
                _accountDAO.Update(account.ID, account);
            }
        }
        /// <summary>
        /// removes a user form an accounts list
        /// </summary>
        /// <param name="userToRemove"></param>
        /// <param name="account"></param>
        public void RemoveUserFromList(SummonerModel userToRemove, AccountModel account)
        {
            //remove all accounts with the same gamename
            account.FollowedUsers.RemoveAll(u => u.GameName == userToRemove.GameName);
            _accountDAO.Update(account.ID, account);
        }
        /// <summary>
        /// gets user by ID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AccountModel getUserByID(string userID)
        {
            //get one user by ID
            AccountModel user =_accountDAO.GetById(userID);
            return user;
        }
        /// <summary>
        /// logs into accout based on username and PW, returns thier user model
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountModel LoginAccount(string username, string password)
        {
            //get one user by their login credentail
            AccountModel account = new AccountModel(username, password);
            account = _accountDAO.GetByNameAndPassword(account);
            return account;
        }
    }
}
