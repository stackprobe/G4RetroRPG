using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameFontUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static List<GameFont> Fonts = new List<GameFont>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Add(GameFont font)
		{
			Fonts.Add(font);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void UnloadAll()
		{
			foreach (GameFont font in Fonts)
				font.Unload();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameFont GetFont(string fontName, int fontSize, int fontThick = 6, bool antiAliasing = true, int edgeSize = 0, bool italicFlag = false)
		{
			GameFont font = Fonts.FirstOrDefault(v =>
				v.FontName == fontName &&
				v.FontSize == fontSize &&
				v.FontThick == fontThick &&
				v.AntiAliasing == antiAliasing &&
				v.EdgeSize == edgeSize &&
				v.ItalicFlag == italicFlag
				);

			if (font == null)
				font = new GameFont(fontName, fontSize, fontThick, antiAliasing, edgeSize, italicFlag);

			return font;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawString(int x, int y, string str, GameFont font, bool tategakiFlag = false, I3Color color = null, I3Color edgeColor = null)
		{
			if (color == null)
				color = new I3Color(255, 255, 255);

			if (edgeColor == null)
				edgeColor = new I3Color(0, 0, 0);

			DX.DrawStringToHandle(x, y, str, GameDxUtils.GetColor(color), font.GetHandle(), GameDxUtils.GetColor(edgeColor), tategakiFlag ? 1 : 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawString_XCenter(int x, int y, string str, GameFont font, bool tategakiFlag = false, I3Color color = null, I3Color edgeColor = null)
		{
			x -= GetDrawStringWidth(str, font, tategakiFlag) / 2;

			DrawString(x, y, str, font, tategakiFlag, color, edgeColor);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int GetDrawStringWidth(string str, GameFont font, bool tategakiFlag = false)
		{
			return DX.GetDrawStringWidthToHandle(str, StringTools.ENCODING_SJIS.GetByteCount(str), font.GetHandle(), tategakiFlag ? 1 : 0);
		}
	}
}
