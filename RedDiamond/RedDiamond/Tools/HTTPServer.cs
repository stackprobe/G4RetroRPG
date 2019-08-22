using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class HTTPServer : SockServer
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Action<HTTPServerChannel> HTTPConnected = channel => { };

		// <---- prm

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public HTTPServer()
		{
			PortNo = 80;
			Connected = channel => HandleDam.Section(hDam =>
			{
				HTTPServerChannel hsChannel = new HTTPServerChannel();

				hsChannel.Channel = channel;
				hsChannel.HDam = hDam;
				hsChannel.RecvRequest();

				HTTPConnected(hsChannel);

				hsChannel.SendResponse();
			});
		}
	}
}
