using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class CsvFileWriter : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private const char DELIMITER = ',';
		//private const char DELIMITER = '\t';

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private StreamWriter Writer;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private bool RowHead;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CsvFileWriter(string file, bool append = false)
			: this(file, append, StringTools.ENCODING_SJIS)
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CsvFileWriter(string file, bool append, Encoding encoding)
		{
			this.Writer = new StreamWriter(file, append, encoding);
			this.RowHead = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WriteCell(string cell)
		{
			if (this.RowHead)
				this.RowHead = false;
			else
				this.Writer.Write(DELIMITER);

			if (
				cell.Contains('"') ||
				cell.Contains('\n') ||
				cell.Contains(DELIMITER)
				)
			{
				this.Writer.Write('"');
				this.Writer.Write(cell.Replace("\"", "\"\""));
				this.Writer.Write('"');
			}
			else
				this.Writer.Write(cell);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void EndRow()
		{
			this.Writer.Write('\n');
			this.RowHead = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WriteCells(string[] cells)
		{
			foreach (string cell in cells)
			{
				this.WriteCell(cell);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WriteRow(string[] row)
		{
			foreach (string cell in row)
			{
				this.WriteCell(cell);
			}
			this.EndRow();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WriteRows(string[][] rows)
		{
			foreach (string[] row in rows)
			{
				this.WriteRow(row);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.Writer != null)
			{
				this.Writer.Dispose();
				this.Writer = null;
			}
		}
	}
}
