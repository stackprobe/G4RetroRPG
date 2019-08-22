using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	/// <summary>
	/// e.g. Common.CUIMain(ar => new Hub().Perform(ar), APP_IDENT, APP_TITLE);
	/// </summary>
	public class Hub
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private ObjectMap Vars = ObjectMap.CreateIgnoreCase();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Perform(ArgsReader ar)
		{
			this.Vars.Add("Hub", this);

			do
			{
				string option1 = ar.NextArg();

				if (ar.ArgIs("="))
				{
					string destVarName = option1;

					if (ar.ArgIs("new")) // <変数> = new <タイプ> <引数>... ;
					{
						string typeName = ar.NextArg();
						object[] prms = this.ReadParams(ar);

						this.Vars[destVarName] = ReflectTools.GetConstructor(GetType(typeName), prms).Construct(prms);
					}
					else
					{
						string option2 = ar.NextArg();

						if (ar.ArgIs(".")) // <変数> = <変数> . <メソッド> <引数>... ;
						{
							string varName = option2;
							string methodName = ar.NextArg();
							object[] prms = this.ReadParams(ar);

							object instance = this.Vars[varName];

							this.Vars[destVarName] = ReflectTools.GetMethod(instance.GetType(), methodName, prms).Invoke(instance, prms);
						}
						else // <変数> = <タイプ> <staticメソッド> <引数>... ;
						{
							string typeName = option2;
							string methodName = ar.NextArg();
							object[] prms = this.ReadParams(ar);

							this.Vars[destVarName] = ReflectTools.GetMethod(GetType(typeName), methodName, prms).Invoke(prms);
						}
					}
				}
				else if (ar.ArgIs(".")) // <変数> . <メソッド> <引数>... ;
				{
					string varName = option1;
					string methodName = ar.NextArg();
					object[] prms = this.ReadParams(ar);

					object instance = this.Vars[varName];

					ReflectTools.GetMethod(instance.GetType(), method =>
						method.Value.Name == methodName &&
						method.Value.IsStatic == false &&
						ReflectTools.CheckParameters(prms, method.GetParameterTypes())
						)
						.Invoke(instance, prms);
				}
				else // <タイプ> <staticメソッド> <引数>... ;
				{
					string typeName = option1;
					string methodName = ar.NextArg();
					object[] prms = this.ReadParams(ar);

					ReflectTools.GetMethod(GetType(typeName), method =>
						method.Value.Name == methodName &&
						method.Value.IsStatic &&
						ReflectTools.CheckParameters(prms, method.GetParameterTypes())
						)
						.Invoke(prms);
				}
			}
			while (ar.HasArgs());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static Type GetType(string typeName)
		{
			foreach (string prefix in new string[] { "", "Charlotte." }) // zantei
			{
				foreach (string suffix in new string[] { "," + Assembly.GetEntryAssembly().GetName().Name, "" }) // zantei
				{
					Type type = Type.GetType(prefix + typeName + suffix);

					if (type != null)
						return type;
				}
			}
			throw new Exception("指定されたタイプは見つかりません。" + typeName);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private object[] ReadParams(ArgsReader ar)
		{
			List<object> prms = new List<object>();

			while (ar.HasArgs() && ar.ArgIs(";") == false)
			{
				string prm = ar.NextArg();

				if (prm.StartsWith("**"))
					prms.Add(prm.Substring(2));
				else if (prm.StartsWith("*"))
					prms.Add(this.Vars[prm.Substring(1)]);
				else
					prms.Add(prm);
			}
			return prms.ToArray();
		}

		// コマンドラインから使うメソッド ...

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Set(string varName, string fieldName, object value) // Hub . Set <変数> <フィールド名> <フィールドに代入する値> ;
		{
			object instance = this.Vars[varName];

			ReflectTools.GetFieldByInstance(instance, fieldName).SetValue(instance, value);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void SetInt(string varName, string fieldName, string value) // Hub . SetInt <変数> <フィールド名> <フィールドに代入する整数> ;
		{
			this.Set(varName, fieldName, int.Parse(value));
		}
	}
}
