using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
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

  public class SecurePassword : IDisposable {
    private string _password;
    private byte[] _salt;

    public string Hash { get; private set; }

    public SecurePassword(string password) {
      if (string.IsNullOrEmpty(password)) { throw new ArgumentException("Password must contain a value.", nameof(password)); }

      _password = password;
      GenerateSalt();
    }

    private void GenerateSalt(int? length = 192) {
      var size = length ?? 192;
      _salt = new byte[size / 8];

      using var rand = RandomNumberGenerator.Create();
      rand.GetBytes(_salt);
    }

    public void HashPassword() {
      var hash = KeyDerivation.Pbkdf2(_password, _salt, KeyDerivationPrf.HMACSHA256, 4741, 48);
      Hash = Convert.ToBase64String(_salt.Concat(hash).ToArray());
    }

    public bool IsValid(string checksum) {
      var bytes = Convert.FromBase64String(checksum);
      _salt = bytes.Take(24).ToArray();
      HashPassword();

      if (Hash.Equals(checksum)) { return true; }
      return false;
    }

    #region IDisposable
    private bool disposed = false;

    protected virtual void Dispose(bool disposing) {
      if (!disposed) {
        if (disposing) {
          _password = null;
          _salt = null;
          Hash = null;
        }
        disposed = true;
      }
    }

    public void Dispose() {
      Dispose(true);
    }
    #endregion

  }
}
