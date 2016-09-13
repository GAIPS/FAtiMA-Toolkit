using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using EmotionalAppraisalWF.Properties;
using EmotionalAppraisalWF.ViewModels;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;

namespace EmotionalAppraisalWF
{
	public partial class MainForm : BaseEAForm
    {
        private const string MOOD_FORMAT = "0.00";

        private EmotionalStateVM _emotionalStateVM;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private AppraisalRulesVM _appraisalRulesVM;
        private EmotionDispositionsVM _emotionDispositionsVM;
        private AutobiographicalMemoryVM _autobiographicalMemoryVM;

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

			//KB
			_knowledgeBaseVM = new KnowledgeBaseVM(this);
			dataGridViewBeliefs.DataSource = _knowledgeBaseVM.Beliefs;

			//AM
			_autobiographicalMemoryVM = new AutobiographicalMemoryVM(this);
			dataGridViewAM.DataSource = _autobiographicalMemoryVM.Events;

			//Emotional State Tab
			_emotionalStateVM = new EmotionalStateVM(this);

			this.textBoxPerspective.Text = _knowledgeBaseVM.Perspective;
			this.richTextBoxDescription.Text = asset.Description;
			this.moodValueLabel.Text = Math.Round(_emotionalStateVM.Mood).ToString(MOOD_FORMAT);
			this.moodTrackBar.Value = (int)float.Parse(this.moodValueLabel.Text);
			this.StartTickField.Value = _emotionalStateVM.Start;
			this.emotionsDataGridView.DataSource = _emotionalStateVM.Emotions;
		}

		protected sealed override void OnWillSaveAsset(EmotionalAppraisalAsset asset)
		{
			_knowledgeBaseVM.UpdatePerspective();
		}

		private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            moodValueLabel.Text = moodTrackBar.Value.ToString(MOOD_FORMAT);
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
                var selectedBelief = ((ObjectView<BeliefDTO>) dataGridViewBeliefs.SelectedRows[0].DataBoundItem).Object;
                var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM, selectedBelief);
                addBeliefForm.ShowDialog();
            }
        }

        private void removeBeliefButton_Click(object sender, EventArgs e)
        {
            IList<BeliefDTO> beliefsToRemove = new List<BeliefDTO>();
            for (int i = 0; i < dataGridViewBeliefs.SelectedRows.Count; i++)
            {
                var belief = ((ObjectView<BeliefDTO>) dataGridViewBeliefs.SelectedRows[i].DataBoundItem).Object;
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
		
        private void textBoxPerspective_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPerspective.Text))
            {
				_knowledgeBaseVM.Perspective = textBoxPerspective.Text;
            }
        }

        private void textBoxStartTick_TextChanged(object sender, EventArgs e)
        {
	        _emotionalStateVM.Start = (ulong)StartTickField.Value;
        }
        
        private void addEmotionButton_Click(object sender, EventArgs e)
        {
            new AddOrEditEmotionForm(_emotionalStateVM).ShowDialog();
        }

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
		
        private void buttonAddEventRecord_Click(object sender, EventArgs e)
        {
            new AddOrEditAutobiographicalEventForm(_autobiographicalMemoryVM).ShowDialog();
        }

        private void buttonRemoveEventRecord_Click(object sender, EventArgs e)
        {
            IList<EventDTO> eventsToRemove = new List<EventDTO>();
            for (int i = 0; i < dataGridViewAM.SelectedRows.Count; i++)
            {
                var evt = ((ObjectView<EventDTO>)dataGridViewAM.SelectedRows[i].DataBoundItem).Object;
                eventsToRemove.Add(evt);
            }
            _autobiographicalMemoryVM.RemoveEventRecords(eventsToRemove);
		}

        private void buttonRemoveEmotion_Click(object sender, EventArgs e)
        {
            IList<EmotionDTO> emotionsToRemove = new List<EmotionDTO>();
            for (int i = 0; i < emotionsDataGridView.SelectedRows.Count; i++)
            {
                var emotion = ((ObjectView<EmotionDTO>)emotionsDataGridView.SelectedRows[i].DataBoundItem).Object;
                emotionsToRemove.Add(emotion);
            }
            _emotionalStateVM.RemoveEmotions(emotionsToRemove);
		}

        private void buttonEditEmotion_Click(object sender, EventArgs e)
        {
       
            if (emotionsDataGridView.SelectedRows.Count == 1)
            {
                var selectedEmotion = ((ObjectView<EmotionDTO>)emotionsDataGridView.
                    SelectedRows[0].DataBoundItem).Object;
                new AddOrEditEmotionForm(_emotionalStateVM, selectedEmotion).ShowDialog();
            }
        
        }

        private void dataGridViewAppraisalRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditAppraisalRule_Click(sender, e);
            }
        }

        private void buttonEditEvent_Click(object sender, EventArgs e)
        {
            if (dataGridViewAM.SelectedRows.Count == 1)
            {
                var selectedEvent = ((ObjectView<EventDTO>)dataGridViewAM.
                    SelectedRows[0].DataBoundItem).Object;
                new AddOrEditAutobiographicalEventForm(_autobiographicalMemoryVM, selectedEvent).ShowDialog();
            }
        }

        private void dataGridViewAM_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditEvent_Click(sender, e);
            }
        }

		private void richTextBoxDescription_TextChanged(object sender, EventArgs e)
        {
			CurrentAsset.Description = richTextBoxDescription.Text;
			SetModified();
		}

		private void OnScreenChanged(object sender, EventArgs e)
		{
			_knowledgeBaseVM.UpdatePerspective();
		}
    }
}
