using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private BindingList<EmotionListItem> _emotionList;
        
        public class EmotionListItem
        {
            public string Type { get; set; }
            public double Intensity { get; set; }
            public string Event { get; set; }

        }


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

            _emotionList = new BindingList<EmotionListItem>()
            {
                new EmotionListItem {Event = "Event(Player, Speak, Self, Self, Self, Jonas)", Intensity = 2.3, Type = "Reproach"},
                new EmotionListItem {Event = "Event(Player, Speak, Self)", Intensity = 2.5, Type = "Admiration"},
                new EmotionListItem {Event = "Event(Player, Speak, Self)", Intensity = 2, Type = "Fears-Confirmed"},
            };

            emotionListItemBindingSource.Add(new EmotionListItem()
            {
                Event = "1Event(Player, Speak, Self, Self, Self, Jonas)",
                Intensity = 2.3,
                Type = "Reproach"
            });


            emotionListItemBindingSource.Add(new EmotionListItem()
            {
                Event = "Event(Player, Speak, Self, Self, Self, Jonas)",
                Intensity = 2.3,
                Type = "Adeproach"
            });

          
            //this.refreshDataGridView(emotionsDataGridView, _emotionList);

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
                using (var file = File.Create(_saveFileName))
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
                _emotionalAppraisalAsset.RemoveBelief(eachItem.Text);
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

        private void beliefsListView_ItemActivate(object sender, EventArgs e)
        {
            this.editButton_Click(sender, e);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            var moodValue = (double)moodTrackBar.Value;

           moodValueLabel.Text = moodValue.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void moodGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void addEmotionButton_Click(object sender, EventArgs e)
        {
            this._emotionList.Add(new EmotionListItem {Type = "Distress", Intensity = 2, Event = "Event(Jonas, JOnas, Jonas)"});
            //refreshDataGridView(emotionsDataGridView, _emotionList);
        }

        private void emotionListItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
