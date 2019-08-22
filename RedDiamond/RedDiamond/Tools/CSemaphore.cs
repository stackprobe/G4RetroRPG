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
	public class CSemaphore
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int Permit;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CSemaphore(int permit)
		{
			if (permit < 1 || IntTools.IMAX < permit)
				throw new ArgumentException();

			this.Permit = permit;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object Enter_SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int Entry = 0;

		// @ 2019.1.7
		// Monitor.Enter -> Monitor.Exit は同一スレッドでなければならないっぽい。
		// Enter -> 別スレッドで Leave 出来るように ---> Monitor.Wait -> Monitor.Pulse にした。

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Enter()
		{
			lock (Enter_SYNCROOT)
			{
				lock (SYNCROOT)
				{
					Entry++;

					if (Entry == Permit + 1)
						Monitor.Wait(SYNCROOT);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Leave()
		{
			lock (SYNCROOT)
			{
				if (Entry == 0)
					throw null; // never

				Entry--;

				if (Entry == Permit)
					Monitor.Pulse(SYNCROOT);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T Section_Get<T>(Func<T> routine)
		{
			this.Enter();
			try
			{
				return routine();
			}
			finally
			{
				this.Leave();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Section(Action routine)
		{
			this.Enter();
			try
			{
				routine();
			}
			finally
			{
				this.Leave();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T Unsection_Get<T>(Func<T> routine)
		{
			this.Leave();
			try
			{
				return routine();
			}
			finally
			{
				this.Enter();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Unsection(Action routine)
		{
			this.Leave();
			try
			{
				routine();
			}
			finally
			{
				this.Enter();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void ContextSwitching()
		{
			this.Leave();
			this.Enter();
		}
	}
}
