using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DEDrake.Services {
  public class AuthService : IAuthService {
    public Task<ILoginModel> CreateTokenAsync(IUserDocument user, string secret) {
      var login = new UserLoginModel {
        ID = user.ID,
        FirstName = user.FirstName,
        LastName = user.LastName,
        DisplayName = user.DisplayName,
        Email = user.Email,
        Password = null
      };

      var handler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(secret);
      var claims = new List<Claim>() {
        new Claim(ClaimTypes.Name, login.ID),
        new Claim(ClaimTypes.Email, login.Email),
        new Claim(ClaimTypes.GivenName, login.FirstName),
        new Claim(ClaimTypes.Surname, login.LastName)
      };

      foreach (var role in user.Roles) {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var descriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = handler.CreateToken(descriptor);
      login.Token = handler.WriteToken(token);

      return Task.FromResult((ILoginModel)login);
    }

    public Task<IUserDocument> ParseClaimsAsync(ClaimsPrincipal claims) {
      var user = new UserDocument {
        ID = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
        FirstName = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
        LastName = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value,
        Roles = claims.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
      };
      return Task.FromResult((IUserDocument)user);
    }
  }
}
