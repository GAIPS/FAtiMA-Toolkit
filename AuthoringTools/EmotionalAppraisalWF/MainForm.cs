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
        private string _currentFilePath;

        public MainForm()
        {
            InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        public EmotionalAppraisalAsset Asset
        {
            get { return _loadedAsset; }
            set { _loadedAsset = value; OnAssetDataLoaded(); }
        }


        private void OnAssetDataLoaded()
        {
            //Appraisal Rule
            _appraisalRulesVM = new AppraisalRulesVM(Asset);
            dataGridViewAppraisalRules.DataSource = _appraisalRulesVM.AppraisalRules;
            EditorTools.HideColumns(dataGridViewAppraisalRules, new[]
            {
                PropertyUtil.GetPropertyName<AppraisalRuleDTO>(dto => dto.Id)           
            });
               
            conditionSetEditor.View = _appraisalRulesVM.CurrentRuleConditions;

            EditorTools.UpdateFormTitle("Emotional Appraisal", _currentFilePath, this);
        }

        private void buttonAddGoal_Click(object sender, EventArgs e)
        {
            }
            

        private void buttonAddAppraisalRule_Click(object sender, EventArgs e)
        {
            new AddOrEditAppraisalRuleForm(_appraisalRulesVM).ShowDialog();
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


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = null;
            _storage = new AssetStorage();
            _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Asset Storage File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                _currentFilePath = aux;
                _storage = AssetStorage.FromJson(File.ReadAllText(_currentFilePath));
                _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
                OnAssetDataLoaded();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath, _storage, _loadedAsset);
            EditorTools.UpdateFormTitle("Emotional Decision Making", _currentFilePath, this);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var old = _currentFilePath;
            _currentFilePath = null;
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath, _storage, _loadedAsset);
            if (_currentFilePath == null) _currentFilePath = old;
            EditorTools.UpdateFormTitle("Emotional Appraisal", _currentFilePath, this);
        }
    }
}