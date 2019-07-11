#include "all.h"

void Test0001(void)
{
	SetCurtain();
	FreezeInput();

	forscene(600)
	{
		if(1 <= GetInput(INP_A))
		{
			break;
		}

		DrawCurtain();

		DrawBegin(P_DUMMY, SCREEN_CENTER_X, SCREEN_CENTER_Y);
		DrawZoom(5.0);
		DrawRotate(sc_rate * 5.0);
		DrawEnd();

		EachFrame();
	}
	sceneLeave();

	FreezeInput();
}
