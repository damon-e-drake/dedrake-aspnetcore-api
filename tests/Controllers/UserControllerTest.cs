using DEDrake.Services;

namespace DEDrake.Controllers.Tests {
  public class UserControllerTests {
    private readonly UserController _controller;

    public UserControllerTests() {
      _controller = new UserController(new UserCacheService());
    }
  }
}