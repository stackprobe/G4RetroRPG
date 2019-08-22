using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class SortedList<T>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<T> InnerList;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Comparison<T> Comp;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private bool SortedFlag;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public SortedList(List<T> bindingList, Comparison<T> comp, bool sortedFlag = false)
		{
			this.InnerList = bindingList;
			this.Comp = comp;
			this.SortedFlag = sortedFlag;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public SortedList(Comparison<T> comp)
		{
			this.InnerList = new List<T>();
			this.Comp = comp;
			this.SortedFlag = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Clear()
		{
			this.InnerList.Clear();
			this.SortedFlag = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(T element)
		{
			this.InnerList.Add(element);
			this.SortedFlag = false;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void AddRange(T[] elements)
		{
			this.InnerList.AddRange(elements);
			this.SortedFlag = false;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count
		{
			get
			{
				return this.InnerList.Count;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private void BeforeAccessElement()
		{
			if (SortedFlag == false)
			{
				this.InnerList.Sort(this.Comp);
				this.SortedFlag = true;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T Get(int index)
		{
			this.BeforeAccessElement();
			return this.InnerList[index];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public List<T> GetRange(int index = 0)
		{
			return this.GetRange(index, this.Count - index);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public List<T> GetRange(int index, int count)
		{
			this.BeforeAccessElement();
			return this.InnerList.GetRange(index, count);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool Contains(Func<T, int> ferret)
		{
			return this.IndexOf(ferret) != -1;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int IndexOf(Func<T, int> ferret)
		{
			this.BeforeAccessElement();

			int l = -1;
			int r = this.InnerList.Count;

			while (l + 1 < r)
			{
				int m = (l + r) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (ret < 0)
				{
					l = m;
				}
				else if (0 < ret)
				{
					r = m;
				}
				else
				{
					return m;
				}
			}
			return -1; // not found
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int LeftIndexOf(Func<T, int> ferret) // ret: ターゲット以上になる最初の位置、無ければ要素数
		{
			this.BeforeAccessElement();

			int l = 0;
			int r = this.InnerList.Count;

			while (l < r)
			{
				int m = (l + r) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (ret < 0)
				{
					l = m + 1;
				}
				else
				{
					r = m;
				}
			}
			return l;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int RightIndexOf(Func<T, int> ferret, int l = -1) // ret: ターゲット以下になる最後の位置、無ければ(-1)
		{
			this.BeforeAccessElement();

			//int l = -1;
			int r = this.InnerList.Count - 1;

			while (l < r)
			{
				int m = (l + r + 1) / 2;
				int ret = ferret(this.InnerList[m]); // as this.Comp(this.InnerList[m], target);

				if (0 < ret)
				{
					r = m - 1;
				}
				else
				{
					l = m;
				}
			}
			return r;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public List<T> GetMatch(Func<T, int> ferret)
		{
#if true
			int l = this.LeftIndexOf(ferret);
			int r = this.RightIndexOf(ferret, l - 1);
			int count = r - l + 1;

			return this.GetRange(l, count);
#else // smpl_same
			List<T> dest = new List<T>();

			for (int index = this.LeftIndexOf(ferret); index <= this.RightIndexOf(ferret); index++)
			{
				dest.Add(this.InnerList[index]);
			}
			return dest;
#endif
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Func<T, int> GetFerret(T target)
		{
			return (T value) => this.Comp(value, target);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Func<T, int> GetFerret(T mintrg, T maxtrg)
		{
			return (T value) =>
			{
				if (this.Comp(value, mintrg) < 0)
					return -1;

				if (this.Comp(value, maxtrg) > 0)
					return 1;

				return 0;
			};
		}
	}
}
