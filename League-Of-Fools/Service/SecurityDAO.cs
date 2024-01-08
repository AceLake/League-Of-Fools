using MongoDB.Driver;
using League_Of_Fools.Models;
using System.Data.SqlClient;

namespace League_Of_Fools.Service
{
    public class SecurityDAO
    {

        private readonly IMongoCollection<UserModel> _users;

        public SecurityDAO()
        {
            var client = new MongoClient("mongodb+srv://Ace:squirty115@cluster0.og5dfyn.mongodb.net/?retryWrites=true&w=majority");
            var database = client.GetDatabase("insta-gramp-2");
            _users = database.GetCollection<UserModel>("users");
        }

        public async Task Create(UserModel user)
        {
            await _users.InsertOneAsync(user);
        }

        

        public UserModel GetById(string id)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Id, id);
            return _users.Find(filter).FirstOrDefault();
        }

        public bool GetByNameAndPassword(UserModel user)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Username, user.Username) &
                         Builders<UserModel>.Filter.Eq(u => u.Password, user.Password);
            return _users.Find(filter).Any();
        }

        public List<UserModel> GetAll()
        {
            return _users.Find(_ => true).ToList();
        }

        public void Update(string id, UserModel post)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Id, id);
            _users.ReplaceOne(filter, post);
        }

        public void Delete(string id)
        {
            var filter = Builders<UserModel>.Filter.Eq(u => u.Id, id);
            _users.DeleteOne(filter);
        }

        
    }
}
