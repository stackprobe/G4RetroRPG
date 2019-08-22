using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class ExtraTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void AntiWindowsDefenderSmartScreen()
		{
			// Chocolate.dll と同じ場所でないと実行出来ないので、CheckAloneExe()は不要と判断した。@ 201x.x.x

			ProcMain.WriteLog("awdss_1");

			if (Is初回起動())
			{
				ProcMain.WriteLog("awdss_2");

				foreach (string exeFile in Directory.GetFiles(ProcMain.SelfDir, "*.exe", SearchOption.TopDirectoryOnly))
				{
					try
					{
						ProcMain.WriteLog("awdss_exeFile: " + exeFile);

						if (StringTools.EqualsIgnoreCase(exeFile, ProcMain.SelfFile))
						{
							ProcMain.WriteLog("awdss_self_noop");
						}
						else
						{
							byte[] exeData = File.ReadAllBytes(exeFile);
							File.Delete(exeFile);
							File.WriteAllBytes(exeFile, exeData);
						}
						ProcMain.WriteLog("awdss_OK");
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}
				}
				ProcMain.WriteLog("awdss_3");
			}
			ProcMain.WriteLog("awdss_4");
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static bool? _初回起動 = null;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Is初回起動()
		{
			if (_初回起動 == null)
			{
				string sigFile = ProcMain.SelfFile + ".awdss.sig";

				_初回起動 = File.Exists(sigFile) == false;

				File.WriteAllBytes(sigFile, BinTools.EMPTY);
			}
			return _初回起動.Value;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void PostShown(Form f)
		{
			CallAllControl(f.Controls, control =>
			{
				TextBox tb = control as TextBox;

				if (tb != null)
				{
					if (tb.ContextMenuStrip == null)
					{
						ToolStripMenuItem item = new ToolStripMenuItem();

						item.Text = "項目なし";
						item.Enabled = false;

						ContextMenuStrip menu = new ContextMenuStrip();

						menu.Items.Add(item);

						tb.ContextMenuStrip = menu;
					}
				}
			});
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void CallAllControl(Control.ControlCollection controls, Action<Control> rtn)
		{
			foreach (Control control in controls)
			{
				rtn(control);

				GroupBox gb = control as GroupBox;

				if (gb != null)
				{
					CallAllControl(gb.Controls, rtn);
				}
				TabControl tc = control as TabControl;

				if (tc != null)
				{
					foreach (TabPage tp in tc.TabPages)
					{
						CallAllControl(tp.Controls, rtn);
					}
				}
				SplitContainer sc = control as SplitContainer;

				if (sc != null)
				{
					CallAllControl(sc.Panel1.Controls, rtn);
					CallAllControl(sc.Panel2.Controls, rtn);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetEnabledDoubleBuffer(Control control)
		{
			control.GetType().InvokeMember(
				"DoubleBuffered",
				BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
				null,
				control,
				new object[] { true }
				);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static string SemiRemoveDestDir = null;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SemiRemovePath(string path, bool keepSrcPath = false)
		{
			if (SemiRemoveDestDir == null)
				SemiRemoveDestDir = MakeFreeDir();

			string destPath = Path.Combine(SemiRemoveDestDir, Path.GetFileName(path));
			destPath = ToCreatablePath(destPath);

			if (File.Exists(path))
			{
				if (keepSrcPath)
					File.Copy(path, destPath);
				else
					File.Move(path, destPath);
			}
			else if (Directory.Exists(path))
			{
				if (keepSrcPath)
					FileTools.CopyDir(path, destPath);
				else
					FileTools.MoveDir(path, destPath);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakeFreeDir()
		{
			for (int c = 1; ; c++)
			{
				string dir = @"C:\" + c;

				if (Accessible(dir) == false)
				{
					FileTools.CreateDir(dir);
					return dir;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ToCreatablePath(string path)
		{
			if (Accessible(path))
			{
				string prefix = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
				string suffix = Path.GetExtension(path);

				for (int c = 2; ; c++)
				{
					path = prefix + "~" + c + suffix;

					if (Accessible(path) == false)
						break;
				}
			}
			return path;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Accessible(string path)
		{
			return File.Exists(path) || Directory.Exists(path);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T DesertElement<T>(List<T> list, int index)
		{
			T ret = list[index];
			list.RemoveAt(index);
			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T UnaddElement<T>(List<T> list)
		{
			return DesertElement(list, list.Count - 1);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static T FastDesertElement<T>(List<T> list, int index)
		{
			T ret = UnaddElement(list);

			if (index < list.Count)
			{
				T ret2 = list[index];
				list[index] = ret;
				ret = ret2;
			}
			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string GetHomeDir(string marker = "_home")
		{
			return GetHomeDir(marker, Directory.GetCurrentDirectory());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string GetHomeDir(string marker, string dir)
		{
			dir = FileTools.MakeFullPath(dir);

			for (; ; )
			{
				string file = Path.Combine(dir, marker);

				if (File.Exists(file))
					break;

				if (dir.Length <= 3) // ? ルートディレクトリに達した。
					throw new Exception("no " + marker);

				dir = Path.GetDirectoryName(dir);
			}
			return dir;
		}
	}
}
