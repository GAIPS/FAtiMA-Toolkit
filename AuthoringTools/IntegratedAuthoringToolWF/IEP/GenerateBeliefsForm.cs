using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class GenerateBeliefsForm : Form
    {
        ConnectToServerForm _server;
        OutputManager _manager;
        public GenerateBeliefsForm(ConnectToServerForm server, OutputManager manager)
        {
            InitializeComponent();
            _server = server;
            _manager = manager;
            resultingScenarioGroupBox.Enabled = false;
            outputBox.Enabled = false;
            processOutputButton.Enabled = false;
            processInputButton.Enabled = false;
            if (!_server.connected)
            {
                MessageBox.Show("Please connect to the Wizard Server");
                _server.currentForm = this;
                _server.ShowDialog();
            }

            if(_server.connected)
            {
                processInputButton.Enabled = true;
            }

            InitializePanels();
        }


        private void InitializePanels()
        {
            var _characters = new BindingListView<CharacterNameAndMoodDTO>(new List<CharacterNameAndMoodDTO>());
            var _beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
            var _goals = new BindingListView<GoalDTO>(new List<GoalDTO>());

            internalCharacterView.DataSource = _characters;
            dataGridViewBeliefs.DataSource = _beliefs;
            dataGridViewGoals.DataSource = _goals;

            // Making sure everything doesn't get too crowded
            internalCharacterView.ColumnHeadersVisible = false;
            dataGridViewGoals.ColumnHeadersVisible = false;
            this.internalCharacterView.Columns["Mood"].Visible = false;
            this.dataGridViewGoals.Columns["Significance"].Visible = false;
            this.dataGridViewGoals.Columns["Likelihood"].Visible = false;
            this.dataGridViewBeliefs.Columns["Perspective"].Visible = false;
            this.dataGridViewBeliefs.Columns["Certainty"].Visible = false;
        }

        private void LoadOutput()
        {
            // Characters
            var _characters = new BindingListView<CharacterNameAndMoodDTO>(new List<CharacterNameAndMoodDTO>());

            _characters.DataSource = _manager._iatAux.Characters.Select(c =>
                        new CharacterNameAndMoodDTO
                        {
                            Name = c.CharacterName.ToString(),
                            Mood = c.Mood
                        }).ToList();

            internalCharacterView.DataSource = _characters;
            internalCharacterView.ClearSelection();
            internalCharacterView.Refresh();
            this.internalCharacterView_SelectionChanged(this, new EventArgs());

            resultingScenarioGroupBox.Enabled = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //Accept Button

            _manager.AcceptOutput();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _manager.RejectOutput();
            this.Close();
            //Cancel Button
        }

        private void processInputButton_Click(object sender, EventArgs e)
        {
            var result = this._server.ProcessDescription(this.descriptionText.Text);
            if (result != "")
            {

                scenarioTextBox.Text = result;
                outputBox.Enabled = true;
                processOutputButton.Enabled = true;
            }
           
        }

        private void internalCharacterView_SelectionChanged(object sender, EventArgs e)
        {
            // Update Beliefs and Goals
            var beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
            var goals = new BindingListView<GoalDTO>(new List<GoalDTO>());

            if (internalCharacterView.SelectedRows.Count > 0)
            {
                var rpc = _manager._iatAux.Characters.ToList()[internalCharacterView.SelectedRows[0].Index];
                foreach (var b in rpc.GetAllBeliefs())
                    beliefs.DataSource.Add(b);

                foreach (var g in rpc.GetAllGoals())
                    goals.DataSource.Add(g);

                beliefs.Refresh();
                goals.Refresh();

                dataGridViewBeliefs.DataSource = beliefs;
                dataGridViewGoals.DataSource = goals;
            }

            else
            {
                if (_manager._iatAux.Characters.Count() > 0)
                {
                    var rpc = _manager._iatAux.Characters.ToList()[0];
                    foreach (var b in rpc.GetAllBeliefs())
                        beliefs.DataSource.Add(b);

                    foreach (var g in rpc.GetAllGoals())
                        goals.DataSource.Add(g);


                    beliefs.Refresh();
                    goals.Refresh();
                    dataGridViewBeliefs.DataSource = beliefs;
                    dataGridViewGoals.DataSource = goals;
                }
            }
        }

        private void processOutputButton_Click(object sender, EventArgs e)
        {
           
            _manager.ComputeStory(outputBox.Text);

            LoadOutput();
        }

        private void GenerateBeliefsForm_Load(object sender, EventArgs e)
        {
            if (_server.connected)
            {
                processInputButton.Enabled = true;
            }
        }
    }
}
