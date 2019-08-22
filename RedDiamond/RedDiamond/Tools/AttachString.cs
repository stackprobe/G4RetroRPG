using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class AttachString
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public AttachString()
			: this(':', '$', '.')
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public AttachString(char delimiter, char escapeChr, char escapedDelimiter)
			: this(delimiter, new EscapeString(
				delimiter.ToString(),
				escapeChr,
				escapedDelimiter.ToString()
				))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private char Delimiter;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private EscapeString ES;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public AttachString(char delimiter, EscapeString es)
		{
			this.Delimiter = delimiter;
			this.ES = es;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Untokenize(IEnumerable<string> tokens)
		{
			List<string> dest = new List<string>();

			foreach (string token in tokens)
				dest.Add(this.ES.Encode(token));

			dest.Add("");
			return string.Join(this.Delimiter.ToString(), dest);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string[] Tokenize(string str)
		{
			List<string> dest = new List<string>();

			foreach (string token in StringTools.Tokenize(str, this.Delimiter.ToString()))
				dest.Add(this.ES.Decode(token));

			dest.RemoveAt(dest.Count - 1);
			return dest.ToArray();
		}
	}
}
