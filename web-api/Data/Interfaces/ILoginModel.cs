using System.Collections.Generic;

namespace DEDrake.Data.Interfaces {
  public interface ILoginModel {
    string FirstName { get; set; }
    string LastName { get; set; }
    string DisplayName { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    string Token { get; set; }
    IEnumerable<string> Errors { get; set; }
  }
}
