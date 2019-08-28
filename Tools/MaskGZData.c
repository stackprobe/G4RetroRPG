#include "C:\Factory\Common\all.h"

static void MaskGZData(autoBlock_t *fileData)
{
	uint index;

	for(index = 0; index < getSize(fileData); index++)
	{
		uint bChr = b_(fileData)[index];

		bChr = (bChr & 0xf0) >> 4 | (bChr & 0x0f) << 4;

		b_(fileData)[index] = bChr;
	}
}
int main(int argc, char **argv)
{
	// sync > @ MaskGZData_main

	char *file;
	autoBlock_t *fileData;

	errorCase(!argIs("MASK-GZ-DATA"));

	file = nextArg();
	cout("MaskGZData_file: %s\n", file);
	fileData = readBinary(file);
	LOGPOS();
	MaskGZData(fileData);
	LOGPOS();
	writeBinary(file, fileData);
	LOGPOS();
	releaseAutoBlock(fileData);
	LOGPOS();

	// < sync
}
