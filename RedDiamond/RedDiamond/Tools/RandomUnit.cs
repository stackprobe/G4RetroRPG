using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class RandomUnit : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public interface IRandomNumberGenerator : IDisposable
		{
			byte[] GetBlock();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private IRandomNumberGenerator Rng;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public RandomUnit(IRandomNumberGenerator rng)
		{
			this.Rng = rng;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.Rng != null)
			{
				this.Rng.Dispose();
				this.Rng = null;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private byte[] Cache = BinTools.EMPTY;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int RIndex = 0;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte GetByte()
		{
			if (this.Cache.Length <= this.RIndex)
			{
				this.Cache = this.Rng.GetBlock();
				this.RIndex = 0;
			}
			return this.Cache[this.RIndex++];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] GetBytes(int length)
		{
			byte[] dest = new byte[length];

			for (int index = 0; index < length; index++)
				dest[index] = this.GetByte();

			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public uint GetUInt16()
		{
			return
				((uint)this.GetByte() << 8) |
				((uint)this.GetByte() << 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public uint GetUInt()
		{
			return
				(this.GetUInt16() << 16) |
				(this.GetUInt16() << 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public ulong GetUInt64()
		{
			return
				((ulong)this.GetUInt() << 32) |
				((ulong)this.GetUInt() << 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public ulong GetRandom64(ulong modulo)
		{
			if (modulo == 0UL)
				throw new ArgumentOutOfRangeException("modulo == 0");

			if (modulo == 1UL)
				return 0UL;

			ulong r_mod = (ulong.MaxValue % modulo + 1UL) % modulo;
			ulong r;

			do
			{
				r = this.GetUInt64();
			}
			while (r < r_mod);

			r %= modulo;

			return r;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public uint GetRandom(uint modulo)
		{
			return (uint)this.GetRandom64((ulong)modulo);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public long GetRange64(long minval, long maxval)
		{
			return (long)this.GetRandom64((ulong)(maxval + 1L - minval)) + minval;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetRange(int minval, int maxval)
		{
			return (int)this.GetRandom((uint)(maxval + 1 - minval)) + minval;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public long GetInt64(long modulo)
		{
			return (long)this.GetRandom64((ulong)modulo);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetInt(int modulo)
		{
			return (int)this.GetRandom((uint)modulo);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// [0,1]
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal()
		{
			return this.GetUInt() / (double)uint.MaxValue;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// [0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal2()
		{
			return this.GetUInt() / (double)(uint.MaxValue + 1L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// (0,1)
		/// </summary>
		/// <returns>乱数</returns>
		public double GetReal3()
		{
			return this.GetUInt() / (double)(uint.MaxValue + 1L) + 0.5;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Shuffle<T>(T[] arr)
		{
			for (int index = arr.Length; 1 < index; index--)
			{
				ArrayTools.Swap(arr, GetInt(index), index - 1);
			}
		}
	}
}
