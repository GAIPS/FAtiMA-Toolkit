using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthoringToolWF
{
    public partial class AuthTool : Form
    {
        public AuthTool()
        {
            InitializeComponent();
        }

        private void interestManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management();
            managementForm.ShowDialog();
        }

        private void classManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management();
            managementForm.ShowDialog();
        }

        private void occupationManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management();
            managementForm.ShowDialog();
        }

        private void raceManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management();
            managementForm.ShowDialog();
        }

        private void religionManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management();
            managementForm.ShowDialog();
        }
    }
}
