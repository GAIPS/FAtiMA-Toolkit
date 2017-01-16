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
       //     _influenceRuleVm = new InfluenceRuleVM(vm, "ay");
            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        public AddSocialExchange(SocialExchangesVM vm, SocialExchange social)
        {

            InitializeComponent();
            AddedObject = social.ToDTO();

            _vm = vm;

            moveName.Text = social.ActionName.ToString();
            IntentTextBox.Text = social.Intent;
            InstantiationTextBox.Text = social.Instantiation;

           
            AddedObject = social.ToDTO();

            MessageBox.Show("AddSocialExchange Constructor " + AddedObject.Action);

            //         _influenceRuleVm = new InfluenceRuleVM(vm, social.ActionName.ToString());

            if (social.InfluenceRules != null)
            {
              foreach (var cond in social.InfluenceRules)
              {
             
                        this.listBox1.Items.Add(cond.RuleName);
                }
            }

            
            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        private void NameBox_Click(object sender, EventArgs e)
        {


           SocialExchangeDTO  _dto = new SocialExchangeDTO()
            {
               
                Action = moveName.Text,
                Intent =  IntentTextBox.Text,
                Instantiation = InstantiationTextBox.Text,
                InfluenceRules = new List<InfluenceRuleDTO>()
            };

            if (AddedObject!= null)
                if(AddedObject.InfluenceRules != null)
                if(AddedObject.InfluenceRules.Count > 0)
                _dto.InfluenceRules = AddedObject.InfluenceRules;
         

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
           
            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);

            if (listBox1.SelectedItem == null)
                new AddInfluenceRule(_influenceRuleVm, new InfluenceRuleDTO(), moveName.Text).ShowDialog();
            else
            {
                new AddInfluenceRule(_influenceRuleVm, AddedObject.InfluenceRules[listBox1.SelectedIndex], moveName.Text).ShowDialog();

            }

            AddedObject.InfluenceRules = _influenceRuleVm.RuleList.ToList();

            MessageBox.Show(" Button2 Click" + AddedObject.Action + " count " + AddedObject.InfluenceRules.Count);
          //  _vm.AddSocialMove(AddedObject);
            listBox1.Items.Clear();



            if (AddedObject.InfluenceRules != null)
            {
                foreach (var cond in AddedObject.InfluenceRules)
                {
                    
                    this.listBox1.Items.Add(cond.RuleName);
                }
            }
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
    }
}
