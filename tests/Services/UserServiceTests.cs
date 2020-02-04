using DEDrake.Data.Interfaces;
using DEDrake.Services.Interfaces;
using DEDrake.Tests.MockServices;
using DEDrake.Tests.TestData;
using System.Linq;
using Xunit;

namespace DEDrake.Services.Tests {
  public class UserServiceTests {
    private readonly IUserService _service;

    public UserServiceTests() {
      _service = new MockUserService();
    }

    [Fact]
    public void UserServiceTest() {
      Assert.NotNull(_service);
    }

    [Fact]
    public async void GetAsyncTest() {
      var users = await _service.GetAsync();

      Assert.NotNull(users);
      Assert.Equal(3, users.Count());
    }

    [Theory]
    [InlineData("f2cbf908-a704-4543-9d39-f497be0fe5ab", true)]
    [InlineData("4c9d835b-cf3b-4f70-b54e-5754e6d68595", true)]
    [InlineData("0392df34-b902-47d8-a560-9557e65cba79", false)]
    public async void FindAsyncTest(string id, bool expected) {
      var user = await _service.FindAsync(id, null);
      var found = user != null;

      Assert.Equal(expected, found);
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