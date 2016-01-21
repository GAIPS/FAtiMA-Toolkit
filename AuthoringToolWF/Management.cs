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
    public partial class Management : Form
    {

        private ComboBox _occupationComboBox;

        public ComboBox OccupationComboBox
        {
            get { return _occupationComboBox; }

            set { _occupationComboBox = value; }
        }

        public Management(ComboBox occupationComboBox)
        {
            _occupationComboBox = occupationComboBox;
            InitializeComponent();
        }

        private void addOccupationButton_Click(object sender, EventArgs e)
        {
            
        }

        private void editOccupationButton_Click(object sender, EventArgs e)
        {
            
        }

        private void removeOccupationButton_Click(object sender, EventArgs e)
        {

        }
    }
}
