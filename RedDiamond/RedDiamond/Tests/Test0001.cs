using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			GameCurtain.SetCurtain();
			GameEngine.FreezeInput();

			foreach (GameScene scene in GameSceneUtils.Create(600))
			{
				if (GameInput.A.GetInput() == 1)
				{
					break;
				}

				GameCurtain.DrawCurtain();

				GameDraw.DrawBegin(GameGround.GeneralResource.Dummy, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0);
				GameDraw.DrawZoom(5.0);
				GameDraw.DrawRotate(scene.Rate * 5.0);
				GameDraw.DrawEnd();

				GameEngine.EachFrame();
			}

			GameEngine.FreezeInput();
		}
	}
}
