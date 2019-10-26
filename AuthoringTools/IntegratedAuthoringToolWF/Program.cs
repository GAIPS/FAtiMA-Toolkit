using System;
using System.Globalization;
using System.Windows.Forms;
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
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en"); 
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo("en");
            var form = new MainForm();
            Application.Run(form);
        }
    }
}
