using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class JsonTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static string Encode(object src, bool noBlank = false)
		{
			Encoder e = new Encoder();

			if (noBlank)
			{
				e.Blank = "";
				e.Indent = "";
				e.NewLine = "";
			}
			e.Add(src, 0);

			return e.GetString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class Encoder
		{
			public string Blank = " ";
			public string Indent = "\t";
			public string NewLine = "\r\n";

			private StringBuilder Buff = new StringBuilder();

			public void Add(object src, int indent)
			{
				if (src is ObjectMap)
				{
					ObjectMap om = (ObjectMap)src;
					bool secondOrLater = false;

					this.Buff.Append("{");
					this.Buff.Append(this.NewLine);

					foreach (string key in om.GetKeys())
					{
						object value = om[key];

						if (secondOrLater)
						{
							this.Buff.Append(",");
							this.Buff.Append(this.NewLine);
						}
						this.AddIndent(indent + 1);
						this.Add(key, 0); // key is string
						this.Buff.Append(this.Blank);
						this.Buff.Append(":");
						this.Buff.Append(this.Blank);
						this.Add(value, indent + 1);

						secondOrLater = true;
					}
					this.Buff.Append(this.NewLine);
					this.AddIndent(indent);
					this.Buff.Append("}");
				}
				else if (src is ObjectList)
				{
					ObjectList ol = (ObjectList)src;
					bool secondOrLater = false;

					this.Buff.Append("[");
					this.Buff.Append(this.NewLine);

					foreach (object value in ol.Direct())
					{
						if (secondOrLater)
						{
							this.Buff.Append(",");
							this.Buff.Append(this.NewLine);
						}
						this.AddIndent(indent + 1);
						this.Add(value, indent + 1);

						secondOrLater = true;
					}
					this.Buff.Append(this.NewLine);
					this.AddIndent(indent);
					this.Buff.Append("]");
				}
				else if (src is Word)
				{
					this.Buff.Append("" + ((Word)src).Value);
				}
				else //if (src is string)
				{
					string str = "" + src;
					//string str = (string)src;

					this.Buff.Append("\"");

					foreach (char chr in str)
					{
						if (chr == '"')
						{
							this.Buff.Append("\\\"");
						}
						else if (chr == '\\')
						{
							this.Buff.Append("\\\\");
						}
						else if (chr == '\b')
						{
							this.Buff.Append("\\b");
						}
						else if (chr == '\f')
						{
							this.Buff.Append("\\f");
						}
						else if (chr == '\n')
						{
							this.Buff.Append("\\n");
						}
						else if (chr == '\r')
						{
							this.Buff.Append("\\r");
						}
						else if (chr == '\t')
						{
							this.Buff.Append("\\t");
						}
						else
						{
							this.Buff.Append(chr);
						}
					}
					this.Buff.Append("\"");
				}
			}

			private void AddIndent(int count)
			{
				while (0 < count)
				{
					this.Buff.Append(this.Indent);
					count--;
				}
			}

			public string GetString()
			{
				return this.Buff.ToString();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static object Decode(byte[] src)
		{
			return Decode(GetEncoding(src).GetString(src)); // src に BOM が付いている場合、encoding.GetString(src) にも BOM が付く！
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Encoding GetEncoding(byte[] src)
		{
			if (4 <= src.Length)
			{
				string x4 = BinTools.Hex.ToString(BinTools.GetSubBytes(src, 0, 4));

				if ("0000feff" == x4 || "fffe0000" == x4)
				{
					return Encoding.UTF32;
				}
			}
			if (2 <= src.Length)
			{
				string x2 = BinTools.Hex.ToString(BinTools.GetSubBytes(src, 0, 2));

				if ("feff" == x2 || "fffe" == x2)
				{
					return Encoding.Unicode;
				}
			}
			return Encoding.UTF8;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static object Decode(string src)
		{
			return new Decoder()
			{
				Src = src,
			}
			.GetObject();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class Decoder
		{
			public string Src;

			private int RPos = 0;

			private char Next()
			{
				return this.Src[this.RPos++];
			}

			private char NextNS()
			{
				char chr;

				do
				{
					chr = this.Next();
				}
				while (chr <= ' ' || chr == 0xfeff); // ? 空白系文字 || BOM

				return chr;
			}

			private char NextNS(string allowedChrs)
			{
				char chr = this.NextNS();

				if (allowedChrs.Contains(chr) == false)
					throw new Exception("JSON format error: " + allowedChrs + ", " + chr);

				return chr;
			}

			public object GetObject()
			{
				return GetObject(this.NextNS());
			}

			private int ObjectCount = 0;

			public object GetObject(char chr)
			{
				if (DecodeObjectCountMax != -1 && DecodeObjectCountMax <= this.ObjectCount)
					throw new Exception("JSON format error: over " + DecodeObjectCountMax + " objects");

				this.ObjectCount++;

				if (chr == '{')
				{
					ObjectMap om = ObjectMap.CreateIgnoreCase();

					if ((chr = this.NextNS()) != '}')
					{
						for (; ; )
						{
							object key = this.GetObject(chr);

							if (key is string == false)
								ProcMain.WriteLog("JSON format warning: key is not string");

							this.NextNS(":");

							om.Add(key, this.GetObject());

							if (this.NextNS(",}") == '}')
								break;

							if ((chr = this.NextNS()) == '}')
							{
								ProcMain.WriteLog("JSON format warning: found ',' before '}'");
								break;
							}
						}
					}
					return om;
				}
				if (chr == '[')
				{
					ObjectList ol = new ObjectList();

					if ((chr = this.NextNS()) != ']')
					{
						for (; ; )
						{
							ol.Add(this.GetObject(chr));

							if (this.NextNS(",]") == ']')
								break;

							if ((chr = this.NextNS()) == ']')
							{
								ProcMain.WriteLog("JSON format warning: found ',' before ']'");
								break;
							}
						}
					}
					return ol;
				}
				if (chr == '"')
				{
					StringBuilder buff = new StringBuilder();

					for (; ; )
					{
						chr = this.Next();

						if (chr == '"')
						{
							break;
						}
						if (chr == '\\')
						{
							chr = this.Next();

							if (chr == 'b')
							{
								chr = '\b';
							}
							else if (chr == 'f')
							{
								chr = '\f';
							}
							else if (chr == 'n')
							{
								chr = '\n';
							}
							else if (chr == 'r')
							{
								chr = '\r';
							}
							else if (chr == 't')
							{
								chr = '\t';
							}
							else if (chr == 'u')
							{
								char c1 = this.Next();
								char c2 = this.Next();
								char c3 = this.Next();
								char c4 = this.Next();

								chr = (char)Convert.ToInt32(new string(new char[] { c1, c2, c3, c4 }), 16);
							}
						}
						buff.Append(chr);
					}
					string str = buff.ToString();
					str = DecodeStringFilter(str);
					return str;
				}

				{
					StringBuilder buff = new StringBuilder();

					buff.Append(chr);

					while (this.RPos < this.Src.Length)
					{
						chr = this.Next();

						if (
							chr == '}' ||
							chr == ']' ||
							chr == ',' ||
							chr == ':'
							)
						{
							this.RPos--;
							break;
						}
						buff.Append(chr);
					}
					Word word = new Word()
					{
						Value = buff.ToString().Trim(),
					};

					if (word.IsFairJsonWord() == false)
						ProcMain.WriteLog("JSON format warning: value is not fair JSON word");

					word.Value = DecodeStringFilter(word.Value);
					return word;
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class Word
		{
			public string Value;

			public override string ToString()
			{
				return this.Value;
			}

			public bool IsFairJsonWord()
			{
				return
					this.Value == "true" ||
					this.Value == "false" ||
					this.Value == "null" ||
					this.IsNumber();
			}

			private bool IsNumber() // XXX
			{
				return StringTools.LiteValidate(this.Value, StringTools.DECIMAL + "+-.Ee");
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Func<string, string> DecodeStringFilter = str => str;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static int DecodeObjectCountMax = -1;
	}
}
