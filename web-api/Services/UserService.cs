using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using DEDrake.Services.Utils;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Services {
  public class UserService : IUserService {

    private readonly IMongoCollection<UserDocument> _collection;
    private UserCacheService _cache;

    public UserService(IConfiguration config, string collection) {
      var endpoint = config.GetValue<string>("MongoDB:Endpoint");
      var database = config.GetValue<string>("MongoDB:Database");

      var client = new MongoClient(endpoint);
      var db = client.GetDatabase(database);
      _collection = db.GetCollection<UserDocument>(collection);
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync() {
      if (_cache != null) {
        var users = await _cache.GetAsync();
        if (users.Count() > 0) { return users; }
      }

      var cursor = await _collection.FindAsync(x => true);
      return await cursor.ToListAsync();
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync(Expression<Func<IUserDocument, bool>> predicate) {
      if (_cache != null) {
        var users = await _cache.GetAsync(predicate);
        if (users.Count() > 0) { return users; }
      }

      var query = ExpressionTransform<IUserDocument, UserDocument>.Transform(predicate);
      var cursor = await _collection.FindAsync(query);
      return await cursor.ToListAsync();
    }

    public async Task<IUserDocument> FindAsync(string id, string partitionKey = null) {
      if (_cache != null) {
        var user = await _cache.FindAsync(id, partitionKey);
        if (user != null) { return user; }
      }

      var cursor = await _collection.FindAsync(x => x.ID == id);
      return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IUserDocument> AddAsync(IUserDocument item) {
      _collection.InsertOne((UserDocument)item);

      if (_cache != null) {
        var user = await GetAsync(x => x.Email == item.Email);
        if (user.Count() == 1) { await _cache.AddAsync(user.First()); }
      }

      return await Task.FromResult(item);
    }

    public async Task<IUserDocument> UpdateAsync(string id, IUserDocument item) {
      var doc = (UserDocument)item;
      var result = await _collection.ReplaceOneAsync(x => x.ID == id, doc);

      if (result.IsAcknowledged && result.ModifiedCount == 1) {
        if (_cache != null) { await _cache.UpdateAsync(id, item); }
        return item;
      }

      return null;
    }

    public async Task<IUserDocument> DeleteAsync(string id, string partitionKey = null) {
      var user = await FindAsync(id, partitionKey);
      var result = await _collection.DeleteOneAsync(x => x.ID == id);

      if (result.IsAcknowledged && result.DeletedCount == 1) {
        if (_cache != null) { await _cache.DeleteAsync(id, partitionKey); }
        return user;
      }

      return null;
    }
  }

}
