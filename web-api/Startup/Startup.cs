using DEDrake.Security;
using DEDrake.Services;
using DEDrake.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DEDrake {
  public class Startup {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services) {
      services.AddControllers();
      services.AddOptions();

      services.Configure<AuthConfiguration>(Configuration.GetSection("Authentication"));
      services.AddScoped<IAuthService, AuthService>();

      services.AddJwtAuthentication(Configuration);
      services.AddMongoServices(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
        app.UseCors(builder => {
          builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        });
      }
      else {
        app.UseCors(builder => {
          builder.WithOrigins("https://www.dedrake.com", "https://dedrake.com", "https://dropflies.org", "https://www.dropflies.org").AllowAnyHeader().AllowAnyMethod();
        });
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
