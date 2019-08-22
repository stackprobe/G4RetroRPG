using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class XmlNode
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Name;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public string Value;
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public List<XmlNode> Children;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode(string name = "element", string value = "")
			: this(name, value, new List<XmlNode>())
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode(string name, string value, List<XmlNode> children)
		{
			this.Name = name;
			this.Value = value;
			this.Children = children;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static XmlNode LoadFile(string xmlFile, string attributeNamePrefix = "", string attributeNameSuffix = "")
		{
			XmlNode node = new XmlNode();
			Stack<XmlNode> parents = new Stack<XmlNode>();

			using (XmlReader reader = XmlReader.Create(xmlFile))
			{
				while (reader.Read())
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								XmlNode child = new XmlNode(reader.LocalName);

								node.Children.Add(child);
								parents.Push(node);
								node = child;

								bool singleTag = reader.IsEmptyElement;

								while (reader.MoveToNextAttribute())
									node.Children.Add(new XmlNode(attributeNamePrefix + reader.Name + attributeNameSuffix, reader.Value));

								if (singleTag)
									node = parents.Pop();
							}
							break;

						case XmlNodeType.Text:
							node.Value = reader.Value;
							break;

						case XmlNodeType.EndElement:
							node = parents.Pop();
							break;

						default:
							break;
					}
				}
			}
			node = node.Children[0];
			PostLoad(node);
			return node;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void PostLoad(XmlNode node)
		{
			node.Name = "" + node.Name;
			node.Value = "" + node.Value;

			{
				int index = node.Name.IndexOf(':');

				if (index != -1)
					node.Name = node.Name.Substring(index + 1);
			}

			node.Name = node.Name.Trim();
			node.Value = node.Value.Trim();

			foreach (XmlNode child in node.Children)
				PostLoad(child);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void WriteToFile(string xmlFile)
		{
			File.WriteAllLines(xmlFile, this.GetLines(), Encoding.UTF8);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private const string INDENT = "\t";

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private string[] GetLines()
		{
			List<string> dest = new List<string>();
			dest.Add("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			this.AddTo(dest, "");
			return dest.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private void AddTo(List<string> dest, string indent)
		{
			if (Children.Count != 0)
			{
				dest.Add(indent + "<" + this.Name + ">" + this.Value);

				foreach (XmlNode child in Children)
					child.AddTo(dest, indent + INDENT);

				dest.Add(indent + "</" + this.Name + ">");
			}
			else if (this.Value != "")
			{
				dest.Add(indent + "<" + this.Name + ">" + this.Value + "</" + this.Name + ">");
			}
			else
			{
				dest.Add(indent + "<" + this.Name + "/>");
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode Ref(string path)
		{
			XmlNode[] dest = this.Collect(path);

			if (dest.Length == 0)
				return this.Add(path);

			return dest[0];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode Get(string path)
		{
			return this.Collect(path)[0];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode[] Collect(string path)
		{
			List<XmlNode> dest = new List<XmlNode>();
			this.Collect(path.Split('/'), 0, dest);
			return dest.ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private void Collect(string[] pTkns, int pTknIndex, List<XmlNode> dest)
		{
			if (pTknIndex < pTkns.Length)
			{
				foreach (XmlNode child in this.Children)
					if (child.Name == pTkns[pTknIndex])
						child.Collect(pTkns, pTknIndex + 1, dest);
			}
			else
			{
				dest.Add(this);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Delete(string path)
		{
			this.Delete(path.Split('/'), 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private void Delete(string[] pTkns, int pTknIndex)
		{
			if (pTknIndex + 1 < pTkns.Length)
			{
				foreach (XmlNode child in this.Children)
					if (child.Name == pTkns[pTknIndex])
						child.Delete(pTkns, pTknIndex + 1);
			}
			else
			{
				for (int index = this.Children.Count - 1; 0 <= index; index++)
					if (this.Children[index].Name == pTkns[pTknIndex])
						this.Children.RemoveAt(index);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public XmlNode Add(string path)
		{
			return this.Add(path.Split('/'), 0);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private XmlNode Add(string[] pTkns, int pTknIndex)
		{
			if (pTknIndex < pTkns.Length)
			{
				XmlNode child = Find(this.Children, pTkns[pTknIndex]);

				if (child == null)
				{
					child = new XmlNode(pTkns[pTknIndex]);
					this.Children.Add(child);
				}
				return child.Add(pTkns, pTknIndex + 1);
			}
			else
			{
				return this;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static XmlNode Find(List<XmlNode> children, string name)
		{
			foreach (XmlNode child in children)
				if (child.Name == name)
					return child;

			return null;
		}
	}
}
