using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AssetManagerPackage;
using GAIPS.AssetEditorTools;

namespace CommeillFautWF
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
            var f = new MainForm();
            f.CreateNewAsset();
            Application.Run(f);
        }
    }
}
