using DEDrake.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface ICredentialService : IDocumentService<ICredentialDocument> {
    Task<ILoginModel> Authenticate(ILoginModel model);
    Task<ICredentialDocument> SetPassword(string email, string password);
    Task<ICredentialDocument> SetResetPin(string email);
    Task<ICredentialDocument> SetExpires(string email, DateTime? expires);
  }
}
