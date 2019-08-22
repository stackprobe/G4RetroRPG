using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class Wrapper
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Unit<T> Create<T>(T value)
		{
			return new Unit<T>(value);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class Unit<T>
		{
			public T Value;

			public Unit(T value)
			{
				this.Value = value;
			}

			public Unit<T> Accept(Action<T> routine)
			{
				routine(this.Value);
				return this;
			}

			public Unit<R> Change<R>(Func<T, R> routine)
			{
				return new Unit<R>(routine(this.Value));
			}
		}
	}
}
