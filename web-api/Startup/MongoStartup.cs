using DEDrake.Data;
using DEDrake.Data.Models;
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
    }
  }
}
