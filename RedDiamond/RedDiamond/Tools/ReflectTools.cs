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
	public static class ReflectTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class FieldUnit
		{
			public FieldInfo Value;

			public FieldUnit(FieldInfo value)
			{
				this.Value = value;
			}

			/// <summary>
			/// static フィールド 取得
			/// </summary>
			/// <returns></returns>
			public object GetValue()
			{
				return this.Value.GetValue(null);
			}

			/// <summary>
			/// インスタンス フィールド 取得
			/// </summary>
			/// <param name="instance"></param>
			/// <returns></returns>
			public object GetValue(object instance)
			{
				return this.Value.GetValue(instance);
			}

			/// <summary>
			/// static フィールド 設定
			/// </summary>
			/// <param name="value"></param>
			public void SetValue(object value)
			{
				this.Value.SetValue(null, value);
			}

			/// <summary>
			/// インスタンス フィールド 設定
			/// </summary>
			/// <param name="instance"></param>
			/// <param name="value"></param>
			public void SetValue(object instance, object value)
			{
				this.Value.SetValue(instance, value);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class PropertyUnit
		{
			public PropertyInfo Value;

			public PropertyUnit(PropertyInfo value)
			{
				this.Value = value;
			}

			/// <summary>
			/// static プロパティ 取得
			/// </summary>
			/// <returns></returns>
			public object GetValue()
			{
				try
				{
					return this.Value.GetValue(null, null);
				}
				catch
				{
					return null; // getter無し || 配列 || etc.
				}
			}

			/// <summary>
			/// インスタンス プロパティ 取得
			/// </summary>
			/// <param name="instance"></param>
			/// <returns></returns>
			public object GetValue(object instance)
			{
				try
				{
					return this.Value.GetValue(instance, null);
				}
				catch
				{
					return null; // getter無し || 配列 || etc.
				}
			}

			/// <summary>
			/// static プロパティ 設定
			/// </summary>
			/// <param name="value"></param>
			public void SetValue(object value)
			{
				try
				{
					this.Value.SetValue(null, value, null);
				}
				catch
				{
					// setter無し || 配列 || etc.
				}
			}

			/// <summary>
			/// インスタンス プロパティ 設定
			/// </summary>
			/// <param name="value"></param>
			public void SetValue(object instance, object value)
			{
				try
				{
					this.Value.SetValue(instance, value, null);
				}
				catch
				{
					// setter無し || 配列 || etc.
				}
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class MethodUnit
		{
			public MethodBase Value; // MethodInfo | ConstructorInfo

			public MethodUnit(MethodBase value)
			{
				this.Value = value;
			}

			public ParameterType[] GetParameterTypes()
			{
				return this.Value.GetParameters().Select<ParameterInfo, ParameterType>(prmType => new ParameterType(prmType)).ToArray();
			}

			/// <summary>
			/// invoke static method
			/// </summary>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Invoke(object[] prms)
			{
				return this.Value.Invoke(null, prms);
			}

			/// <summary>
			/// invoke instance method
			/// </summary>
			/// <param name="instance"></param>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Invoke(object instance, object[] prms)
			{
				return this.Value.Invoke(instance, prms);
			}

			/// <summary>
			/// invoke constructor
			/// </summary>
			/// <param name="prms"></param>
			/// <returns></returns>
			public object Construct(object[] prms)
			{
				return ((ConstructorInfo)this.Value).Invoke(prms);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public class ParameterType
		{
			public ParameterInfo Value;

			public ParameterType(ParameterInfo value)
			{
				this.Value = value;
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static FieldUnit[] GetFieldsByInstance(object instance)
		{
			return GetFields(instance.GetType());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static PropertyUnit[] GetPropertiesByInstance(object instance)
		{
			return GetProperties(instance.GetType());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static FieldUnit GetFieldByInstance(object instance, string name)
		{
			return GetField(instance.GetType(), name);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static PropertyUnit GetPropertyByInstance(object instance, string name)
		{
			return GetProperty(instance.GetType(), name);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		/// <summary>
		/// 親クラスの private, private static を取得出来ない模様。現状名前が被った場合を考慮していないので、このままにする。
		/// </summary>
		private const BindingFlags _bindingFlags =
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Static |
			BindingFlags.Instance |
			BindingFlags.FlattenHierarchy;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static FieldUnit[] GetFields(Type type)
		{
			return type.GetFields(_bindingFlags).Select<FieldInfo, FieldUnit>(field => new FieldUnit(field)).ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static PropertyUnit[] GetProperties(Type type)
		{
			return type.GetProperties(_bindingFlags).Select<PropertyInfo, PropertyUnit>(prop => new PropertyUnit(prop)).ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static FieldUnit GetField(Type type, string name) // ret: null == not found
		{
			FieldInfo field = type.GetField(name, _bindingFlags);

			if (field == null)
				return null;

			return new FieldUnit(field);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static PropertyUnit GetProperty(Type type, string name)
		{
			PropertyInfo prop = type.GetProperty(name, _bindingFlags);

			if (prop == null)
				return null;

			return new PropertyUnit(prop);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Equals(FieldUnit a, Type b)
		{
			return Equals(a.Value.FieldType, b);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool Equals(Type a, Type b)
		{
			return a.ToString() == b.ToString();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool EqualsOrBase(FieldUnit a, Type b)
		{
			return EqualsOrBase(a.Value.FieldType, b);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool EqualsOrBase(Type a, Type b) // ret: ? a == b || a は b を継承している。
		{
			foreach (Type ai in a.GetInterfaces())
				if (Equals(ai, b))
					return true;

			do
			{
				if (Equals(a, b))
					return true;

				a = a.BaseType;
			}
			while (a != null);

			return false;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool IsList(Type type)
		{
			try
			{
				return typeof(List<>).IsAssignableFrom(type.GetGenericTypeDefinition());
			}
			catch
			{
				return false; // ジェネリック型じゃない || etc.
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit[] GetMethodsByInstance(object instance)
		{
			return GetMethods(instance.GetType());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit[] GetMethods(Type type)
		{
			return type.GetMethods(_bindingFlags).Select<MethodInfo, MethodUnit>(method => new MethodUnit(method)).ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetMethod(Type type, Predicate<MethodUnit> match)
		{
			MethodUnit[] ret = GetMethods(type).Where(method => match(method)).ToArray();

			if (ret.Length == 0)
				throw new Exception("メソッドが見つかりません。");

			return ret[0];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetMethod(Type type, string name)
		{
			return GetMethod(type, method => name == method.Value.Name);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetMethod(Type type, string name, object[] prms)
		{
			return GetMethod(type, method => name == method.Value.Name && CheckParameters(prms, method.GetParameterTypes()));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit[] GetConstructorsByInstance(object instance)
		{
			return GetConstructors(instance.GetType());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit[] GetConstructors(Type type)
		{
			return type.GetConstructors(_bindingFlags).Select<ConstructorInfo, MethodUnit>(ctor => new MethodUnit(ctor)).ToArray();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetConstructor(Type type, Predicate<MethodUnit> match)
		{
			MethodUnit[] ret = GetConstructors(type).Where(method => match(method)).ToArray();

			if (ret.Length == 0)
				throw new Exception("コンストラクタが見つかりません。");

			return ret[0];
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetConstructor(Type type)
		{
			return GetConstructor(type, ctor => true);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static MethodUnit GetConstructor(Type type, object[] prms)
		{
			return GetConstructor(type, ctor => CheckParameters(prms, ctor.GetParameterTypes()));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool CheckParameters(object[] prms, ParameterType[] prmTypes)
		{
			return prms.Length == prmTypes.Length; // XXX
		}
	}
}
