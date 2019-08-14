using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	/// <summary>
	/// その他の機能の寄せ集め、そのうち DxLib に関係有るもの。関係無いものは GameUtils へ
	/// </summary>
	public static class GameDxUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool IsWindowActive()
		{
			return DX.GetActiveFlag() != 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static long GetCurrTime()
		{
			return DX.GetNowHiPerformanceCount() / 1000L;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool GetMouseDispMode()
		{
			return DX.GetMouseDispFlag() != 0;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetMouseDispMode(bool mode)
		{
			DX.SetMouseDispFlag(mode ? 1 : 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static uint GetColor(I3Color color)
		{
			return DX.GetColor(color.R, color.G, color.B);
		}
	}
}
