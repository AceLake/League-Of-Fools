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

        public AccountDAO()
        {
            var client = new MongoClient("mongodb+srv://Ace:squirty115@cluster0.og5dfyn.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("league-of-fools");
            _accounts = database.GetCollection<AccountModel>("users");
        }

        public async Task Create(AccountModel user)
        {
            await _accounts.InsertOneAsync(user);
        }

        

        public AccountModel GetById(string id)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            return _accounts.Find(filter).FirstOrDefault();
        }

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

        public List<AccountModel> GetAll()
        {
            return _accounts.Find(_ => true).ToList();
        }

        public void Update(string id, AccountModel post)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            _accounts.ReplaceOne(filter, post);
        }

        public void Delete(string id)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.ID, id);
            _accounts.DeleteOne(filter);
        }

        
    }
}
