using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisalWF;



namespace RolePlayCharacterWF
{
    public partial class RolePlayCharacter : Form
    {
        public RolePlayCharacter()
        {
            InitializeComponent();
        }

        private void addNewEA_Click(object sender, EventArgs e)
        {
            var emotionalAppraisalWF = new EmotionalAppraisalWF.MainForm();
            emotionalAppraisalWF.ShowDialog();
        }

        private void editEA_Click(object sender, EventArgs e)
        {
            var emotionalAppraisalWF = new EmotionalAppraisalWF.MainForm();
            emotionalAppraisalWF.ShowDialog();
        }
    }
}
