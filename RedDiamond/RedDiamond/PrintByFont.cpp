#include "all.h"

static int P_X;
static int P_Y;
static int P_YStep;
static char *P_FontName;
static int P_FontSize;

void SetPrintByFont(int x, int y, int yStep, char *fontName, int fontSize)
{
	P_X = x;
	P_Y = y;
	P_YStep = yStep;
	P_FontName = strx(fontName);
	P_FontSize = fontSize;
}
void PrintByFont(char *line)
{
	errorCase(!P_FontName);

	DrawStringByFont(P_X, P_Y, line, GetFontHandle(P_FontName, P_FontSize));
	P_Y += P_YStep;
}
void PrintByFont_x(char *line)
{
	PrintByFont(line);
	memFree(line);
}
