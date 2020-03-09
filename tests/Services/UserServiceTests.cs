using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using DEDrake.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DEDrake.Services.Tests {
  public class UserServiceTests {
    private readonly IUserService _service;

    public UserServiceTests() {
      _service = new UserCacheService();
      SeedData();
    }

    private void SeedData() {
      var users = new List<IUserDocument> {
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
        new UserDocument {
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

      ((UserCacheService)_service).InitData(users);
    }

    [Fact(DisplayName = "User service should be created")]
    public void UserServiceTest() {
      Assert.NotNull(_service);
    }

    [Fact]
    public async void GetAsyncTest() {
      var users = await _service.GetAsync();

      Assert.NotNull(users);
      Assert.Equal(3, users.Count());
    }

    [Theory(DisplayName = "Should return users")]
    [InlineData("f2cbf908-a704-4543-9d39-f497be0fe5ab", true)]
    [InlineData("4c9d835b-cf3b-4f70-b54e-5754e6d68595", true)]
    [InlineData("0392df34-b902-47d8-a560-9557e65cba79", false)]
    public async void FindAsyncTest(string id, bool expected) {
      var user = await _service.FindAsync(id, null);
      var found = user != null;

      Assert.Equal(expected, found);
    }

    [Fact]
    public async void GetAsyncWithQueryTest() {
      var users = await _service.GetAsync(x => x.DisplayName.StartsWith("Mem"));

      Assert.Single(users);
    }

    [Theory]
    [ClassData(typeof(UserAddTestData))]
    public async void AddAsyncTest(IUserDocument doc, bool expected) {
      var user = await _service.AddAsync(doc);
      var added = user != null;

      Assert.Equal(expected, added);
    }

    [Theory]
    [ClassData(typeof(UserUpdateTestData))]
    public async void UpdateAsyncTest(string id, IUserDocument doc, bool expected) {
      var user = await _service.UpdateAsync(id, doc);
      var updated = user != null;

      Assert.Equal(expected, updated);
    }

    [Theory]
    [InlineData("f2cbf908-a704-4543-9d39-f497be0fe5ab", true)]
    [InlineData("4c9d835b-cf3b-4f70-b54e-5754e6d68595", true)]
    [InlineData("0392df34-b902-47d8-a560-9557e65cba79", false)]
    public async void DeleteAsyncTest(string id, bool expected) {
      var user = await _service.DeleteAsync(id, null);
      var deleted = user != null;

      Assert.Equal(expected, deleted);
    }
  }
}