using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DictionaryTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Dictionary<string, V> Create<V>()
		{
			return new Dictionary<string, V>(new StringTools.IEComp());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Dictionary<string, V> CreateIgnoreCase<V>()
		{
			return new Dictionary<string, V>(new StringTools.IECompIgnoreCase());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static OrderedMap<string, V> CreateOrdered<V>()
		{
			return new OrderedMap<string, V>(new StringTools.IEComp());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static OrderedMap<string, V> CreateOrderedIgnoreCase<V>()
		{
			return new OrderedMap<string, V>(new StringTools.IECompIgnoreCase());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static TreeSet<string> CreateSet()
		{
			return new TreeSet<string>(new StringTools.IEComp());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static TreeSet<string> CreateSetIgnoreCase()
		{
			return new TreeSet<string>(new StringTools.IECompIgnoreCase());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static CacheMap<string, V> CreateCache<V>(Func<string, V> createValue)
		{
			return new CacheMap<string, V>(Create<V>(), createValue);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static CacheMap<string, V> CreateCacheIgnoreCase<V>(Func<string, V> createValue)
		{
			return new CacheMap<string, V>(CreateIgnoreCase<V>(), createValue);
		}
	}
}
