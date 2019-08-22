using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class BitList
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private AutoList<uint> Buffer;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BitList(long capacity = 0)
		{
			this.Buffer = new AutoList<uint>((int)((capacity + 31L) >> 5));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void EnsureCapacity(long capacity)
		{
			this.Buffer.EnsureCapacity((int)((capacity + 31L) >> 5));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool this[long index]
		{
			get
			{
				return this.GetBit(index);
			}

			set
			{
				this.SetBit(index, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool GetBit(long index)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31L);

			return ((this.Buffer[i] >> b) & 1u) == 1u;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetBit(long index, bool value)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31);

			uint c = this.Buffer[i];

			if (value)
				c |= 1u << b;
			else
				c &= ~(1u << b);

			this.Buffer[i] = c;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetBits(long index, long size, bool value)
		{
			long bgn = index;
			long end = index + size;

			while (bgn < end && (bgn & 31L) != 0L)
			{
				this.SetBit(bgn, value);
				bgn++;
			}
			while (bgn < end && (end & 31L) != 0L)
			{
				end--;
				this.SetBit(end, value);
			}
			if (bgn < end)
			{
				int ib = (int)(bgn >> 5);
				int ie = (int)(end >> 5);

				uint c = value ? 0xffffffffu : 0u;

				for (int i = ib; ib < ie; i++)
					this.Buffer[i] = c;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void InvBit(long index)
		{
			int i = (int)(index >> 5);
			int b = (int)(index & 31);

			uint c = this.Buffer[i];

			c ^= 1u << b;

			this.Buffer[i] = c;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void InvBits(long index, long size)
		{
			long bgn = index;
			long end = index + size;

			while (bgn < end && (bgn & 31L) != 0L)
			{
				this.InvBit(bgn);
				bgn++;
			}
			while (bgn < end && (end & 31L) != 0L)
			{
				end--;
				this.InvBit(end);
			}
			if (bgn < end)
			{
				int ib = (int)(bgn >> 5);
				int ie = (int)(end >> 5);

				for (int i = ib; ib < ie; i++)
					this.Buffer[i] ^= 0xffffffffu;
			}
		}
	}
}
