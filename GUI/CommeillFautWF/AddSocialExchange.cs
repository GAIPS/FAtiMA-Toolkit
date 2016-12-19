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
        private SocialExchangeDTO _dto;

        private SocialExchangesVM _vm;

      

        public SocialExchangeDTO AddedObject { get; private set; } = null;

        public AddSocialExchange(SocialExchangesVM vm)
        {

            InitializeComponent();
            AddedObject = new SocialExchangeDTO();
           
            _vm = vm;

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

            
            if (social.InfluenceRules != null )
            {
              foreach (var cond in social.InfluenceRules)
                {
                 
                            this.listBox1.Items.Add(cond.RuleName);
                }
            }


            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        private void NameBox_Click(object sender, EventArgs e) {

             _dto = new SocialExchangeDTO()
            {
                
                Action = moveName.Text,
                Intent =  IntentTextBox.Text,
                Instantiation = InstantiationTextBox.Text,
                InfluenceRules = _vm._rules.Values.ToList()
            };
            
            try
            {
              
                AddedObject = _dto;
                _vm.AddSocialMove(_dto);
                MessageBox.Show("Added Social Exchange: " + _dto.Action);
               
            Close();
        }
        catch (Exception ex) {

       MessageBox.Show(ex.Message, Resources.ErrorDialogTitle , MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void button2_Click(object sender, EventArgs e)
        {
            new AddInfluenceRule(this._vm, new InfluenceRuleDTO()).ShowDialog();

            if (_vm._rules != null)
            {
                listBox1.Items.Clear();
                foreach (var move in _vm._rules)
                {
                    if (move.Value != null)
                        if (move.Value.RuleName != null)
                            this.listBox1.Items.Add(move.Value.RuleName);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
