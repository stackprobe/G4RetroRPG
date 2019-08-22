using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class LimitedWriter
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private long Remaining;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private FileTools.Write_d AnotherWriter;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public LimitedWriter(long limit, FileTools.Write_d writer)
		{
			this.Remaining = limit;
			this.AnotherWriter = writer;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Write(byte[] buff, int offset, int count)
		{
			if (this.Remaining < (long)count)
				throw new Exception("Over the limit !!!");

			this.Remaining -= (long)count;
			this.AnotherWriter(buff, offset, count);
		}
	}
}
