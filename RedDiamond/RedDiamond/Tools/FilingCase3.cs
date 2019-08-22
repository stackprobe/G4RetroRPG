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
	public class FilingCase3 : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string Domain;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int PortNo;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string BasePath;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Thread Th;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private NamedEventPair EvStop = new NamedEventPair();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object SYNCROOT = new object();
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private FilingCase3Client Client = null;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int ClientAliveCount = -1;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public FilingCase3(string domain = "localhost", int portNo = 65123, string basePath = "Common")
		{
			this.Domain = domain;
			this.PortNo = portNo;
			this.BasePath = basePath;

			this.Th = new Thread(() =>
			{
				while (this.EvStop.WaitForMillis(5000) == false)
				{
					lock (SYNCROOT)
					{
						if (this.Client != null)
						{
							if (180 / 5 < ++this.ClientAliveCount) // 3 min
							{
								this.Client.Dispose();
								this.Client = null;
							}
							else
							{
								try
								{
									this.Client.Hello();
								}
								catch (Exception e)
								{
									ProcMain.WriteLog(e);

									this.Client.Dispose();
									this.Client = null;
								}
							}
						}
					}
				}
			});

			this.Th.Start();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private T Perform<T>(Func<T> rtn)
		{
			lock (SYNCROOT)
			{
				try
				{
					if (this.Client == null)
						this.Client = new FilingCase3Client(this.Domain, this.PortNo, this.BasePath);

					this.ClientAliveCount = 0;

					return rtn();
				}
				catch
				{
					if (this.Client != null)
					{
						this.Client.Dispose();
						this.Client = null;
					}
					throw;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] Get(string path)
		{
			return this.Perform(() => this.Client.Get(path));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Post(string path, byte[] data)
		{
			return this.Perform(() => this.Client.Post(path, data));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] GetPost(string path, byte[] data)
		{
			return this.Perform(() => this.Client.GetPost(path, data));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string[] List(string path)
		{
			return this.Perform(() => this.Client.List(path));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int Delete(string path)
		{
			return this.Perform(() => this.Client.Delete(path));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.Th != null) // at once
			{
				this.EvStop.Set();

				this.Th.Join();
				this.Th = null;

				this.EvStop.Dispose();
				this.EvStop = null;

				if (this.Client != null)
				{
					this.Client.Dispose();
					this.Client = null;
				}
			}
		}
	}
}
