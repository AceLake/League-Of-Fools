using MongoDB.Driver;
using League_Of_Fools.Models;
using System.Data.SqlClient;

namespace League_Of_Fools.Service
{
    public class AccountDAO
    {

        private readonly IMongoCollection<AccountModel> _accounts;

        public AccountDAO()
        {
            var client = new MongoClient("mongodb+srv://Ace:squirty115@cluster0.og5dfyn.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("insta-gramp-2");
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

        public bool GetByNameAndPassword(AccountModel user)
        {
            var filter = Builders<AccountModel>.Filter.Eq(u => u.Username, user.Username) &
                         Builders<AccountModel>.Filter.Eq(u => u.Password, user.Password);
            return _accounts.Find(filter).Any();
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
