using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameMain2
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Perform(Action routine)
		{
			try
			{
				GameMain.GameStart();

				try
				{
					routine();
				}
				catch (GameCoffeeBreak)
				{ }

				GameMain.GameEnd();
			}
			catch
			{
				GameMain.GameErrorEnd();
				throw;
			}
		}
	}
}
