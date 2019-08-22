using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class ThreadEx : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Thread Th;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Exception Ex = null;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public ThreadEx(Action routine)
		{
			Th = new Thread(() =>
			{
				try
				{
					routine();
				}
				catch (Exception e)
				{
					Ex = e;
				}
			});

			Th.Start();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool IsEnded(int millis = 0)
		{
			if (Th != null && Th.Join(millis))
				Th = null;

			return Th == null;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WaitToEnd()
		{
			if (Th != null)
			{
				Th.Join();
				Th = null;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WaitToEnd(Critical critical)
		{
			if (Th != null)
			{
				critical.Unsection(() => Th.Join());
				Th = null;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void RelayThrow()
		{
			this.WaitToEnd();

			if (this.Ex != null)
				throw new AggregateException("Relay", this.Ex);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Exception GetException()
		{
			this.WaitToEnd();
			return this.Ex;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
