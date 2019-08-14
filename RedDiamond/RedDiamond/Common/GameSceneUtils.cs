using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameSceneUtils
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static IEnumerable<GameScene> Create(int frameMax)
		{
			for (int frame = 0; frame <= frameMax; frame++)
			{
				yield return new GameScene()
				{
					Numer = frame,
					Denom = frameMax,
					Rate = (double)frame / frameMax,
				};
			}
		}
	}
}
