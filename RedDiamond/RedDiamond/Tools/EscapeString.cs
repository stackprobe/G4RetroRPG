using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class EscapeString
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public EscapeString()
			: this("\t\r\n ", '$', "trns")
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string DisallowedChrs;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private char EscapeChr;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string AllowedChrs;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public EscapeString(string disallowedChrs, char escapeChr, string allowedChrs)
		{
			if (
				disallowedChrs == null ||
				allowedChrs == null ||
				disallowedChrs.Length != allowedChrs.Length ||
				StringTools.HasSameChar(disallowedChrs + escapeChr + allowedChrs)
				)
				throw new ArgumentException();

			this.DisallowedChrs = disallowedChrs + escapeChr;
			this.EscapeChr = escapeChr;
			this.AllowedChrs = allowedChrs + escapeChr;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Encode(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				int chrPos = this.DisallowedChrs.IndexOf(chr);

				if (chrPos == -1)
				{
					buff.Append(chr);
				}
				else
				{
					buff.Append(this.EscapeChr);
					buff.Append(this.AllowedChrs[chrPos]);
				}
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Decode(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == this.EscapeChr && index + 1 < str.Length)
				{
					index++;
					chr = str[index];
					int chrPos = this.AllowedChrs.IndexOf(chr);

					if (chrPos != -1)
					{
						chr = this.DisallowedChrs[chrPos];
					}
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
