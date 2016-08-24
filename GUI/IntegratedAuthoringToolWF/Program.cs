using System;
using System.Windows.Forms;
using AssetManagerPackage;
using GAIPS.Rage;

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
			AssetManager.Instance.Bridge = new BasicIOBridge();
			Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
