using DEDrake.Data.Models;
using System;
using System.Collections.Generic;

namespace DEDrake.Data.Interfaces {
  public interface IUserDocument : IMongoDocument {
    string ID { get; set; }
    bool Enabled { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string DisplayName { get; set; }
    string Email { get; set; }
    DateTime CreatedAt { get; set; }
    IEnumerable<UserDocumentPhone> Phones { get; set; }
    IEnumerable<string> Roles { get; set; }
  }

  public interface IUserDocumentPhone {
    string Number { get; set; }
    bool Mobile { get; set; }
    bool IsPrimary { get; set; }
  }
}
