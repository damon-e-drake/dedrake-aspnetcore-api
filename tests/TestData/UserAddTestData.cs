
using DEDrake.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DEDrake.Tests.TestData {
  public class UserAddTestData : IEnumerable<object[]> {

    public IEnumerator<object[]> GetEnumerator() {
      yield return new object[] {
        new UserDocument {
          ID = Guid.NewGuid().ToString(),
          Enabled = true,
          FirstName = "Add",
          LastName = "User",
          DisplayName = "Add User 1",
          Email = "add.user.2@example.com",
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        true
      };

      // Should Fail - unique email index
      yield return new object[] {
        new UserDocument {
          ID = Guid.NewGuid().ToString(),
          Enabled = true,
          FirstName = "Add",
          LastName = "User",
          DisplayName = "Add User 2",
          Email = "admin.user@example.com",
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        false
      };

      yield return new object[] {
        new UserDocument {
          ID = Guid.NewGuid().ToString(),
          Enabled = true,
          FirstName = "Add",
          LastName = "User",
          DisplayName = "Add User 3",
          Email = "add.user.3@example.com",
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        true
      };

      // Should Fail - required email address
      yield return new object[] {
        new UserDocument {
          ID = Guid.NewGuid().ToString(),
          Enabled = true,
          FirstName = "Add",
          LastName = "User",
          DisplayName = "Add User 4",
          Email = null,
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        false
      };

      // Should Fail - required display name
      yield return new object[] {
        new UserDocument {
          ID = Guid.NewGuid().ToString(),
          Enabled = true,
          FirstName = "Add",
          LastName = "User",
          DisplayName = null,
          Email = "add.user.5@example.com",
          CreatedAt = DateTime.UtcNow,
          Phones = null,
          Roles = new[] { "Authenticated User" }
        },
        false
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
