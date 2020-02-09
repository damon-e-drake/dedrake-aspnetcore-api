using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DEDrake.Security {
  public class AuthConfiguration {
    public string JWTSecret { get; set; }
    public List<SecurePasswordConFiguration> HashVersions { get; set; }
  }
  public class SecurePasswordConFiguration {
    public string Version { get; set; }
    public int SaltSize { get; set; }
    public int Iterations { get; set; }
    public int Bitness { get; set; }
    public string KeyDerivation { get; set; }
  }

  public class SecurePassword {
    private readonly string _password;
    private byte[] _salt;
    // private PasswordVersion _version;

    public string Version { get; set; }
    public string Salt { get; private set; }
    public int? Interations { get; private set; }

    public SecurePassword(string password, string version = null) {
      if (string.IsNullOrEmpty(password)) { throw new ArgumentException("Password must contain a value.", nameof(password)); }

      _password = password;
      SetVersion(version);
    }

    private void SetVersion(string version) {
      //_version = Versions.SingleOrDefault(x => x.Version == "V1");
      //if (!string.IsNullOrEmpty(version)) {
      //  var v = Versions.SingleOrDefault(x => x.Version == version.ToUpper());
      //  if (v != null) { _version = v; }
      //}
    }

    private void GenerateSalt(int? length = 192) {
      var size = length.HasValue ? length.Value : 192;
      _salt = new byte[size / 8];

      using var rand = RandomNumberGenerator.Create();
      rand.GetBytes(_salt);
    }

    public static string GetPasswordHash(string password) {
      if (string.IsNullOrEmpty(password)) {
        throw new ArgumentException("Password cannot be empty or null.", nameof(password));
      }



      return null;
    }


  }
}
