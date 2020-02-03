using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface IUserService<T> : IDocumentService<T> where T : class {
    Task<bool> SetPasswordAsync(string id, string password);
  }
}
