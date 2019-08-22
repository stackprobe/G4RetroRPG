using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class ZipTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] Compress(byte[] src)
		{
			using (MemoryStream reader = new MemoryStream(src))
			using (MemoryStream writer = new MemoryStream())
			{
				Compress(reader, writer);
				return writer.ToArray();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] Decompress(byte[] src, int limit = -1)
		{
			using (MemoryStream reader = new MemoryStream(src))
			using (MemoryStream writer = new MemoryStream())
			{
				Decompress(reader, writer, (long)limit);
				return writer.ToArray();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Compress(string rFile, string wFile)
		{
			using (FileStream reader = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (FileStream writer = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{
				Compress(reader, writer);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Decompress(string rFile, string wFile, long limit = -1L)
		{
			using (FileStream reader = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (FileStream writer = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			{
				Decompress(reader, writer, limit);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Compress(Stream reader, Stream writer)
		{
			using (GZipStream gz = new GZipStream(writer, CompressionMode.Compress))
			{
				reader.CopyTo(gz);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Decompress(Stream reader, Stream writer, long limit = -1L)
		{
			using (GZipStream gz = new GZipStream(reader, CompressionMode.Decompress))
			{
				if (limit == -1L)
				{
					gz.CopyTo(writer);
				}
				else
				{
					FileTools.ReadToEnd(gz.Read, new LimitedWriter(limit, writer.Write).Write);
				}
			}
		}
	}
}
