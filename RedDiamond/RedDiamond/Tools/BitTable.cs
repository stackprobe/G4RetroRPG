using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class BitTable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private BitList Buffer;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int W;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BitTable(int w, int h)
		{
			if (w < 1 || h < 1)
				throw new ArgumentException();

			this.Buffer = new BitList((long)w * h);
			this.W = w;
			this.H = h;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool this[int x, int y]
		{
			get
			{
				return this.GetBit(x, y);
			}

			set
			{
				this.SetBit(x, y, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool GetBit(int x, int y)
		{
			if (
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y
				)
				throw new ArgumentException();

			return this.Buffer[x + (long)y * this.W];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetBit(int x, int y, bool value)
		{
			if (
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y
				)
				throw new ArgumentException();

			this.Buffer[x + (long)y * this.W] = value;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetBits(int l, int t, int w, int h, bool value) // w, h: 0 ok
		{
			if (
				l < 0 || this.W <= l ||
				t < 0 || this.H <= t ||
				w < 0 || this.W - l < w ||
				h < 0 || this.H - t < h
				)
				throw new ArgumentException();

			for (int y = 0; y < h; y++)
			{
				this.Buffer.SetBits(l + (long)(t + y) * this.W, (long)w, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void InvBit(int x, int y)
		{
			if (
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y
				)
				throw new ArgumentException();

			this.Buffer.InvBit(x + (long)y * this.W);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void InvBits(int l, int t, int w, int h, bool value) // w, h: 0 ok
		{
			if (
				l < 0 || this.W <= l ||
				t < 0 || this.H <= t ||
				w < 0 || this.W - l < w ||
				h < 0 || this.H - t < h
				)
				throw new ArgumentException();

			for (int y = 0; y < h; y++)
			{
				this.Buffer.InvBits(l + (long)(t + y) * this.W, (long)w);
			}
		}
	}
}
