using DEDrake.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DEDrake.Tests.TestData {
  public class UserUpdateTestData : IEnumerable<object[]> {

    public IEnumerator<object[]> GetEnumerator() {
      yield return new object[] { null, null, false };
      yield return new object[] { "f2cbf908-a704-4543-9d39-f497be0fe5ab", null, false };

      yield return new object[] {
        "f2cbf908-a704-4543-9d39-f497be0fe5ab",
        new UserDocument {
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
        true
      };

      yield return new object[] {
        "f2cbf908-a704-4543-9d39-f497be0fe5ab",
        new UserDocument {
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
        false
      };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

  }
}
