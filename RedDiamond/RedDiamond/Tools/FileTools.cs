using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class FileTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Delete(string path)
		{
			if (string.IsNullOrEmpty(path))
				throw new Exception("削除しようとしたパスは null 又は空文字列です。");

			if (File.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						File.Delete(path);
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e + " <---- 例外ここまで、処理を続行します。");
					}
					if (File.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ファイルの削除に失敗しました。" + path);

					ProcMain.WriteLog("ファイルの削除をリトライします。" + path);
					Thread.Sleep(c * 100);
				}
			}
			else if (Directory.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						Directory.Delete(path, true);
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e + " <---- 例外ここまで、処理を続行します。");
					}
					if (Directory.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ディレクトリの削除に失敗しました。" + path);

					ProcMain.WriteLog("ディレクトリの削除をリトライします。" + path);
					Thread.Sleep(c * 100);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void CreateDir(string dir)
		{
			if (string.IsNullOrEmpty(dir))
				throw new Exception("作成しようとしたディレクトリは null 又は空文字列です。");

			for (int c = 1; ; c++)
			{
				try
				{
					Directory.CreateDirectory(dir); // dirが存在するときは何もしない。
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e + " <---- 例外ここまで、処理を続行します。");
				}
				if (Directory.Exists(dir))
					break;

				if (10 < c)
					throw new Exception("ディレクトリを作成出来ません。" + dir);

				ProcMain.WriteLog("ディレクトリの作成をリトライします。" + dir);
				Thread.Sleep(c * 100);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void CleanupDir(string dir)
		{
			if (string.IsNullOrEmpty(dir))
				throw new Exception("配下を削除しようとしたディレクトリは null 又は空文字列です。");

			foreach (Func<IEnumerable<string>> getPaths in new Func<IEnumerable<string>>[]
			{
				() => Directory.EnumerateFiles(dir),
				() => Directory.EnumerateDirectories(dir),
			})
			{
				foreach (string path in getPaths())
				{
					Delete(path);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void CopyDir(string rDir, string wDir)
		{
			CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				CopyDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Copy(file, Path.Combine(wDir, Path.GetFileName(file)));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void MoveDir(string rDir, string wDir)
		{
			CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				MoveDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Move(file, Path.Combine(wDir, Path.GetFileName(file)));

			Delete(rDir);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ChangeRoot(string path, string oldRoot, string rootNew)
		{
			return PutYen(rootNew) + ChangeRoot(path, oldRoot);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ChangeRoot(string path, string oldRoot)
		{
			oldRoot = PutYen(oldRoot);

			if (StringTools.StartsWithIgnoreCase(path, oldRoot) == false)
				throw new Exception("パスの配下ではありません。" + oldRoot + " -> " + path);

			return path.Substring(oldRoot.Length);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string PutYen(string path)
		{
			if (path.EndsWith("\\") == false)
				path += "\\";

			return path;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MakeFullPath(string path)
		{
			if (path == null)
				throw new Exception("パスが定義されていません。(null)");

			if (path == "")
				throw new Exception("パスが定義されていません。(空文字列)");

			path = Path.GetFullPath(path);

			if (path.Contains('/')) // Path.GetFullPath が '/' を '\\' に置換するはず。
				throw null;

			if (path.StartsWith("\\\\"))
				throw new Exception("ネットワークパスまたはデバイス名は使用出来ません。");

			if (path.Substring(1, 2) != ":\\") // ネットワークパスでないならローカルパスのはず。
				throw null;

			path = PutYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ToFullPath(string path)
		{
			path = Path.GetFullPath(path);
			path = PutYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] Read(FileStream reader, int size)
		{
			byte[] buff = new byte[size];
			Read(reader, buff);
			return buff;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Read(FileStream reader, byte[] buff, int offset = 0)
		{
			Read(reader, buff, offset, buff.Length - offset);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Read(FileStream reader, byte[] buff, int offset, int count)
		{
			if (reader.Read(buff, offset, count) != count)
			{
				throw new Exception("データの途中でファイルの終端に到達しました。");
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Write(FileStream writer, byte[] data, int offset = 0)
		{
			writer.Write(data, offset, data.Length - offset);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void AppendAllBytes(string file, byte[] data)
		{
			using (FileStream writer = new FileStream(file, FileMode.Append, FileAccess.Write))
			{
				Write(writer, data);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string LinesToText(string[] lines)
		{
			return lines.Length == 0 ? "" : string.Join("\r\n", lines) + "\r\n";
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string[] TextToLines(string text)
		{
			text = text.Replace("\r", "");

			string[] lines = text.Split('\n');

			if (1 <= lines.Length && lines[lines.Length - 1] == "")
			{
				lines = new List<string>(lines).GetRange(0, lines.Length - 1).ToArray();
			}
			return lines;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public delegate int Read_d(byte[] buffer, int offset, int count);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public delegate void Write_d(byte[] buffer, int offset, int count);

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void ReadToEnd(Read_d reader, Write_d writer)
		{
			byte[] buff = new byte[4 * 1024 * 1024];

			for (; ; )
			{
				int readSize = reader(buff, 0, buff.Length);

				if (readSize < 0)
					break;

				writer(buff, 0, readSize);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static IEnumerable<byte[]> Iterate(Read_d reader)
		{
			byte[] buff = new byte[4 * 1024 * 1024];
			byte[] part;

			for (; ; )
			{
				int readSize = reader(buff, 0, buff.Length);

				if (readSize < 0)
					break;

				if (readSize < buff.Length)
				{
					part = new byte[readSize];
					Array.Copy(buff, 0, part, 0, readSize);
				}
				else
					part = buff;

				yield return part;
			}
		}
	}
}
