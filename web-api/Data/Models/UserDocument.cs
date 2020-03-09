using DEDrake.Data.Helpers;
using DEDrake.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DEDrake.Data.Models {

  public class UserDocument : IUserDocument {
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    [BsonElement("enabled")]
    public bool Enabled { get; set; }

    [BsonElement("firstName")]
    public string FirstName { get; set; }

    [BsonElement("lastName")]
    public string LastName { get; set; }

    [BsonElement("displayName"), BsonRequired]
    public string DisplayName { get; set; }

    [BsonElement("email"), BsonRequired]
    public string Email { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("phones")]
    public IEnumerable<UserDocumentPhone> Phones { get; set; }

    [BsonElement("roles")]
    public IEnumerable<string> Roles { get; set; }

    public bool IsValid() {
      return MongoRequiredProperties.IsValid(this);
    }
  }

  public class UserDocumentPhone : IUserDocumentPhone {
    [BsonElement("number")]
    public string Number { get; set; }

    [BsonElement("mobile")]
    public bool Mobile { get; set; }

    [BsonElement("isPrimary")]
    public bool IsPrimary { get; set; }
  }
}
