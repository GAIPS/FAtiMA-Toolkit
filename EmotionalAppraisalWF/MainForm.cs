using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisal;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private EmotionalAppraisalAsset m_emotionalAppraisalAsset;
        
        public MainForm()
        {
            InitializeComponent();
            this.m_emotionalAppraisalAsset = new EmotionalAppraisalAsset("SELF");

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var emotionaAppraisalFile = ofd.FileName;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void addBeliefButton_Click(object sender, EventArgs e)
        {
            var addBeliefForm = new AddOrEditBeliefForm(beliefsListView);
            addBeliefForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (beliefsListView.SelectedItems.Count == 1)
            {
                var addBeliefForm = new AddOrEditBeliefForm(beliefsListView, true);
                addBeliefForm.ShowDialog();
            }
        }

        private void removeBeliefButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in beliefsListView.SelectedItems)
            {
                beliefsListView.Items.Remove(eachItem);
            }
        }

        private void mainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void beliefsListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
