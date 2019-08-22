using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class AutoList<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private T[] Buffer;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public AutoList(int capacity = 0)
		{
			this.Buffer = new T[capacity];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void EnsureCapacity(int capacity)
		{
			if (this.Buffer.Length < capacity)
			{
				T[] tmp = new T[capacity];

				Array.Copy(this.Buffer, tmp, this.Buffer.Length);

				this.Buffer = tmp;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Capacity
		{
			get
			{
				return this.Buffer.Length;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T this[int index]
		{
			get
			{
#if true
				return index < this.Buffer.Length ? this.Buffer[index] : default(T);
#else
				this.EnsureCapacity(index + 1);
				return this.Buffer[index];
#endif
			}

			set
			{
				this.EnsureCapacity(index + 1);
				this.Buffer[index] = value;
			}
		}
	}
}
