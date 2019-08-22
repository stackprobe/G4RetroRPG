using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class LongTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const long IMAX_64 = 1000000000000000000L; // 10^18

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp(long a, long b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long Range(long value, long minval, long maxval)
		{
			return Math.Max(minval, Math.Min(maxval, value));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long ToLong(string str, long minval, long maxval, long defval)
		{
			try
			{
				int value = int.Parse(str);

				if (value < minval || maxval < value)
					throw null;

				return value;
			}
			catch
			{
				return defval;
			}
		}
	}
}
