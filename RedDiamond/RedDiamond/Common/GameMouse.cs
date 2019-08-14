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
	public static class GameMouse
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Rot;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int L;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int R;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int M;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void EachFrame()
		{
			uint status;

			if (GameEngine.WindowIsActive)
			{
				Rot = DX.GetMouseHWheelRotVol();
				status = (uint)DX.GetMouseInput();
			}
			else
			{
				Rot = 0;
				status = 0u;
			}
			Rot = IntTools.Range(Rot, -IntTools.IMAX, IntTools.IMAX);

			GameUtils.UpdateInput(ref L, (status & (uint)DX.MOUSE_INPUT_LEFT) != 0u);
			GameUtils.UpdateInput(ref R, (status & (uint)DX.MOUSE_INPUT_RIGHT) != 0u);
			GameUtils.UpdateInput(ref M, (status & (uint)DX.MOUSE_INPUT_MIDDLE) != 0u);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int GetInput(int status)
		{
			return 1 <= GameEngine.FreezeInputFrame ? 0 : status;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Get_L()
		{
			return GetInput(L);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Get_R()
		{
			return GetInput(R);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Get_M()
		{
			return GetInput(M);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int X = (int)(GameConsts.Screen_W / 2.0);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Y = (int)(GameConsts.Screen_H / 2.0);

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void UpdatePos()
		{
			if (DX.GetMousePoint(out X, out Y) != 0) // ? 失敗
				throw new GameError();

			X *= GameConsts.Screen_W;
			X /= GameGround.RealScreen_W;
			Y *= GameConsts.Screen_H;
			Y /= GameGround.RealScreen_H;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ApplyPos()
		{
			int mx = X;
			int my = Y;

			mx *= GameGround.RealScreen_W;
			mx /= GameConsts.Screen_W;
			my *= GameGround.RealScreen_H;
			my /= GameConsts.Screen_H;

			if (DX.SetMousePoint(mx, my) != 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int MoveX;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int MoveY;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static int UM_LastFrame = -IntTools.IMAX;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void UpdateMove()
		{
			const int centerX = (int)(GameConsts.Screen_W / 2.0);
			const int centerY = (int)(GameConsts.Screen_H / 2.0);

			if (GameEngine.ProcFrame <= UM_LastFrame) // ? 2回以上更新した。
				throw new GameError();

			UpdatePos();

			MoveX = X - centerX;
			MoveY = Y - centerY;

			X = centerX;
			Y = centerY;

			ApplyPos();

			if (UM_LastFrame + 1 < GameEngine.ProcFrame) // ? 1フレーム以上更新しなかった。
			{
				MoveX = 0;
				MoveY = 0;
			}
			UM_LastFrame = GameEngine.ProcFrame;
		}
	}
}
