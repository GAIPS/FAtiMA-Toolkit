using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			var a = Assembly.GetExecutingAssembly();
			var fvi = FileVersionInfo.GetVersionInfo(a.Location);
			_versionLabel.Text = $"Version {fvi.FileVersion}";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
