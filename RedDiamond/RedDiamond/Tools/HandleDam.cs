using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class HandleDam
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Section_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Section(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T Transaction_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				throw null; // never
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Transaction(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				//throw null; // never
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private List<IDisposable> Handles = new List<IDisposable>();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public T Add<T>(T handle) where T : IDisposable
		{
			this.Handles.Add(handle);
			return handle;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Burst()
		{
			ExceptionDam.Section(eDam =>
			{
				this.Burst(eDam);
			});
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Burst(Exception e)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Add(e);
				this.Burst(eDam);
			});
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private void Burst(ExceptionDam eDam)
		{
			for (int index = this.Handles.Count - 1; 0 <= index; index--)
				eDam.Invoke(() => this.Handles[index].Dispose());

			this.Handles.Clear();
		}
	}
}
