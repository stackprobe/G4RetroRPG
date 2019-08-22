using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class ProcMain
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string APP_IDENT;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string APP_TITLE;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string SelfFile;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string SelfDir;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static ArgsReader ArgsReader;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool CUIError = false;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void CUIMain(Action<ArgsReader> mainFunc, string appIdent, string appTitle)
		{
			try
			{
				WriteLog = message => Console.WriteLine("[" + DateTime.Now + "] " + message);

				APP_IDENT = appIdent;
				APP_TITLE = appTitle;

				OnBoot();

				WorkingDir.Root = WorkingDir.CreateProcessRoot();

				ArgsReader = GetArgsReader();

				mainFunc(ArgsReader);

				WorkingDir.Root.Delete();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				WriteLog(e);
				CUIError = true;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void GUIMain(Func<Form> getMainForm, string appIdent, string appTitle)
		{
			Application.ThreadException += new ThreadExceptionEventHandler(ApplicationThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			WriteLog = message => Log.Enqueue("[" + DateTime.Now + "] " + message);

			APP_IDENT = appIdent;
			APP_TITLE = appTitle;

			OnBoot();

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0))
			{
				if (GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
				{
					CheckSelfFile();
					Directory.SetCurrentDirectory(SelfDir);
					CheckLogonUserAndTmp();

					WorkingDir.Root = WorkingDir.CreateRoot();

					ArgsReader = GetArgsReader();

					if (BeforeMainForm(ArgsReader))
					{
						// core >

						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(getMainForm());

						// < core
					}
					WorkingDir.Root.Delete();
					WorkingDir.Root = null;

					GlobalProcMtx.Release();
				}
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Action<object> WriteLog = message => { };
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Func<ArgsReader, bool> BeforeMainForm = ar => true;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static ArgsReader GetArgsReader()
		{
			ArgsReader ar = new ArgsReader(Environment.GetCommandLineArgs(), 1);

			if (ar.ArgIs("//R"))
			{
				ar = new ArgsReader(File.ReadAllLines(ar.NextArg(), StringTools.ENCODING_SJIS), 0);
			}
			return ar;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void SessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void CheckSelfFile()
		{
			string file = SelfFile;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (file != SJIS.GetString(SJIS.GetBytes(file)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (file.Substring(1, 2) != ":\\")
			{
				MessageBox.Show(
					"ネットワークパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void CheckLogonUserAndTmp()
		{
			string userName = Environment.GetEnvironmentVariable("UserName");
			Encoding SJIS = Encoding.GetEncoding(932);

			if (
				userName == null ||
				userName == "" ||
				userName != SJIS.GetString(SJIS.GetBytes(userName)) ||
				userName.StartsWith(" ") ||
				userName.EndsWith(" ")
				)
			{
				MessageBox.Show(
					"Windows ログオン・ユーザー名に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(7);
			}

			string tmp = Environment.GetEnvironmentVariable("TMP");

			if (
				tmp == null ||
				tmp == "" ||
				tmp != SJIS.GetString(SJIS.GetBytes(tmp)) ||
				//tmp.Length < 3 ||
				tmp.Length < 4 || // ルートDIR禁止
				tmp[1] != ':' ||
				tmp[2] != '\\' ||
				Directory.Exists(tmp) == false ||
				tmp.Contains(' ')
				)
			{
				MessageBox.Show(
					"環境変数 TMP に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(8);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class GlobalProcMtx
		{
			private static Mutex ProcMtx;

			public static bool Create(string procMtxName, string title)
			{
				try
				{
					MutexSecurity security = new MutexSecurity();

					security.AddAccessRule(
						new MutexAccessRule(
							new SecurityIdentifier(
								WellKnownSidType.WorldSid,
								null
								),
							MutexRights.FullControl,
							AccessControlType.Allow
							)
						);

					bool createdNew;
					ProcMtx = new Mutex(false, @"Global\Global_" + procMtxName, out createdNew, security);

					if (ProcMtx.WaitOne(0))
						return true;

					ProcMtx.Close();
					ProcMtx = null;

					ProcMain.WriteLog(new Exception());
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}

				CloseProcMtx();

				MessageBox.Show(
					"Already started on the other logon session !",
					title + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				return false;
			}

			public static void Release()
			{
				CloseProcMtx();
			}

			private static void CloseProcMtx()
			{
				try { ProcMtx.ReleaseMutex(); }
				catch { }

				try { ProcMtx.Close(); }
				catch { }

				ProcMtx = null;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static SyncQueue<string> Log = new SyncQueue<string>();
	}
}
