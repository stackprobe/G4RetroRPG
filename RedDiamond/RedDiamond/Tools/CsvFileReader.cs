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
	public class CsvFileReader : IDisposable
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private const char DELIMITER = ',';
		//private const char DELIMITER = '\t';

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private StreamReader Reader;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CsvFileReader(string file)
			: this(file, StringTools.ENCODING_SJIS)
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public CsvFileReader(string file, Encoding encoding)
		{
			this.Reader = new StreamReader(file, encoding);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int LastChar;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private int ReadChar()
		{
			do
			{
				this.LastChar = this.Reader.Read();
			}
			while (this.LastChar == '\r');

			return this.LastChar;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private bool EnclosedCell;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string ReadCell()
		{
			StringBuilder buff = new StringBuilder();

			if (this.ReadChar() == '"')
			{
				while (this.ReadChar() != -1 && (this.LastChar != '"' || this.ReadChar() == '"'))
				{
					buff.Append((char)this.LastChar);
				}
				this.EnclosedCell = true;
			}
			else
			{
				while (this.LastChar != -1 && this.LastChar != '\n' && this.LastChar != DELIMITER)
				{
					buff.Append((char)this.LastChar);
					this.ReadChar();
				}
				this.EnclosedCell = false;
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string[] ReadRow()
		{
			List<string> row = new List<string>();

			do
			{
				row.Add(this.ReadCell());
			}
			while (this.LastChar != -1 && this.LastChar != '\n');

			if (this.LastChar == -1 && row.Count == 1 && row[0] == "" && this.EnclosedCell == false)
				return null;

			return row.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string[][] ReadToEnd()
		{
			List<string[]> rows = new List<string[]>();

			for (; ; )
			{
				string[] row = this.ReadRow();

				if (row == null)
					break;

				rows.Add(row);
			}
			return rows.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Dispose()
		{
			if (this.Reader != null)
			{
				this.Reader.Dispose();
				this.Reader = null;
			}
		}
	}
}
