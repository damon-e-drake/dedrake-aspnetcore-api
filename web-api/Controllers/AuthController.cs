using DEDrake.Data.Models;
using DEDrake.Security;
using DEDrake.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace DEDrake.Controllers {
  [Route("v1/auth")]
  [ApiController]
  public class AuthController : ControllerBase {
    private ICredentialService _creds;
    private IUserService _users;
    private IAuthService _auth;
    private AuthConfiguration _config;

    public AuthController(
      ICredentialService credentials,
      IUserService users,
      IAuthService auth,
      IOptions<AuthConfiguration> conf
    ) {
      _creds = credentials;
      _users = users;
      _auth = auth;
      _config = conf.Value;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate(UserLoginModel model) {
      if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)) {
        return BadRequest("Login missing email or password.");
      }

      var users = await _users.GetAsync(x => x.Email == model.Email.ToLower());
      var creds = await _creds.GetAsync(x => x.Email == model.Email.ToLower());
      var user = users.FirstOrDefault();
      var cred = creds.FirstOrDefault();

      if (user == null || cred == null) {
        return BadRequest("Invalid login credentials. null");
      }

      using (var secp = new SecurePassword(model.Password)) {
        if (!secp.IsValid(cred.Password)) { return BadRequest("Invalid login credentials."); }
      }

      if (!user.Enabled) {
        return BadRequest("This account has been disabled.");
      }

      var login = await _auth.CreateTokenAsync(user, _config.JWTSecret);
      return Ok(login);
    }

    [Authorize]
    [HttpGet("user")]
    public async Task<IActionResult> CurrentUser() {
      var user = await _auth.ParseClaimsAsync(User);
      return Ok(user);
    }

  }
}