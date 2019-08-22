using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using DxLibDLL;
using Charlotte.Tools;

namespace Charlotte.Main01
{
	public static class Title
	{
		private static DDSubScreen WorkScreen;
		private static double WallBokashiRate = 1.0;
		private static double WallBokashiRateDest = 1.0;
		private static double WallZRate = 1.0;
		private static double WallZRateDest = 1.0;

		private const double TITLE_BACK_W = 410;
		private const double TITLE_BACK_H = DDConsts.Screen_H;
		private const double TITLE_BACK_L = ((DDConsts.Screen_W - TITLE_BACK_W) / 2);
		private const double TITLE_BACK_T = 0;
		private const double TITLE_BACK_A = 0.5;

		private static double P_TitleBackW = TITLE_BACK_W;
		private static double P_TitleBackWDest = TITLE_BACK_W;

		private static void DrawWall()
		{
			DDUtils.Approach(ref WallBokashiRate, WallBokashiRateDest, 0.93);
			DDUtils.Approach(ref WallZRate, WallZRateDest, 0.9);

			// ---

			DDSubScreenUtils.ChangeDrawScreen(WorkScreen);
			DDDraw.DrawBegin(Ground.I.Picture.TitleWall, DDConsts.Screen_W / 2.0, DDConsts.Screen_H / 2.0);
			DDDraw.DrawZoom(WallZRate);
			DDDraw.DrawEnd();
			DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, DoubleTools.ToInt(WallBokashiRate * 1000.0)); // 1
			DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, DoubleTools.ToInt(WallBokashiRate * 1000.0)); // 2
			DDSubScreenUtils.RestoreDrawScreen();

