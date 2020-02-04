using DEDrake.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface ICredentialService<T> : IDocumentService<T> where T : class {
    Task<ILoginModel> Authenticate(ILoginModel model);
    Task<T> SetPassword(string email, string password);
    Task<T> SetResetPin(string email);
    Task<T> SetExpires(string email, DateTime? expires);
  }
}
