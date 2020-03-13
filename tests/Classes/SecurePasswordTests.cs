using DEDrake.Security;
using Xunit;

namespace DEDrake.Tests.Classes {
  public class SecurePasswordTests {

    [Theory]
    [InlineData("dogman", false)]
    public void SecurePassword(string password, bool expected) {
      var passwd = new SecurePassword(password);

      Assert.NotNull(passwd);

    }
  }
}
