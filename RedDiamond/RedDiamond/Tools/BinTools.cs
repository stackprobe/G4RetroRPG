using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class BinTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] EMPTY = new byte[0];

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp(byte a, byte b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp(byte[] a, byte[] b)
		{
			return ArrayTools.Comp(a, b, Comp);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int CompFile(string file1, string file2)
		{
#if false
			byte[] hash1 = SecurityTools.GetSHA512File(file1);
			byte[] hash2 = SecurityTools.GetSHA512File(file2);

			return BinTools.Comp(hash1, hash2);
#else
			const int BUFF_SIZE = 50000000; // 50 MB

			using (FileStream nb_reader1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
			using (FileStream nb_reader2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
			using (BufferedStream reader1 = new BufferedStream(nb_reader1, BUFF_SIZE))
			using (BufferedStream reader2 = new BufferedStream(nb_reader2, BUFF_SIZE))
			{
				for (; ; )
				{
					int chr1 = reader1.ReadByte();
					int chr2 = reader2.ReadByte();

					if (chr1 != chr2)
						return chr1 - chr2;

					if (chr1 == -1)
						return 0;
				}
			}
#endif
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static class Hex
		{
			public static string ToString(byte chr)
			{
				return ToString(new byte[] { chr });
			}

			public static string ToString(byte[] src)
			{
				StringBuilder buff = new StringBuilder();

				foreach (byte chr in src)
				{
					buff.Append(StringTools.hexadecimal[chr >> 4]);
					buff.Append(StringTools.hexadecimal[chr & 0x0f]);
				}
				return buff.ToString();
			}

			public static byte[] ToBytes(string src)
			{
				if (src.Length % 2 != 0)
					throw new ArgumentException();

				byte[] dest = new byte[src.Length / 2];

				for (int index = 0; index < dest.Length; index++)
					dest[index] = (byte)((To4Bit(src[index * 2]) << 4) | To4Bit(src[index * 2 + 1]));

				return dest;
			}

			private static int To4Bit(char chr)
			{
				int ret = StringTools.hexadecimal.IndexOf(char.ToLower(chr));

				if (ret == -1)
					throw new ArgumentException();

				return ret;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetSubBytes(byte[] src, int offset = 0)
		{
			return GetSubBytes(src, offset, src.Length - offset);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetSubBytes(byte[] src, int offset, int size)
		{
			byte[] dest = new byte[size];
			Array.Copy(src, offset, dest, 0, size);
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] ToBytes(int value)
		{
			byte[] dest = new byte[4];
			ToBytes(value, dest);
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ToBytes(int value, byte[] dest, int index = 0)
		{
			dest[index + 0] = (byte)((value >> 0) & 0xff);
			dest[index + 1] = (byte)((value >> 8) & 0xff);
			dest[index + 2] = (byte)((value >> 16) & 0xff);
			dest[index + 3] = (byte)((value >> 24) & 0xff);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int ToInt(byte[] src, int index = 0)
		{
			return
				((int)src[index + 0] << 0) |
				((int)src[index + 1] << 8) |
				((int)src[index + 2] << 16) |
				((int)src[index + 3] << 24);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] ToBytes64(long value)
		{
			byte[] dest = new byte[8];
			ToBytes64(value, dest);
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ToBytes64(long value, byte[] dest, int index = 0)
		{
			dest[index + 0] = (byte)((value >> 0) & 0xff);
			dest[index + 1] = (byte)((value >> 8) & 0xff);
			dest[index + 2] = (byte)((value >> 16) & 0xff);
			dest[index + 3] = (byte)((value >> 24) & 0xff);
			dest[index + 4] = (byte)((value >> 32) & 0xff);
			dest[index + 5] = (byte)((value >> 40) & 0xff);
			dest[index + 6] = (byte)((value >> 48) & 0xff);
			dest[index + 7] = (byte)((value >> 56) & 0xff);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long ToInt64(byte[] src, int index = 0)
		{
			return
				((long)src[index + 0] << 0) |
				((long)src[index + 1] << 8) |
				((long)src[index + 2] << 16) |
				((long)src[index + 3] << 24) |
				((long)src[index + 4] << 32) |
				((long)src[index + 5] << 40) |
				((long)src[index + 6] << 48) |
				((long)src[index + 7] << 56);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] Join(params byte[][] src)
		{
			int offset = 0;

			foreach (byte[] block in src)
				offset += block.Length;

			byte[] dest = new byte[offset];
			offset = 0;

			foreach (byte[] block in src)
			{
				Array.Copy(block, 0, dest, offset, block.Length);
				offset += block.Length;
			}
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] SplittableJoin(params byte[][] src)
		{
			int offset = 0;

			foreach (byte[] block in src)
				offset += 4 + block.Length;

			byte[] dest = new byte[offset];
			offset = 0;

			foreach (byte[] block in src)
			{
				Array.Copy(ToBytes(block.Length), 0, dest, offset, 4);
				offset += 4;
				Array.Copy(block, 0, dest, offset, block.Length);
				offset += block.Length;
			}
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[][] Split(byte[] src)
		{
			List<byte[]> dest = new List<byte[]>();

			for (int offset = 0; offset < src.Length; )
			{
				int size = ToInt(src, offset);
				offset += 4;
				dest.Add(GetSubBytes(src, offset, size));
				offset += size;
			}
			return dest.ToArray();
		}
	}
}
