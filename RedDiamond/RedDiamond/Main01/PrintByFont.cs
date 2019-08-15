using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;

namespace Charlotte.Main01
{
	public static class PrintByFont
	{
		private static int P_X = -1; // -1 == 未初期化
		private static int P_Y;
		private static int P_YStep;
		private static string P_FontName;
		private static int P_FontSize;

		public static void SetPrint(int x = 0, int y = 0, int yStep = 40, string fontName = "りいてがき筆", int fontSize = 30)
		{
			P_X = x;
			P_Y = y;
			P_YStep = yStep;
			P_FontName = fontName;
			P_FontSize = fontSize;
		}

		public static void Print(string line)
		{
			if (P_X == -1)
				throw new DDError();

			DDFontUtils.DrawString(P_X, P_Y, line, DDFontUtils.GetFont(P_FontName, P_FontSize));
			P_Y += P_YStep;
		}
	}
}
