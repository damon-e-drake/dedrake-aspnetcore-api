using DEDrake.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DEDrake.Data.Models {
  public class CredentialDocument : ICredentialDocument {
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("expires")]
    public DateTime Expires { get; set; }

    [BsonElement("resetPin")]
    public string ResetPin { get; set; }

    [BsonElement("resetExpires")]
    public DateTime? ResetExpires { get; set; }
  }
}
