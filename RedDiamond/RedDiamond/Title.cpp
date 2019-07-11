#include "all.h"

void TitleMain(void)
{
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

	const int TITLE_Y_ADD = -50;
	const int TITLE_BACK_W = SCREEN_W;
	const int TITLE_BACK_H = 100;
	const int TITLE_BACK_L = 0;
	const int TITLE_BACK_T = (SCREEN_H - TITLE_BACK_H) / 2 + TITLE_Y_ADD;
	const double TITLE_BACK_A = 0.5;

	const int TITLE_BTN_START_X = 150;
	const int TITLE_BTN_START_Y = 400;

	const int TITLE_BTN_CONFIG_X = 550;
	const int TITLE_BTN_CONFIG_Y = 410;

	const int TITLE_BTN_EXIT_X = 800;
	const int TITLE_BTN_EXIT_Y = 410;

	{
		double a = 0.0;
		double z = 1.3;
		int titleBackOn = 0;
		double titleBackA = 0.0;
		int titleOn = 0;
		double titleA = 0.0;
		double titleZ = 1.3;
		int titleBtnsOn[3]   = { 0 };
		double titleBtnsA[3] = { 0 };
		double titleBtnsZ[3] = { 1.05, 1.1, 1.15 };

		forscene(180)
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

			DPE_SetAlpha(a);
			DrawBegin(P_TITLE_WALL, SCREEN_W / 2, SCREEN_H / 2);
			DrawZoom(z);
			DrawEnd();
			DPE_Reset();

			DPE_SetAlpha(titleBackA);
			DPE_SetBright(GetColor(0, 0, 0));
			DrawRect(P_WHITEBOX, TITLE_BACK_L, TITLE_BACK_T, TITLE_BACK_W, TITLE_BACK_H);
			DPE_Reset();

			DPE_SetAlpha(titleA);
			DrawBegin(P_TITLE, SCREEN_W / 2, SCREEN_H / 2 + TITLE_Y_ADD);
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
			m_approach(z, 1.0, 0.96);

			if(titleBackOn)
				m_approach(titleBackA, TITLE_BACK_A, 0.95);

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

	for(; ; )
	{
		DrawSimple(P_TITLE_WALL, 0, 0);

		DPE_SetAlpha(TITLE_BACK_A);
		DPE_SetBright(GetColor(0, 0, 0));
		DrawRect(P_WHITEBOX, TITLE_BACK_L, TITLE_BACK_T, TITLE_BACK_W, TITLE_BACK_H);
		DPE_Reset();

		DrawCenter(P_TITLE, SCREEN_W / 2, SCREEN_H / 2 + TITLE_Y_ADD);

		DrawCenter(P_TITLE_BTN_START, TITLE_BTN_START_X, TITLE_BTN_START_Y);
		DrawCenter(P_TITLE_BTN_CONFIG, TITLE_BTN_CONFIG_X, TITLE_BTN_CONFIG_Y);
		DrawCenter(P_TITLE_BTN_EXIT, TITLE_BTN_EXIT_X, TITLE_BTN_EXIT_Y);

		EachFrame();
	}
}
