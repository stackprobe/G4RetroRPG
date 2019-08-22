using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class ObjectMap
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private OrderedMap<string, object> Inner;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static ObjectMap Create()
		{
			return new ObjectMap()
			{
				Inner = DictionaryTools.CreateOrdered<object>(),
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static ObjectMap CreateIgnoreCase()
		{
			return new ObjectMap()
			{
				Inner = DictionaryTools.CreateOrderedIgnoreCase<object>(),
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private ObjectMap()
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(Dictionary<object, object> dict)
		{
			foreach (KeyValuePair<object, object> pair in dict)
			{
				this.Add(pair.Key, pair.Value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(object key, object value)
		{
			this.Inner.Add("" + key, value);
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
		public object this[object key]
		{
			get
			{
				return this.Inner["" + key];
			}

			set
			{
				this.Inner["" + key] = value;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<string> GetKeySet()
		{
			return this.Inner.GetKeySet();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<KeyValuePair<string, object>> GetEntrySet()
		{
			return this.Inner.GetEntrySet();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<object> GetValueSet()
		{
			return this.Inner.GetValueSet();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<string> GetKeys()
		{
			return this.Inner.GetKeys();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<KeyValuePair<string, object>> GetEntries()
		{
			return this.Inner.GetEntries();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public IEnumerable<object> GetValues()
		{
			return this.Inner.GetValues();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public OrderedMap<string, object> Direct()
		{
			return this.Inner;
		}
	}
}
