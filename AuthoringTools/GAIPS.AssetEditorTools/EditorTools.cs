using Equin.ApplicationFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

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

        public static T GetSelectedDtoFromTable<T>(DataGridView table )
        {
            if(table.SelectedRows.Count > 0)
                return ((ObjectView<T>)table.SelectedRows[0].DataBoundItem).Object;
            return default(T);
        }

        public static void HideColumns(DataGridView table, string[] columnNames)
        {
            foreach (var s in columnNames)
            {
                table.Columns[s].Visible = false;
            }
        }

        public static void RefreshTable<T>(DataGridView table, List<T> list)
        {
            table.DataSource = list;
            table.Refresh();
        }
	}
}