namespace DEDrake.Services {

  //public interface IUserService {
  //  Task<IEnumerable<UserDocument>> GetAsync();
  //  Task<IEnumerable<UserDocument>> GetAsync(Expression<Func<UserDocument, bool>> predicate);
  //  Task<UserDocument> FindAsync(Guid id);
  //  Task<UserDocument> AddAsync(UserDocument user);
  //  Task<UserDocument> UpdateAsync(UserDocument user);
  //  Task<UserDocument> DeleteAsync(Guid id);
  //  Task<string> AuthenticateAsync(string username, string password);
  //}

  //public class UserService : IUserService {

  //  private readonly IDocumentRepository<UserDocument> _repository;

  //  public UserService(IDocumentRepository<UserDocument> repository) {
  //    _repository = repository;
  //  }

  //  public async Task<IEnumerable<UserDocument>> GetAsync() => await _repository.GetAllAsync();
  //  public async Task<IEnumerable<UserDocument>> GetAsync(Expression<Func<UserDocument, bool>> predicate) => await _repository.GetAsync(predicate);
  //  public async Task<UserDocument> FindAsync(Guid id) => await _repository.FindAsync(id.ToString(), null);
  //  public async Task<UserDocument> AddAsync(UserDocument user) => await _repository.AddAsync(user);
  //  public async Task<UserDocument> UpdateAsync(UserDocument user) => await _repository.UpdateAsync(user.ID, user);
  //  public async Task<UserDocument> DeleteAsync(Guid id) => await _repository.DeleteAsync(id.ToString(), null);

  //  public async Task<string> AuthenticateAsync(string username, string password) {
  //    throw new NotImplementedException();
  //  }
  //}

}
