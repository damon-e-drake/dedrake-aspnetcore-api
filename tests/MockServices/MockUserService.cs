using DEDrake.Data.Interfaces;
using DEDrake.Services.Interfaces;
using DEDrake.Tests.MockData;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Tests.MockServices {
  public class MockUserService : IUserService {
    private ConcurrentBag<MockUserDocument> _users;

    public MockUserService() {
      InitData();
    }

    private void InitData() {
      _users = new ConcurrentBag<MockUserDocument> {
        new MockUserDocument {
          ID = "f2cbf908-a704-4543-9d39-f497be0fe5ab",
          Enabled = true,
          FirstName = "Admin",
          LastName = "User",
          DisplayName = "Admin User",
          Email = "admin.user@example.com",
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Global Administrator, Authenticated User" }
        },
        new MockUserDocument {
          ID = "2bdc66ec-6552-42e7-896f-05b153ec3ef6",
          Enabled = true,
          FirstName = "Member",
          LastName = "User",
          DisplayName = "Member User",
          Email = "member.user@example.com",
          CreatedAt = DateTime.UtcNow.AddDays(-10),
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        new MockUserDocument {
          ID = "4c9d835b-cf3b-4f70-b54e-5754e6d68595",
          Enabled = false,
          FirstName = "Disabled",
          LastName = "User",
          DisplayName = "Disabled User",
          Email = "disabed.user@example.com",
          CreatedAt = DateTime.UtcNow.AddDays(-15),
          Phones = null,
          Roles = new[] { "Authenticated User" }
        }
      };
    }

    private bool ContainsEmail(string email) {
      var count = _users.Count(x => x.Email == email.ToLower());
      return count > 0;
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
      if (ContainsEmail(item.Email)) { return null; }

      _users.Add((MockUserDocument)item);
      return await Task.FromResult(item);
    }

    public async Task<IUserDocument> UpdateAsync(string id, IUserDocument item) {
      if (item == null) { return null; }
      if (item.ID != id) { return null; }

      var user = (MockUserDocument)await FindAsync(id, null);
      if (user == null) { return null; }

      if (_users.TryTake(out user)) {
        item.ID = user.ID;
        item.Email = user.Email;
        _users.Add((MockUserDocument)item);
        return await Task.FromResult(item);
      }
      else {
        return null;
      }
    }

    public async Task<IUserDocument> DeleteAsync(string id, string partitionKey = null) {
      var user = (MockUserDocument)await FindAsync(id, null);
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
