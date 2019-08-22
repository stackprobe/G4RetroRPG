using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class CacheMap<K, V>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Dictionary<K, V> Inner;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Func<K, V> CreateValue;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CacheMap(Dictionary<K, V> inner, Func<K, V> createValue)
		{
			this.Inner = inner;
			this.CreateValue = createValue;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public V this[K key]
		{
			get
			{
				if (this.Inner.ContainsKey(key) == false)
				{
					V value = this.CreateValue(key);
					this.Inner[key] = value;
					return value;
				}
				return this.Inner[key];
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count
		{
			get
			{
				return this.Inner.Count;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Clear()
		{
			this.Inner.Clear();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Remove(K key)
		{
			this.Inner.Remove(key);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<K> Keys
		{
			get
			{
				return this.Inner.Keys;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<V> Values
		{
			get
			{
				return this.Inner.Values;
			}
		}
	}
}
