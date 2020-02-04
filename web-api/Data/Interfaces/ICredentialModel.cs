using System;

namespace DEDrake.Data.Interfaces {
  public interface ICredentialDocument {
    string ID { get; set; }
    string Email { get; set; }
    string Password { get; set; }
    DateTime Expires { get; set; }
    string ResetPin { get; set; }
    DateTime? ResetExpires { get; set; }
  }
}
