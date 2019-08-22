using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class OrderedMap<K, V>
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class ValueInfo
		{
			public V Value;
			public long Index;
			public K Key;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Dictionary<K, ValueInfo> Inner;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private long Counter = 0L;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public OrderedMap(IEqualityComparer<K> comp)
		{
			this.Inner = new Dictionary<K, ValueInfo>(comp);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(Dictionary<K, V> dict)
		{
			foreach (KeyValuePair<K, V> pair in dict)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(K key, V value)
		{
			this.Inner.Add(key, new ValueInfo()
			{
				Value = value,
				Index = this.Counter++,
				Key = key,
			});
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
		public V this[K key]
		{
			get
			{
				return this.Inner[key].Value;
			}

			set
			{
				if (this.Inner.ContainsKey(key))
					this.Inner[key].Value = value;
				else
					this.Add(key, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<K> GetKeySet()
		{
			return this.Inner.Keys;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<KeyValuePair<K, V>> GetEntrySet()
		{
			foreach (ValueInfo info in this.Inner.Values)
			{
				yield return new KeyValuePair<K, V>(info.Key, info.Value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<V> GetValueSet()
		{
			foreach (ValueInfo info in this.Inner.Values)
			{
				yield return info.Value;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<ValueInfo> GetInfos()
		{
			List<ValueInfo> infos = new List<ValueInfo>(this.Inner.Values);

			infos.Sort((a, b) => LongTools.Comp(a.Index, b.Index));

			return infos;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<K> GetKeys()
		{
			return GetInfos().Select(info => info.Key);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<KeyValuePair<K, V>> GetEntries()
		{
			return GetInfos().Select(info => new KeyValuePair<K, V>(info.Key, info.Value));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<V> GetValues()
		{
			return GetInfos().Select(info => info.Value);
		}
	}
}
