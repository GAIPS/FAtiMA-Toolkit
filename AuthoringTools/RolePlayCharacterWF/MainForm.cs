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
using GAIPS.Rage;
using System.Linq;
using GAIPS.AssetEditorTools;
using System.Drawing;
using System.IO;

namespace RolePlayCharacterWF
{
    public sealed partial class MainForm : Form
    {
        public delegate void OnNameChange(string newName);
        public delegate void OnMoodChange(float newMood);

        private const string MOOD_FORMAT = "0.00";
        private EmotionalStateVM _emotionalStateVM;
        private AutobiographicalMemoryVM _autobiographicalMemoryVM;
        public KnowledgeBaseVM _knowledgeBaseVM;
        public RolePlayCharacterAsset _loadedAsset;
            
        public int tabSelected;
        private Form _parent; 

        public event OnNameChange OnNameChangeEvent;
        public event OnMoodChange OnMoodChangeEvent;
        

        public int SelectedTab
        {
            get { return tabSelected;}
            set { tabSelected = value; tabControl1.SelectedIndex = value; }
        }

        public RolePlayCharacterAsset Asset
        {
            get { return _loadedAsset; }
            set { _loadedAsset = value; OnAssetDataLoaded(); }
        }

        public MainForm()
        {
            InitializeComponent();
            _loadedAsset = new RolePlayCharacterAsset();
        }

        public MainForm(Form parent)
        {
            InitializeComponent();
            _parent = parent;
            _loadedAsset = new RolePlayCharacterAsset();
            this.toolTip1.SetToolTip(parent, "Ayy");
        }

        public void OnAssetDataLoaded()
        {
            _emotionalStateVM = new EmotionalStateVM(Asset);
            _autobiographicalMemoryVM = new AutobiographicalMemoryVM(Asset);
            _knowledgeBaseVM = new KnowledgeBaseVM(Asset);

            
            textBoxCharacterName.Text = Asset.CharacterName == null ? string.Empty : Asset.CharacterName.ToString();
            textBoxCharacterBody.Text = Asset.BodyName;
            textBoxCharacterVoice.Text = Asset.VoiceName;

            this.moodValueLabel.Text = Math.Round(_emotionalStateVM.Mood).ToString(MOOD_FORMAT);
            this.moodTrackBar.Value = (int)float.Parse(this.moodValueLabel.Text);
            this.StartTickField.Value = _emotionalStateVM.Start;
            this.emotionsDataGridView.DataSource = _emotionalStateVM.Emotions;
            this.dataGridViewAM.DataSource = _autobiographicalMemoryVM.Events;
            this.dataGridViewBeliefs.DataSource = _knowledgeBaseVM.Beliefs;
            dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(Asset.GetAllGoals().ToList());
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
                    OnNameChangeEvent(newName.ToString());
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
            OnMoodChangeEvent(moodTrackBar.Value);
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

        public void addBeliefButton_Click(object sender, EventArgs e)
        {
            var addBeliefForm = new AddOrEditBeliefForm(_knowledgeBaseVM);
            addBeliefForm.ShowDialog(this);
            _knowledgeBaseVM.UpdateBeliefList();
            UpdatBeliefs();
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
            UpdatBeliefs();
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

        private void UpdatBeliefs()
        {
            if (_knowledgeBaseVM.Beliefs.Any())
            {
                removeBeliefButton.Enabled = true;
                buttonEdit.Enabled = true;
                buttonReasonKB.Enabled = true;
            }
            else
            {
                removeBeliefButton.Enabled = false;
                buttonEdit.Enabled = false;
                buttonReasonKB.Enabled = false;

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

        private void buttonAddGoal_Click(object sender, EventArgs e)
        {
            new AddOrEditGoalForm(_loadedAsset, null).ShowDialog(this);
            dataGridViewGoals.DataSource = new BindingListView<GoalDTO>(_loadedAsset.GetAllGoals().ToList());
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

        private void buttonRemoveGoal_Click(object sender, EventArgs e)
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

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void reasonKB_Click(object sender, EventArgs e)
        {
            _loadedAsset.m_kb.Reason();
            _knowledgeBaseVM.UpdateBeliefList();
        }

        private void buttonReasonKB_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Infers new beliefs from is(x,y) statements. \n Example: is(Amy, dog)=True -> is(Amy, mammal)=True -> is(Amy, animal)=True \n Rules of Inference were created according to Wordnet's Hypernim hierarchy", buttonReasonKB);
        }

        private void addEmotionButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Adds emotions to the Agent's Emotional State", addEmotionButton);
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void buttonEditEmotion_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Edits already added emotions", buttonEditEmotion);

        }

        private void addBeliefButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Adds beliefs to the Agent's KB, make sure it contains ()  \n Example:  Likes(Orange)=True | Dialogue(State) = Start", addBeliefButton);

        }

        private void buttonEdit_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Edits already added beliefs", buttonEdit);
        }

        private void removeBeliefButton_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Removes beliefs from the Agent's KB", removeBeliefButton);
        }

        private void buttonRemoveEmotion_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Removes emotions from the Agent's Emotional State", buttonRemoveEmotion);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var nodes =_loadedAsset.m_kb.GetCommonSense();

            Dictionary<string, List<string>> states = new Dictionary<string, List<string>>();


            foreach (var n in nodes.Keys)
            {

                List<string> nextStates = new List<string>();

                foreach(var st in nodes[n].childNodes)
                {
                    nextStates.Add(st.value);
                }

                    states.Add(n, nextStates);
            }

            string writer = "";

            writer += "digraph { \n";
            writer += "node[fontsize=10, labelloc = \"t\", labeljust = \"l\"]; \n";

            foreach (var s in states.Keys)
            {
                foreach (var ns in states[s])
                {
                    if (s != "-")
                        writer += s + "->" + ns + "\n";
                    else if (ns != "-")
                        writer += "Any" + "->" + ns + "\n";
                    else writer += "Any" + "->" + "Any" + "\n";
                }
            }

            writer += "}";

            Bitmap bit =  Run(writer);
            var image = new IntegratedAuthoringToolWF.ImageForm(bit);
            image.Show();
        }

        public static Bitmap Run(string dot)
        {
            string executable = @".\external\dot.exe";
            string output = @".\external\tempgraph";
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
            Bitmap bitmap = null; ;

            try
            {
                using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
                {
                    Image image = Image.FromStream(bmpStream);
                    bitmap = new Bitmap(image);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            File.Delete(output);
            File.Delete(output + ".jpg");
            return bitmap;
        }

    }
}

