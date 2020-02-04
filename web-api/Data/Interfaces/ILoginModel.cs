using System.Collections.Generic;

namespace DEDrake.Data.Interfaces {
  public interface ILoginModel {
    string Username { get; set; }
    string Password { get; set; }
    string Token { get; set; }
    IEnumerable<string> Errors { get; set; }
  }
}
