using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class HTTPBodyOutputStream : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private WorkingDir WD = null;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string BuffFile = null;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int WroteSize = 0;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string GetBuffFile()
		{
			if (this.WD == null)
			{
				this.WD = new WorkingDir();
				this.BuffFile = this.WD.MakePath();
			}
			return this.BuffFile;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Write(byte[] data, int offset = 0)
		{
			this.Write(data, offset, data.Length - offset);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Write(byte[] data, int offset, int count)
		{
			using (FileStream writer = new FileStream(this.GetBuffFile(), FileMode.Append, FileAccess.Write))
			{
				writer.Write(data, offset, count);
			}
			this.WroteSize += count;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count
		{
			get
			{
				return this.WroteSize;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] ToByteArray()
		{
			return this.WroteSize == 0 ? BinTools.EMPTY : File.ReadAllBytes(this.GetBuffFile());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.WD != null)
			{
				this.WD.Dispose();
				this.WD = null;
			}
		}
	}
}
