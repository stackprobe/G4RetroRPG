using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class DoubleTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp(double a, double b)
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
		public static double Range(double value, double minval, double maxval)
		{
			return Math.Max(minval, Math.Min(maxval, value));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double ToDouble(string str, double minval, double maxval, double defval)
		{
			try
			{
				return Range(double.Parse(str), minval, maxval);
			}
			catch
			{
				return defval;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int ToInt(double value)
		{
			if (value < 0.0)
				return (int)(value - 0.5);
			else
				return (int)(value + 0.5);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long ToLong(double value)
		{
			if (value < 0.0)
				return (long)(value - 0.5);
			else
				return (long)(value + 0.5);
		}
	}
}
