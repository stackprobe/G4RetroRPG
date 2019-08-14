using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameFontRegister
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static WorkingDir WD;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void INIT()
		{
			WD = new WorkingDir();

			GameMain.Finalizers.Add(() =>
			{
				UnloadAll();

				WD.Dispose();
				WD = null;
			});
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Add(string file)
		{
			Add(GameResource.Load(file), Path.GetFileName(file));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Add(byte[] fileData, string localFile)
		{
			string dir = WD.MakePath();
			string file = Path.Combine(dir, localFile);

			FileTools.CreateDir(dir);
			File.WriteAllBytes(file, fileData);

			if (GameWin32.AddFontResourceEx(file, GameWin32.FR_PRIVATE, IntPtr.Zero) == 0) // ? 失敗
				throw new GameError();

			FontFiles.Add(file);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static List<string> FontFiles = new List<string>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void Unload(string file)
		{
			if (GameWin32.RemoveFontResourceEx(file, GameWin32.FR_PRIVATE, IntPtr.Zero) == 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void UnloadAll()
		{
			foreach (string file in FontFiles)
				Unload(file);
		}
	}
}
