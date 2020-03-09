using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Services {
  public class UserCacheService : IUserService {
    private ConcurrentBag<UserDocument> _users;

    public UserCacheService() {
      _users = new ConcurrentBag<UserDocument>();
    }

    private bool ContainsEmail(string email) {
      var count = _users.Count(x => x.Email == email.ToLower());
      return count > 0;
    }

    public void InitData(IEnumerable<IUserDocument> data) {
      foreach (var user in data) {
        _users.Add((UserDocument)user);
      }
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync() {
      return await Task.FromResult(_users.ToList());
    }

    public async Task<IEnumerable<IUserDocument>> GetAsync(Expression<Func<IUserDocument, bool>> predicate) {
      var filtered = _users.Where(predicate.Compile()).ToList();
      return await Task.FromResult(filtered);
    }

    public async Task<IUserDocument> FindAsync(string id, string partitionKey = null) {
      var user = _users.SingleOrDefault(x => x.ID == id);
      return await Task.FromResult(user);
    }

    public async Task<IUserDocument> AddAsync(IUserDocument item) {
      if (item == null) { return null; }
      if (!item.IsValid()) { return null; }
      if (ContainsEmail(item.Email)) { return null; }

      _users.Add((UserDocument)item);
      return await Task.FromResult(item);
    }

    public async Task<IUserDocument> UpdateAsync(string id, IUserDocument item) {
      if (item == null) { return null; }
      if (item.ID != id) { return null; }

      var user = (UserDocument)await FindAsync(id, null);
      if (user == null) { return null; }

      if (_users.TryTake(out user)) {
        item.ID = user.ID;
        item.Email = user.Email;
        _users.Add((UserDocument)item);
        return await Task.FromResult(item);
      }
      else {
        return null;
      }
    }

    public async Task<IUserDocument> DeleteAsync(string id, string partitionKey = null) {
      var user = (UserDocument)await FindAsync(id, null);
      if (user == null) { return null; }

      if (_users.TryTake(out user)) {
        return await Task.FromResult(user);
      }
      else {
        return null;
      }
    }

  }
}
