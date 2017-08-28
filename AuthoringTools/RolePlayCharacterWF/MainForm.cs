using System;
using RolePlayCharacter;
using WellFormedNames;
using WellFormedNames.Exceptions;
using System.Windows.Forms;
using RolePlayCharacterWF.ViewModels;
using System.Collections.Generic;
using Equin.ApplicationFramework;
using AutobiographicMemory.DTOs;
using EmotionalAppraisal.DTOs;
using GAIPS.AssetEditorTools;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : BaseRPCForm
    {
        private const string MOOD_FORMAT = "0.00";
        private EmotionalStateVM _emotionalStateVM;
        private AutobiographicalMemoryVM _autobiographicalMemoryVM;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private EmotionalAppraisalWF.MainForm _eaForm = new EmotionalAppraisalWF.MainForm();


        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnAssetDataLoaded(RolePlayCharacterAsset asset)
        {
            textBoxCharacterName.Text = asset.CharacterName == null ? string.Empty : asset.CharacterName.ToString();
            textBoxCharacterBody.Text = asset.BodyName;
            textBoxCharacterVoice.Text = asset.VoiceName;

            _emotionalStateVM = new EmotionalStateVM(this);
            _autobiographicalMemoryVM = new AutobiographicalMemoryVM(this);

            this.moodValueLabel.Text = Math.Round(_emotionalStateVM.Mood).ToString(MOOD_FORMAT);
            this.moodTrackBar.Value = (int)float.Parse(this.moodValueLabel.Text);
            this.StartTickField.Value = _emotionalStateVM.Start;
            this.emotionsDataGridView.DataSource = _emotionalStateVM.Emotions;
            this.dataGridViewAM.DataSource = _autobiographicalMemoryVM.Events;

            
            edmAssetControl1.SetAsset(asset.EmotionalDecisionMakingSource, () =>
             {
                 LoadedAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
                 return EmotionalDecisionMaking.EmotionalDecisionMakingAsset.LoadFromFile(LoadedAsset.EmotionalDecisionMakingSource);
             });
            siAssetControl1.SetAsset(asset.SocialImportanceAssetSource, () =>
            {
                LoadedAsset.SocialImportanceAssetSource = siAssetControl1.Path;
                return SocialImportance.SocialImportanceAsset.LoadFromFile(LoadedAsset.SocialImportanceAssetSource);
            });

            //KB
            _knowledgeBaseVM = new KnowledgeBaseVM(this);
            dataGridViewBeliefs.DataSource = _knowledgeBaseVM.Beliefs;
        }

        private void OnScreenChanged(object sender, EventArgs e)
        {
            _knowledgeBaseVM.UpdateBeliefList();
        }

  
        private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {

            if (IsLoading)
                return;

            if (!string.IsNullOrWhiteSpace(textBoxCharacterName.Text))
            {
                try
                {
                    var newName = (Name)textBoxCharacterName.Text;
                    LoadedAsset.CharacterName = newName;
                    _knowledgeBaseVM.UpdateBeliefList();
                }
                catch (ParsingException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SetModified();
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            LoadedAsset.BodyName = textBoxCharacterBody.Text;
            SetModified();
        }

        private void textBoxCharacterVoice_TextChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            LoadedAsset.VoiceName = textBoxCharacterVoice.Text;
            SetModified();
        }

        private void eaAssetControl1_OnPathChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            LoadedAsset.EmotionalAppraisalAssetSource = pathTextBoxEA.Text;
            SetModified();
        }

        private void edmAssetControl1_OnPathChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            LoadedAsset.EmotionalDecisionMakingSource = edmAssetControl1.Path;
            SetModified();
        }

        private void siAssetControl1_OnPathChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            LoadedAsset.SocialImportanceAssetSource = siAssetControl1.Path;
            SetModified();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void eaAssetControl1_Load(object sender, EventArgs e)
        {

        }

        private void edmAssetControl1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addEmotionButton_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addEmotionButton_Click_1(object sender, EventArgs e)
        {
            new AddOrEditEmotionForm(_emotionalStateVM).ShowDialog();
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

        private void buttonRemoveEmotion_Click_1(object sender, EventArgs e)
        {
            IList<EmotionDTO> emotionsToRemove = new List<EmotionDTO>();
            for (int i = 0; i < emotionsDataGridView.SelectedRows.Count; i++)
            {
                var emotion = ((ObjectView<EmotionDTO>)emotionsDataGridView.SelectedRows[i].DataBoundItem).Object;
                emotionsToRemove.Add(emotion);
            }
            _emotionalStateVM.RemoveEmotions(emotionsToRemove);
        }

        private void moodTrackBar_Scroll(object sender, EventArgs e)
        {
            if (IsLoading)
                return;

            moodValueLabel.Text = moodTrackBar.Value.ToString(MOOD_FORMAT);
            _emotionalStateVM.Mood = moodTrackBar.Value;
            SetModified();
        }

        private void StartTickField_ValueChanged(object sender, EventArgs e)
        {
            if (IsLoading)
                return;
            _emotionalStateVM.Start = (ulong)StartTickField.Value;
        }

        private void buttonAddEventRecord_Click(object sender, EventArgs e)
        {
            new AddOrEditAutobiographicalEventForm(_autobiographicalMemoryVM).ShowDialog();
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

        private void buttonDuplicateEventRecord_Click(object sender, EventArgs e)
        {
            if (dataGridViewAM.SelectedRows.Count == 1)
            {
                var selectedEvent = ((ObjectView<EventDTO>)dataGridViewAM.SelectedRows[0].DataBoundItem).Object;
                _autobiographicalMemoryVM.AddEventRecord(selectedEvent);
            }
        }


        private void dataGridViewAM_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditEvent_Click(sender, e);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addBeliefButton_Click(object sender, EventArgs e)
        {
            var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM);
            addBeliefForm.ShowDialog();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewBeliefs.SelectedRows.Count == 1)
            {

                var selectedBelief = ((ObjectView<RolePlayCharacter.BeliefDTO>)dataGridViewBeliefs.SelectedRows[0].DataBoundItem).Object;
                
                var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM, selectedBelief);
                addBeliefForm.ShowDialog();
            }
            
        }

        private void removeBeliefButton_Click(object sender, EventArgs e)
        {
            IList<RolePlayCharacter.BeliefDTO> beliefsToRemove = new List<RolePlayCharacter.BeliefDTO>();
            for (int i = 0; i < dataGridViewBeliefs.SelectedRows.Count; i++)
            {
                var belief = ((ObjectView<RolePlayCharacter.BeliefDTO>)dataGridViewBeliefs.SelectedRows[i].DataBoundItem).Object;
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

        private void tableLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pathTextBoxEA_TextChanged(object sender, EventArgs e)
        {

        }

        private void createNewEAButton_Click(object sender, EventArgs e)
        {
            var asset = _eaForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            LoadedAsset.EmotionalAppraisalAssetSource = asset.AssetFilePath;

            SetModified();
            _eaForm.EditAssetInstance(() => asset);
            FormHelper.ShowFormInContainerControl(this.panelEA, _eaForm);
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            LoadedAsset.EmotionalAppraisalAssetSource = null;
            SetModified();
            _eaForm.Hide();
        }
    }
}

