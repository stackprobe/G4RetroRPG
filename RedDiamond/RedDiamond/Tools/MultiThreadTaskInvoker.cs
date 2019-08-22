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
	public class MultiThreadTaskInvoker : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int ThreadCountMax = Math.Max(1, Environment.ProcessorCount);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int ExceptionCountMax = 10;

		// <---- prop

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<Thread> Ths = new List<Thread>();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Queue<Action> Tasks = new Queue<Action>();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<Exception> Exs = new List<Exception>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void AddTask(Action task)
		{
			lock (SYNCROOT)
			{
				this.Tasks.Enqueue(task);

				if (this.Ths.Count < this.ThreadCountMax)
				{
					Thread th = new Thread(() =>
					{
						for (; ; )
						{
							Action nextTask;

							lock (SYNCROOT)
							{
								if (this.Tasks.Count <= 0)
								{
									this.Ths.Remove(Thread.CurrentThread);
									return;
								}
								nextTask = this.Tasks.Dequeue();
							}

							try
							{
								nextTask();
							}
							catch (Exception e)
							{
								lock (SYNCROOT)
								{
									if (this.Exs.Count < this.ExceptionCountMax)
										this.Exs.Add(e);
								}
							}
						}
					});

					th.Start();

					this.Ths.Add(th);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool IsEnded()
		{
			lock (SYNCROOT)
			{
				return this.Ths.Count <= 0;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WaitToEnd()
		{
			for (; ; )
			{
				Thread th;

				lock (SYNCROOT)
				{
					if (this.Ths.Count <= 0)
						break;

					th = this.Ths[0];
				}
				th.Join();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void RelayThrow()
		{
			this.WaitToEnd();

			if (1 <= this.Exs.Count)
				throw new AggregateException("Relay", this.Exs);
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
