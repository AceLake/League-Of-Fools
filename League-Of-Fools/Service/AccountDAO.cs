using MongoDB.Driver;
using League_Of_Fools.Models;
using System.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace League_Of_Fools.Service
{
    public class AccountDAO
    {

        private readonly IMongoCollection<AccountModel> _accounts;
        /// <summary>
        /// initalize the Db connection
        /// </summary>
        public AccountDAO()
        {
            var client = new MongoClient("mongodb+srv://Ace:squirty115@cluster0.og5dfyn.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("league-of-fools");
            _accounts = database.GetCollection<AccountModel>("users");
        }
        /// <summary>
        /// create an account
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Create(AccountModel user)
        {
            await _accounts.InsertOneAsync(user);
        }

        /// <summary>
        /// get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AccountModel GetById(string id)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            return _accounts.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// get accountModel by username and PW
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public AccountModel GetByNameAndPassword(AccountModel user)
        {
            var filterBuilder = Builders<AccountModel>.Filter;
            var filter = Builders<AccountModel>.Filter.Eq(u => u.Username, user.Username) &
                         Builders<AccountModel>.Filter.Eq(u => u.Password, user.Password);
            AccountModel accountToReturn = null;
            try
            {
                accountToReturn=_accounts.Find(filter).First();
            }
            catch (Exception ex)
            {
                //inccorect login
            }
            return accountToReturn;
        }
        /// <summary>
        /// updates a account based on the ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="post"></param>
        public void Update(string id, AccountModel post)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            _accounts.ReplaceOne(filter, post);
        }
        /// <summary>
        /// Delete a user (not currently used)
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            _accounts.DeleteOne(filter);
        }

        
    }
}
