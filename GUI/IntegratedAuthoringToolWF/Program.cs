using System;
using System.Windows.Forms;
using AssetManagerPackage;
using GAIPS.AssetEditorTools;

namespace IntegratedAuthoringToolWF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			AssetManager.Instance.Bridge = new ApplicationBridge();

			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

	        var form = new MainForm();
			form.CreateNewAsset();
            form.InitializeDialogueForm();
			Application.Run(form);
        }
    }
}
