using System;
using System.Windows.Forms;
using GAIPS.Rage;

namespace RolePlayCharacterWF
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
			var f = new MainForm();
			Application.Run(f);
		}
	}
}