using System;
using System.Windows.Forms;
using GAIPS.AssetEditorTools;

namespace WorldModelWF
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
            var m = new MainForm();
            Application.Run(m);
        }
    }
}
