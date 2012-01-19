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
			return salt + BitConverter.ToString(hash).Replace("-", "");
		}
	}
}
