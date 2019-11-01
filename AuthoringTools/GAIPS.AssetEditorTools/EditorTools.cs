using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools.TypedTextBoxes;
using GAIPS.Rage;
using System;
using System.Drawing;
using System.IO;
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

        public static void UpdateFormTitle(string asset, string path, Form form)
        {
            form.Text = (string.IsNullOrEmpty(path)) ?
              form.Text = asset :
              form.Text = asset + " - " + path;
        }

        public static string SaveFileDialog(string currentFilePath, AssetStorage storage, IAsset asset)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if (currentFilePath != null)
            {
                asset.Save();
                File.WriteAllText(currentFilePath, storage.ToJson());
            }
            else
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    asset.Save();
                    File.WriteAllText(sfd.FileName, storage.ToJson());
                    return sfd.FileName;
                }
            }
            return currentFilePath;
        }

        public static string OpenFileDialog(string filter)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return null;
            }
        }


        public static void AllowOnlyGroundedLiteral(WFNameFieldBox box)
        {
            box.AllowLiteral = true;
            box.AllowNil = false;
            box.AllowVariable = false;
            box.AllowUniversal = false;
            box.AllowComposedName = false;
        }

        public static void AllowOnlyGroundedLiteralOrNil(WFNameFieldBox box)
        {
            box.AllowLiteral = true;
            box.AllowNil = true;
            box.AllowVariable = false;
            box.AllowUniversal = false;
            box.AllowComposedName = false;
        }

        public static void AllowOnlyGroundedLiteralOrUniversal(WFNameFieldBox box)
        {
            box.AllowLiteral = true;
            box.AllowNil = false;
            box.AllowVariable = false;
            box.AllowUniversal = true;
            box.AllowComposedName = false;
        }

        public static void WriteText(RichTextBox box, string text, Color color, bool newLine)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;
            box.SelectionColor = color;
            text = text + (newLine ? "\n" : "");
            box.Focus();
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static void AllowOnlyVariable(WFNameFieldBox box)
        {
            box.AllowVariable = true;
            box.AllowLiteral = false;
            box.AllowNil = false;
            box.AllowUniversal = false;
            box.AllowComposedName = false;
        }

        public static void HighlightItemInGrid<T>(DataGridView grid, Guid selectId)
        {
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