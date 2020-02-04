using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using DEDrake.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DEDrake.Services {
  public class CredentialService : ICredentialService {

    private readonly IMongoCollection<CredentialDocument> _collection;

    public CredentialService(IConfiguration config, string collection) {
      var endpoint = config.GetValue<string>("MongoDB:Endpoint");
      var database = config.GetValue<string>("MongoDB:Database");

      var client = new MongoClient(endpoint);
      var db = client.GetDatabase(database);
      _collection = db.GetCollection<CredentialDocument>(collection);
    }

    public Task<IEnumerable<ICredentialDocument>> GetAsync() {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ICredentialDocument>> Get(Expression<Func<CredentialDocument, bool>> predicate) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> FindAsync(string id, string partitionKey) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> AddAsync(ICredentialDocument item) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> UpdateAsync(string id, ICredentialDocument item) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> DeleteAsync(string id, string partitionKey = null) {
      throw new NotImplementedException();
    }

    public Task<ILoginModel> Authenticate(ILoginModel model) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> SetExpires(string email, DateTime? expires) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> SetPassword(string email, string password) {
      throw new NotImplementedException();
    }

    public Task<ICredentialDocument> SetResetPin(string email) {
      throw new NotImplementedException();
    }

  }
}
