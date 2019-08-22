using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class MultiThreadEx : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<ThreadEx> Ths = new List<ThreadEx>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(Action routine)
		{
			Ths.Add(new ThreadEx(routine));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool IsEnded(int millis = 0)
		{
			foreach (ThreadEx th in Ths)
				if (th.IsEnded(millis) == false)
					return false;

			return true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WaitToEnd()
		{
			foreach (ThreadEx th in Ths)
				th.WaitToEnd();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void RelayThrow()
		{
			this.WaitToEnd();

			Exception[] es = Ths.Select(th => th.GetException()).Where(e => e != null).ToArray();

			if (1 <= es.Length)
				throw new AggregateException("Relay", es);
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
