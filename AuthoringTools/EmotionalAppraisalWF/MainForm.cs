using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.ViewModels;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using Conditions.DTOs;

namespace EmotionalAppraisalWF
{
	public partial class MainForm : BaseEAForm
    {
        private AppraisalRulesVM _appraisalRulesVM;
        private EmotionDispositionsVM _emotionDispositionsVM;
        
		public MainForm()
        {
            InitializeComponent();
        }

		protected override void OnAssetDataLoaded(EmotionalAppraisalAsset asset)
		{
			//Emotion Dispositions
			_emotionDispositionsVM = new EmotionDispositionsVM(this);
			comboBoxDefaultDecay.SelectedIndex =
				comboBoxDefaultDecay.FindString(_emotionDispositionsVM.DefaultDecay.ToString());
			comboBoxDefaultThreshold.SelectedIndex =
				comboBoxDefaultThreshold.FindString(_emotionDispositionsVM.DefaultThreshold.ToString());
			dataGridViewEmotionDispositions.DataSource = _emotionDispositionsVM.EmotionDispositions;

			//Appraisal Rule
			_appraisalRulesVM = new AppraisalRulesVM(this);
			dataGridViewAppraisalRules.DataSource = _appraisalRulesVM.AppraisalRules;
			dataGridViewAppraisalRules.Columns[PropertyUtil.GetPropertyName<AppraisalRuleDTO>(dto => dto.Id)].Visible = false;
			dataGridViewAppraisalRules.Columns[PropertyUtil.GetPropertyName<AppraisalRuleDTO>(dto => dto.Conditions)].Visible = false;
			conditionSetEditor.View = _appraisalRulesVM.CurrentRuleConditions;

            this.richTextBoxDescription.Text = asset.Description;
            _wasModified = false;
		}

		#region EmotionalStateTab

		private void comboBoxDefaultDecay_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (IsLoading)
				return;

			_emotionDispositionsVM.DefaultDecay = int.Parse(comboBoxDefaultDecay.Text);
        }

        private void comboBoxDefaultThreshold_SelectedIndexChanged(object sender, EventArgs e)
        {
			if (IsLoading)
				return;

			_emotionDispositionsVM.DefaultThreshold = int.Parse(comboBoxDefaultThreshold.Text);
		}

        #endregion

        

		#region Appraisal Rules
		
		private void dataGridViewAppraisalRules_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var rule = ((ObjectView<AppraisalRuleDTO>) dataGridViewAppraisalRules.Rows[e.RowIndex].DataBoundItem).Object;
            _appraisalRulesVM.ChangeCurrentRule(rule);
        }

        private void buttonAddAppraisalRule_Click(object sender, EventArgs e)
        {
            new AddOrEditAppraisalRuleForm(_appraisalRulesVM).ShowDialog();
        }

        private void buttonEditAppraisalRule_Click(object sender, EventArgs e)
        {
            if (dataGridViewAppraisalRules.SelectedRows.Count == 1)
            {
                var selectedAppraisalRule = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditAppraisalRuleForm(_appraisalRulesVM, selectedAppraisalRule).ShowDialog();
            }
        }


        private void buttonDuplicateAppraisalRule_Click(object sender, EventArgs e)
        {
            if (dataGridViewAppraisalRules.SelectedRows.Count == 1)
            {
                var selectedAppraisalRule = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.SelectedRows[0].DataBoundItem).Object;
                var duplicateRule = new AppraisalRuleDTO
                {
                    EventMatchingTemplate = selectedAppraisalRule.EventMatchingTemplate,
                    Praiseworthiness = selectedAppraisalRule.Praiseworthiness,
                    Desirability = selectedAppraisalRule.Desirability,
                    Conditions = new ConditionSetDTO
                    {
                        Quantifier = selectedAppraisalRule.Conditions.Quantifier,
                        ConditionSet = (string[])selectedAppraisalRule.Conditions.ConditionSet.Clone()
                    }
                };
                _appraisalRulesVM.AddOrUpdateAppraisalRule(duplicateRule);
            }
        }

        private void buttonRemoveAppraisalRule_Click(object sender, EventArgs e)
        {
            IList<AppraisalRuleDTO> rulesToRemove = new List<AppraisalRuleDTO>();
            for (int i = 0; i < dataGridViewAppraisalRules.SelectedRows.Count; i++)
            {
                var rule  = ((ObjectView<AppraisalRuleDTO>)dataGridViewAppraisalRules.SelectedRows[i].DataBoundItem).Object;
                rulesToRemove.Add(rule);
            }
            _appraisalRulesVM.RemoveAppraisalRules(rulesToRemove);
        }

		#endregion
        
    
        private void buttonAddEmotionDisposition_Click(object sender, EventArgs e)
        {
            new AddOrEditEmotionDispositionForm(_emotionDispositionsVM).ShowDialog();
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

        private void dataGridViewEmotionDispositions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditEmotionDisposition_Click(sender, e);
            }
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
		            
        private void dataGridViewAppraisalRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditAppraisalRule_Click(sender, e);
            }
        }

		private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

       
        private void richTextBoxDescription_TextChanged_1(object sender, EventArgs e)
        {
            LoadedAsset.Description = richTextBoxDescription.Text;
            SetModified();
        }

        private void dataGridViewBeliefs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
