using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DEDrake.Controllers {
  [ApiController, Route("v1/users")]
  public class UserController : ControllerBase {
    private readonly IUserService _service;
    // private readonly IUserPrincipal _user;

    public UserController(IUserService service) {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() {
      var users = await _service.GetAsync();
      return Ok(users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName));
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetById(string id) {
      var document = await _service.FindAsync(id, null);
      if (document == null) { return NotFound(); }

      return Ok(document);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyAccount() {
      var claim = User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Email).Value;

      var document = await _service.GetAsync();
      var user = document.FirstOrDefault();
      if (user == null) { return NotFound(); }

      return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserDocument user) {
      user.CreatedAt = DateTime.UtcNow;

      var document = await _service.AddAsync(user);
      if (document == null) {
        return StatusCode(409, new UserDocument());
      }
      return Created($"api/v1/users/{user.ID}", document);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateUser(string id, UserDocument user) {
      if (user.ID != id) { return StatusCode(409, "User ID did not match user object body."); }

      var document = await _service.UpdateAsync(user.ID, user);
      return Ok(document);
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> DeleteUser(string id) {
      await _service.DeleteAsync(id);
      return Ok();
    }
  }
}
