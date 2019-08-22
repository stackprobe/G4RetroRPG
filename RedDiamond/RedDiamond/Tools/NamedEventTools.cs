using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class NamedEventTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static EventWaitHandle Create(string name)
		{
			return new EventWaitHandle(false, EventResetMode.AutoReset, name);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static EventWaitHandle CreateGlobal(string name)
		{
			EventWaitHandleSecurity security = new EventWaitHandleSecurity();

			security.AddAccessRule(
				new EventWaitHandleAccessRule(
					new SecurityIdentifier(
						WellKnownSidType.WorldSid,
						null
						),
					EventWaitHandleRights.FullControl,
					AccessControlType.Allow
					)
				);

			bool createdNew;
			return new EventWaitHandle(false, EventResetMode.AutoReset, @"Global\Global_" + name, out createdNew, security);
		}
	}
}
