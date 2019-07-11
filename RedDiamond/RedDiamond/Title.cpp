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
}
