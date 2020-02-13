using DEDrake.Data.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface IAuthService {

    Task<ILoginModel> CreateTokenAsync(IUserDocument user, string secret);
    Task<IUserDocument> ParseClaimsAsync(ClaimsPrincipal claims);
  }
}
