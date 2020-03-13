using System;
using System.Runtime.Serialization;

namespace DEDrake.Security {
  [Serializable]
  public class SecurePasswordException : Exception {

    public SecurePasswordException() : base("Password did not meet requirements.") { }

    public SecurePasswordException(string message) : base(message) { }

    public SecurePasswordException(string message, Exception inner) : base(message, inner) { }

    protected SecurePasswordException(SerializationInfo serializationInfo, StreamingContext streamingContext) {
      throw new NotImplementedException();
    }
  }
}
