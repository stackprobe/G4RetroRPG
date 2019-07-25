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
	m_approach(P_TitleBackW, P_TitleBackWDest, 0.9);

	// ---

	DPE_SetAlpha(TITLE_BACK_A);
	DPE_SetBright(GetColor(0, 0, 0));
	DrawBegin(P_WHITEBOX, SCREEN_W / 2, SCREEN_H / 2);
	DrawSetSize_W(P_TitleBackW);
	DrawSetSize_H(SCREEN_H);
	DrawEnd();
	DPE_Reset();
}
static void TitleConfig(void)
{
	P_TitleBackWDest = SCREEN_W;

	for(; ; )
	{
		DrawWall();
		DrawTitleBack();

		// TODO

		EachFrame();
	}
}
static void TitleGameStart(void)
{
	P_TitleBackWDest = 500;

	for(; ; )
	{
		DrawWall();
		DrawTitleBack();

		// TODO

		EachFrame();
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
	const int TITLE_BTN_START_Y = 410;

	const int TITLE_BTN_CONFIG_X = 830;
	const int TITLE_BTN_CONFIG_Y = 70;

	const int TITLE_BTN_EXIT_X = 830;
	const int TITLE_BTN_EXIT_Y = 410;

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

titleStart:
		WallBokashiRateDest = 1.0;
		P_TitleBackWDest = TITLE_BACK_W;

		for(; ; )
		{
			UpdateMousePos();

			if(selStart = GetDistance(TITLE_BTN_START_X, TITLE_BTN_START_Y, MouseX, MouseY) < 100.0)
				m_approach(selRateStart, 1.0, 0.8);
			else
				m_approach(selRateStart, 0.0, 0.9);

			if(selConfig = GetDistance(TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y, MouseX, MouseY) < 90.0)
				m_approach(selRateConfig, 1.0, 0.85);
			else
				m_approach(selRateConfig, 0.0, 0.93);

			if(selExit = GetDistance(TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y, MouseX, MouseY) < 90.0)
				m_approach(selRateExit, 1.0, 0.85);
			else
				m_approach(selRateExit, 0.0, 0.93);

			if(GetMouInput(MOUBTN_L) == 1)
			{
				if(selExit)
					break;

				if(selConfig)
				{
					TitleConfig();
					goto titleStart;
				}
				if(selStart)
				{
					TitleGameStart();
					goto titleStart;
				}
			}

			DrawWall();
			DrawTitleBack();

			DrawCenter(P_TITLE, SCREEN_W / 2, SCREEN_H / 2);

			DrawBegin(P_TITLE_BTN_START, TITLE_BTN_START_X, TITLE_BTN_START_Y);
			DrawZoom(1.0 + selRateStart * 0.2);
			DrawEnd();
			DrawBegin(P_TITLE_BTN_CONFIG, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
			DrawZoom(1.0 + selRateConfig * 0.15);
			DrawEnd();
			DrawBegin(P_TITLE_BTN_EXIT, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);
			DrawZoom(1.0 + selRateExit * 0.15);
			DrawEnd();

			EachFrame();
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
