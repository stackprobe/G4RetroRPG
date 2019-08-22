using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class Base64Unit
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Base64Unit CreateByC6364P(string c6364P)
		{
			return new Base64Unit(StringTools.ALPHA + StringTools.alpha + StringTools.DECIMAL + c6364P);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Base64Unit()
			: this(StringTools.ALPHA + StringTools.alpha + StringTools.DECIMAL + "+/=")
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private char[] Chrs;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private byte[] ChrMap;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Base64Unit(string chrs)
		{
			if (chrs.Length != 65)
				throw new ArgumentException();

			if (StringTools.HasSameChar(chrs))
				throw new ArgumentException();

			this.Chrs = chrs.ToCharArray();
			this.ChrMap = new byte[(int)char.MaxValue + 1];

			for (int index = 0; index < 64; index++)
				this.ChrMap[this.Chrs[index]] = (byte)index;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Encode(byte[] src)
		{
			char[] dest = new char[((src.Length + 2) / 3) * 4];
			int writer = 0;
			int index = 0;
			int chr;

			while (index + 3 <= src.Length)
			{
				chr = (src[index++] & 0xff) << 16;
				chr |= (src[index++] & 0xff) << 8;
				chr |= src[index++] & 0xff;
				dest[writer++] = this.Chrs[chr >> 18];
				dest[writer++] = this.Chrs[(chr >> 12) & 0x3f];
				dest[writer++] = this.Chrs[(chr >> 6) & 0x3f];
				dest[writer++] = this.Chrs[chr & 0x3f];
			}
			if (index + 2 == src.Length)
			{
				chr = (src[index++] & 0xff) << 8;
				chr |= src[index++] & 0xff;
				dest[writer++] = this.Chrs[chr >> 10];
				dest[writer++] = this.Chrs[(chr >> 4) & 0x3f];
				dest[writer++] = this.Chrs[(chr << 2) & 0x3c];
				dest[writer++] = this.Chrs[64];
			}
			else if (index + 1 == src.Length)
			{
				chr = src[index++] & 0xff;
				dest[writer++] = this.Chrs[chr >> 2];
				dest[writer++] = this.Chrs[(chr << 4) & 0x30];
				dest[writer++] = this.Chrs[64];
				dest[writer++] = this.Chrs[64];
			}
			return new string(dest);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] Decode(string src)
		{
			int destSize = (src.Length / 4) * 3;

			if (destSize != 0)
			{
				if (src[src.Length - 2] == this.Chrs[64])
				{
					destSize -= 2;
				}
				else if (src[src.Length - 1] == this.Chrs[64])
				{
					destSize--;
				}
			}
			byte[] dest = new byte[destSize];
			int writer = 0;
			int index = 0;
			int chr;

			while (writer + 3 <= destSize)
			{
				chr = (this.ChrMap[src[index++]] & 0x3f) << 18;
				chr |= (this.ChrMap[src[index++]] & 0x3f) << 12;
				chr |= (this.ChrMap[src[index++]] & 0x3f) << 6;
				chr |= this.ChrMap[src[index++]] & 0x3f;
				dest[writer++] = (byte)(chr >> 16);
				dest[writer++] = (byte)((chr >> 8) & 0xff);
				dest[writer++] = (byte)(chr & 0xff);
			}
			if (writer + 2 == destSize)
			{
				chr = (this.ChrMap[src[index++]] & 0x3f) << 10;
				chr |= (this.ChrMap[src[index++]] & 0x3f) << 4;
				chr |= (this.ChrMap[src[index++]] & 0x3c) >> 2;
				dest[writer++] = (byte)(chr >> 8);
				dest[writer++] = (byte)(chr & 0xff);
			}
			else if (writer + 1 == destSize)
			{
				chr = (this.ChrMap[src[index++]] & 0x3f) << 2;
				chr |= (this.ChrMap[src[index++]] & 0x30) >> 4;
				dest[writer++] = (byte)chr;
			}
			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string RemovePadding(string data)
		{
			if (data.Length != 0)
			{
				if (data[data.Length - 2] == this.Chrs[64])
				{
					return data.Substring(0, data.Length - 2);
				}
				if (data[data.Length - 1] == this.Chrs[64])
				{
					return data.Substring(0, data.Length - 1);
				}
			}
			return data;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string AddPadding(string data)
		{
			int rem = data.Length % 4;

			if (rem == 2)
			{
				data = data + this.Chrs[64] + this.Chrs[64];
			}
			else if (rem == 3)
			{
				data = data + this.Chrs[64];
			}
			return data;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public NoPadding GetNoPadding()
		{
			return new NoPadding(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class NoPadding
		{
			private Base64Unit Inner;

			public NoPadding(Base64Unit inner)
			{
				this.Inner = inner;
			}

			public string Encode(byte[] src)
			{
				return this.Inner.RemovePadding(this.Inner.Encode(src));
			}

			public byte[] Decode(string src)
			{
				return this.Inner.Decode(this.Inner.AddPadding(src));
			}
		}
	}
}
