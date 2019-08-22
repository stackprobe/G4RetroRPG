using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class SyncQueue<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Queue<T> Inner = new Queue<T>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Enqueue(T element)
		{
			lock (SYNCROOT)
			{
				this.Inner.Enqueue(element);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T[] Dequeue(int count = 1)
		{
			List<T> dest = new List<T>();

			lock (SYNCROOT)
			{
				while (dest.Count < count && 1 <= this.Inner.Count)
					dest.Add(this.Inner.Dequeue());
			}
			return dest.ToArray();
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
		public void Rotate(Predicate<T> match, int count = 1)
		{
			foreach (T element in this.Dequeue(count))
				if (match(element))
					this.Enqueue(element);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Invoke(Action<Queue<T>> routine)
		{
			lock (SYNCROOT)
			{
				routine(this.Inner);
			}
		}
	}
}
