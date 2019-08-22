using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class WorkingDir : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static RootInfo Root;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class RootInfo
		{
			private string Dir;
			private bool Created = false;

			public RootInfo(string dir)
			{
				this.Dir = dir;
			}

			public string GetDir()
			{
				if (this.Created == false)
				{
					FileTools.Delete(this.Dir);
					FileTools.CreateDir(this.Dir);

					this.Created = true;
				}
				return this.Dir;
			}

			public void Delete()
			{
				if (this.Created)
				{
					FileTools.Delete(this.Dir);

					this.Created = false;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static RootInfo CreateRoot()
		{
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), ProcMain.APP_IDENT));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static RootInfo CreateProcessRoot()
		{
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), "{41266ce2-7655-413e-b8bb-780aaf640f4d}_" + Process.GetCurrentProcess().Id));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static long CtorCounter = 0L;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string Dir;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public WorkingDir()
		{
			//this.Dir = Path.Combine(Root.GetDir(), Guid.NewGuid().ToString("B"));
			//this.Dir = Path.Combine(Root.GetDir(), SecurityTools.MakePassword_9A());
			this.Dir = Path.Combine(Root.GetDir(), "$" + CtorCounter++);

			FileTools.CreateDir(this.Dir);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private long PathCounter = 0L;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string MakePath()
		{
			//return this.GetPath(Guid.NewGuid().ToString("B"));
			//return this.GetPath(SecurityTools.MakePassword_9A());
			return this.GetPath("$" + this.PathCounter++);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string GetPath(string localName)
		{
			return Path.Combine(this.Dir, localName);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.Dir != null)
			{
				try
				{
					Directory.Delete(this.Dir, true);
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}

				this.Dir = null;
			}
		}
	}
}
