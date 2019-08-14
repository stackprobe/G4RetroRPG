using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameGround
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameTaskList EL = new GameTaskList();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int PrimaryPadId = -1; // -1 == 未設定
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameSubScreen MainScreen = null; // null == 不使用
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static I4Rect MonitorRect;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreen_W = GameConsts.Screen_W;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreen_H = GameConsts.Screen_H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_L;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_T;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_W = -1; // -1 == RealScreenDraw_LTWH 不使用
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int RealScreenDraw_H;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double MusicVolume = GameConsts.DefaultVolume;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double SEVolume = GameConsts.DefaultVolume;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool RO_MouseDispMode = false;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static GameGeneralResource GeneralResource;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static D2Point Camera = new D2Point();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static I2Point ICamera = new I2Point();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void INIT()
		{
			GameInput.DIR_2.BtnId = 0;
			GameInput.DIR_4.BtnId = 1;
			GameInput.DIR_6.BtnId = 2;
			GameInput.DIR_8.BtnId = 3;
			GameInput.A.BtnId = 4;
			GameInput.B.BtnId = 7;
			GameInput.C.BtnId = 5;
			GameInput.D.BtnId = 8;
			GameInput.E.BtnId = 6;
			GameInput.F.BtnId = 9;
			GameInput.L.BtnId = 10;
			GameInput.R.BtnId = 11;
			GameInput.PAUSE.BtnId = 13;
			GameInput.START.BtnId = 12;

			GameInput.DIR_2.KeyId = DX.KEY_INPUT_DOWN;
			GameInput.DIR_4.KeyId = DX.KEY_INPUT_LEFT;
			GameInput.DIR_6.KeyId = DX.KEY_INPUT_RIGHT;
			GameInput.DIR_8.KeyId = DX.KEY_INPUT_UP;
			GameInput.A.KeyId = DX.KEY_INPUT_Z;
			GameInput.B.KeyId = DX.KEY_INPUT_X;
			GameInput.C.KeyId = DX.KEY_INPUT_C;
			GameInput.D.KeyId = DX.KEY_INPUT_V;
			GameInput.E.KeyId = DX.KEY_INPUT_A;
			GameInput.F.KeyId = DX.KEY_INPUT_S;
			GameInput.L.KeyId = DX.KEY_INPUT_D;
			GameInput.R.KeyId = DX.KEY_INPUT_F;
			GameInput.PAUSE.KeyId = DX.KEY_INPUT_SPACE;
			GameInput.START.KeyId = DX.KEY_INPUT_RETURN;

			GameAdditionalEvents.Ground_INIT();
		}
	}
}
