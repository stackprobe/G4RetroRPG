using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourcePicture
	{
		public GamePicture Logo = GamePictureLoaders.Standard(@"Title\Logo.png");
		public GamePicture Title = GamePictureLoaders.Standard(@"Title\Title.png");
		public GamePicture TitleBtnBack = GamePictureLoaders.Standard(@"Title\TitleBtnBack.png");
		public GamePicture TitleBtnConfig = GamePictureLoaders.Standard(@"Title\TitleBtnConfig.png");
		public GamePicture TitleBtnExit = GamePictureLoaders.Standard(@"Title\TitleBtnExit.png");
		public GamePicture TitleBtnStart = GamePictureLoaders.Standard(@"Title\TitleBtnStart.png");
		public GamePicture TitleItemContinue = GamePictureLoaders.Standard(@"Title\TitleItemContinue.png");
		public GamePicture TitleItemStart = GamePictureLoaders.Standard(@"Title\TitleItemStart.png");
		public GamePicture TitleWall = GamePictureLoaders.Standard(@"Title\TitleWall.png");
	}
}
