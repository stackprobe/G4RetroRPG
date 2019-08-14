using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Game
{
	public static class Title
	{
		private static GameSubScreen WorkScreen;
		private static double WallBokashiRate = 1.0;
		private static double WallBokashiRateDest = 1.0;
		private static double WallZRate = 1.0;
		private static double WallZRateDest = 1.0;

		private const double TITLE_BACK_W = 410;
		private const double TITLE_BACK_H = GameConsts.Screen_H;
		private const double TITLE_BACK_L = ((GameConsts.Screen_W - TITLE_BACK_W) / 2);
		private const double TITLE_BACK_T = 0;
		private const double TITLE_BACK_A = 0.5;

		private static double P_TitleBackW = TITLE_BACK_W;
		private static double P_TitleBackWDest = TITLE_BACK_W;

		private static void DrawWall()
		{
			GameUtils.Approach(ref WallBokashiRate, WallBokashiRateDest, 0.93);
			GameUtils.Approach(ref WallZRate, WallZRateDest, 0.9);

			// ---

			GameSubScreenUtils.ChangeDrawScreen(WorkScreen);
			GameDraw.DrawBegin(Ground.I.Picture.TitleWall, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0);
			GameDraw.DrawZoom(WallZRate);
			GameDraw.DrawEnd();
			DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, DoubleTools.ToInt(WallBokashiRate * 1000.0)); // 1
			DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, DoubleTools.ToInt(WallBokashiRate * 1000.0)); // 2
			GameSubScreenUtils.RestoreDrawScreen();

			GameDraw.DrawSimple(GamePictureLoaders2.Wrapper(WorkScreen), 0, 0);
		}

		private static void DrawTitleBack()
		{
			GameUtils.Approach(ref P_TitleBackW, P_TitleBackWDest, 0.85);

			// ---

			if (P_TitleBackW < 0.01)
				return;

			GameDraw.SetAlpha(TITLE_BACK_A);
			GameDraw.SetBright(0, 0, 0);
			GameDraw.DrawBegin(GameGround.GeneralResource.WhiteBox, GameConsts.Screen_W / 2, GameConsts.Screen_H / 2);
			GameDraw.DrawSetSize_W(P_TitleBackW);
			GameDraw.DrawSetSize_H(GameConsts.Screen_H);
			GameDraw.DrawEnd();
			GameDraw.Reset();
		}

		private static void TitleConfigResetPlayData()
		{
			GameEngine.FreezeInput();

			for (; ; )
			{
				GameMouse.UpdatePos();

				if (GameMouse.Get_L() == 1)
				{
					int x = GameMouse.X;
					int y = GameMouse.Y;

					if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(270, 280, 370, 320)) == false) // はい
					{
						// TODO SE

						// TODO reset play data

						SaveData.HasSaveData = false; // セーブデータもリセットする必要あり。
						break;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(520, 280, 690, 320)) == false) // いいえ
					{
						break;
					}
				}
				DrawWall();
				DrawTitleBack();

				PrintByFont.SetPrint();
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　プレイデータを消去します。よろしいですか？");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　は　い　　　　　い　い　え");

				GameEngine.EachFrame();
			}
		}

		private static void TitleConfig()
		{
			P_TitleBackWDest = GameConsts.Screen_W;

			GameEngine.FreezeInput();

			for (; ; )
			{
				GameMouse.UpdatePos();

				if (GameUtils.IsPound(GameMouse.Get_L()))
				{
					int x = GameMouse.X;
					int y = GameMouse.Y;

					if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(270, 120, 470, 160)) == false) // [960x540]
					{
						GameMain.SetScreenSize(960, 540);
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(530, 120, 800, 160)) == false) // [1440x810]
					{
						GameMain.SetScreenSize(1440, 810);
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(270, 160, 470, 200)) == false) // [1920x1080]
					{
						GameMain.SetScreenSize(1920, 1080);
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(530, 160, 800, 200)) == false) // [フルスクリーン]
					{
						int w = GameGround.MonitorRect.W;
						int h = (GameConsts.Screen_H * GameGround.MonitorRect.W) / GameConsts.Screen_W;

						if (GameGround.MonitorRect.H < h)
						{
							h = GameGround.MonitorRect.H;
							w = (GameConsts.Screen_W * GameGround.MonitorRect.H) / GameConsts.Screen_H;

							if (GameGround.MonitorRect.W < w)
								throw new GameError();
						}
						GameMain.SetScreenSize(GameGround.MonitorRect.W, GameGround.MonitorRect.H);

						GameGround.RealScreenDraw_L = (GameGround.MonitorRect.W - w) / 2;
						GameGround.RealScreenDraw_T = (GameGround.MonitorRect.H - h) / 2;
						GameGround.RealScreenDraw_W = w;
						GameGround.RealScreenDraw_H = h;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(400, 240, 530, 280)) == false)
					{
						GameGround.MusicVolume += 0.01;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(550, 240, 690, 280)) == false)
					{
						GameGround.MusicVolume -= 0.01;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(700, 240, 900, 280)) == false)
					{
						GameGround.MusicVolume = GameConsts.DefaultVolume;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(400, 320, 530, 360)) == false)
					{
						GameGround.SEVolume += 0.01;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(550, 320, 690, 360)) == false)
					{
						GameGround.SEVolume -= 0.01;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(700, 320, 900, 360)) == false)
					{
						GameGround.SEVolume = GameConsts.DefaultVolume;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(270, 400, 600, 440)) == false)
					{
						TitleConfigResetPlayData();
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(830, 480, 930, 520)) == false)
					{
						break;
					}
					GameGround.MusicVolume = DoubleTools.Range(GameGround.MusicVolume, 0.0, 1.0);
					GameGround.SEVolume = DoubleTools.Range(GameGround.SEVolume, 0.0, 1.0);
				}
				DrawWall();
				DrawTitleBack();

				PrintByFont.SetPrint();
				PrintByFont.Print("");
				PrintByFont.Print("　設定");
				PrintByFont.Print("");
				PrintByFont.Print("　画面サイズ　　　[960x540]      [1440x810]");
				PrintByFont.Print("　　　　　　　　　[1920x1080]    [フルスクリーン]");
				PrintByFont.Print("");
				PrintByFont.Print(string.Format("　ＢＧＭ音量　　　%.2f　　[上げる]　[下げる]　[デフォルト]", GameGround.MusicVolume));
				PrintByFont.Print("");
				PrintByFont.Print(string.Format("　ＳＥ音量　　　　%.2f　　[上げる]　[下げる]　[デフォルト]", GameGround.SEVolume));
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　プレイデータリセット");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　　　　　　　　　　　　　　　　　　　[戻る]");

				GameEngine.EachFrame();
			}
		}

		private static void TitleGameStart2()
		{
			WallBokashiRateDest = 0.0;
			WallZRateDest = 1.02;
			P_TitleBackWDest = 0;

			const int TGS_BACK_X = 830;
			const int TGS_BACK_Y = 460;

			double selRateBack = 0.0;

			GameEngine.FreezeInput();

			for (; ; )
			{
				DrawWall();
				DrawTitleBack();

				GameDraw.DrawBegin(Ground.I.Picture.TitleBtnBack, TGS_BACK_X, TGS_BACK_Y);
				GameDraw.DrawZoom(1.0 + selRateBack * 0.15);
				GameDraw.DrawEnd();
				NamedRect.SetLastDrawedPicRectName("BACK");

				// <---- 描画

				GameMouse.UpdatePos();

				string pointingName = NamedRect.GetRectName(GameMouse.X, GameMouse.Y);

				if (pointingName == "BACK")
					GameUtils.Approach(ref selRateBack, 1.0, 0.85);
				else
					GameUtils.Approach(ref selRateBack, 0.0, 0.93);

				GameEngine.EachFrame(); // ★★★ EachFrame

				if (GameMouse.Get_L() == 1)
				{
					if (pointingName == "BACK")
						break;

					int x = GameMouse.X;
					int y = GameMouse.Y;

					if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(330, 90, 640, 360)) == false) // 入口
					{
						// TODO

						SaveData.HasSaveData = true; // kari
					}
				}
			}
		}

		private static bool TitleGameStartConfirm()
		{
			bool ret = false;

			P_TitleBackWDest = GameConsts.Screen_W;

			GameEngine.FreezeInput();

			for (; ; )
			{
				GameMouse.UpdatePos();

				if (GameMouse.Get_L() == 1)
				{
					int x = GameMouse.X;
					int y = GameMouse.Y;

					if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(270, 280, 370, 320)) == false) // はい
					{
						ret = true;
						break;
					}
					else if (GameUtils.IsOut(new D2Point(x, y), new D4Rect(520, 280, 690, 320)) == false) // いいえ
					{
						break;
					}
				}
				DrawWall();
				DrawTitleBack();

				PrintByFont.SetPrint();
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　セーブデータを消去します。よろしいですか？");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　は　い　　　　　い　い　え");

				GameEngine.EachFrame();
			}
			return ret;
		}

		private static void TitleGameStart()
		{
			double selRateContinue = 0.0;
			double selRateStart = 0.0;
			double selRateBack = 0.0;

			const int TGS_CONTINUE_X = GameConsts.Screen_W / 2;
			const int TGS_CONTINUE_Y = 150;
			const int TGS_START_X = GameConsts.Screen_W / 2;
			const int TGS_START_Y = 390;
			const int TGS_BACK_X = 830;
			const int TGS_BACK_Y = 460;

		returned:
			GameEngine.FreezeInput();

			WallBokashiRateDest = 1.0;
			WallZRateDest = 1.0;
			P_TitleBackWDest = 450;

			for (; ; )
			{
				bool continueEnabled = SaveData.HasSaveData;

				// 描画 ---->

				DrawWall();
				DrawTitleBack();

				if (continueEnabled)
				{
					GameDraw.DrawBegin(Ground.I.Picture.TitleItemContinue, TGS_CONTINUE_X, TGS_CONTINUE_Y);
					GameDraw.DrawZoom(1.0 + selRateContinue * 0.1);
					GameDraw.DrawEnd();
					NamedRect.SetLastDrawedPicRectName("CONTINUE");
				}
				else
				{
					GameDraw.SetBright(0.6, 0.6, 0.6);
					GameDraw.DrawCenter(Ground.I.Picture.TitleItemContinue, TGS_CONTINUE_X, TGS_CONTINUE_Y);
					GameDraw.Reset();
				}
				GameDraw.DrawBegin(Ground.I.Picture.TitleItemStart, TGS_START_X, TGS_START_Y);
				GameDraw.DrawZoom(1.0 + selRateStart * 0.1);
				GameDraw.DrawEnd();
				NamedRect.SetLastDrawedPicRectName("START");
				GameDraw.DrawBegin(Ground.I.Picture.TitleBtnBack, TGS_BACK_X, TGS_BACK_Y);
				GameDraw.DrawZoom(1.0 + selRateBack * 0.15);
				GameDraw.DrawEnd();
				NamedRect.SetLastDrawedPicRectName("BACK");

				// <---- 描画

				GameMouse.UpdatePos();

				string pointingName = NamedRect.GetRectName(GameMouse.X, GameMouse.Y);

				if (pointingName == "CONTINUE")
					GameUtils.Approach(ref selRateContinue, 1.0, 0.8);
				else
					GameUtils.Approach(ref selRateContinue, 0.0, 0.85);

				if (pointingName == "START")
					GameUtils.Approach(ref selRateStart, 1.0, 0.8);
				else
					GameUtils.Approach(ref selRateStart, 0.0, 0.85);

				if (pointingName == "BACK")
					GameUtils.Approach(ref selRateBack, 1.0, 0.85);
				else
					GameUtils.Approach(ref selRateBack, 0.0, 0.93);

				GameEngine.EachFrame(); // ★★★ EachFrame

				if (GameMouse.Get_L() == 1)
				{
					if (pointingName == "BACK")
						break;

					if (pointingName == "START")
					{
						if (SaveData.HasSaveData)
						{
							if (TitleGameStartConfirm())
							{
								SaveData.HasSaveData = false;
								TitleGameStart2();
							}
						}
						else
							TitleGameStart2();

						selRateContinue = 0.0;
						selRateStart = 0.0;
						selRateBack = 1.0;

						goto returned;
					}
					if (pointingName == "CONTINUE")
					{
						TitleGameStart2();

						selRateContinue = 0.0;
						selRateStart = 0.0;
						selRateBack = 1.0;

						goto returned;
					}
				}
			}
		}

		public static void TitleMain()
		{
			WorkScreen = new GameSubScreen(GameConsts.Screen_W, GameConsts.Screen_H);

			foreach (GameScene scene in GameSceneUtils.Create(30))
			{
				GameCurtain.DrawCurtain();
				GameEngine.EachFrame();
			}
			foreach (GameScene scene in GameSceneUtils.Create(30))
			{
				double a = -1.0 + scene.Rate;

				GameDraw.DrawBegin(Ground.I.Picture.Logo, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0);
				GameDraw.DrawZoom(1.0 + a * a * 0.1);
				GameDraw.DrawEnd();
				//GameDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);

				GameCurtain.DrawCurtain(a);
				GameEngine.EachFrame();
			}
			foreach (GameScene scene in GameSceneUtils.Create(60))
			{
				GameDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);
				GameEngine.EachFrame();
			}
			foreach (GameScene scene in GameSceneUtils.Create(30))
			{
				double a = -scene.Rate;

				GameDraw.DrawBegin(Ground.I.Picture.Logo, GameConsts.Screen_W / 2.0, GameConsts.Screen_H / 2.0);
				GameDraw.DrawZoom(1.0 + a * a * 0.1);
				GameDraw.DrawEnd();
				//GameDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);

				GameCurtain.DrawCurtain(a);
				GameEngine.EachFrame();
			}
			foreach (GameScene scene in GameSceneUtils.Create(30))
			{
				GameCurtain.DrawCurtain();
				GameEngine.EachFrame();
			}

			const int TITLE_BTN_START_X = 130;
			const int TITLE_BTN_START_Y = 460;

			const int TITLE_BTN_CONFIG_X = 830;
			const int TITLE_BTN_CONFIG_Y = 70;

			const int TITLE_BTN_EXIT_X = 830;
			const int TITLE_BTN_EXIT_Y = 460;

			{
				double a = 0.0;
				double z = 1.3;
				bool titleBackOn = false;
				double titleBackA = 0.0;
				double titleBackZ = 0.1;
				bool titleOn = false;
				double titleA = 0.0;
				double titleZ = 1.3;
				bool[] titleBtnsOn = new bool[] { false, false, false };
				double[] titleBtnsA = new double[3] { 0, 0, 0 };
				double[] titleBtnsZ = new double[3] { 1.05, 1.1, 1.15 };

				foreach (GameScene scene in GameSceneUtils.Create(120))
				{
					if (scene.Numer == 30)
						titleBackOn = true;

					if (scene.Numer == 60)
						titleOn = true;

					if (scene.Numer == 90)
						titleBtnsOn[0] = true;

					if (scene.Numer == 100)
						titleBtnsOn[1] = true;

					if (scene.Numer == 110)
						titleBtnsOn[2] = true;

					GameCurtain.DrawCurtain();

					// Wall >

					GameSubScreenUtils.ChangeDrawScreen(WorkScreen.GetHandle());

					GameDraw.SetAlpha(a);
					GameDraw.DrawBegin(Ground.I.Picture.TitleWall, GameConsts.Screen_W / 2, GameConsts.Screen_H / 2);
					GameDraw.DrawZoom(z);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, 1000); // 1
					DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, 1000); // 2

					GameSubScreenUtils.RestoreDrawScreen();

					GameDraw.DrawSimple(GamePictureLoaders2.Wrapper(WorkScreen), 0, 0);

					// < Wall

					GameDraw.SetAlpha(titleBackA);
					GameDraw.SetBright(0, 0, 0);
					GameDraw.DrawBeginRect(GameGround.GeneralResource.WhiteBox, TITLE_BACK_L, TITLE_BACK_T, TITLE_BACK_W, TITLE_BACK_H);
					GameDraw.DrawZoom_X(titleBackZ);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					GameDraw.SetAlpha(titleA);
					GameDraw.DrawBegin(Ground.I.Picture.Title, GameConsts.Screen_W / 2, GameConsts.Screen_H / 2);
					GameDraw.DrawZoom(titleZ);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					GameDraw.SetAlpha(titleBtnsA[0]);
					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnStart, TITLE_BTN_START_X, TITLE_BTN_START_Y);
					GameDraw.DrawZoom(titleBtnsZ[0]);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					GameDraw.SetAlpha(titleBtnsA[1]);
					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnConfig, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
					GameDraw.DrawZoom(titleBtnsZ[1]);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					GameDraw.SetAlpha(titleBtnsA[2]);
					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnExit, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
					GameDraw.DrawZoom(titleBtnsZ[2]);
					GameDraw.DrawEnd();
					GameDraw.Reset();

					GameUtils.Approach(ref a, 1.0, 0.97);
					GameUtils.Approach(ref z, 1.0, 0.95);

					if (titleBackOn)
					{
						GameUtils.Approach(ref titleBackA, TITLE_BACK_A, 0.95);
						GameUtils.Approach(ref titleBackZ, 1.0, 0.9);
					}
					if (titleOn)
					{
						GameUtils.Approach(ref titleA, 1.0, 0.93);
						GameUtils.Approach(ref titleZ, 1.0, 0.8);
					}
					for (int c = 0; c < 3; c++)
					{
						if (titleBtnsOn[c])
						{
							GameUtils.Approach(ref titleBtnsA[c], 1.0, 0.77);
							GameUtils.Approach(ref titleBtnsZ[c], 1.0, 0.73);
						}
					}
					GameEngine.EachFrame();
				}
			}

			{
				double selRateStart = 0.0;
				double selRateConfig = 0.0;
				double selRateExit = 0.0;

			returned:
				GameEngine.FreezeInput();

				WallBokashiRateDest = 1.0;
				P_TitleBackWDest = TITLE_BACK_W;

				for (; ; )
				{
					DrawWall();
					DrawTitleBack();

					GameDraw.DrawCenter(Ground.I.Picture.Title, GameConsts.Screen_W / 2, GameConsts.Screen_H / 2);

					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnStart, TITLE_BTN_START_X, TITLE_BTN_START_Y);
					GameDraw.DrawZoom(1.0 + selRateStart * 0.2);
					GameDraw.DrawEnd();
					NamedRect.SetLastDrawedPicRectName("START");
					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnConfig, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
					GameDraw.DrawZoom(1.0 + selRateConfig * 0.15);
					GameDraw.DrawEnd();
					NamedRect.SetLastDrawedPicRectName("CONFIG");
					GameDraw.DrawBegin(Ground.I.Picture.TitleBtnExit, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
					GameDraw.DrawZoom(1.0 + selRateExit * 0.15);
					GameDraw.DrawEnd();
					NamedRect.SetLastDrawedPicRectName("EXIT");

					// <---- 描画

					GameMouse.UpdatePos();

					string pointingName = NamedRect.GetRectName(GameMouse.X, GameMouse.Y);

					if (pointingName == "START")
						GameUtils.Approach(ref selRateStart, 1.0, 0.85);
					else
						GameUtils.Approach(ref selRateStart, 0.0, 0.9);

					if (pointingName == "CONFIG")
						GameUtils.Approach(ref selRateConfig, 1.0, 0.9);
					else
						GameUtils.Approach(ref selRateConfig, 0.0, 0.93);

					if (pointingName == "EXIT")
						GameUtils.Approach(ref selRateExit, 1.0, 0.9);
					else
						GameUtils.Approach(ref selRateExit, 0.0, 0.93);

					GameEngine.EachFrame(); // ★★★ EachFrame

					if (GameMouse.Get_L() == 1)
					{
						if (pointingName == "EXIT")
							break;

						if (pointingName == "CONFIG")
						{
							TitleConfig();
							goto returned;
						}
						if (pointingName == "START")
						{
							TitleGameStart();

							selRateStart = 0.0;
							selRateExit = 1.0;

							goto returned;
						}
					}
				}
			}

			GameCurtain.SetCurtain(30, -1.0);
			GameMusicUtils.Fade();

			foreach (GameScene scene in GameSceneUtils.Create(40))
			{
				DrawWall();
				GameEngine.EachFrame();
			}
		}
	}
}
