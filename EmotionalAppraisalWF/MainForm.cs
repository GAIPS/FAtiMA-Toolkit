using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AutobiographicMemory.Interfaces;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;
        private string _saveFileName;

        private EmotionalStateVM _emotionalStateVM;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private AppraisalRulesVM _appraisalRulesVM;
        private EmotionDispositionsVM _emotionDispositionsVM;

        public MainForm()
        {
            InitializeComponent();
            Reset(true);
        }

        private void Reset(bool newFile)
        {
            if (newFile)
            {
                this.Text = Resources.MainFormPrincipalTitle;
                this._emotionalAppraisalAsset = new EmotionalAppraisalAsset("Agent");
            }
            else
            {
                this.Text = Resources.MainFormPrincipalTitle + Resources.TitleSeparator + _saveFileName;
            }

            //Emotional State Tab
            _emotionalStateVM = new EmotionalStateVM(_emotionalAppraisalAsset);
            this.moodValueLabel.Text = Math.Round(_emotionalStateVM.Mood).ToString();
            this.moodTrackBar.Value = int.Parse(this.moodValueLabel.Text);
            this.textBoxStartTick.Text = _emotionalStateVM.Start.ToString();
            this.emotionsDataGridView.DataSource = _emotionalStateVM.Emotions;
            
            
            //Emotion Dispositions
            _emotionDispositionsVM = new EmotionDispositionsVM(_emotionalAppraisalAsset);
            comboBoxDefaultDecay.SelectedIndex = comboBoxDefaultDecay.FindString(_emotionDispositionsVM.DefaultDecay.ToString());
            comboBoxDefaultThreshold.SelectedIndex = comboBoxDefaultThreshold.FindString(_emotionDispositionsVM.DefaultThreshold.ToString());
            dataGridViewEmotionDispositions.DataSource = _emotionDispositionsVM.EmotionDispositions;
            
            //Appraisal Rule
            _appraisalRulesVM = new AppraisalRulesVM(_emotionalAppraisalAsset);
            dataGridViewAppraisalRules.DataSource = _appraisalRulesVM.AppraisalRules;
            dataGridViewAppraisalRules.Columns[PropertyUtil.GetName<BaseDTO>(dto => dto.Id)].Visible = false;
            dataGridViewAppraisalRules.Columns[PropertyUtil.GetName<AppraisalRuleDTO>(dto => dto.Conditions)].Visible = false;
            dataGridViewAppRuleConditions.DataSource = _appraisalRulesVM.CurrentRuleConditions;
            dataGridViewAppRuleConditions.Columns[PropertyUtil.GetName<BaseDTO>(dto => dto.Id)].Visible = false;

            //KB
            _knowledgeBaseVM = new KnowledgeBaseVM(_emotionalAppraisalAsset);
            dataGridViewBeliefs.DataSource = _knowledgeBaseVM.Beliefs;
            dataGridViewBeliefs.Columns[PropertyUtil.GetName<BaseDTO>(dto => dto.Id)].Visible = false;

            //AM
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

            grid.Columns[grid.ColumnCount - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                    MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            _emotionalStateVM.Mood = moodTrackBar.Value;
        }
        
        #region EmotionalStateTab

        
        private void validateDecayHelper(TextBox textBoxDecay, CancelEventArgs e)
        {
            try
            {
                var newDecay = int.Parse(textBoxDecay.Text);
                if (newDecay < 1)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                decayErrorProvider.SetError(textBoxDecay, Resources.ErrorHalfLifeDecay);
                e.Cancel = true;
            }
        }

        private void comboBoxDefaultDecay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionDispositionsVM.DefaultDecay = int.Parse(comboBoxDefaultDecay.Text);
        }

        private void comboBoxDefaultThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionDispositionsVM.DefaultThreshold = int.Parse(comboBoxDefaultThreshold.Text);
        }

        #endregion

        #region KnowledgeBaseTab

        private void addBeliefButton_Click(object sender, EventArgs e)
        {
            var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM);
            addBeliefForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewBeliefs.SelectedRows.Count == 1)
            {
                var selectedBelief = ((ObjectView<BeliefDTO>)dataGridViewBeliefs.SelectedRows[0].DataBoundItem).Object;
                var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM, selectedBelief);
                addBeliefForm.ShowDialog();
            }
        }

        private void removeBeliefButton_Click(object sender, EventArgs e)
        {
            IList<BeliefDTO> beliefsToRemove = new List<BeliefDTO>();
            for (int i = 0; i < dataGridViewBeliefs.SelectedRows.Count; i++)
            {
                var belief = ((ObjectView<BeliefDTO>)dataGridViewBeliefs.SelectedRows[i].DataBoundItem).Object;
                beliefsToRemove.Add(belief);
            }
            _knowledgeBaseVM.RemoveBeliefs(beliefsToRemove);
        }

        private void dataGridViewBeliefs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.editButton_Click(sender, e);
            }
        }

        #endregion

        private void moodValueLabel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewAppRuleConditions_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridViewAppraisalRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewAppraisalRules_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var rule = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.Rows[e.RowIndex].DataBoundItem).Object;
            _appraisalRulesVM.ChangeCurrentRule(rule);
            dataGridViewAppRuleConditions.DataSource = _appraisalRulesVM.CurrentRuleConditions;
        }

        private void buttonAddAppraisalRuleCondition_Click(object sender, EventArgs e)
        {

        }

        private void buttonAddAppraisalRule_Click(object sender, EventArgs e)
        {
            new AddOrEditAppraisalRuleForm(_appraisalRulesVM).ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPerspective_TextChanged(object sender, EventArgs e)
        {
            this._emotionalStateVM.Perspective = textBoxPerspective.Text;
        }

        private void textBoxStartTick_TextChanged(object sender, EventArgs e)
        {
            ulong time;
            if (ulong.TryParse(textBoxStartTick.Text, out time))
            {
                _emotionalStateVM.Start = time;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBoxUpdate.Checked)
            {
                
            }
        }
    }
}
