using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class SockClient : SockChannel, IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public SockClient()
		{
			SockChannel.Critical.Enter();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Connect(string domain, int portNo, int connectTimeoutMillis = 20000) // 20 sec
		{
			// TODO connectTimeoutMillis 対応

			IPHostEntry hostEntry = Dns.GetHostEntry(domain);
			IPAddress address = GetFairAddress(hostEntry.AddressList);
			IPEndPoint endPoint = new IPEndPoint(address, portNo);

			this.Handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.Handler.Connect(endPoint);

			this.PostSetHandler();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static IPAddress GetFairAddress(IPAddress[] addresses)
		{
			foreach (IPAddress address in addresses)
			{
				if (address.AddressFamily == AddressFamily.InterNetwork) // ? IPv4
				{
					return address;
				}
			}
			return addresses[0];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// このメソッドは例外を投げないこと。
		/// </summary>
		public void Dispose()
		{
			if (this.Handler != null)
			{
				try
				{
					this.Handler.Disconnect(false);
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}

				try
				{
					this.Handler.Dispose();
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}

				this.Handler = null;

				try
				{
					SockChannel.Critical.Leave();
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}
			}
		}
	}
}
