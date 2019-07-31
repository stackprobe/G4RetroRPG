extern iRect_t LastDrawedPicRect;

void ClearNamedRects(void);
void SetRectName(char *name, int l, int t, int w, int h);
void SetLastDrawedPicRectName(char *name);
char *GetRectName(int x, int y, char *defaultName = "NONE");
