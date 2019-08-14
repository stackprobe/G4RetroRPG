using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameAdditionalEvents
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action Ground_INIT = () =>
		{
			//GameGround.RO_MouseDispMode = true;
		};
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action Ground_FNLZ = () => { };
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action PostGameStart = () =>
		{
			//GameFontRegister.Add(@"Font\Genkai-Mincho-font\genkai-mincho.ttf");
		};
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action PostGameStart_G2 = () =>
		{
			//this.Visible = false;
		};
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action<List<string>> Save = lines =>
		{
			lines.Add(DateTime.Now.ToString()); // Dummy
		};
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action<string[]> Load = lines =>
		{
			int c = 0;

			GameUtils.Noop(lines[c++]); // Dummy
		};
	}
}
