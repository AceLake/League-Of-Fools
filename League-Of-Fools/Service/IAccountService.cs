using League_Of_Fools.Models;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace League_Of_Fools.Service
{
    public interface IAccountService
    {
        /// <summary>
        /// Retruns null if no password found
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountModel LoginAccount(string username, string password);
        public bool AddAccountAsync(AccountModel newAccount);
        public void addUserToList(SummonerModel userToAdd, AccountModel account);
        /// <summary>
        /// This can retun null
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public AccountModel getUserByID(string userID);
    }
}
