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
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using SocialImportance;
using CommeillFaut;
using GAIPS.Rage;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : Form
    {
        private const string MOOD_FORMAT = "0.00";
        private EmotionalStateVM _emotionalStateVM;
        private AutobiographicalMemoryVM _autobiographicalMemoryVM;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private EmotionalAppraisalWF.MainForm _eaForm = new EmotionalAppraisalWF.MainForm();
        private EmotionalDecisionMakingWF.MainForm _edmForm = new EmotionalDecisionMakingWF.MainForm();
        private SocialImportanceWF.MainForm _siForm = new SocialImportanceWF.MainForm();
        private CommeillFautWF.MainForm _cifForm = new CommeillFautWF.MainForm();
        private RolePlayCharacterAsset _loadedAsset;
        private AssetStorage _storage;
            
        private int tabSelected;

        public int SelectedTab
        {
            get { return tabSelected;}
            set { tabSelected = value; tabControl1.SelectedIndex = value; }
        }

        public MainForm(AssetStorage storage)
        {
            InitializeComponent();
            _storage = storage;
            _loadedAsset = new RolePlayCharacterAsset();
            _loadedAsset.LoadAssociatedAssets(storage);
            OnAssetDataLoaded(_loadedAsset);
        }

        private void OnAssetDataLoaded(RolePlayCharacterAsset asset)
        {
            textBoxCharacterName.Text = asset.CharacterName == null ? string.Empty : asset.CharacterName.ToString();
            textBoxCharacterBody.Text = asset.BodyName;
            textBoxCharacterVoice.Text = asset.VoiceName;

            _emotionalStateVM = new EmotionalStateVM(asset);
            _autobiographicalMemoryVM = new AutobiographicalMemoryVM(asset);
            _knowledgeBaseVM = new KnowledgeBaseVM(asset);

            this.moodValueLabel.Text = Math.Round(_emotionalStateVM.Mood).ToString(MOOD_FORMAT);
            this.moodTrackBar.Value = (int)float.Parse(this.moodValueLabel.Text);
            this.StartTickField.Value = _emotionalStateVM.Start;
            this.emotionsDataGridView.DataSource = _emotionalStateVM.Emotions;
            this.dataGridViewAM.DataSource = _autobiographicalMemoryVM.Events;
            this.dataGridViewBeliefs.DataSource = _knowledgeBaseVM.Beliefs;
            //EA ASSET
            /*  if (string.IsNullOrEmpty(asset.EmotionalAppraisalAssetSource))
              {
                  _eaForm.Hide();
              }else
              {
                  this.pathTextBoxEA.Text = LoadableAsset<EmotionalDecisionMakingAsset>.ToRelativePath(LoadedAsset.AssetFilePath, asset.EmotionalAppraisalAssetSource);
                  var ea = EmotionalAppraisalAsset.LoadFromFile(asset.EmotionalAppraisalAssetSource);
                  _eaForm.LoadedAsset = ea;
                  FormHelper.ShowFormInContainerControl(this.panelEA, _eaForm);
              }

              //EDM ASSET
              if (string.IsNullOrEmpty(asset.EmotionalDecisionMakingSource))
              {
                  _edmForm.Hide();
              }
              else
              {
                  this.textBoxPathEDM.Text = LoadableAsset<EmotionalDecisionMakingAsset>.ToRelativePath(LoadedAsset.AssetFilePath, asset.EmotionalDecisionMakingSource);
                  var edm = EmotionalDecisionMakingAsset.LoadFromFile(asset.EmotionalDecisionMakingSource);
                  _edmForm.LoadedAsset = edm;
                  FormHelper.ShowFormInContainerControl(this.panelEDM, _edmForm);
              }

              //SI ASSET
              if (string.IsNullOrEmpty(asset.SocialImportanceAssetSource))
              {
                  _siForm.Hide();
              }
              else
              {
                  this.textBoxPathSI.Text = LoadableAsset<EmotionalDecisionMakingAsset>.ToRelativePath(LoadedAsset.AssetFilePath, asset.SocialImportanceAssetSource);
                  var si = SocialImportanceAsset.LoadFromFile(asset.SocialImportanceAssetSource);
                  _siForm.LoadedAsset = si;
                  FormHelper.ShowFormInContainerControl(this.panelSI, _siForm);
              }

              //CIF ASSET
              if (string.IsNullOrEmpty(asset.CommeillFautAssetSource))
              {
                  _cifForm.Hide();
              }
              else
              {
                  this.textBoxPathCIF.Text = LoadableAsset<EmotionalDecisionMakingAsset>.ToRelativePath(LoadedAsset.AssetFilePath, asset.CommeillFautAssetSource);
                  var cif = CommeillFautAsset.LoadFromFile(asset.CommeillFautAssetSource);
                  _cifForm.LoadedAsset = cif;
                  FormHelper.ShowFormInContainerControl(this.panelCIF, _cifForm);
              }*/


        }

        private void OnScreenChanged(object sender, EventArgs e)
        {
            _knowledgeBaseVM.UpdateBeliefList();
        }

  
        private void textBoxCharacterName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxCharacterName.Text))
            {
                try
                {
                    var newName = (Name)textBoxCharacterName.Text;
                    _loadedAsset.CharacterName = newName;
                    _knowledgeBaseVM.UpdateBeliefList();
                }
                catch (ParsingException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCharacterBody_TextChanged(object sender, EventArgs e)
        {
            _loadedAsset.BodyName = textBoxCharacterBody.Text;
        }

        private void textBoxCharacterVoice_TextChanged(object sender, EventArgs e)
        {
            _loadedAsset.VoiceName = textBoxCharacterVoice.Text;
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
            moodValueLabel.Text = moodTrackBar.Value.ToString(MOOD_FORMAT);
            _emotionalStateVM.Mood = moodTrackBar.Value;
        }

        private void StartTickField_ValueChanged(object sender, EventArgs e)
        {
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
            addBeliefForm.ShowDialog(this);
            _knowledgeBaseVM.UpdateBeliefList();
        }

  
        private void removeBeliefButton_Click(object sender, EventArgs e)
        {
            IList<RolePlayCharacter.BeliefDTO> beliefsToRemove = new List<RolePlayCharacter.BeliefDTO>();
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
                this.buttonEdit_Click(sender, e);
            }
        }
        
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewBeliefs.SelectedRows.Count == 1)
            {
                var selectedBelief = ((ObjectView<BeliefDTO>)dataGridViewBeliefs.SelectedRows[0].DataBoundItem).Object;

                var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM, selectedBelief);
                addBeliefForm.ShowDialog();
                _knowledgeBaseVM.UpdateBeliefList();
            }
        }

        private void duplicateButton_Click(object sender, EventArgs e)
        {
            if (dataGridViewAM.SelectedRows.Count == 1)
            {
                var selectedEvent = ((ObjectView<EventDTO>)dataGridViewAM.SelectedRows[0].DataBoundItem).Object;
                _autobiographicalMemoryVM.AddEventRecord(selectedEvent);
            }
        }

        private void dataGridViewBeliefs_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEdit_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    this.removeBeliefButton_Click(sender, e);
                    break;
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dataGridViewBeliefs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewAM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewAM_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEditEvent_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.D:
                    if (e.Control) this.buttonDuplicateEventRecord_Click(sender, e);
                    break;
                case Keys.Delete:
                    this.buttonRemoveEventRecord_Click(sender, e);
                    break;
            }
        }

        private void textBoxPathEDM_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedTab = tabControl1.SelectedIndex;
        }

        private void emotionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

