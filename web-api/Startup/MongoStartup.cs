using DEDrake.Data.Models;
using DEDrake.Services;
using DEDrake.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEDrake {
  public class MongoStartup {
    private readonly IServiceCollection _services;
    private readonly IConfiguration _config;

    public MongoStartup(IServiceCollection services, IConfiguration config) {
      _services = services;
      _config = config;
    }

    public void AddDocumentServices() {
      _services.AddSingleton<IUserService<UserDocument>>(new UserService(_config, "Users"));
      _services.AddSingleton<ICredentialService<CredentialDocument>>(new CredentialService(_config, "Credentials"));
    }
  }
}
