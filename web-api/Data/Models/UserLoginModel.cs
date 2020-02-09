using DEDrake.Data.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DEDrake.Data.Models {
  public class UserLoginModel : ILoginModel {
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string DisplayName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public string Token { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public UserLoginModel() {
      Errors = new List<string>();
    }
  }
}
