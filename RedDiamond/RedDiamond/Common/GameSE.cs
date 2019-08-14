using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameSE
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public const int HANDLE_COUNT = 64;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameSound Sound;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public double Volume = 0.5; // 0.0 ～ 1.0
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int HandleIndex = 0;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameSE(string file)
			: this(new GameSound(file, HANDLE_COUNT))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameSE(Func<byte[]> getFileData)
			: this(new GameSound(getFileData, HANDLE_COUNT))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameSE(GameSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = this.UpdateVolume_NoCheck;

			GameSEUtils.Add(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Play()
		{
			GameSEUtils.Play(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Stop()
		{
			GameSEUtils.Stop(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetVolume(double volume)
		{
			this.Volume = volume;
			this.UpdateVolume();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void UpdateVolume()
		{
			if (this.Sound.IsLoaded())
				this.UpdateVolume_NoCheck();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void UpdateVolume_NoCheck()
		{
			double mixedVolume = GameSoundUtils.MixVolume(GameGround.SEVolume, this.Volume);

			for (int index = 0; index < HANDLE_COUNT; index++)
				GameSoundUtils.SetVolume(this.Sound.GetHandle(index), mixedVolume);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Touch()
		{
			this.Sound.GetHandle(0);
		}
	}
}
