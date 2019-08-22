using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class ExceptionDam
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Section(Action<ExceptionDam> routine)
		{
			ExceptionDam eDam = new ExceptionDam();
			try
			{
				routine(eDam);
			}
			catch (Exception e)
			{
				eDam.Add(e);
			}
			eDam.Burst();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<Exception> Errors = new List<Exception>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Add(Exception e)
		{
			this.Errors.Add(e);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Invoke(Action routine)
		{
			try
			{
				routine();
			}
			catch (Exception e)
			{
				this.Add(e);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Burst()
		{
			if (1 <= this.Errors.Count)
			{
				Exception[] errors = this.Errors.ToArray();

				this.Errors.Clear();

				throw new AggregateException("Has some errors.", errors);
			}
		}
	}
}
