using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class ArrayTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return IntTools.Comp(a.Length, b.Length);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Swap<T>(T[] arr, int a, int b)
		{
			T tmp = arr[a];
			arr[a] = arr[b];
			arr[b] = tmp;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int IndexOf<T>(T[] arr, T target, Comparison<T> comp, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (comp(arr[index], target) == 0)
					return index;

			return defval;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int IndexOf<T>(T[] arr, Predicate<T> match, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (match(arr[index]))
					return index;

			return defval;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Contains<T>(T[] arr, T target, Comparison<T> comp)
		{
			return IndexOf<T>(arr, target, comp) != -1;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Contains<T>(T[] arr, Predicate<T> match)
		{
			return IndexOf<T>(arr, match) != -1;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static List<T> ToList<T>(IEnumerable<T> src)
		{
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T[] ToArray<T>(IEnumerable<T> src)
		{
#if !true
			return src.ToArray();
#else // old same
			List<T> list = ToList(src);
			T[] dest = new T[list.Count];

			for (int index = 0; index < list.Count; index++)
				dest[index] = list[index];

			return dest;
#endif
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T[] Repeat<T>(T element, int count)
		{
			T[] dest = new T[count];

			for (int index = 0; index < count; index++)
				dest[index] = element;

			return dest;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="arr1"></param>
		/// <param name="arr2"></param>
		/// <param name="destOnly1">null可</param>
		/// <param name="destBoth1">null可</param>
		/// <param name="destBoth2">null可</param>
		/// <param name="destOnly2">null可</param>
		/// <param name="comp"></param>
		public static void Merge<T>(T[] arr1, T[] arr2, List<T> destOnly1, List<T> destBoth1, List<T> destBoth2, List<T> destOnly2, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			int index1 = 0;
			int index2 = 0;

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					if (destOnly1 != null)
						destOnly1.Add(arr1[index1]);

					index1++;
				}
				else if (0 < ret)
				{
					if (destOnly2 != null)
						destOnly2.Add(arr2[index2]);

					index2++;
				}
				else
				{
					if (destBoth1 != null)
						destBoth1.Add(arr1[index1]);

					if (destBoth2 != null)
						destBoth2.Add(arr2[index2]);

					index1++;
					index2++;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T[][] GetMergedPairs<T>(T[] arr1, T[] arr2, T defval, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			int index1 = 0;
			int index2 = 0;

			List<T[]> dest = new List<T[]>();

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					dest.Add(new T[] { arr1[index1++], defval });
				}
				else if (0 < ret)
				{
					dest.Add(new T[] { defval, arr2[index2++] });
				}
				else
				{
					dest.Add(new T[] { arr1[index1++], arr2[index2++] });
				}
			}
			return dest.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static IEnumerable<T> Distinct<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			IEnumerator<T> reader = src.GetEnumerator();

			if (reader.MoveNext())
			{
				T lastElement = reader.Current;

				yield return lastElement;

				while (reader.MoveNext())
				{
					T element = reader.Current;

					if (comp(element, lastElement) != 0)
					{
						yield return element;

						lastElement = element;
					}
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Lightest<T>(IEnumerable<T> src, Func<T, double> toWeight)
		{
			IEnumerator<T> reader = src.GetEnumerator();

			if (reader.MoveNext() == false)
				throw new ArgumentException("空のリストです。");

			T ret = reader.Current;
			double ret_weight = toWeight(ret);

			while (reader.MoveNext())
			{
				T value = reader.Current;
				double weight = toWeight(value);

				if (weight < ret_weight)
				{
					ret = value;
					ret_weight = weight;
				}
			}
			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Heaviest<T>(IEnumerable<T> src, Func<T, double> toWeight)
		{
			return Lightest(src, value => toWeight(value) * -1);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Smallest<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			IEnumerator<T> reader = src.GetEnumerator();

			if (reader.MoveNext() == false)
				throw new ArgumentException("空のリストです。");

			T ret = reader.Current;

			while (reader.MoveNext())
			{
				T value = reader.Current;

				if (comp(value, ret) < 0)
				{
					ret = value;
				}
			}
			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Largest<T>(IEnumerable<T> src, Comparison<T> comp)
		{
#if true
			return Smallest(src, (a, b) => comp(b, a));
#else // old same
			return Smallest(src, (a, b) => comp(a, b) * -1);
#endif
		}
	}
}
