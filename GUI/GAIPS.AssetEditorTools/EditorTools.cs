using System;
using System.Linq;
using System.Reflection;

namespace GAIPS.AssetEditorTools
{
	public static class EditorTools
	{
		public static T GetFieldValue<T>(object obj, string fieldName)
		{
			var type = obj.GetType();

			var fieldInfo = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).Where(f => typeof (T).IsAssignableFrom(f.FieldType))
				.FirstOrDefault(f => f.Name == fieldName);

			if(fieldInfo == null)
				throw new Exception($"Unable to find field \"{fieldName}\" of type {typeof(T).FullName}.");

			var r = fieldInfo.GetValue(obj);;
			return (T) r;
		}
	}
}