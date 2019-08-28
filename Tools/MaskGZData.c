#include "C:\Factory\SubTools\libs\MakeGZData.h"

void MaskGZData(autoBlock_t *fileData)
{
	uint index;

	for(index = 0; index < getSize(fileData); index++)
	{
		uint bChr = b_(fileData)[index];

		bChr = (bChr & 0xf0) >> 4 | (bChr & 0x0f) << 4;

		b_(fileData)[index] = bChr;
	}
}
