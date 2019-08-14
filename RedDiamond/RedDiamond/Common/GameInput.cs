using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameInput
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class Button
		{
			public int BtnId = -1; // -1 == 未割り当て
			public int KeyId = -1; // -1 == 未割り当て

			// <---- prm

			public int Status = 0;

			public int GetInput()
			{
				return 1 <= GameEngine.FreezeInputFrame ? 0 : this.Status;
			}

			public bool IsPound()
			{
				return GameUtils.IsPound(this.GetInput());
			}

			private int[] BackupData = null;

			public void Backup()
			{
				this.BackupData = new int[] { this.BtnId, this.KeyId };
			}

			public void Restore()
			{
				int c = 0;

				this.BtnId = this.BackupData[c++];
				this.KeyId = this.BackupData[c++];

				this.BackupData = null;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button DIR_2 = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button DIR_4 = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button DIR_6 = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button DIR_8 = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button A = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button B = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button C = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button D = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button E = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button F = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button L = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button R = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button PAUSE = new Button();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Button START = new Button();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void MixInput(Button button)
		{
			bool keyDown = 1 <= GameKey.GetInput(button.KeyId);
			bool btnDown = 1 <= GamePad.GetInput(GameGround.PrimaryPadId, button.BtnId);

			GameUtils.UpdateInput(ref button.Status, keyDown || btnDown);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void EachFrame()
		{
			int freezeInputFrame_BKUP = GameEngine.FreezeInputFrame;
			GameEngine.FreezeInputFrame = 0;

			MixInput(DIR_2);
			MixInput(DIR_4);
			MixInput(DIR_6);
			MixInput(DIR_8);
			MixInput(A);
			MixInput(B);
			MixInput(C);
			MixInput(D);
			MixInput(E);
			MixInput(F);
			MixInput(L);
			MixInput(R);
			MixInput(PAUSE);
			MixInput(START);

			GameEngine.FreezeInputFrame = freezeInputFrame_BKUP;
		}
	}
}
