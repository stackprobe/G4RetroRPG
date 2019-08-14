using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameGeneralResource
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GamePicture Dummy = GamePictureLoaders.Standard(@"General\Dummy.png");
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GamePicture WhiteBox = GamePictureLoaders.Standard(@"General\WhiteBox.png");

		// 新しいリソースをここへ追加...
	}
}
