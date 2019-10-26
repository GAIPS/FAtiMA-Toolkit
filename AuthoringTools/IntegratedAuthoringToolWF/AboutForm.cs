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
            var a = Assembly.GetEntryAssembly();
            
            _versionLabel.Text = $"Version {a.GetName().Version}";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void _versionLabel_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var a = Assembly.GetExecutingAssembly();
            string output = string.Empty;
            foreach (AssemblyName an in a.GetReferencedAssemblies())
            {
                output += string.Format("Name={0}, Version={1} \n", an.Name, an.Version);
            }
            MessageBox.Show(output);
            

        }
    }
}
