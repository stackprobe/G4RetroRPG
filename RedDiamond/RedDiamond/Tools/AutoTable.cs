using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class AutoTable<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private AutoList<AutoList<T>> Rows;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int W;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public AutoTable(int w, int h)
		{
			if (w < 1 || h < 1)
				throw new ArgumentException();

			this.Rows = new AutoList<AutoList<T>>(h);

			this.W = w;
			this.H = h;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private AutoList<T> GetRow(int y)
		{
			AutoList<T> row = this.Rows[y];

			if (row == null)
			{
				row = new AutoList<T>(this.W);
				this.Rows[y] = row;
			}
			return row;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T this[int x, int y]
		{
			get
			{
				return this.GetRow(y)[x];
			}

			set
			{
				this.GetRow(y)[x] = value;
			}
		}
	}
}
