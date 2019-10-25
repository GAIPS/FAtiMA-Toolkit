using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.ViewModels;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private AssetStorage _storage;
        private EmotionalAppraisalAsset _loadedAsset;
        private AppraisalRulesVM _appraisalRulesVM;
        private EmotionDispositionsVM _emotionDispositionsVM;
        private string _currentFilePath;

        public MainForm()
        {
            InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
            OnAssetDataLoaded(_loadedAsset);
        }

        private void OnAssetDataLoaded(EmotionalAppraisalAsset asset)
        {
            //Emotion Dispositions
            _emotionDispositionsVM = new EmotionDispositionsVM(asset);
            comboBoxDefaultDecay.SelectedIndex = comboBoxDefaultDecay.FindString(_emotionDispositionsVM.DefaultDecay.ToString());
            comboBoxDefaultThreshold.SelectedIndex = comboBoxDefaultThreshold.FindString(_emotionDispositionsVM.DefaultThreshold.ToString());
            dataGridViewEmotionDispositions.DataSource = _emotionDispositionsVM.EmotionDispositions;

            //Appraisal Rule
            _appraisalRulesVM = new AppraisalRulesVM(asset);
            dataGridViewAppraisalRules.DataSource = _appraisalRulesVM.AppraisalRules;
            EditorTools.HideColumns(dataGridViewAppraisalRules, new[]
            {
                PropertyUtil.GetPropertyName<AppraisalRuleDTO>(dto => dto.Id)           
            });
               
            conditionSetEditor.View = _appraisalRulesVM.CurrentRuleConditions;
            dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(asset.GetAllGoals().ToList());
        }

        private void buttonAddGoal_Click(object sender, EventArgs e)
        {
            new AddOrEditGoalForm(_loadedAsset, null).ShowDialog(this);
            dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(_loadedAsset.GetAllGoals().ToList());
        }
            

        private void buttonAddAppraisalRule_Click(object sender, EventArgs e)
        {
            new AddOrEditAppraisalRuleForm(_appraisalRulesVM).ShowDialog();
        }

        private void buttonAddEmotionDisposition_Click(object sender, EventArgs e)
        {
            new AddOrEditEmotionDispositionForm(_emotionDispositionsVM).ShowDialog();
        }

        private void buttonAppVariables_Click(object sender, EventArgs e)
        {
              var selectedAppraisalRule = EditorTools.GetSelectedDtoFromTable<AppraisalRuleDTO>(dataGridViewAppraisalRules);

            new AddOrEditAppraisalVariablesForm(_appraisalRulesVM, selectedAppraisalRule ).ShowDialog(this);
        }

        private void buttonDuplicateAppraisalRule_Click(object sender, EventArgs e)
        {
            var selectedAppraisalRule = EditorTools.GetSelectedDtoFromTable<AppraisalRuleDTO>(dataGridViewAppraisalRules);

            if (selectedAppraisalRule != null)
            {
                var duplicateRule = CloneHelper.Clone(selectedAppraisalRule);
                duplicateRule.Id = Guid.Empty;
                _appraisalRulesVM.AddOrUpdateAppraisalRule(duplicateRule);
            }
        }

        private void buttonEditAppraisalRule_Click(object sender, EventArgs e)
        {
            var selectedAppraisalRule = EditorTools.GetSelectedDtoFromTable<AppraisalRuleDTO>(dataGridViewAppraisalRules);
            if (selectedAppraisalRule != null)
                new AddOrEditAppraisalRuleForm(_appraisalRulesVM, selectedAppraisalRule).ShowDialog();
        }

        private void buttonEditEmotionDisposition_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmotionDispositions.SelectedRows.Count == 1)
            {
                var selectedEmotionDisposition = ((ObjectView<EmotionDispositionDTO>)dataGridViewEmotionDispositions.
                    SelectedRows[0].DataBoundItem).Object;
                new AddOrEditEmotionDispositionForm(_emotionDispositionsVM, selectedEmotionDisposition).ShowDialog();
            }
        }

        private void buttonRemoveAppraisalRule_Click(object sender, EventArgs e)
        {
            IList<AppraisalRuleDTO> rulesToRemove = new List<AppraisalRuleDTO>();
            for (int i = 0; i < dataGridViewAppraisalRules.SelectedRows.Count; i++)
            {
                var rule = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.SelectedRows[i].DataBoundItem).Object;
                rulesToRemove.Add(rule);
            }
            _appraisalRulesVM.RemoveAppraisalRules(rulesToRemove);
        }

        private void buttonRemoveEmotionDisposition_Click(object sender, EventArgs e)
        {
            IList<EmotionDispositionDTO> dispositionsToRemove = new List<EmotionDispositionDTO>();
            for (int i = 0; i < dataGridViewEmotionDispositions.SelectedRows.Count; i++)
            {
                var disposition = ((ObjectView<EmotionDispositionDTO>)dataGridViewEmotionDispositions.SelectedRows[i].DataBoundItem).Object;
                dispositionsToRemove.Add(disposition);
            }
            _emotionDispositionsVM.RemoveDispositions(dispositionsToRemove);
        }

        private void comboBoxDefaultDecay_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionDispositionsVM.DefaultDecay = int.Parse(comboBoxDefaultDecay.Text);
        }

        private void comboBoxDefaultThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionDispositionsVM.DefaultThreshold = int.Parse(comboBoxDefaultThreshold.Text);
        }

        private void conditionSetEditor_Load(object sender, EventArgs e)
        {
        }

        private void dataGridViewAppraisalRules_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewAppraisalRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditAppraisalRule_Click(sender, e);
            }
        }

        private void dataGridViewAppraisalRules_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEditAppraisalRule_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.D:
                    if (e.Control) this.buttonDuplicateAppraisalRule_Click(sender, e);
                    break;

                case Keys.Delete:
                    this.buttonRemoveAppraisalRule_Click(sender, e);
                    break;
            }
        }

        private void dataGridViewAppraisalRules_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var rule = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.Rows[e.RowIndex].DataBoundItem).Object;
            _appraisalRulesVM.ChangeCurrentRule(rule);
        }
        private void dataGridViewBeliefs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        }

        private void dataGridViewEmotionDispositions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditEmotionDisposition_Click(sender, e);
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void buttonEditGoal_Click(object sender, EventArgs e)
        {
            var selectedGoal = EditorTools.GetSelectedDtoFromTable<GoalDTO>(dataGridViewGoals);
            if (selectedGoal != null)
            {
                new AddOrEditGoalForm(_loadedAsset, selectedGoal).ShowDialog();
                dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(_loadedAsset.GetAllGoals().ToList());
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            IList<GoalDTO> goalsToRemove = new List<GoalDTO>();
            for (int i = 0; i < dataGridViewGoals.SelectedRows.Count; i++)
            {
                var goal = ((ObjectView<GoalDTO>)dataGridViewGoals.SelectedRows[i].DataBoundItem).Object;
                goalsToRemove.Add(goal);
            }
            _loadedAsset.RemoveGoals(goalsToRemove);
            dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(_loadedAsset.GetAllGoals().ToList());
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                _currentFilePath = ofd.FileName;
                _storage = AssetStorage.FromJson(File.ReadAllText(_currentFilePath));
                _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
                OnAssetDataLoaded(_loadedAsset);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if(_currentFilePath != null)
            {
                using (var writer = File.CreateText(_currentFilePath))
                {
                    _loadedAsset.Save();
                    writer.Write(_storage.ToJson());
                }
            }
        }
    }
}