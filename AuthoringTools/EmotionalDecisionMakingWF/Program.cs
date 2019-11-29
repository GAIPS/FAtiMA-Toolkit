using System;
using System.Windows.Forms;

namespace EmotionalDecisionMakingWF
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