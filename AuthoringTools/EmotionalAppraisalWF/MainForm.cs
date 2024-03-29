﻿using ActionLibrary.DTOs;
using AutobiographicMemory;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.ViewModels;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WellFormedNames;

namespace EmotionalAppraisalWF
{
    public partial class MainForm : Form
    {
        private AssetStorage _storage;
        private EmotionalAppraisalAsset _loadedAsset;
        public AppraisalRulesVM _appraisalRulesVM;
        private string _currentFilePath;

        public MainForm()
        {
            InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        public MainForm(Form parent)
        {
            InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = EmotionalAppraisalAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
            this.toolTip.SetToolTip(parent, "Ayy");
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
            UpdateAppraisalRules();
        }

        private void buttonAddGoal_Click(object sender, EventArgs e)
        {
            
        }
            

        public void buttonAddAppraisalRule_Click(object sender, EventArgs e)
        {
            new AddOrEditAppraisalRuleForm(_appraisalRulesVM).ShowDialog();
            UpdateAppraisalRules();
        }

        public void AddAppraisalRulewithEmotions(ActionRuleDTO rule, EmotionalAppraisal.OCCModel.OCCEmotionType targetEmotion,
            EmotionalAppraisal.OCCModel.OCCEmotionType subjectEmotion)
        {

            if (targetEmotion != null)
            {
                var targetVariables = EmotionalAppraisal.OCCModel.OCCEmotionType.getVariableFromEmotion(targetEmotion.Name);

                if (rule == null)
                    rule = new ActionRuleDTO();

                AppraisalRuleDTO targetDto = new AppraisalRuleDTO()
                {
                    EventMatchingTemplate =
                     WellFormedNames.Name.BuildName(
                    (Name)AMConsts.EVENT,
                    (Name)AMConsts.ACTION_END,
                    WellFormedNames.Name.UNIVERSAL_SYMBOL,
                    rule.Action,
                   WellFormedNames.Name.SELF_SYMBOL),

                    AppraisalVariables = new AppraisalVariables()
                    {
                        appraisalVariables = targetVariables
                    }
                };

                _appraisalRulesVM.AddOrUpdateAppraisalRule(targetDto);
            }

            if (subjectEmotion != null)
            {

                var subjectVariables = EmotionalAppraisal.OCCModel.OCCEmotionType.getVariableFromEmotion(subjectEmotion.Name);

                AppraisalRuleDTO subjectDto = new AppraisalRuleDTO()
                {
                    EventMatchingTemplate =
                     WellFormedNames.Name.BuildName(
                    (Name)AMConsts.EVENT,
                    (Name)AMConsts.ACTION_END,
                    WellFormedNames.Name.SELF_SYMBOL,
                    rule.Action,
                   WellFormedNames.Name.UNIVERSAL_SYMBOL),

                    AppraisalVariables = new AppraisalVariables()
                    {
                        appraisalVariables = subjectVariables
                    }
                };

                _appraisalRulesVM.AddOrUpdateAppraisalRule(subjectDto);

            }
            UpdateAppraisalRules();
        }
        public void buttonAddAppraisalRule_Click(object sender, ActionRuleDTO rule)
        {

            AppraisalRuleDTO dto = new AppraisalRuleDTO()
            {
                EventMatchingTemplate = EventHelper.ActionEnd(WellFormedNames.Name.BuildName("Subject"), rule.Action, WellFormedNames.Name.SELF_SYMBOL)
                
                
               // WellFormedNames.Name.BuildName(rule.Action.ToString()),
               
            };

            new AddOrEditAppraisalRuleForm(_appraisalRulesVM, dto).ShowDialog();
            UpdateAppraisalRules();

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
            UpdateAppraisalRules();
        }

        private void conditionSetEditor_Load(object sender, EventArgs e)
        {
        }


        private void UpdateAppraisalRules()
        {
            if (_appraisalRulesVM.AppraisalRules.Any())
            {
                buttonEditAppraisalRule.Enabled = true;
                buttonRemoveAppraisalRule.Enabled = true;
                buttonAppVariables.Enabled = true;
                buttonDuplicateAppraisalRule.Enabled = true;
            }
            else
            {
                buttonEditAppraisalRule.Enabled = false;
                buttonRemoveAppraisalRule.Enabled = false;
                buttonAppVariables.Enabled = false;
                buttonDuplicateAppraisalRule.Enabled = false;
            }
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