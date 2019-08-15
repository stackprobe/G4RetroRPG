using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Main01
{
	public static class NamedRect
	{
		private class Info
		{
			public string Name;
			public D4Rect Rect;
		}

		private static List<Info> Infos = new List<Info>();

		public static void Clear()
		{
			Infos.Clear();
		}

		public static void SetRectName(string name, D4Rect rect)
		{
			Infos.Add(new Info()
			{
				Name = name,
				Rect = rect,
			});
		}

		public static D4Rect LastDrawedRect = null;

		public static void SetLastDrawedRectName(string name)
		{
			SetRectName(name, LastDrawedRect);
		}

		public static string GetRectName(double x, double y, string defaultName = "NONE")
		{
			Info info = Infos.FirstOrDefault(v => DDUtils.IsOut(new D2Point(x, y), v.Rect) == false);

			if (info == null)
				return defaultName;

			return info.Name;
		}
	}
}
