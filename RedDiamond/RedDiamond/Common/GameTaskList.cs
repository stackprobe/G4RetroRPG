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
	public class GameTaskList
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<IGameTask> Tasks = new List<IGameTask>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(IGameTask task)
		{
			this.Tasks.Add(task);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void ExecuteAllTask()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				IGameTask task = this.Tasks[index];

				if (task.Routine() == false) // ? 終了
				{
					task.Dispose();
					ExtraTools.FastDesertElement(this.Tasks, index--);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Clear()
		{
			for (int index = 0; index < this.Tasks.Count; index++)
			{
				this.Tasks[index].Dispose();
			}
			this.Tasks.Clear();
		}
	}
}
