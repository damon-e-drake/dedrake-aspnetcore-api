using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DEDrake {
  public class AuthStartup {
    private readonly IServiceCollection _services;
    private readonly IConfiguration _config;

    public AuthStartup(IServiceCollection services, IConfiguration config) {
      _services = services;
      _config = config;
    }

    public void AddJwtAuthentication() {
      var jwtSecret = _config.GetValue<string>("Authentication:JWTSecret");
      var key = Encoding.ASCII.GetBytes(jwtSecret);

      _services.AddAuthentication(auth => {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(jwt => {
        jwt.RequireHttpsMetadata = false;
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
    }
  }
}
