using DEDrake.Data.Models;
using DEDrake.Security;
using DEDrake.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DEDrake.Controllers {
  [Route("v1/auth")]
  [ApiController]
  public class AuthController : ControllerBase {
    private readonly ICredentialService _service;
    private readonly IOptions<AuthConfiguration> _auth;

    public AuthController(ICredentialService service, IOptions<AuthConfiguration> auth) {
      _service = service;
      _auth = auth;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(UserLoginModel model) {
      throw new NotImplementedException();
    }

    [HttpGet("options")]
    public async Task<IActionResult> GetOptions() {
      return Ok(_auth.Value.HashVersions);
    }

  }
}