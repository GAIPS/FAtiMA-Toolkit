using EmotionalAppraisalWF.ViewModels;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using System.Collections.Generic;
using EmotionalAppraisal.OCCModel;
using System;

namespace EmotionalAppraisalWF
{
    public partial class AddOrEditAppraisalVariablesForm : Form
    {
        AppraisalRulesVM _vm;
        //  AppraisalVariables _toEdit;
        AppraisalRuleDTO _selectedRule;

        public BindingListView<AppraisalVariableDTO> appraisalVariables { get; private set; }

        public AddOrEditAppraisalVariablesForm(AppraisalRulesVM appRulesVM, AppraisalRuleDTO selectedRule)
        {
            InitializeComponent();

            this.appraisalVariables = new BindingListView<AppraisalVariableDTO>(new List<AppraisalVariableDTO>());

            _vm = appRulesVM;
            this.dataGridViewAppraisalVariables.AutoGenerateColumns = true;
            _selectedRule = selectedRule;
            if (selectedRule == null) return;
            if (_selectedRule.AppraisalVariables == null)
                _selectedRule.AppraisalVariables = new AppraisalVariables(new System.Collections.Generic.List<AppraisalVariableDTO>());


            this.appraisalVariables.DataSource = null;
            this.appraisalVariables.DataSource = selectedRule.AppraisalVariables.appraisalVariables;

            this.dataGridViewAppraisalVariables.DataSource = this.appraisalVariables;
            this.dataGridViewAppraisalVariables.Refresh();

        }

        private void groupBox7_Enter(object sender, System.EventArgs e)
        {

        }

        private void buttonAddAppraisalRule_Click(object sender, System.EventArgs e)
        {


            new AddOrEditAppraisalVariableForm(_vm, _selectedRule, new AppraisalVariableDTO()).ShowDialog(this);


            this.appraisalVariables.DataSource = _selectedRule.AppraisalVariables.appraisalVariables;
            this.appraisalVariables.Refresh();

        }


        public void buttonEditAppraisalRule_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAppraisalVariables.SelectedRows.Count > 0) {


                var selectedAppVar = _selectedRule.AppraisalVariables.appraisalVariables.Find(x => x.Name.ToString() == dataGridViewAppraisalVariables.SelectedRows[0].Cells[0].Value.ToString());


                if (selectedAppVar != null)

                    new AddOrEditAppraisalVariableForm(_vm, _selectedRule, selectedAppVar).ShowDialog(this);

            }
            else
                new AddOrEditAppraisalVariableForm(_vm, _selectedRule, new AppraisalVariableDTO()).ShowDialog(this);


            this.appraisalVariables.DataSource = _selectedRule.AppraisalVariables.appraisalVariables;
            this.appraisalVariables.Refresh();

        }

        public void dataGridViewAppraisalVariables_CellContentClick(object sender, EventArgs e)
        {
            if (dataGridViewAppraisalVariables.SelectedCells.Count == 0) return;

            var text = "";
        
            var emotions = OCCEmotionType.getEmotionsFromRule(_selectedRule);

          
            foreach(var emo in emotions)
            {
                text += emo.Name + " | ";
            }

            EmotionsLabel.Text = text;


        }

        
        private void buttonRemoveAppraisalRule_Click(object sender, System.EventArgs e)
        {
            if(dataGridViewAppraisalVariables.SelectedRows.Count > 0){
       
            var selectedAppVar = _selectedRule.AppraisalVariables.appraisalVariables.Find(x=>x.Name.ToString() == dataGridViewAppraisalVariables.SelectedRows[0].Cells[0].Value.ToString());


            _selectedRule.AppraisalVariables.appraisalVariables.Remove(selectedAppVar);

            this.appraisalVariables.DataSource = _selectedRule.AppraisalVariables.appraisalVariables;
            this.appraisalVariables.Refresh();
            }
        }

        private void fatimaLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://fatima-toolkit.eu/5-emotional-appraisal/");

        }

        private void dataGridViewAppraisalVariables_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void imageButton_Click(object sender, EventArgs e)
        {
            new OCCModelForm().ShowDialog();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new OCCSimplex(this._vm, _selectedRule).ShowDialog();
            this.appraisalVariables.Refresh();
            dataGridViewAppraisalVariables_CellContentClick(sender, e);
        }
    }
}
