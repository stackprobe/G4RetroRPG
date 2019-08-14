using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;
using System.Drawing;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameSubScreen : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int W; // -1 == Disposed
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int H;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private bool AFlag;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int Handle = -1; // -1 == Unloaded

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameSubScreen(int w, int h, bool aFlag = false)
		{
			this.W = w;
			this.H = h;
			this.AFlag = aFlag;
			this.Handle = -1;

			GameSubScreenUtils.Add(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.W == -1) // ? Already disposed
				return;

			GameSubScreenUtils.Remove(this);

			this.Unload();

			this.W = -1;
			this.H = -1;
			this.AFlag = false;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Unload()
		{
			if (this.Handle != -1)
			{
				if (DX.DeleteGraph(this.Handle) != 0) // ? 失敗
					throw new GameError();

				this.Handle = -1;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetHandle()
		{
			if (this.Handle == -1)
			{
				this.Handle = DX.MakeScreen(this.W, this.H, this.AFlag ? 1 : 0);

				if (this.Handle == -1) // ? 失敗
					throw new GameError();
			}
			return this.Handle;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void ChangeDrawScreen()
		{
			GameSubScreenUtils.ChangeDrawScreen(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public I2Size GetSize()
		{
			return new I2Size(this.W, this.H);
		}
	}
}
