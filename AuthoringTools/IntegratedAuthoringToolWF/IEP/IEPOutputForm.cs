using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActionLibrary.DTOs;
using EmotionalAppraisal.DTOs;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;
using RolePlayCharacterWF.ViewModels;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class IEPOutputForm : Form
    {
        private ComputeDescriptionForm parent;
        public IEPOutputForm(ComputeDescriptionForm f, string extrapolations)
        {
            InitializeComponent();
            parent = f;
            this.scenarioTextBox.Text = extrapolations;
            parent.ComputeOutput(this.scenarioTextBox.Text);
            
            // Initalizing Data Grid Views
            var _characters = new BindingListView<CharacterNameAndMoodDTO>(new List<CharacterNameAndMoodDTO>());
            var _beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
            var _goals = new BindingListView<GoalDTO>(new List<GoalDTO>());
            var _actions = new BindingListView<ActionRuleDTO>(new List<GoalDTO>());
            var _emotions = new BindingListView<AppraisalRuleDTO>(new List<AppraisalRuleDTO>());

            internalCharacterView.DataSource = _characters;
            dataGridViewBeliefs.DataSource = _beliefs;
            dataGridViewGoals.DataSource = _goals;
            dataGridViewReactiveActions.DataSource = _actions;
            dataGridViewEmotions.DataSource = _emotions;

            // Making sure everything doesn't get too crowded
            internalCharacterView.ColumnHeadersVisible = false;
            dataGridViewGoals.ColumnHeadersVisible = false;
            this.internalCharacterView.Columns["Mood"].Visible = false;
            this.dataGridViewGoals.Columns["Significance"].Visible = false;
            this.dataGridViewGoals.Columns["Likelihood"].Visible = false;
            this.dataGridViewBeliefs.Columns["Perspective"].Visible = false;
            this.dataGridViewBeliefs.Columns["Certainty"].Visible = false;

            this.dataGridViewReactiveActions.Columns["Priority"].Visible = false;
            this.dataGridViewReactiveActions.Columns["Layer"].Visible = false;
            this.dataGridViewReactiveActions.Columns["Id"].Visible = false;

            this.dataGridViewEmotions.Columns["Id"].Visible = false;


            LoadOutput();
        }

        private void outputForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadOutput()
        {
            // Characters
           var _characters = new BindingListView<CharacterNameAndMoodDTO>(new List<CharacterNameAndMoodDTO>());

            _characters.DataSource = parent._iatAux.Characters.Select(c =>
                        new CharacterNameAndMoodDTO
                        {
                            Name = c.CharacterName.ToString(),
                            Mood = c.Mood
                        }).ToList();

            internalCharacterView.DataSource = _characters;
            internalCharacterView.ClearSelection();
            internalCharacterView.Refresh();
            this.internalCharacterView_SelectionChanged(this,new EventArgs());

           //Actions
            if (parent.edmAux.GetAllActionRules().Count() > 0)
           {
                var _actions = new BindingListView<ActionRuleDTO>(new List<GoalDTO>());
                _actions.DataSource = parent.edmAux.GetAllActionRules().ToList();

                this.dataGridViewReactiveActions.DataSource = _actions;
                this.dataGridViewReactiveActions.Refresh();
            }

            //Actions
            if (parent.eaAux.GetAllAppraisalRules().Count() > 0)
            {
                var _emotions = new BindingListView<AppraisalRuleDTO>(new List<GoalDTO>());
                _emotions.DataSource = parent.eaAux.GetAllAppraisalRules().ToList();

                this.dataGridViewEmotions.DataSource = _emotions;
                this.dataGridViewEmotions.Refresh();
            }
        }   


        private void button1_Click(object sender, EventArgs e)
        {
            parent.AcceptedOutput(this.scenarioTextBox.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.RejectedOutput();
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            parent.ComputeOutput(this.scenarioTextBox.Text);
            this.LoadOutput();
        }

        private void OutputInformation_Click(object sender, EventArgs e)
        {
            
        }

        private void internalCharacterView_SelectionChanged(object sender, EventArgs e)
        {
            // Update Beliefs and Goals
            var beliefs = new BindingListView<BeliefDTO>(new List<BeliefDTO>());
            var goals = new BindingListView<GoalDTO>(new List<GoalDTO>());

            if (internalCharacterView.SelectedRows.Count > 0)
            {
                var rpc = parent._iatAux.Characters.ToList()[internalCharacterView.SelectedRows[0].Index];
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
                if (parent._iatAux.Characters.Count() > 0)
                {
                    var rpc = parent._iatAux.Characters.ToList()[0];
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

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
