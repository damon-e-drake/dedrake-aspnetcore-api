using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using DEDrake.Services.Utils;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Services {
  public class UserService : IUserService {

    private readonly IMongoCollection<UserDocument> _collection;

    public UserService(IConfiguration config, string collection) {
      var endpoint = config.GetValue<string>("MongoDB:Endpoint");
      var database = config.GetValue<string>("MongoDB:Database");

      var client = new MongoClient(endpoint);
      var db = client.GetDatabase(database);
      _collection = db.GetCollection<UserDocument>(collection);
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync() {
      var cursor = await _collection.FindAsync(x => true);
      return await cursor.ToListAsync();
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync(Expression<Func<IUserDocument, bool>> predicate) {
      var query = ExpressionTransform<IUserDocument, UserDocument>.Transform(predicate);
      var cursor = await _collection.FindAsync(query);
      return await cursor.ToListAsync();
    }

    public async Task<IUserDocument> FindAsync(string id, string partitionKey = null) {
      var cursor = await _collection.FindAsync(x => x.ID == id);
      return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IUserDocument> AddAsync(IUserDocument item) {
      _collection.InsertOne((UserDocument)item);
      return await Task.FromResult(item);
    }

    public async Task<IUserDocument> UpdateAsync(string id, IUserDocument item) {
      var doc = (UserDocument)item;
      var result = await _collection.ReplaceOneAsync(x => x.ID == id, doc);
      if (result.IsAcknowledged && result.ModifiedCount == 1) { return item; }
      return null;
    }

    public async Task<IUserDocument> DeleteAsync(string id, string partitionKey = null) {
      var user = await FindAsync(id, partitionKey);
      var result = await _collection.DeleteOneAsync(x => x.ID == id);
      if (result.IsAcknowledged && result.DeletedCount == 1) { return user; }
      return null;
    }
  }
}
