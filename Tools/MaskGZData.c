#include "C:\Factory\Common\all.h"

static void MaskGZData(autoBlock_t *fileData)
{
	// noop ???
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
