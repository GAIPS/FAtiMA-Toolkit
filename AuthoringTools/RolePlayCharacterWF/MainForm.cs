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

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : BaseRPCForm
    {
        private const string MOOD_FORMAT = "0.00";
        private EmotionalStateVM _emotionalStateVM;
        private AutobiographicalMemoryVM _autobiographicalMemoryVM;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private EmotionalAppraisalWF.MainForm _eaForm = new EmotionalAppraisalWF.MainForm();
        private EmotionalDecisionMakingWF.MainForm _edmForm = new EmotionalDecisionMakingWF.MainForm();
        private SocialImportanceWF.MainForm _siForm = new SocialImportanceWF.MainForm();
        private CommeillFautWF.MainForm _cifForm = new CommeillFautWF.MainForm();


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

            //EA ASSET
            this.pathTextBoxEA.Text = asset.EmotionalAppraisalAssetSource;
            if (string.IsNullOrEmpty(asset.EmotionalAppraisalAssetSource))
            {
                _eaForm.Hide();
            }else
            {
                var ea = EmotionalAppraisalAsset.LoadFromFile(asset.EmotionalAppraisalAssetSource);
                _eaForm.LoadedAsset = ea;
                FormHelper.ShowFormInContainerControl(this.panelEA, _eaForm);
            }

            //EDM ASSET
            this.textBoxPathEDM.Text = asset.EmotionalDecisionMakingSource;
            if (string.IsNullOrEmpty(asset.EmotionalDecisionMakingSource))
            {
                _edmForm.Hide();
            }
            else
            {
                var edm = EmotionalDecisionMakingAsset.LoadFromFile(asset.EmotionalDecisionMakingSource);
                _edmForm.LoadedAsset = edm;
                FormHelper.ShowFormInContainerControl(this.panelEDM, _edmForm);
            }

            //SI ASSET
            this.textBoxPathSI.Text = asset.SocialImportanceAssetSource;
            if (string.IsNullOrEmpty(asset.SocialImportanceAssetSource))
            {
                _siForm.Hide();
            }
            else
            {
                var si = SocialImportanceAsset.LoadFromFile(asset.SocialImportanceAssetSource);
                _siForm.LoadedAsset = si;
                FormHelper.ShowFormInContainerControl(this.panelSI, _siForm);
            }

            //CIF ASSET
            this.textBoxPathCIF.Text = asset.CommeillFautAssetSource;
            if (string.IsNullOrEmpty(asset.CommeillFautAssetSource))
            {
                _cifForm.Hide();
            }
            else
            {
                var cif = CommeillFautAsset.LoadFromFile(asset.CommeillFautAssetSource);
                _cifForm.LoadedAsset = cif;
                FormHelper.ShowFormInContainerControl(this.panelCIF, _cifForm);
            }

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
            addBeliefForm.ShowDialog(this);
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
                this.buttonEdit_Click(sender, e);
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
            if(LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }

            _eaForm = new EmotionalAppraisalWF.MainForm();
            var asset = _eaForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            LoadedAsset.EmotionalAppraisalAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        private void buttonNewEDM_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }

            _edmForm = new EmotionalDecisionMakingWF.MainForm();
            var asset = _edmForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            LoadedAsset.EmotionalDecisionMakingSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }
        private void openEAButton_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }
            _eaForm = new EmotionalAppraisalWF.MainForm();
            var asset = _eaForm.SelectAndOpenAssetFromBrowser();
            if (asset == null)
                return;

            LoadedAsset.EmotionalAppraisalAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }


        private void _clearButton_Click(object sender, EventArgs e)
        {
            LoadedAsset.EmotionalAppraisalAssetSource = null;
            pathTextBoxEA.Text = null; 
            SetModified();
            _eaForm.Hide();
        }



        private void buttonNewCIF_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }

            _cifForm = new CommeillFautWF.MainForm();
            var asset = _cifForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            LoadedAsset.CommeillFautAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        protected override void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewAsset();
        }


        

        protected override bool SaveAsset()
        {
            SaveSubAssets();
            return base.SaveAsset();
        }

        protected override bool SaveAssetAs()
        {
            SaveSubAssets();
            return base.SaveAssetAs();
        }

        public void SaveSubAssets()
        {
            if (_eaForm.LoadedAsset != null)
            {
                _eaForm.SaveAssetToFile(_eaForm.LoadedAsset, _eaForm.LoadedAsset.AssetFilePath);
            }
            if (_edmForm.LoadedAsset != null)
            {
                _edmForm.SaveAssetToFile(_edmForm.LoadedAsset, _edmForm.LoadedAsset.AssetFilePath);
            }
            if (_siForm.LoadedAsset != null)
            {
                _siForm.SaveAssetToFile(_siForm.LoadedAsset, _siForm.LoadedAsset.AssetFilePath);
            }
            if (_cifForm.LoadedAsset != null)
            {
                _cifForm.SaveAssetToFile(_cifForm.LoadedAsset, _cifForm.LoadedAsset.AssetFilePath);
            }

        }

        private void buttonOpenEDM_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }
            _edmForm = new EmotionalDecisionMakingWF.MainForm();
            var asset = _edmForm.SelectAndOpenAssetFromBrowser();
            if (asset == null)
                return;

            LoadedAsset.EmotionalDecisionMakingSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        private void buttonClearEDM_Click(object sender, EventArgs e)
        {
            LoadedAsset.EmotionalDecisionMakingSource = null;
            textBoxPathEDM.Text = null;
            SetModified();
            _edmForm.Hide();
        }

        private void buttonNewSI_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }

            _siForm = new SocialImportanceWF.MainForm();
            var asset = _siForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            LoadedAsset.SocialImportanceAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        private void buttonOpenSI_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }
            _siForm = new SocialImportanceWF.MainForm();
            var asset = _siForm.SelectAndOpenAssetFromBrowser();
            if (asset == null)
                return;

            LoadedAsset.SocialImportanceAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        private void buttonClearSI_Click(object sender, EventArgs e)
        {
            LoadedAsset.SocialImportanceAssetSource = null;
            textBoxPathSI.Text = null;
            SetModified();
            _siForm.Hide();
        }

        private void buttonOpenCif_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the RPC asset");
                return;
            }
            _cifForm = new CommeillFautWF.MainForm();
            var asset = _cifForm.SelectAndOpenAssetFromBrowser();
            if (asset == null)
                return;

            LoadedAsset.CommeillFautAssetSource = asset.AssetFilePath;
            SetModified();
            ReloadEditor();
        }

        private void buttonClearCIF_Click(object sender, EventArgs e)
        {
            LoadedAsset.CommeillFautAssetSource = null;
            textBoxPathCIF.Text = null;
            SetModified();
            _cifForm.Hide();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewBeliefs.SelectedRows.Count == 1)
            {
                var selectedBelief = ((ObjectView<RolePlayCharacter.BeliefDTO>)dataGridViewBeliefs.SelectedRows[0].DataBoundItem).Object;

                var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM, selectedBelief);
                addBeliefForm.ShowDialog();
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
    }
}