			DDDraw.DrawSimple(DDPictureLoaders2.Wrapper(WorkScreen), 0, 0);
		}

		private static void DrawTitleBack()
		{
			DDUtils.Approach(ref P_TitleBackW, P_TitleBackWDest, 0.85);

			// ---

			if (P_TitleBackW < 0.01)
				return;

			DDDraw.SetAlpha(TITLE_BACK_A);
			DDDraw.SetBright(0, 0, 0);
			DDDraw.DrawBegin(DDGround.GeneralResource.WhiteBox, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
			DDDraw.DrawSetSize_W(P_TitleBackW);
			DDDraw.DrawSetSize_H(DDConsts.Screen_H);
			DDDraw.DrawEnd();
			DDDraw.Reset();
		}

		private static void TitleConfigResetPlayData()
		{
			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDMouse.L.GetInput() == 1)
				{
					int x = DDMouse.X;
					int y = DDMouse.Y;

					if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(270, 280, 370, 320)) == false) // はい
					{
						// TODO SE

						// TODO reset play data

						SaveData.HasSaveData = false; // セーブデータもリセットする必要あり。
						break;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(520, 280, 690, 320)) == false) // いいえ
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

				DDEngine.EachFrame();
			}
		}

		private static void TitleConfig()
		{
			P_TitleBackWDest = DDConsts.Screen_W;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDUtils.IsPound(DDMouse.L.GetInput()))
				{
					int x = DDMouse.X;
					int y = DDMouse.Y;

					if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(270, 120, 470, 160)) == false) // [960x540]
					{
						DDMain.SetScreenSize(960, 540);
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(530, 120, 800, 160)) == false) // [1440x810]
					{
						DDMain.SetScreenSize(1440, 810);
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(270, 160, 470, 200)) == false) // [1920x1080]
					{
						DDMain.SetScreenSize(1920, 1080);
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(530, 160, 800, 200)) == false) // [フルスクリーン]
					{
						int w = DDGround.MonitorRect.W;
						int h = (DDConsts.Screen_H * DDGround.MonitorRect.W) / DDConsts.Screen_W;

						if (DDGround.MonitorRect.H < h)
						{
							h = DDGround.MonitorRect.H;
							w = (DDConsts.Screen_W * DDGround.MonitorRect.H) / DDConsts.Screen_H;

							if (DDGround.MonitorRect.W < w)
								throw new DDError();
						}
						DDMain.SetScreenSize(DDGround.MonitorRect.W, DDGround.MonitorRect.H);

						DDGround.RealScreenDraw_L = (DDGround.MonitorRect.W - w) / 2;
						DDGround.RealScreenDraw_T = (DDGround.MonitorRect.H - h) / 2;
						DDGround.RealScreenDraw_W = w;
						DDGround.RealScreenDraw_H = h;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(400, 240, 530, 280)) == false)
					{
						DDGround.MusicVolume += 0.01;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(550, 240, 690, 280)) == false)
					{
						DDGround.MusicVolume -= 0.01;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(700, 240, 900, 280)) == false)
					{
						DDGround.MusicVolume = DDConsts.DefaultVolume;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(400, 320, 530, 360)) == false)
					{
						DDGround.SEVolume += 0.01;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(550, 320, 690, 360)) == false)
					{
						DDGround.SEVolume -= 0.01;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(700, 320, 900, 360)) == false)
					{
						DDGround.SEVolume = DDConsts.DefaultVolume;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(270, 400, 600, 440)) == false)
					{
						TitleConfigResetPlayData();
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(830, 480, 930, 520)) == false)
					{
						break;
					}
					DDGround.MusicVolume = DoubleTools.Range(DDGround.MusicVolume, 0.0, 1.0);
					DDGround.SEVolume = DoubleTools.Range(DDGround.SEVolume, 0.0, 1.0);
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
				PrintByFont.Print(string.Format("　ＢＧＭ音量　　　{0:F2}　　[上げる]　[下げる]　[デフォルト]", DDGround.MusicVolume));
				PrintByFont.Print("");
				PrintByFont.Print(string.Format("　ＳＥ音量　　　　{0:F2}　　[上げる]　[下げる]　[デフォルト]", DDGround.SEVolume));
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　プレイデータリセット");
				PrintByFont.Print("");
				PrintByFont.Print("　　　　　　　　　　　　　　　　　　　　　　　　　　　[戻る]");

				DDEngine.EachFrame();
			}
		}

		private static void TitleDDStart2()
		{
			WallBokashiRateDest = 0.0;
			WallZRateDest = 1.02;
			P_TitleBackWDest = 0;

			const int TGS_BACK_X = 830;
			const int TGS_BACK_Y = 460;

			double selRateBack = 0.0;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DrawWall();
				DrawTitleBack();

				DDDraw.DrawBegin(Ground.I.Picture.TitleBtnBack, TGS_BACK_X, TGS_BACK_Y);
				DDDraw.DrawZoom(1.0 + selRateBack * 0.15);
				DDDraw.DrawEnd();
				NamedRect.SetLastDrawedRectName("BACK");

				// <---- 描画

				DDMouse.UpdatePos();

				string pointingName = NamedRect.GetRectName(DDMouse.X, DDMouse.Y);

				if (pointingName == "BACK")
					DDUtils.Approach(ref selRateBack, 1.0, 0.85);
				else
					DDUtils.Approach(ref selRateBack, 0.0, 0.93);

				DDEngine.EachFrame(); // ★★★ EachFrame

				if (DDMouse.L.GetInput() == 1)
				{
					if (pointingName == "BACK")
						break;

					int x = DDMouse.X;
					int y = DDMouse.Y;

					if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(330, 90, 640, 360)) == false) // 入口
					{
						// TODO

						SaveData.HasSaveData = true; // kari
					}
				}
			}
		}

		private static bool TitleDDStartConfirm()
		{
			bool ret = false;

			P_TitleBackWDest = DDConsts.Screen_W;

			DDEngine.FreezeInput();

			for (; ; )
			{
				DDMouse.UpdatePos();

				if (DDMouse.L.GetInput() == 1)
				{
					int x = DDMouse.X;
					int y = DDMouse.Y;

					if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(270, 280, 370, 320)) == false) // はい
					{
						ret = true;
						break;
					}
					else if (DDUtils.IsOut(new D2Point(x, y), D4Rect.LTRB(520, 280, 690, 320)) == false) // いいえ
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

				DDEngine.EachFrame();
			}
			return ret;
		}

		private static void TitleDDStart()
		{
			double selRateContinue = 0.0;
			double selRateStart = 0.0;
			double selRateBack = 0.0;

			const int TGS_CONTINUE_X = DDConsts.Screen_W / 2;
			const int TGS_CONTINUE_Y = 150;
			const int TGS_START_X = DDConsts.Screen_W / 2;
			const int TGS_START_Y = 390;
			const int TGS_BACK_X = 830;
			const int TGS_BACK_Y = 460;

		returned:
			DDEngine.FreezeInput();

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
					DDDraw.DrawBegin(Ground.I.Picture.TitleItemContinue, TGS_CONTINUE_X, TGS_CONTINUE_Y);
					DDDraw.DrawZoom(1.0 + selRateContinue * 0.1);
					DDDraw.DrawEnd();
					NamedRect.SetLastDrawedRectName("CONTINUE");
				}
				else
				{
					DDDraw.SetBright(0.6, 0.6, 0.6);
					DDDraw.DrawCenter(Ground.I.Picture.TitleItemContinue, TGS_CONTINUE_X, TGS_CONTINUE_Y);
					DDDraw.Reset();
				}
				DDDraw.DrawBegin(Ground.I.Picture.TitleItemStart, TGS_START_X, TGS_START_Y);
				DDDraw.DrawZoom(1.0 + selRateStart * 0.1);
				DDDraw.DrawEnd();
				NamedRect.SetLastDrawedRectName("START");
				DDDraw.DrawBegin(Ground.I.Picture.TitleBtnBack, TGS_BACK_X, TGS_BACK_Y);
				DDDraw.DrawZoom(1.0 + selRateBack * 0.15);
				DDDraw.DrawEnd();
				NamedRect.SetLastDrawedRectName("BACK");

				// <---- 描画

				DDMouse.UpdatePos();

				string pointingName = NamedRect.GetRectName(DDMouse.X, DDMouse.Y);

				if (pointingName == "CONTINUE")
					DDUtils.Approach(ref selRateContinue, 1.0, 0.8);
				else
					DDUtils.Approach(ref selRateContinue, 0.0, 0.85);

				if (pointingName == "START")
					DDUtils.Approach(ref selRateStart, 1.0, 0.8);
				else
					DDUtils.Approach(ref selRateStart, 0.0, 0.85);

				if (pointingName == "BACK")
					DDUtils.Approach(ref selRateBack, 1.0, 0.85);
				else
					DDUtils.Approach(ref selRateBack, 0.0, 0.93);

				DDEngine.EachFrame(); // ★★★ EachFrame

				if (DDMouse.L.GetInput() == 1)
				{
					if (pointingName == "BACK")
						break;

					if (pointingName == "START")
					{
						if (SaveData.HasSaveData)
						{
							if (TitleDDStartConfirm())
							{
								SaveData.HasSaveData = false;
								TitleDDStart2();
							}
						}
						else
							TitleDDStart2();

						selRateContinue = 0.0;
						selRateStart = 0.0;
						selRateBack = 1.0;

						goto returned;
					}
					if (pointingName == "CONTINUE")
					{
						TitleDDStart2();

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
			WorkScreen = new DDSubScreen(DDConsts.Screen_W, DDConsts.Screen_H);

			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				double a = -1.0 + scene.Rate;

				DDDraw.DrawBegin(Ground.I.Picture.Logo, DDConsts.Screen_W / 2.0, DDConsts.Screen_H / 2.0);
				DDDraw.DrawZoom(1.0 + a * a * 0.1);
				DDDraw.DrawEnd();
				//DDDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);

				DDCurtain.DrawCurtain(a);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(60))
			{
				DDDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				double a = -scene.Rate;

				DDDraw.DrawBegin(Ground.I.Picture.Logo, DDConsts.Screen_W / 2.0, DDConsts.Screen_H / 2.0);
				DDDraw.DrawZoom(1.0 + a * a * 0.1);
				DDDraw.DrawEnd();
				//DDDraw.DrawSimple(Ground.I.Picture.Logo, 0, 0);

				DDCurtain.DrawCurtain(a);
				DDEngine.EachFrame();
			}
			foreach (DDScene scene in DDSceneUtils.Create(30))
			{
				DDCurtain.DrawCurtain();
				DDEngine.EachFrame();
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

				foreach (DDScene scene in DDSceneUtils.Create(120))
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

					DDCurtain.DrawCurtain();

					// Wall >

					DDSubScreenUtils.ChangeDrawScreen(WorkScreen.GetHandle());

					DDDraw.SetAlpha(a);
					DDDraw.DrawBegin(Ground.I.Picture.TitleWall, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDDraw.DrawZoom(z);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, 1000); // 1
					DX.GraphFilter(WorkScreen.GetHandle(), DX.DX_GRAPH_FILTER_GAUSS, 16, 1000); // 2

					DDSubScreenUtils.RestoreDrawScreen();

					DDDraw.DrawSimple(DDPictureLoaders2.Wrapper(WorkScreen), 0, 0);

					// < Wall

					DDDraw.SetAlpha(titleBackA);
					DDDraw.SetBright(0, 0, 0);
					DDDraw.DrawBeginRect(DDGround.GeneralResource.WhiteBox, TITLE_BACK_L, TITLE_BACK_T, TITLE_BACK_W, TITLE_BACK_H);
					DDDraw.DrawZoom_X(titleBackZ);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DDDraw.SetAlpha(titleA);
					DDDraw.DrawBegin(Ground.I.Picture.Title, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);
					DDDraw.DrawZoom(titleZ);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DDDraw.SetAlpha(titleBtnsA[0]);
					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnStart, TITLE_BTN_START_X, TITLE_BTN_START_Y);
					DDDraw.DrawZoom(titleBtnsZ[0]);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DDDraw.SetAlpha(titleBtnsA[1]);
					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnConfig, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
					DDDraw.DrawZoom(titleBtnsZ[1]);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DDDraw.SetAlpha(titleBtnsA[2]);
					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnExit, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
					DDDraw.DrawZoom(titleBtnsZ[2]);
					DDDraw.DrawEnd();
					DDDraw.Reset();

					DDUtils.Approach(ref a, 1.0, 0.97);
					DDUtils.Approach(ref z, 1.0, 0.95);

					if (titleBackOn)
					{
						DDUtils.Approach(ref titleBackA, TITLE_BACK_A, 0.95);
						DDUtils.Approach(ref titleBackZ, 1.0, 0.9);
					}
					if (titleOn)
					{
						DDUtils.Approach(ref titleA, 1.0, 0.93);
						DDUtils.Approach(ref titleZ, 1.0, 0.8);
					}
					for (int c = 0; c < 3; c++)
					{
						if (titleBtnsOn[c])
						{
							DDUtils.Approach(ref titleBtnsA[c], 1.0, 0.77);
							DDUtils.Approach(ref titleBtnsZ[c], 1.0, 0.73);
						}
					}
					DDEngine.EachFrame();
				}
			}

			{
				double selRateStart = 0.0;
				double selRateConfig = 0.0;
				double selRateExit = 0.0;

			returned:
				DDEngine.FreezeInput();

				WallBokashiRateDest = 1.0;
				P_TitleBackWDest = TITLE_BACK_W;

				for (; ; )
				{
					DrawWall();
					DrawTitleBack();

					DDDraw.DrawCenter(Ground.I.Picture.Title, DDConsts.Screen_W / 2, DDConsts.Screen_H / 2);

					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnStart, TITLE_BTN_START_X, TITLE_BTN_START_Y);
					DDDraw.DrawZoom(1.0 + selRateStart * 0.2);
					DDDraw.DrawEnd();
					NamedRect.SetLastDrawedRectName("START");
					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnConfig, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
					DDDraw.DrawZoom(1.0 + selRateConfig * 0.15);
					DDDraw.DrawEnd();
					NamedRect.SetLastDrawedRectName("CONFIG");
					DDDraw.DrawBegin(Ground.I.Picture.TitleBtnExit, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
					DDDraw.DrawZoom(1.0 + selRateExit * 0.15);
					DDDraw.DrawEnd();
					NamedRect.SetLastDrawedRectName("EXIT");

					// <---- 描画

					DDMouse.UpdatePos();

					string pointingName = NamedRect.GetRectName(DDMouse.X, DDMouse.Y);

					if (pointingName == "START")
						DDUtils.Approach(ref selRateStart, 1.0, 0.85);
					else
						DDUtils.Approach(ref selRateStart, 0.0, 0.9);

					if (pointingName == "CONFIG")
						DDUtils.Approach(ref selRateConfig, 1.0, 0.9);
					else
						DDUtils.Approach(ref selRateConfig, 0.0, 0.93);

					if (pointingName == "EXIT")
						DDUtils.Approach(ref selRateExit, 1.0, 0.9);
					else
						DDUtils.Approach(ref selRateExit, 0.0, 0.93);

					DDEngine.EachFrame(); // ★★★ EachFrame

					if (DDMouse.L.GetInput() == 1)
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
							TitleDDStart();

							selRateStart = 0.0;
							selRateExit = 1.0;

							goto returned;
						}
					}
				}
			}

			DDCurtain.SetCurtain(30, -1.0);
			DDMusicUtils.Fade();

			foreach (DDScene scene in DDSceneUtils.Create(40))
			{
				DrawWall();
				DDEngine.EachFrame();
			}
		}
	}
}
