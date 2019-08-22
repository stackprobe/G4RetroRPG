using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class NamedEventPair : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private NamedEventUnit HandleForSet;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private NamedEventUnit HandleForWait;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public NamedEventPair() :
			this(Guid.NewGuid().ToString("B"))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public NamedEventPair(string name)
		{
			this.HandleForSet = new NamedEventUnit(name);
			this.HandleForWait = new NamedEventUnit(name); // fixme これが失敗すると HandleForSet が宙に浮く。
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Set()
		{
			this.HandleForSet.Set();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WaitForever()
		{
			this.HandleForWait.WaitForever();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool WaitForMillis(int millis)
		{
			return this.HandleForWait.WaitForMillis(millis);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.HandleForSet != null)
			{
				this.HandleForSet.Dispose();
				this.HandleForWait.Dispose();

				this.HandleForSet = null;
			}
		}
	}
}
