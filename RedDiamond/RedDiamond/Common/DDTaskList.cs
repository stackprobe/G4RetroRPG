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
	public class DDTaskList
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<IDDTask> Tasks = new List<IDDTask>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(IDDTask task)
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
				IDDTask task = this.Tasks[index];

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

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Count
		{
			get
			{
				return this.Tasks.Count;
			}
		}
	}
}
