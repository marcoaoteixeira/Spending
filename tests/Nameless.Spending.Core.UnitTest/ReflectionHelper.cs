using System;
using System.Reflection;

namespace Nameless.Spending.Core.UnitTest {
	public static class ReflectionHelper {
		public static object GetPrivateFieldValue(object instance, string name) {
			var field = GetPrivateField(instance.GetType(), name);

			return field.GetValue(instance);
		}

		public static void SetPrivateFieldValue(object instance, string name, object value) {
			var field = GetPrivateField(instance.GetType(), name);

			field.SetValue(instance, value);
		}

		private static FieldInfo GetPrivateField(Type type, string name) {
			var result = type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);

			if (result == null && type.BaseType != null) {
				return GetPrivateField(type.BaseType, name);
			}

			return result;
		}
	}
}