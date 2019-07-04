using System;
using System.Drawing;
using System.Windows.Forms;

namespace GAIPS.AssetEditorTools
{
	public static class EditorUtilities
	{
		public static void DisplayProgressBar(string taskTitle, Action<IProgressBarControler> task)
		{
			var f = new ProgressForm(taskTitle,task);
			f.ShowDialog();
		}

        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}