using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisalWF.Properties;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private string _saveFileName;


        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormPrincipalTitle;
                this._emotionalAppraisalAsset = new EmotionalAppraisalAsset("SELF");
            }
            else
            {
                this.Text = Resources.MainFormPrincipalTitle + Resources.TitleSeparator + _saveFileName;
            }

            beliefsListView.Items.Clear();

            if (!newFile)
            {
                foreach (var b in _emotionalAppraisalAsset.Kb.GetAllBeliefs())
                {
                    var listItem = new ListViewItem(new string[]{b.Name.ToString(), b.Value.ToString(), b.Visibility.ToString()});
                    beliefsListView.Items.Add(listItem);
                }
            }

         

        }

        public MainForm()
        {
            InitializeComponent();
            Reset(true);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reset(true);
        }

        private void saveHelper(bool newSaveFile)
        {
            if (newSaveFile)
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "JSON File|*.json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(sfd.FileName))
                    {
                        _saveFileName = sfd.FileName;
                    }
                }
                else
                {
                    return;
                }
            }
            try
            {
                using (var file = File.OpenWrite(_saveFileName))
                {
                    _emotionalAppraisalAsset.SaveToFile(file);
                }
                this.Text = Resources.MainFormPrincipalTitle + Resources.TitleSeparator + _saveFileName;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.UnableToSaveFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_saveFileName))
            {
                saveHelper(true);
            }
            else
            {
                saveHelper(false);
            }
        }

        private void saveAsStripMenuItem_Click(object sender, EventArgs e)
        {
            saveHelper(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _emotionalAppraisalAsset = EmotionalAppraisalAsset.LoadFromFile(ofd.FileName);
                    _saveFileName = ofd.FileName;
                    Reset(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Resources.InvalidFileError, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
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
            var addBeliefForm = new AddOrEditBeliefForm(beliefsListView, _emotionalAppraisalAsset);
            addBeliefForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (beliefsListView.SelectedItems.Count == 1)
            {
                var addBeliefForm = new AddOrEditBeliefForm(beliefsListView, _emotionalAppraisalAsset, true);
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

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
    }
}
