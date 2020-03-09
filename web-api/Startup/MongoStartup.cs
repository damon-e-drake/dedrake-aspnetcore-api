using DEDrake.Services;
using DEDrake.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEDrake {
  public static class MongoStartup {

    public static void AddMongoServices(this IServiceCollection services, IConfiguration config) {
      services.AddSingleton<IUserService>(new UserService(config, "Users"));
      services.AddSingleton<ICredentialService>(new CredentialService(config, "Credentials"));
    }

  }
}
