using Equin.ApplicationFramework;
using IntegratedAuthoringTool.DTOs;
using IntegratedAuthoringToolWF.IEP;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegratedAuthoringToolWF.DialogueHelpers
{
    public partial class GPTOutputForm : Form
    {
        GenerateDialogue_Wizard _parent;
        OutputManager _manager;
        List<DialogueStateActionDTO> dialogueStateActions;

        public GPTOutputForm(GenerateDialogue_Wizard parent, OutputManager manager, string dialogueOutput)
        {
            InitializeComponent();
            dialogueStateActions = new List<DialogueStateActionDTO>();
            _parent = parent;
            _manager = manager;

            if (!dialogueOutput.Contains("Error"))
            {
                this.gptOutputTextBox.Text = dialogueOutput;

                // dialogueStateActions = _manager.ComputeGPTDialogues(dialogueOutput);
            }

            else
            {
                this.gptOutputTextBox.Text = "Error when processing input \n Please try again";
            }

            var _dialogues = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
            dataGridViewDialogueActions.DataSource = _dialogues;

            this.dataGridViewDialogueActions.Columns["UtteranceId"].Visible = false;
            this.dataGridViewDialogueActions.Columns["Id"].Visible = false;

            this.splitContainer1.Panel2.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void LoadOutput()
        {

            if (dialogueStateActions.Count() > 0)
            {
                var _dialogues = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
                _dialogues.DataSource = dialogueStateActions.ToList();

                this.dataGridViewDialogueActions.DataSource = _dialogues;
                this.dataGridViewDialogueActions.Refresh();

                this.splitContainer1.Panel2.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void processOutputButton_Click(object sender, EventArgs e)
        {
            Log.Information("Log:IEP_Dialogue_Output_\n%" + gptOutputTextBox.Text + "%");
            dialogueStateActions.AddRange(_manager.ComputeGPTDialogues(gptOutputTextBox.Text, _parent.nextStateBox.Checked, _parent.meaningBox.Checked, _parent.styleBox.Checked));
            this.LoadOutput();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var d in dialogueStateActions)
                _manager._mainIAT.AddDialogAction(d);
            Log.Information("Log:IEP_Dialogue_AcceptedOutput_");
            this.Close();
            _parent.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            //  parent.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var input = _parent.GetDialogues(this.gptOutputTextBox.Text);

            _parent._server.ProcessDialogues(input, this.HandleOutput);
        }

        public void HandleOutput()
        {
            if (_parent._server.dialogResult != "")
            {
                var result = _parent._server.dialogResult;
                this.gptOutputTextBox.Text += result;
            }
        }
    }
}
