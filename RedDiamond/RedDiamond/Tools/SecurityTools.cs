using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class SecurityTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static RandomUnit CRandom = new RandomUnit(new CSPRandomNumberGenerator());

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakePassword(string allowChars, int length)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < length; index++)
				buff.Append(allowChars[CRandom.GetInt(allowChars.Length)]);

			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakePassword()
		{
			return MakePassword(StringTools.DECIMAL + StringTools.ALPHA + StringTools.alpha, 22);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakePassword_9A()
		{
			return MakePassword(StringTools.DECIMAL + StringTools.ALPHA, 25);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakePassword_9()
		{
			return MakePassword(StringTools.DECIMAL, 39);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class AESRandomNumberGenerator : RandomUnit.IRandomNumberGenerator
		{
			private CipherTools.AES Aes;
			private byte[] Counter = new byte[16];
			private byte[] Block = new byte[16];

			public AESRandomNumberGenerator(int seed)
				: this(seed.ToString())
			{ }

			public AESRandomNumberGenerator(string seed)
				: this(Encoding.UTF8.GetBytes(seed))
			{ }

			public AESRandomNumberGenerator(byte[] seed)
			{
				using (SHA512 sha512 = SHA512.Create())
				{
					byte[] hash = sha512.ComputeHash(seed);
					//byte[] rawKey = new byte[16];
					//byte[] rawKey = new byte[24];
					byte[] rawKey = new byte[32];

					//Array.Copy(hash, 0, rawKey, 0, 16);
					//Array.Copy(hash, 0, rawKey, 0, 24);
					Array.Copy(hash, 0, rawKey, 0, 32);

					this.Aes = new CipherTools.AES(rawKey);
				}
			}

			public byte[] GetBlock()
			{
				this.Aes.EncryptBlock(this.Counter, this.Block);

				for (int index = 0; index < 16; index++)
				{
					if (this.Counter[index] < 0xff)
					{
						this.Counter[index]++;
						break;
					}
					this.Counter[index] = 0x00;
				}
				return this.Block;
			}

			public void Dispose()
			{
				if (this.Aes != null)
				{
					this.Aes.Dispose();
					this.Aes = null;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class CSPRandomNumberGenerator : RandomUnit.IRandomNumberGenerator
		{
			private RandomNumberGenerator Rng = new RNGCryptoServiceProvider();
			private byte[] Cache = new byte[4096];

			public byte[] GetBlock()
			{
				this.Rng.GetBytes(this.Cache);
				return this.Cache;
			}

			public void Dispose()
			{
				if (this.Rng != null)
				{
					this.Rng.Dispose();
					this.Rng = null;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetSHA512(byte[] src)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				return sha512.ComputeHash(src);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetSHA512(IEnumerable<byte[]> src)
		{
			using (SHA512 sha512 = SHA512.Create())
			{
				foreach (byte[] part in src)
				{
					sha512.TransformBlock(part, 0, part.Length, null, 0);
				}
				sha512.TransformFinalBlock(BinTools.EMPTY, 0, 0);
				return sha512.Hash;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetSHA512File(string file)
		{
			using (SHA512 sha512 = SHA512.Create())
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return sha512.ComputeHash(reader);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetMD5(byte[] src)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				return md5.ComputeHash(src);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetMD5(IEnumerable<byte[]> src)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			{
				foreach (byte[] part in src)
				{
					md5.TransformBlock(part, 0, part.Length, null, 0);
				}
				md5.TransformFinalBlock(BinTools.EMPTY, 0, 0);
				return md5.Hash;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetMD5File(string file)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				return md5.ComputeHash(reader);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ToFiarIdent(string ident)
		{
			if (IsFiarIdent(ident) == false)
				ident = BinTools.Hex.ToString(BinTools.GetSubBytes(GetSHA512(Encoding.UTF8.GetBytes(ident)), 0, 16));

			return ident;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool IsFiarIdent(string ident)
		{
			return StringTools.LiteValidate(ident, StringTools.DECIMAL + StringTools.alpha + "-{}", 1, 38);
		}
	}
}
