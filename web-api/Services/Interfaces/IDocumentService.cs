using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Services.Interfaces {
  public interface IDocumentService<T> {
    Task<IEnumerable<T>> GetAsync();
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> FindAsync(string id, string partitionKey = null);
    Task<T> AddAsync(T item);
    Task<T> UpdateAsync(string id, T item);
    Task<T> DeleteAsync(string id, string partitionKey = null);
  }
}
