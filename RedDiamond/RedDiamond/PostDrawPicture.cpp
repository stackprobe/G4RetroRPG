#include "all.h"

iRect_t LastDrawedPicRect;

typedef struct DrawedPic_st
{
	char *Name;
	iRect_t Rect;
}
DrawedPic_t;

static oneObject(autoList<DrawedPic_t>, new autoList<DrawedPic_t>(), GetDrawedPics);

void ClearSavedDrawedPic(void)
{
	for(int index = 0; index < GetDrawedPics()->GetCount(); index++)
	{
		DrawedPic_t *drawedPic = GetDrawedPics()->ElementAt(index);

		memFree(drawedPic->Name);
	}
	GetDrawedPics()->SetCount(0);
}
void SaveLastDrawedPic(char *name)
{
	DrawedPic_t drawedPic;

	drawedPic.Name = strx(name);
	drawedPic.Rect = LastDrawedPicRect;

	GetDrawedPics()->AddElement(drawedPic);
}
char *GetDrawedPicName(int x, int y)
{
	for(int index = 0; index < GetDrawedPics()->GetCount(); index++)
	{
		DrawedPic_t *drawedPic = GetDrawedPics()->ElementAt(index);

		if(!IsOut(x, y, drawedPic->Rect.L, drawedPic->Rect.T, drawedPic->Rect.L + drawedPic->Rect.W, drawedPic->Rect.T + drawedPic->Rect.H))
		{
			return drawedPic->Name;
		}
	}
	return "NONE";
}
