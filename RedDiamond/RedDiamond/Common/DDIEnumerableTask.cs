using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class DDIEnumerableTask : IDDTask
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private IEnumerator<bool> Sequencer;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Action EndRoutine;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public DDIEnumerableTask(IEnumerable<bool> routine, Action endRoutine)
		{
			this.Sequencer = routine.GetEnumerator();
			this.EndRoutine = endRoutine;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool Routine()
		{
			if (this.Sequencer != null && (this.Sequencer.MoveNext() == false || this.Sequencer.Current == false))
			{
				this.Sequencer.Dispose();
				this.Sequencer = null;
			}
			return this.Sequencer != null;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.EndRoutine != null)
			{
				if (this.Sequencer != null)
				{
					this.Sequencer.Dispose();
					this.Sequencer = null;
				}
				this.EndRoutine();
				this.EndRoutine = null;
			}
		}
	}
}
