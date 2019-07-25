#include "all.h"

static SubScreen_t *WorkScreen;
static double WallBokashiRate = 1.0;
static double WallBokashiRateDest = 1.0;

#define TITLE_BACK_W 410
#define TITLE_BACK_H SCREEN_H
#define TITLE_BACK_L ((SCREEN_W - TITLE_BACK_W) / 2)
#define TITLE_BACK_T 0
#define TITLE_BACK_A 0.5

static double P_TitleBackW = TITLE_BACK_W;
static double P_TitleBackWDest = TITLE_BACK_W;

static void DrawWall(void)
{
	m_approach(WallBokashiRate, WallBokashiRateDest, 0.93);

	// ---

	SetDrawScreen(GetHandle(WorkScreen));
	DrawSimple(P_TITLE_WALL, 0, 0);
	GraphFilter(GetHandle(WorkScreen), DX_GRAPH_FILTER_GAUSS, 16, d2i(WallBokashiRate * 1000.0));
	RestoreDrawScreen();

	DPE_SetGraphicSize(makeI2D(SCREEN_W, SCREEN_H));
	DrawSimple(GetHandle(WorkScreen), 0, 0);
	DPE_Reset();
}
static void DrawTitleBack(void)
{
	m_approach(P_TitleBackW, P_TitleBackWDest, 0.85);

	// ---

	if(P_TitleBackW < 0.01)
		return;

	DPE_SetAlpha(TITLE_BACK_A);
	DPE_SetBright(GetColor(0, 0, 0));
	DrawBegin(P_WHITEBOX, SCREEN_W / 2, SCREEN_H / 2);
	DrawSetSize_W(P_TitleBackW);
	DrawSetSize_H(SCREEN_H);
	DrawEnd();
	DPE_Reset();
}
static void TitleConfigResetPlayData(void)
{
	FreezeInput();

	for(; ; )
	{
		UpdateMousePos();
		
		// debug
		//clsDx();
		//printfDx("%d %d\n", MouseX, MouseY);

		if(GetMouInput(MOUBTN_L) == 1)
		{
			int x = MouseX;
			int y = MouseY;

			if(!IsOut(x, y, 270, 280, 370, 320)) // はい
			{
				// TODO SE

				// TODO reset play data

				Gnd.HasSaveData = 0; // セーブデータもリセットする必要あり。
				break;
			}
			else if(!IsOut(x, y, 520, 280, 690, 320)) // いいえ
			{
				break;
			}
		}
		DrawWall();
		DrawTitleBack();

		SetPrintByFont();
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("　　　　　プレイデータを消去します。よろしいですか？");
		PrintByFont("");
		PrintByFont("　　　　　　　　　は　い　　　　　い　い　え");

		EachFrame();
	}
}
static void TitleConfig(void)
{
	P_TitleBackWDest = SCREEN_W;

	FreezeInput();

	for(; ; )
	{
		UpdateMousePos();
		
		// debug
		//clsDx();
		//printfDx("%d %d\n", MouseX, MouseY);

		if(GetMouPound(MOUBTN_L))
		{
			int x = MouseX;
			int y = MouseY;

			if(!IsOut(x, y, 270, 120, 470, 160)) // [960x540]
			{
				SetScreenSize(960, 540);
			}
			else if(!IsOut(x, y, 530, 120, 800, 160)) // [1440x810]
			{
				SetScreenSize(1440, 810);
			}
			else if(!IsOut(x, y, 270, 160, 470, 200)) // [1920x1080]
			{
				SetScreenSize(1920, 1080);
			}
			else if(!IsOut(x, y, 530, 160, 800, 200)) // [フルスクリーン]
			{
				int w = Gnd.MonitorRect.W;
				int h = (SCREEN_H * Gnd.MonitorRect.W) / SCREEN_W;

				if(Gnd.MonitorRect.H < h)
				{
					h = Gnd.MonitorRect.H;
					w = (SCREEN_W * Gnd.MonitorRect.H) / SCREEN_H;

					errorCase(Gnd.MonitorRect.W < w);
				}
				SetScreenSize(Gnd.MonitorRect.W, Gnd.MonitorRect.H);

				Gnd.RealScreenDraw_L = (Gnd.MonitorRect.W - w) / 2;
				Gnd.RealScreenDraw_T = (Gnd.MonitorRect.H - h) / 2;
				Gnd.RealScreenDraw_W = w;
				Gnd.RealScreenDraw_H = h;
			}
			else if(!IsOut(x, y, 400, 240, 530, 280))
			{
				Gnd.MusicVolume += 0.01;
			}
			else if(!IsOut(x, y, 550, 240, 690, 280))
			{
				Gnd.MusicVolume -= 0.01;
			}
			else if(!IsOut(x, y, 700, 240, 900, 280))
			{
				Gnd.MusicVolume = DEFAULT_VOLUME;
			}
			else if(!IsOut(x, y, 400, 320, 530, 360))
			{
				Gnd.SEVolume += 0.01;
			}
			else if(!IsOut(x, y, 550, 320, 690, 360))
			{
				Gnd.SEVolume -= 0.01;
			}
			else if(!IsOut(x, y, 700, 320, 900, 360))
			{
				Gnd.SEVolume = DEFAULT_VOLUME;
			}
			else if(!IsOut(x, y, 270, 400, 600, 440))
			{
				TitleConfigResetPlayData();
			}
			else if(!IsOut(x, y, 830, 480, 930, 520))
			{
				break;
			}
			m_range(Gnd.MusicVolume, 0.0, 1.0);
			m_range(Gnd.SEVolume, 0.0, 1.0);
		}
		DrawWall();
		DrawTitleBack();

		SetPrintByFont();
		PrintByFont("");
		PrintByFont("　設定");
		PrintByFont("");
		PrintByFont("　画面サイズ　　　[960x540]      [1440x810]");
		PrintByFont("　　　　　　　　　[1920x1080]    [フルスクリーン]");
		PrintByFont("");
		PrintByFont_x(xcout("　ＢＧＭ音量　　　%.2f　　[上げる]　[下げる]　[デフォルト]", Gnd.MusicVolume));
		PrintByFont("");
		PrintByFont_x(xcout("　ＳＥ音量　　　　%.2f　　[上げる]　[下げる]　[デフォルト]", Gnd.SEVolume));
		PrintByFont("");
		PrintByFont("　　　　　　　　　プレイデータリセット");
		PrintByFont("");
		PrintByFont("　　　　　　　　　　　　　　　　　　　　　　　　　　　[戻る]");

		EachFrame();
	}
}
static void TitleGameStart2(void)
{
	WallBokashiRateDest = 0.0;
	P_TitleBackWDest = 0;

	const int TGS_BACK_X = 830;
	const int TGS_BACK_Y = 460;

	double selRateBack = 0.0;
	int selBack = 0;

	FreezeInput();

	for(; ; )
	{
		// debug
		//clsDx();
		//printfDx("%d %d\n", MouseX, MouseY);

		DrawWall();
		DrawTitleBack();
	
		DrawBegin(P_TITLE_BTN_BACK, TGS_BACK_X, TGS_BACK_Y);
		DrawZoom(1.0 + selRateBack * 0.15);
		DrawEnd();
		SaveLastDrawedPic("BACK");

		// <---- 描画

		UpdateMousePos();

		char *pointingName = GetDrawedPicName(MouseX, MouseY);

		if(!strcmp(pointingName, "BACK"))
			m_approach(selRateBack, 1.0, 0.85);
		else
			m_approach(selRateBack, 0.0, 0.93);
		
		EachFrame(); // ★★★ EachFrame

		if(GetMouInput(MOUBTN_L) == 1)
		{
			if(!strcmp(pointingName, "BACK"))
				break;

			int x = MouseX;
			int y = MouseY;

			if(!IsOut(x, y, 330, 90, 640, 360)) // 入口
			{
				// TODO
				
				Gnd.HasSaveData = 1; // kari
			}
		}
	}
}
static int TitleGameStartConfirm(void)
{
	int ret = 0;

	P_TitleBackWDest = SCREEN_W;

	FreezeInput();

	for(; ; )
	{
		UpdateMousePos();
		
		// debug
		//clsDx();
		//printfDx("%d %d\n", MouseX, MouseY);

		if(GetMouInput(MOUBTN_L) == 1)
		{
			int x = MouseX;
			int y = MouseY;

			if(!IsOut(x, y, 270, 280, 370, 320)) // はい
			{
				ret = 1;
				break;
			}
			else if(!IsOut(x, y, 520, 280, 690, 320)) // いいえ
			{
				break;
			}
		}
		DrawWall();
		DrawTitleBack();

		SetPrintByFont();
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("");
		PrintByFont("　　　　　セーブデータを消去します。よろしいですか？");
		PrintByFont("");
		PrintByFont("　　　　　　　　　は　い　　　　　い　い　え");

		EachFrame();
	}
	return ret;
}
static void TitleGameStart(void)
{
	double selRateContinue = 0.0;
	double selRateStart = 0.0;
	double selRateBack = 0.0;
	int selContinue = 0;
	int selStart = 0;
	int selBack = 0;

	const int TGS_CONTINUE_X = SCREEN_W / 2;
	const int TGS_CONTINUE_Y = 150;
	const int TGS_START_X = SCREEN_W / 2;
	const int TGS_START_Y = 390;
	const int TGS_BACK_X = 830;
	const int TGS_BACK_Y = 460;

returned:
	FreezeInput();

	WallBokashiRateDest = 1.0;
	P_TitleBackWDest = 450;

	for(; ; )
	{
		int continueEnabled = Gnd.HasSaveData;

		// 描画 ---->

		DrawWall();
		DrawTitleBack();

		if(continueEnabled)
		{
			DrawBegin(P_TITLE_ITEM_CONTINUE, TGS_CONTINUE_X, TGS_CONTINUE_Y);
			DrawZoom(1.0 + selRateContinue * 0.1);
			DrawEnd();
			SaveLastDrawedPic("CONTINUE");
		}
		else
		{
			DPE_SetBright(0.6, 0.6, 0.6);
			DrawCenter(P_TITLE_ITEM_CONTINUE, TGS_CONTINUE_X, TGS_CONTINUE_Y);
			DPE_Reset();
		}
		DrawBegin(P_TITLE_ITEM_START, TGS_START_X, TGS_START_Y);
		DrawZoom(1.0 + selRateStart * 0.1);
		DrawEnd();
		SaveLastDrawedPic("START");
		DrawBegin(P_TITLE_BTN_BACK, TGS_BACK_X, TGS_BACK_Y);
		DrawZoom(1.0 + selRateBack * 0.15);
		DrawEnd();
		SaveLastDrawedPic("BACK");

		// <---- 描画

		UpdateMousePos();

		char *pointingName = GetDrawedPicName(MouseX, MouseY);

		if(!strcmp(pointingName, "CONTINUE"))
			m_approach(selRateContinue, 1.0, 0.8);
		else
			m_approach(selRateContinue, 0.0, 0.85);

		if(!strcmp(pointingName, "START"))
			m_approach(selRateStart, 1.0, 0.8);
		else
			m_approach(selRateStart, 0.0, 0.85);

		if(!strcmp(pointingName, "BACK"))
			m_approach(selRateBack, 1.0, 0.85);
		else
			m_approach(selRateBack, 0.0, 0.93);

		EachFrame(); // ★★★ EachFrame

		if(GetMouInput(MOUBTN_L) == 1)
		{
			if(!strcmp(pointingName, "BACK"))
				break;

			if(!strcmp(pointingName, "START"))
			{
				if(Gnd.HasSaveData)
				{
					if(TitleGameStartConfirm())
					{
						Gnd.HasSaveData = 0;
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
			if(!strcmp(pointingName, "CONTINUE"))
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
void TitleMain(void)
{
	WorkScreen = CreateSubScreen(SCREEN_W, SCREEN_H);

	forscene(30)
	{
		DrawCurtain();
		EachFrame();
	}
	sceneLeave();

	forscene(30)
	{
		double a = -1.0 + sc_rate;

		DrawBegin(P_LOGO, SCREEN_W / 2, SCREEN_H / 2);
		DrawZoom(1.0 + a * a * 0.1);
		DrawEnd();
//		DrawSimple(P_LOGO, 0, 0);

		DrawCurtain(a);
		EachFrame();
	}
	sceneLeave();

	forscene(60)
	{
		DrawSimple(P_LOGO, 0, 0);
		EachFrame();
	}
	sceneLeave();

	forscene(30)
	{
		double a = -sc_rate;

		DrawBegin(P_LOGO, SCREEN_W / 2, SCREEN_H / 2);
		DrawZoom(1.0 + a * a * 0.1);
		DrawEnd();
//		DrawSimple(P_LOGO, 0, 0);

		DrawCurtain(a);
		EachFrame();
	}
	sceneLeave();

	forscene(30)
	{
		DrawCurtain();
		EachFrame();
	}
	sceneLeave();

	const int TITLE_BTN_START_X = 130;
	const int TITLE_BTN_START_Y = 460;

	const int TITLE_BTN_CONFIG_X = 830;
	const int TITLE_BTN_CONFIG_Y = 70;

	const int TITLE_BTN_EXIT_X = 830;
	const int TITLE_BTN_EXIT_Y = 460;

	{
		double a = 0.0;
		double z = 1.3;
		int titleBackOn = 0;
		double titleBackA = 0.0;
		double titleBackZ = 0.1;
		int titleOn = 0;
		double titleA = 0.0;
		double titleZ = 1.3;
		int titleBtnsOn[3]   = { 0 };
		double titleBtnsA[3] = { 0 };
		double titleBtnsZ[3] = { 1.05, 1.1, 1.15 };

		forscene(120)
		{
			if(sc_numer == 30)
				titleBackOn = 1;

			if(sc_numer == 60)
				titleOn = 1;

			if(sc_numer == 90)
				titleBtnsOn[0] = 1;

			if(sc_numer == 100)
				titleBtnsOn[1] = 1;

			if(sc_numer == 110)
				titleBtnsOn[2] = 1;

			DrawCurtain();

			// Wall >

			SetDrawScreen(GetHandle(WorkScreen));

			DPE_SetAlpha(a);
			DrawBegin(P_TITLE_WALL, SCREEN_W / 2, SCREEN_H / 2);
			DrawZoom(z);
			DrawEnd();
			DPE_Reset();

			GraphFilter(GetHandle(WorkScreen), DX_GRAPH_FILTER_GAUSS, 16, 1000);

			RestoreDrawScreen();

			DPE_SetGraphicSize(makeI2D(SCREEN_W, SCREEN_H));
			DrawSimple(GetHandle(WorkScreen), 0, 0);
			DPE_Reset();

			// < Wall

			DPE_SetAlpha(titleBackA);
			DPE_SetBright(GetColor(0, 0, 0));
			DrawBeginRect(P_WHITEBOX, TITLE_BACK_L, TITLE_BACK_T, TITLE_BACK_W, TITLE_BACK_H);
			DrawZoom_X(titleBackZ);
			DrawEnd();
			DPE_Reset();

			DPE_SetAlpha(titleA);
			DrawBegin(P_TITLE, SCREEN_W / 2, SCREEN_H / 2);
			DrawZoom(titleZ);
			DrawEnd();
			DPE_Reset();

			DPE_SetAlpha(titleBtnsA[0]);
			DrawBegin(P_TITLE_BTN_START, TITLE_BTN_START_X, TITLE_BTN_START_Y);
			DrawZoom(titleBtnsZ[0]);
			DrawEnd();
			DPE_Reset();

			DPE_SetAlpha(titleBtnsA[1]);
			DrawBegin(P_TITLE_BTN_CONFIG, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
			DrawZoom(titleBtnsZ[1]);
			DrawEnd();
			DPE_Reset();

			DPE_SetAlpha(titleBtnsA[2]);
			DrawBegin(P_TITLE_BTN_EXIT, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
			DrawZoom(titleBtnsZ[2]);
			DrawEnd();
			DPE_Reset();

			m_approach(a, 1.0, 0.97);
			m_approach(z, 1.0, 0.95);

			if(titleBackOn)
			{
				m_approach(titleBackA, TITLE_BACK_A, 0.95);
				m_approach(titleBackZ, 1.0, 0.9);
			}
			if(titleOn)
			{
				m_approach(titleA, 1.0, 0.93);
				m_approach(titleZ, 1.0, 0.8);
			}
			for(int c = 0; c < 3; c++)
			{
				if(titleBtnsOn[c])
				{
					m_approach(titleBtnsA[c], 1.0, 0.77);
					m_approach(titleBtnsZ[c], 1.0, 0.73);
				}
			}
			EachFrame();
		}
	}

	{
		double selRateStart = 0.0;
		double selRateConfig = 0.0;
		double selRateExit = 0.0;
		int selStart = 0;
		int selConfig = 0;
		int selExit = 0;

returned:
		FreezeInput();

		WallBokashiRateDest = 1.0;
		P_TitleBackWDest = TITLE_BACK_W;

		for(; ; )
		{
			DrawWall();
			DrawTitleBack();

			DrawCenter(P_TITLE, SCREEN_W / 2, SCREEN_H / 2);

			DrawBegin(P_TITLE_BTN_START, TITLE_BTN_START_X, TITLE_BTN_START_Y);
			DrawZoom(1.0 + selRateStart * 0.2);
			DrawEnd();
			SaveLastDrawedPic("START");
			DrawBegin(P_TITLE_BTN_CONFIG, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
			DrawZoom(1.0 + selRateConfig * 0.15);
			DrawEnd();
			SaveLastDrawedPic("CONFIG");
			DrawBegin(P_TITLE_BTN_EXIT, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
			DrawZoom(1.0 + selRateExit * 0.15);
			DrawEnd();
			SaveLastDrawedPic("EXIT");

			// <---- 描画

			UpdateMousePos();

			char *pointingName = GetDrawedPicName(MouseX, MouseY);

			if(!strcmp(pointingName, "START"))
				m_approach(selRateStart, 1.0, 0.85);
			else
				m_approach(selRateStart, 0.0, 0.9);

			if(!strcmp(pointingName, "CONFIG"))
				m_approach(selRateConfig, 1.0, 0.9);
			else
				m_approach(selRateConfig, 0.0, 0.93);

			if(!strcmp(pointingName, "EXIT"))
				m_approach(selRateExit, 1.0, 0.9);
			else
				m_approach(selRateExit, 0.0, 0.93);

			EachFrame(); // ★★★ EachFrame

			if(GetMouInput(MOUBTN_L) == 1)
			{
				if(!strcmp(pointingName, "EXIT"))
					break;

				if(!strcmp(pointingName, "CONFIG"))
				{
					TitleConfig();
					goto returned;
				}
				if(!strcmp(pointingName, "START"))
				{
					TitleGameStart();

					selRateStart = 0.0;
					selRateExit = 1.0;

					goto returned;
				}
			}
		}
	}

	SetCurtain(30, -1.0);
	MusicFade();

	forscene(40)
	{
		DrawWall();
		EachFrame();
	}
	sceneLeave();
}
