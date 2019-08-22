using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class SyncList<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<T> Inner = new List<T>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Clear()
		{
			lock (SYNCROOT)
			{
				this.Inner.Clear();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(T value)
		{
			lock (SYNCROOT)
			{
				this.Inner.Add(value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void AddRange(IEnumerable<T> values)
		{
			lock (SYNCROOT)
			{
				this.Inner.AddRange(values);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void RemoveAt(int index)
		{
			lock (SYNCROOT)
			{
				this.Inner.RemoveAt(index);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void RemoveAll(Predicate<T> match)
		{
			lock (SYNCROOT)
			{
#if true
				this.Inner.RemoveAll(match);
#else
				int count = 0;

				for (int index = 0; index < this.Inner.Count; index++)
					if (match(this.Inner[index]) == false)
						this.Inner[count++] = this.Inner[index];

				this.Inner.RemoveRange(count, this.Inner.Count - count);
#endif
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T this[int index]
		{
			get
			{
				lock (SYNCROOT)
				{
					return this.Inner[index];
				}
			}

			set
			{
				lock (this.SYNCROOT)
				{
					this.Inner[index] = value;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T GetPost(int index, T value)
		{
			lock (SYNCROOT)
			{
				T ret = this.Inner[index];
				this.Inner[index] = value;
				return ret;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count
		{
			get
			{
				lock (SYNCROOT)
				{
					return this.Inner.Count;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T[] ToArray()
		{
			lock (SYNCROOT)
			{
				return this.Inner.ToArray();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Invoke(Action<List<T>> routine)
		{
			lock (SYNCROOT)
			{
				routine(this.Inner);
			}
		}
	}
}
