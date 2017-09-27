using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using CommeillFautWF.ViewModels;
using Equin.ApplicationFramework;
using WellFormedNames;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
     

        private SocialExchangesVM _vm;
        private InfluenceRuleVM _influenceRuleVm;

      

        public SocialExchangeDTO AddedObject { get; set; } = null;

        public AddSocialExchange(SocialExchangesVM vm)
        {

            InitializeComponent();
            AddedObject = new SocialExchangeDTO();
            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);
            _vm = vm;
            AddedObject.InfluenceRule = new InfluenceRuleDTO();
            conditionSetEditorControl1.View = _influenceRuleVm.ConditionSetView;
        }

        public AddSocialExchange(SocialExchangesVM vm, SocialExchange social)
        {

            InitializeComponent();
            AddedObject = social.ToDTO();
        
            _vm = vm;
            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);
            if (social.ActionName != null)
            moveName.Text = social.ActionName.ToString();
            if (social.Intent != null)
                IntentTextBox.Text = social.Intent.ToString();

           
            AddedObject = social.ToDTO();

            conditionSetEditorControl1.View = _influenceRuleVm.ConditionSetView;

            button1.Text = (AddedObject.Id == Guid.Empty) ? "Add" : "Update";

        }

        private void NameBox_Click(object sender, EventArgs e)
        {


           SocialExchangeDTO  _dto = new SocialExchangeDTO()
            {
               
                Action = moveName.Text,
                Intent = IntentTextBox.Text,
                InfluenceRule = new InfluenceRuleDTO() 
                
            };


            if (AddedObject?.InfluenceRule != null)
                _dto.InfluenceRule.RuleConditions = _influenceRuleVm.ConditionSetView.GetData();

           // MessageBox.Show("We found that " + _dto.InfluenceRule.RuleConditions.ConditionSet.Count());

            try
            {
              
                AddedObject = _dto;
                _vm.AddSocialMove(_dto);
       //         MessageBox.Show("Added Social Exchange: " + _dto.Action);
          
            Close();
        }
        catch (Exception ex) {

       MessageBox.Show(ex.Message, Resources.ErrorDialogTitle , MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void AddSocialExchange_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void OnRuleSelectionChanged()
        {
    
        }


        private void button3_Click(object sender, EventArgs e)
        {
      
        }

     
    
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void influenceRuleBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void genericPropertyDataGridControler1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
    

      
    }
}
