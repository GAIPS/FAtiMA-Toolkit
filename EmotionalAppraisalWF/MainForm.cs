using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal;
using EmotionalAppraisalWF.Properties;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private string _saveFileName;

        private SortableBindingList<EmotionListItem> _emotionList;
        private SortableBindingList<AppraisalRuleListItem> _appraisalRuleList;

        public class EmotionListItem
        {
            public string Type { get; set; }
            public double Intensity { get; set; }
            public string Event { get; set; }

        }

        public class AppraisalRuleListItem
        {
            public string Name { get; set; }
            public int Desirability { get; set; }
            public int Praiseworthiness { get; set; }
        }

        private void ResetEmotionalTab()
        {
            this.moodValueLabel.Text = Math.Round(_emotionalAppraisalAsset.EmotionalState.Mood).ToString();
            this.moodTrackBar.Value = int.Parse(this.moodValueLabel.Text);
            this.emotionalHalfLifeTextBox.Text = ((int)_emotionalAppraisalAsset.EmotionalHalfLifeDecayTime).ToString();
            this.moodHalfLifeTextBox.Text = ((int)_emotionalAppraisalAsset.MoodHalfLifeDecayTime).ToString();

            _emotionList =
                new SortableBindingList<EmotionListItem>(
                    _emotionalAppraisalAsset.EmotionalState.GetEmotionsKeys().Select(key => new EmotionListItem
                    {
                        Event = key
                    }));

            foreach (var emotion in _emotionList)
            {
                emotion.Type = _emotionalAppraisalAsset.EmotionalState.GetEmotion(emotion.Event).EmotionType;
                emotion.Intensity = _emotionalAppraisalAsset.EmotionalState.GetEmotion(emotion.Event).Intensity;
            }

            emotionsDataGridView.DataSource = _emotionList;
            adjustColumnSizeGrid(emotionsDataGridView);
        }

        private void ResetEmotionDispositionsTab()
        {
            comboBoxDefaultDecay.SelectedIndex = comboBoxDefaultDecay.FindString(_emotionalAppraisalAsset.EmotionalState.DefaultEmotionDispositionDecay.ToString());
            comboBoxDefaultThreshold.SelectedIndex = comboBoxDefaultThreshold.FindString(_emotionalAppraisalAsset.EmotionalState.DefaultEmotionDispositionThreshold.ToString());
            dataGridViewEmotionDispositions.DataSource = new SortableBindingList<EmotionDisposition>(_emotionalAppraisalAsset.EmotionalState.GetEmotionDispositions());
        }

        private void ResetAppraisalRulesTab()
        {
            _appraisalRuleList =
                new SortableBindingList<AppraisalRuleListItem>(
                    _emotionalAppraisalAsset.GetAllAppraisalRules().Select(rule => new AppraisalRuleListItem
                    {
                        Name = rule.EventName.ToString()
                    }));
            
            dataGridViewAppraisalRules.DataSource = _appraisalRuleList;
            adjustColumnSizeGrid(dataGridViewAppraisalRules);
        }

        private void ResetAutoBiographicalMemoryTab()
        {
            dataGridViewAM.DataSource = new SortableBindingList<IEvent>(_emotionalAppraisalAsset.GetAllEventRecords());
        }


        private void adjustColumnSizeGrid(DataGridView grid)
        {
            if (grid.ColumnCount > 1)
            {
                for (int i = 0; i < grid.ColumnCount - 1; i++)
                {
                    grid.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            grid.Columns[grid.ColumnCount-1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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

            ResetEmotionalTab();
            ResetEmotionDispositionsTab();
            ResetAppraisalRulesTab();
            ResetAutoBiographicalMemoryTab();

            //Belief Tab
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
            
            //Tooltips:
            toolTip.SetToolTip(emotionalHalfLifeLabel, Resources.TooltipEmotionalHalflife);
            
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
            moodValueLabel.Text = moodTrackBar.Value.ToString();
            _emotionalAppraisalAsset.SetMood(moodTrackBar.Value);
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

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolTip1_Popup_1(object sender, PopupEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void emotionalHalfLifeTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void emotionalHalfLifeTextBox_Validating(object sender, CancelEventArgs e)
        {
            this.validateDecayHelper(this.emotionalHalfLifeTextBox, e);
        }

        private void emotionalHalfLifeTextBox_Validated(object sender, EventArgs e)
        {
            decayErrorProvider.SetError(this.emotionalHalfLifeTextBox, string.Empty);
            _emotionalAppraisalAsset.EmotionalHalfLifeDecayTime = float.Parse(this.emotionalHalfLifeTextBox.Text);
        }

        private void validateDecayHelper(TextBox textBoxDecay, CancelEventArgs e)
        {
            try
            {
                var newDecay = int.Parse(textBoxDecay.Text);
                if (newDecay < Constants.MinimumDecayTime)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                decayErrorProvider.SetError(textBoxDecay, Resources.ErrorHalfLifeDecay);
                e.Cancel = true;
            }
        }

        private void moodHalfLifeTextBox_Validating(object sender, CancelEventArgs e)
        {
            this.validateDecayHelper(this.moodHalfLifeTextBox, e);
        }

        private void moodHalfLifeTextBox_Validated(object sender, EventArgs e)
        {
            decayErrorProvider.SetError(this.moodHalfLifeTextBox, string.Empty);
            _emotionalAppraisalAsset.MoodHalfLifeDecayTime = float.Parse(this.moodHalfLifeTextBox.Text);
        }

        private void comboBoxDefaultDecay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionalAppraisalAsset.EmotionalState.DefaultEmotionDispositionDecay = int.Parse(comboBoxDefaultDecay.Text);
        }

        private void comboBoxDefaultThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionalAppraisalAsset.EmotionalState.DefaultEmotionDispositionThreshold = int.Parse(comboBoxDefaultThreshold.Text);
        }
    }
}
