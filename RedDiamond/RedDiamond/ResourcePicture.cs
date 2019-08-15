using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Logo = DDPictureLoaders.Standard(@"Title\Logo.png");
		public DDPicture Title = DDPictureLoaders.Standard(@"Title\Title.png");
		public DDPicture TitleBtnBack = DDPictureLoaders.Standard(@"Title\TitleBtnBack.png");
		public DDPicture TitleBtnConfig = DDPictureLoaders.Standard(@"Title\TitleBtnConfig.png");
		public DDPicture TitleBtnExit = DDPictureLoaders.Standard(@"Title\TitleBtnExit.png");
		public DDPicture TitleBtnStart = DDPictureLoaders.Standard(@"Title\TitleBtnStart.png");
		public DDPicture TitleItemContinue = DDPictureLoaders.Standard(@"Title\TitleItemContinue.png");
		public DDPicture TitleItemStart = DDPictureLoaders.Standard(@"Title\TitleItemStart.png");
		public DDPicture TitleWall = DDPictureLoaders.Standard(@"Title\TitleWall.png");
	}
}
