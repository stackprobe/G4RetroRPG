using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class GameDerivationTable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private AutoTable<GamePicture> DerTable;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GameDerivationTable(GamePicture picture, int x, int y, int w, int h, int xNum, int yNum, int xStep = -1, int yStep = -1)
		{
			if (xStep == -1) xStep = w;
			if (yStep == -1) yStep = h;

			this.DerTable = new AutoTable<GamePicture>(xNum, yNum);

			for (int xc = 0; xc < xNum; xc++)
			{
				for (int yc = 0; yc < yNum; yc++)
				{
					this.DerTable[xc, yc] = GameDerivations.GetPicture(picture, x + xc * xStep, y + yc * yStep, w, h);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public GamePicture this[int x, int y]
		{
			get
			{
				return this.DerTable[x, y];
			}
		}
	}
}
