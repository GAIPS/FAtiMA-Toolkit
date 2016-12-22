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
using WellFormedNames;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
     

        private SocialExchangesVM _vm;
        private InfluenceRuleVM _influenceRuleVm;

      

        public SocialExchangeDTO AddedObject { get; private set; } = null;

        public AddSocialExchange(SocialExchangesVM vm)
        {

            InitializeComponent();
            AddedObject = new SocialExchangeDTO();
           
            _vm = vm;
            _influenceRuleVm = new InfluenceRuleVM(vm);
            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        public AddSocialExchange(SocialExchangesVM vm, SocialExchange social)
        {

            InitializeComponent();
            AddedObject = new SocialExchangeDTO();

            _vm = vm;

            moveName.Text = social.ActionName.ToString();
            IntentTextBox.Text = social.Intent;
            InstantiationTextBox.Text = social.Instantiation;
            _influenceRuleVm = new InfluenceRuleVM(vm);
              
           
            if (social.InfluenceRules != null)
            {
              foreach (var cond in social.InfluenceRules)
                {
                            _influenceRuleVm.AddOrUpdateInfluenceRule(cond);
                            this.listBox1.Items.Add(cond.RuleName);
                }
            }

            
            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        private void NameBox_Click(object sender, EventArgs e)
        {

            MessageBox.Show(" count: " + _influenceRuleVm.RuleList.Count);

           SocialExchangeDTO  _dto = new SocialExchangeDTO()
            {
                SocialExchangeName = moveName.Text,
                Action = moveName.Text,
                Intent =  IntentTextBox.Text,
                Instantiation = InstantiationTextBox.Text,
                InfluenceRules = _influenceRuleVm.RuleList,
                
            };
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            new AddInfluenceRule(_influenceRuleVm, new InfluenceRuleDTO()).ShowDialog();

            if (_influenceRuleVm.RuleList != null)
            {
                listBox1.Items.Clear();
                foreach (var move in _influenceRuleVm.RuleList)
                {
                  
                            this.listBox1.Items.Add(move.RuleName);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
