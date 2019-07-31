#include "all.h"

iRect_t LastDrawedPicRect;

typedef struct Info_st
{
	char *Name;
	iRect_t Rect;
}
Info_t;

static oneObject(autoList<Info_t>, new autoList<Info_t>(), GetInfos);

void ClearNamedRects(void)
{
	for(int index = 0; index < GetInfos()->GetCount(); index++)
	{
		Info_t *info = GetInfos()->ElementAt(index);

		memFree(info->Name);
	}
	GetInfos()->SetCount(0);
}
void SetRectName(char *name, int l, int t, int w, int h)
{
	Info_t info;

	info.Name = strx(name);
	info.Rect = makeIRect(l, t, w, h);

	GetInfos()->AddElement(info);
}
void SetLastDrawedPicRectName(char *name)
{
	Info_t info;

	info.Name = strx(name);
	info.Rect = LastDrawedPicRect;

	GetInfos()->AddElement(info);
}
char *GetRectName(int x, int y, char *defaultName)
{
	for(int index = 0; index < GetInfos()->GetCount(); index++)
	{
		Info_t *info = GetInfos()->ElementAt(index);

		if(!IsOut(x, y, info->Rect.L, info->Rect.T, info->Rect.L + info->Rect.W, info->Rect.T + info->Rect.H))
		{
			return info->Name;
		}
	}
	return defaultName;
}
