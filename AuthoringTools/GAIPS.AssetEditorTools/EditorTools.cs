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

        public static T GetSelectedDtoFromTable<T>(DataGridView table)
        {
            if(table.SelectedRows.Count > 0)
            {
                var obj = ((ObjectView<T>)table.SelectedRows[0].DataBoundItem).Object;
                return obj;
            }
                
            return default(T);
        }

        public static void HideColumns(DataGridView table, string[] columnNames)
        {
            foreach (var s in columnNames)
            {
                table.Columns[s].Visible = false;
            }
        }

          public static void RefreshTable<T>(DataGridView grid, BindingListView<T> table, IEnumerable<T> list, Guid selectId)
        {
            var aux = list.ToList();
            table.DataSource = aux;

            grid.ClearSelection();

            if (selectId == Guid.Empty) return;

            for (int i = 0; i < grid.RowCount; i++)
            {
                var obj = ((ObjectView<T>)grid.Rows[i].DataBoundItem).Object;
                var id = (Guid)obj.GetType().GetProperty("Id").GetValue(obj);
                if (id == null) return;
                if (selectId == id)
                {
                    grid.Rows[i].Selected = true;
                    return;
                }
            }
        }

    }
}