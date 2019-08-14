using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameMusic
	{
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
		public GameMusic(string file)
			: this(new GameSound(file, 1))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameMusic(Func<byte[]> getFileData)
			: this(new GameSound(getFileData, 1))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameMusic(GameSound sound_binding)
		{
			this.Sound = sound_binding;
			this.Sound.PostLoaded = () => GameSoundUtils.SetVolume(this.Sound.GetHandle(0), 0.0); // ロードしたらミュートしておく。

			GameMusicUtils.Add(this);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Play(bool once = false, bool resume = false, double volume = 1.0, int fadeFrameMax = 30)
		{
			GameMusicUtils.Play(this, once, resume, volume, fadeFrameMax);
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
