using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Game3Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class CrashUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public enum Kind_e
		{
			NONE = 1,
			POINT,
			CIRCLE,
			RECT,
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Crash None()
		{
			return new Crash()
			{
				Kind = Kind_e.NONE,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Crash Point(D2Point pt)
		{
			return new Crash()
			{
				Kind = Kind_e.POINT,
				Pt = pt,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Crash Circle(D2Point pt, double r)
		{
			return new Crash()
			{
				Kind = Kind_e.CIRCLE,
				Pt = pt,
				R = r,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Crash Rect_CenterSize(D2Point centerPt, D2Size size)
		{
			return Rect(new D4Rect(centerPt.X - size.W / 2.0, centerPt.Y - size.H / 2.0, size.W, size.H));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Crash Rect(D4Rect rect)
		{
			return new Crash()
			{
				Kind = Kind_e.RECT,
				Rect = rect,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool IsCrashed(Crash a, Crash b)
		{
			if ((int)b.Kind < (int)a.Kind)
			{
				Crash tmp = a;
				a = b;
				b = tmp;
			}
			if (a.Kind == Kind_e.NONE)
				return false;

			if (a.Kind == Kind_e.POINT)
			{
				if (b.Kind == Kind_e.POINT)
					return false;

				if (b.Kind == Kind_e.CIRCLE)
					return DDUtils.IsCrashed_Circle_Point(b.Pt, b.R, a.Pt);

				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Rect_Point(b.Rect, a.Pt);

				throw new DDError();
			}
			if (a.Kind == Kind_e.CIRCLE)
			{
				if (b.Kind == Kind_e.CIRCLE)
					return DDUtils.IsCrashed_Circle_Circle(a.Pt, a.R, b.Pt, b.R);

				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Circle_Rect(a.Pt, a.R, b.Rect);

				throw new DDError();
			}
			if (a.Kind == Kind_e.RECT)
			{
				if (b.Kind == Kind_e.RECT)
					return DDUtils.IsCrashed_Rect_Rect(a.Rect, b.Rect);

				throw new DDError();
			}
			throw new DDError();
		}
	}
}
