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

    public enum GENDER
    {
        MALE=0,
        FEMALE=1
    }

    public partial class AuthTool : Form
    {
        private int _age;
        private GENDER _gender;

        public int Age
        {
            get { return _age; }

            set { _age = value; }
        }

        public GENDER Gender
        {
            get { return _gender; }

            set { _gender = value; }
        }

        public AuthTool()
        {
            InitializeComponent();
        }

        private void interestManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management(occupationComboBox);
            managementForm.ShowDialog();
        }

        private void classManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management(occupationComboBox);
            managementForm.ShowDialog();
        }

        private void occupationManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management(occupationComboBox);
            managementForm.ShowDialog();
        }

        private void raceManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management(occupationComboBox);
            managementForm.ShowDialog();
        }

        private void religionManagementButton_Click(object sender, EventArgs e)
        {
            var managementForm = new Management(occupationComboBox);
            managementForm.ShowDialog();
        }

        private void ageSelection_ValueChanged(object sender, EventArgs e)
        {
            _age = Convert.ToInt32(Math.Round(ageSelection.Value, 0));
        }

        private void genderSelection_Click(object sender, EventArgs e)
        {
            _gender = (GENDER)genderSelection.SelectedIndex;
        }
    }
}
