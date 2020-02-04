using System.Collections.Generic;
using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface IDocumentService<T> {
    Task<IEnumerable<T>> GetAsync();
    Task<T> FindAsync(string id, string partitionKey = null);
    Task<T> AddAsync(T item);
    Task<T> UpdateAsync(string id, T item);
    Task<T> DeleteAsync(string id, string partitionKey = null);
  }
}
