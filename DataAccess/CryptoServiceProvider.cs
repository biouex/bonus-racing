using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.IO;

namespace BonusRacing.DataAccess
{
	public class CryptoServiceProvider : IDisposable
	{
		private const int Rfc2898KeygenIterations = 100;
		private const int AesKeySizeInBits = 128;
		private const int KeyStrengthInBytes = AesKeySizeInBits / 8;
		private const string SecretKey = "VerySecret!";
		private readonly byte[] _salt = new byte[16] {251, 55, 21, 62, 90, 123, 32, 55, 45, 76, 182, 25, 32, 21, 67, 86};
		private readonly AesManaged _aesManaged;
		

		public CryptoServiceProvider()
		{
			_aesManaged = InitAes();
		}

		public byte[] Encrypt(string input)
		{
			byte[] rawPlaintext = Encoding.Unicode.GetBytes(input);
			byte[] outputBytes = null;
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, _aesManaged.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(rawPlaintext, 0, rawPlaintext.Length);
				}
				outputBytes = ms.ToArray();
			}
            return outputBytes;
        }

		public string Decrypt(byte[] rawCipherText)
		{
			byte[] plainTextBytes = null;
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, _aesManaged.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(rawCipherText, 0, rawCipherText.Length);
				}
				plainTextBytes = ms.ToArray();
			}
            return Encoding.Unicode.GetString(plainTextBytes);
        }

		public void Dispose()
		{
			_aesManaged.Dispose();
		}

		private AesManaged InitAes()
		{
			Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(SecretKey, _salt, Rfc2898KeygenIterations);
			var aesManaged = new AesManaged();
            aesManaged.Padding = PaddingMode.PKCS7;
			aesManaged.KeySize = AesKeySizeInBits;
			aesManaged.Key = rfc2898.GetBytes(KeyStrengthInBytes);
			aesManaged.IV = rfc2898.GetBytes(KeyStrengthInBytes);
			return aesManaged;
		}
	}
}
