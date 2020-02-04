using DEDrake.Tests.MockData;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DEDrake.Tests.TestData {
  public class UserAddTestData : IEnumerable<object[]> {

    public IEnumerator<object[]> GetEnumerator() {
      yield return new object[] {
        new MockUserDocument {
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

      yield return new object[] {
        new MockUserDocument {
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
        new MockUserDocument {
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
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
  }
}
