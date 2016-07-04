using System;

namespace GAIPS.AssetEditorTools
{
	public static class EditorUtilities
	{
		public static void DisplayProgressBar(string taskTitle, Action<IProgressBarControler> task)
		{
			var f = new ProgressForm(taskTitle,task);
			f.ShowDialog();
		}
	}
}