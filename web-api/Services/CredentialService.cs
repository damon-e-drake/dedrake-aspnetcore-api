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
  public class CredentialService : ICredentialService<CredentialDocument> {

    private readonly IMongoCollection<CredentialDocument> _collection;

    public CredentialService(IConfiguration config, string collection) {
      var endpoint = config.GetValue<string>("MongoDB:Endpoint");
      var database = config.GetValue<string>("MongoDB:Database");

      var client = new MongoClient(endpoint);
      var db = client.GetDatabase(database);
      _collection = db.GetCollection<CredentialDocument>(collection);
    }

    public Task<IEnumerable<CredentialDocument>> GetAsync() {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<CredentialDocument>> GetAsync(Expression<Func<CredentialDocument, bool>> predicate) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> FindAsync(string id, string partitionKey) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> AddAsync(CredentialDocument item) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> UpdateAsync(string id, CredentialDocument item) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> DeleteAsync(string id, string partitionKey = null) {
      throw new NotImplementedException();
    }

    public Task<ILoginModel> Authenticate(ILoginModel model) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> SetExpires(string email, DateTime? expires) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> SetPassword(string email, string password) {
      throw new NotImplementedException();
    }

    public Task<CredentialDocument> SetResetPin(string email) {
      throw new NotImplementedException();
    }

  }
}
