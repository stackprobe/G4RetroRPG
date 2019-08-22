using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class StringTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string BINADECIMAL = "01";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string OCTODECIMAL = "012234567";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string DECIMAL = "0123456789";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string HEXADECIMAL = "0123456789ABCDEF";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string hexadecimal = "0123456789abcdef";

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string alpha = "abcdefghijklmnopqrstuvwxyz";
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string PUNCT =
			GetString_SJISHalfCodeRange(0x21, 0x2f) +
			GetString_SJISHalfCodeRange(0x3a, 0x40) +
			GetString_SJISHalfCodeRange(0x5b, 0x60) +
			GetString_SJISHalfCodeRange(0x7b, 0x7e);

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ASCII = DECIMAL + ALPHA + alpha + PUNCT; // == GetString_SJISHalfCodeRange(0x21, 0x7e)
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string KANA = GetString_SJISHalfCodeRange(0xa1, 0xdf);

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string HALF = ASCII + KANA;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string GetString_SJISHalfCodeRange(int codeMin, int codeMax)
		{
			byte[] buff = new byte[codeMax - codeMin + 1];

			for (int code = codeMin; code <= codeMax; code++)
			{
				buff[code - codeMin] = (byte)code;
			}
			return ENCODING_SJIS.GetString(buff);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_DECIMAL = GetString_SJISCodeRange(0x82, 0x4f, 0x58);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_ALPHA = GetString_SJISCodeRange(0x82, 0x60, 0x79);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string mbc_alpha = GetString_SJISCodeRange(0x82, 0x81, 0x9a);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_SPACE = GetString_SJISCodeRange(0x81, 0x40, 0x40);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_PUNCT =
			GetString_SJISCodeRange(0x81, 0x41, 0x7e) +
			GetString_SJISCodeRange(0x81, 0x80, 0xac) +
			GetString_SJISCodeRange(0x81, 0xb8, 0xbf) + // 集合
			GetString_SJISCodeRange(0x81, 0xc8, 0xce) + // 論理
			GetString_SJISCodeRange(0x81, 0xda, 0xe8) + // 数学
			GetString_SJISCodeRange(0x81, 0xf0, 0xf7) +
			GetString_SJISCodeRange(0x81, 0xfc, 0xfc) +
			GetString_SJISCodeRange(0x83, 0x9f, 0xb6) + // ギリシャ語大文字
			GetString_SJISCodeRange(0x83, 0xbf, 0xd6) + // ギリシャ語小文字
			GetString_SJISCodeRange(0x84, 0x40, 0x60) + // キリル文字大文字
			GetString_SJISCodeRange(0x84, 0x70, 0x7e) + // キリル文字小文字(1)
			GetString_SJISCodeRange(0x84, 0x80, 0x91) + // キリル文字小文字(2)
			GetString_SJISCodeRange(0x84, 0x9f, 0xbe) + // 枠線
			GetString_SJISCodeRange(0x87, 0x40, 0x5d) + // 機種依存文字(1)
			GetString_SJISCodeRange(0x87, 0x5f, 0x75) + // 機種依存文字(2)
			GetString_SJISCodeRange(0x87, 0x7e, 0x7e) + // 機種依存文字(3)
			GetString_SJISCodeRange(0x87, 0x80, 0x9c) + // 機種依存文字(4)
			GetString_SJISCodeRange(0xee, 0xef, 0xfc); // 機種依存文字(5)

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_CHOUONPU = GetString_SJISCodeRange(0x81, 0x5b, 0x5b); // 815b == 長音符

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_HIRA = GetString_SJISCodeRange(0x82, 0x9f, 0xf1);
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MBC_KANA =
			GetString_SJISCodeRange(0x83, 0x40, 0x7e) +
			GetString_SJISCodeRange(0x83, 0x80, 0x96) + MBC_CHOUONPU;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static string GetString_SJISCodeRange(int lead, int trailMin, int trailMax)
		{
			byte[] buff = new byte[(trailMax - trailMin + 1) * 2];

			for (int trail = trailMin; trail <= trailMax; trail++)
			{
				buff[(trail - trailMin) * 2 + 0] = (byte)lead;
				buff[(trail - trailMin) * 2 + 1] = (byte)trail;
			}
			return ENCODING_SJIS.GetString(buff);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int Comp(string a, string b)
		{
			return BinTools.Comp(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int CompIgnoreCase(string a, string b)
		{
			return Comp(a.ToLower(), b.ToLower());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class IEComp : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a == b;
			}

			public int GetHashCode(string a)
			{
				return a.GetHashCode();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class IECompIgnoreCase : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a.ToLower() == b.ToLower();
			}

			public int GetHashCode(string a)
			{
				return a.ToLower().GetHashCode();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool StartsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().StartsWith(ptn.ToLower());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool EndsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().EndsWith(ptn.ToLower());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool ContainsIgnoreCase(string str, string ptn)
		{
			return str.ToLower().Contains(ptn.ToLower());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int IndexOfIgnoreCase(string str, string ptn)
		{
			return str.ToLower().IndexOf(ptn.ToLower());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int IndexOfIgnoreCase(string str, char chr)
		{
			return str.ToLower().IndexOf(char.ToLower(chr));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Contains(string str, Predicate<char> match)
		{
			return IndexOf(str, match) != -1;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int IndexOf(string str, Predicate<char> match)
		{
			for (int index = 0; index < str.Length; index++)
				if (match(str[index]))
					return index;

			return -1;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class Island
		{
			public string Str;
			public int Start;
			public int End;

			public string Left
			{
				get
				{
					return this.Str.Substring(0, this.Start);
				}
			}

			public int InnerLength
			{
				get
				{
					return this.End - this.Start;
				}
			}

			public string Inner
			{
				get
				{
					return this.Str.Substring(this.Start, this.InnerLength);
				}
			}

			public string Right
			{
				get
				{
					return this.Str.Substring(this.End);
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Island GetIsland(string str, string mid, int startIndex = 0)
		{
			int index = str.IndexOf(mid, startIndex);

			if (index == -1)
				return null;

			return new Island()
			{
				Str = str,
				Start = index,
				End = index + mid.Length,
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Island GetIslandIgnoreCase(string str, string mid, int startIndex = 0)
		{
			Island ret = GetIsland(
				str.ToLower(),
				mid.ToLower(),
				startIndex
				);

			if (ret != null)
				ret.Str = str;

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Island[] GetAllIsland(string str, string mid, int startIndex = 0)
		{
			List<Island> dest = new List<Island>();

			for (; ; )
			{
				Island island = GetIsland(str, mid, startIndex);

				if (island == null)
					break;

				dest.Add(island);
				startIndex = island.End;
			}
			return dest.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Island[] GetAllIslandIgnoreCase(string str, string mid, int startIndex = 0)
		{
			Island[] ret = GetAllIsland(
				str.ToLower(),
				mid.ToLower(),
				startIndex
				);

			foreach (Island island in ret)
				island.Str = str;

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class Enclosed
		{
			public Island StartPtn;
			public Island EndPtn;

			public string Str
			{
				get
				{
					return this.StartPtn.Str;
				}

				set
				{
					this.StartPtn.Str = value;
					this.EndPtn.Str = value;
				}
			}

			public string Left
			{
				get
				{
					return this.Str.Substring(0, this.StartPtn.End); // == this.StartPtn.Left + this.StartPtn.Inner
				}
			}

			public int InnerLength
			{
				get
				{
					return this.EndPtn.Start - this.StartPtn.End;
				}
			}

			public string Inner
			{
				get
				{
					return this.Str.Substring(this.StartPtn.End, this.InnerLength);
				}
			}

			public string Right
			{
				get
				{
					return this.Str.Substring(this.EndPtn.Start); // == this.EndPtn.Inner + this.EndPtn.Right
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Enclosed GetEnclosed(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed ret = new Enclosed();

			ret.StartPtn = GetIsland(str, startPtn, startIndex);

			if (ret.StartPtn == null)
				return null;

			ret.EndPtn = GetIsland(str, endPtn, ret.StartPtn.End);

			if (ret.EndPtn == null)
				return null;

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Enclosed GetEnclosedIgnoreCase(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed ret = GetEnclosed(
				str.ToLower(),
				startPtn.ToLower(),
				endPtn.ToLower(),
				startIndex
				);

			if (ret != null)
				ret.Str = str;

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Enclosed[] GetAllEnclosed(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			List<Enclosed> dest = new List<Enclosed>();

			for (; ; )
			{
				Enclosed encl = GetEnclosed(str, startPtn, endPtn, startIndex);

				if (encl == null)
					break;

				dest.Add(encl);
				startIndex = encl.EndPtn.End;
			}
			return dest.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Enclosed[] GetAllEnclosedIgnoreCase(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed[] ret = GetAllEnclosed(
				str.ToLower(),
				startPtn.ToLower(),
				endPtn.ToLower(),
				startIndex
				);

			foreach (Enclosed encl in ret)
				encl.Str = str;

			return ret;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string[] Tokenize(string str, string delimiters, bool meaningFlag = false, bool ignoreEmpty = false, int limit = 0)
		{
			StringBuilder buff = new StringBuilder();
			List<string> tokens = new List<string>();

			foreach (char chr in str)
			{
				if (tokens.Count + 1 == limit || delimiters.Contains(chr) == meaningFlag)
				{
					buff.Append(chr);
				}
				else
				{
					if (ignoreEmpty == false || buff.Length != 0)
						tokens.Add(buff.ToString());

					buff = new StringBuilder();
				}
			}
			if (ignoreEmpty == false || buff.Length != 0)
				tokens.Add(buff.ToString());

			return tokens.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool HasSameChar(string str)
		{
			for (int r = 1; r < str.Length; r++)
				for (int l = 0; l < r; l++)
					if (str[l] == str[r])
						return true;

			return false;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ReplaceChars(string str, string rChrs, char wChr)
		{
			foreach (char rChr in rChrs)
				str = str.Replace(rChr, wChr);

			return str;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string RepalceCharsPair(string str, string rChrs, string wChrs)
		{
			if (rChrs.Length != wChrs.Length)
				throw new ArgumentException("置き換え前と置き換え後の文字が対応していません。");

			for (int index = 0; index < rChrs.Length; index++)
				str = str.Replace(rChrs[index], wChrs[index]);

			return str;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string ReplaceLoop(string str, string rPtn, string wPtn, int count = 30)
		{
			while (1 <= count)
			{
				str = str.Replace(rPtn, wPtn);
				count--;
			}
			return str;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MultiReplace(string str, params string[] ptns)
		{
			return MultiReplace(str, ptns, false);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MultiReplaceIgnoreCase(string str, params string[] ptns)
		{
			return MultiReplace(str, ptns, true);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class ReplaceInfo
		{
			public string OldValue;
			public Func<string> GetValueNew;
			public bool IgnoreCase;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MultiReplace(string str, string[] ptns, bool ignoreCase)
		{
			if (ptns.Length % 2 != 0)
				throw new ArgumentException("最後の置き換え「後」パターンが不足しています。");

			ReplaceInfo[] infos = new ReplaceInfo[ptns.Length / 2];

			for (int index = 0; index < infos.Length; index++)
			{
				string valueNew = ptns[index * 2 + 1];

				infos[index] = new ReplaceInfo()
				{
					OldValue = ptns[index * 2 + 0],
					GetValueNew = () => valueNew,
					IgnoreCase = ignoreCase,
				};
			}
			return MultiReplace(str, infos);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string MultiReplace(string str, ReplaceInfo[] infos)
		{
			if (str == null) throw new ArgumentException("str is null");
			if (infos == null) throw new ArgumentException("infos is null");

			foreach (ReplaceInfo info in infos)
			{
				if (info == null) throw new ArgumentException("info is null");
				if (string.IsNullOrEmpty(info.OldValue)) throw new ArgumentException("info.OldValue is null or empty");
				if (info.GetValueNew == null) throw new ArgumentException("info.GetValueNew is null");
				//info.IgnoreCase
			}

			// ここまで引数チェック

			infos = infos.Clone() as ReplaceInfo[];

			Array.Sort(infos, (a, b) =>
			{
				int ret = VariantTools.Comp(a, b, v => v.OldValue.Length) * -1; // OldValue 長い -> 短い 順
				if (ret != 0)
					return ret;

				ret = VariantTools.Comp(a, b, v => v.IgnoreCase ? 1 : 0); // case sensive -> ignore case 順
				if (ret != 0)
					return ret;

				ret = StringTools.Comp(a.OldValue, b.OldValue);
				return ret;
			});

			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				foreach (ReplaceInfo info in infos)
				{
					if (info.OldValue.Length <= str.Length - index)
					{
						string part = str.Substring(index, info.OldValue.Length);

						if (
							info.IgnoreCase ?
							info.OldValue.ToLower() == part.ToLower() :
							info.OldValue == part
							)
						{
							buff.Append(info.GetValueNew());
							index += info.OldValue.Length - 1;
							goto replaced;
						}
					}
				}
				buff.Append(str[index]);

			replaced:
				;
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string AntiNullOrEmpty(string str, string defval = "_")
		{
			return string.IsNullOrEmpty(str) ? defval : str;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string AntiNull(string str)
		{
			return str == null ? "" : str;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string SetCharAt(string str, int index, char chr)
		{
			return str.Substring(0, index) + chr + str.Substring(index + 1);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Repeat(string ptn, int count)
		{
			StringBuilder buff = new StringBuilder(ptn.Length * count);

			while (0 < count)
			{
				buff.Append(ptn);
				count--;
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Escape(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				if (chr <= ' ' || chr == '$' || (0x7f <= chr && chr <= 0xff))
				{
					buff.Append('$');
					buff.Append(((int)chr).ToString("x2"));
				}
				else
					buff.Append(chr);
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Unescape(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == '$')
				{
					chr = (char)Convert.ToInt32(str.Substring(index + 1, 2), 16);
					index += 2;
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool LiteValidate(string target, string allowChars) // target == "" --> false
		{
			string format = target;

			char escapeChar = allowChars[0];
			allowChars = allowChars.Substring(1);
			string escape = new string(new char[] { escapeChar });
			string escape2 = new string(new char[] { escapeChar, escapeChar });

			format = StringTools.ReplaceChars(format, allowChars, escapeChar);
			format = StringTools.ReplaceLoop(format, escape2, escape);

			return format == escape;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool LiteValidate(string target, string allowChars, int minlen)
		{
			if (minlen == 0 && target == "")
				return true;

			return minlen <= target.Length && LiteValidate(target, allowChars);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool LiteValidate(string target, string allowChars, int minlen, int maxlen)
		{
			return target.Length <= maxlen && LiteValidate(target, allowChars, minlen);
		}
	}
}
