using System;
using System.Security.Cryptography;
using System.Text;

namespace ProcessManager.Utilities
{
	public static class Cryptographer
	{
		public static string CreateSHA512Hash(string value, string salt = null)
		{
			salt = (string.IsNullOrEmpty(salt) ? Guid.NewGuid().ToString().Substring(0, 6).ToUpper() : salt.Substring(0, 6));
			value = salt + value;
			byte[] buffer = new UnicodeEncoding().GetBytes(value);
			byte[] hash = new SHA512CryptoServiceProvider().ComputeHash(buffer);
			return salt + BitConverter.ToString(hash).Replace("-", string.Empty);
		}

		public static string CreateSHA1HashNoSalt(string value)
		{
			byte[] buffer = new UnicodeEncoding().GetBytes(value);
			byte[] hash = new SHA1CryptoServiceProvider().ComputeHash(buffer);
			return BitConverter.ToString(hash).Replace("-", string.Empty);
		}

		public static Guid CreateGUID(string value)
		{
			string hash = CreateSHA1HashNoSalt(value);
			return new Guid(hash.Substring(0, 8) + "-" + hash.Substring(8, 4) + "-" + hash.Substring(12, 4) + "-" + hash.Substring(16, 4) + "-" + hash.Substring(20, 12));
		}
	}
}
