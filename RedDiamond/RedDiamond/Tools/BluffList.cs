using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class BluffList<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count = 0;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Func<int, T> GetElement = null;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Action<int, T> SetElement = null;

		// <---- prm

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList()
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList(T[] entity)
		{
			this.Count = entity.Length;
			this.GetElement = index => entity[index];
			this.SetElement = (index, value) => entity[index] = value;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList(T element = default(T), int count = 1)
		{
			this.Count = count;
			this.GetElement = index => element;
			this.SetElement = null;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}

			set
			{
				this.SetElement(index, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<T> Iterate()
		{
			for (int index = 0; index < this.Count; index++)
			{
				yield return this.GetElement(index);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList<T> FreeRange(T defval = default(T))
		{
			return new BluffList<T>()
			{
				Count = this.Count,
				GetElement = index =>
				{
					if (index < 0 || this.Count <= index) // out of range
					{
						return defval;
					}
					return this.GetElement(index);
				},
				SetElement = this.SetElement,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList<T> Reverse()
		{
			return new BluffList<T>()
			{
				Count = this.Count,
				GetElement = index => this.GetElement(this.Count - 1 - index),
				SetElement = (index, value) => this.SetElement(this.Count - 1 - index, value),
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList<T> SubRange(int start, int count)
		{
			return new BluffList<T>()
			{
				Count = count,
				GetElement = index => this.GetElement(start + index),
				SetElement = (index, value) => this.SetElement(start + index, value),
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public BluffList<T> AddRange(BluffList<T> trailer)
		{
			return new BluffList<T>()
			{
				Count = this.Count + trailer.Count,
				GetElement = index => index < this.Count ? this.GetElement(index) : trailer.GetElement(index - this.Count),
				SetElement = (index, value) =>
				{
					if (index < this.Count)
						this.SetElement(index, value);
					else
						trailer.SetElement(index - this.Count, value);
				},
			};
		}
	}
}
