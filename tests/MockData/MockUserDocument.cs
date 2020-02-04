using DEDrake.Data.Interfaces;
using DEDrake.Data.Models;
using System;
using System.Collections.Generic;

namespace DEDrake.Tests.MockData {
  public class MockUserDocument : IUserDocument {
    public string ID { get; set; }
    public bool Enabled { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<UserDocumentPhone> Phones { get; set; }
    public IEnumerable<string> Roles { get; set; }
  }
}
