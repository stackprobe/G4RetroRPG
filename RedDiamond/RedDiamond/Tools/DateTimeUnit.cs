using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class DateTimeUnit
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Y;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int M;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int D;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int H;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int I;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int S;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit FromDay(int day)
		{
			return FromSec(day * 86400L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit FromDate(int date)
		{
			return FromDateTime(date * 1000000L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit Now()
		{
			return FromDateTime(DateTimeToSec.Now.GetDateTime());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit FromSec(long sec)
		{
			return FromDateTime(DateTimeToSec.Allow11To13Dig.ToDateTime(sec));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit FromDateTime(long dateTime)
		{
			int s = (int)(dateTime % 100L);
			dateTime /= 100L;
			int i = (int)(dateTime % 100L);
			dateTime /= 100L;
			int h = (int)(dateTime % 100L);
			dateTime /= 100L;
			int d = (int)(dateTime % 100L);
			dateTime /= 100L;
			int m = (int)(dateTime % 100L);
			int y = (int)(dateTime / 100L);

			return new DateTimeUnit()
			{
				Y = y,
				M = m,
				D = d,
				H = h,
				I = i,
				S = s,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public long GetDateTime()
		{
			return
				this.Y * 10000000000L +
				this.M * 100000000L +
				this.D * 1000000L +
				this.H * 10000L +
				this.I * 100L +
				this.S;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public long GetSec()
		{
			return DateTimeToSec.Allow11To13Dig.ToSec(this.GetDateTime());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetDate()
		{
			return (int)(this.GetDateTime() / 1000000L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetDay()
		{
			return (int)(this.GetSec() / 86400L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetWeekdayIndex()
		{
			return this.GetDay() % 7;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string GetWeekday()
		{
			return "月火水木金土日".Substring(this.GetWeekdayIndex(), 1);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public override string ToString()
		{
			return this.ToString("Y/M/D (W) h:m:s");
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string ToString(string format)
		{
			string ret = format;

			ret = ret.Replace("Y", this.Y.ToString());
			ret = ret.Replace("M", this.M.ToString("D2"));
			ret = ret.Replace("D", this.D.ToString("D2"));
			ret = ret.Replace("h", this.H.ToString("D2"));
			ret = ret.Replace("m", this.I.ToString("D2"));
			ret = ret.Replace("s", this.S.ToString("D2"));
			ret = ret.Replace("W", this.GetWeekday());

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static DateTimeUnit FromString(string str)
		{
			string[] tokens = StringTools.Tokenize(str, StringTools.DECIMAL, true, true);

			if (tokens.Length == 1)
			{
				string token = tokens[0];

				if (token.Length == 8)
					return FromDate(int.Parse(token));

				if (token.Length == 14)
					return FromDateTime(long.Parse(token));
			}
			else if (tokens.Length == 3)
			{
				int y = int.Parse(tokens[0]);
				int m = int.Parse(tokens[1]);
				int d = int.Parse(tokens[2]);

				return new DateTimeUnit()
				{
					Y = y,
					M = m,
					D = d,
				};
			}
			else if (tokens.Length == 6)
			{
				int y = int.Parse(tokens[0]);
				int m = int.Parse(tokens[1]);
				int d = int.Parse(tokens[2]);
				int h = int.Parse(tokens[3]);
				int i = int.Parse(tokens[4]);
				int s = int.Parse(tokens[5]);

				return new DateTimeUnit()
				{
					Y = y,
					M = m,
					D = d,
					H = h,
					I = i,
					S = s,
				};
			}
			throw new ArgumentException(str);
		}
	}
}
