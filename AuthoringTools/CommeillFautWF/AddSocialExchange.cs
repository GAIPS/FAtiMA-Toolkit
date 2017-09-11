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

//           AddedObject.Effects = new Dictionary<int, List<string>>();
            AddedObject.InfluenceRules = new List<InfluenceRuleDTO>();
        }

        public AddSocialExchange(SocialExchangesVM vm, SocialExchange social)
        {

            InitializeComponent();
            AddedObject = social.ToDTO();

            _vm = vm;

            moveName.Text = social.ActionName.ToString();
           

           
            AddedObject = social.ToDTO();

          

            //         _influenceRuleVm = new InfluenceRuleVM(vm, social.ActionName.ToString());

            if (social.InfluenceRules != null)
            {
                dataGridView2.DataSource = social.InfluenceRules;

            }

            if(AddedObject.Effects != null)
                dataGridView1.DataSource = AddedObject.Effects;

            //   else AddedObject.Effects = new Dictionary<int, List<string>>();

            //	NameBox.Text = (_dto. == Guid.Empty) ? "Add" : "Update";
        }

        private void NameBox_Click(object sender, EventArgs e)
        {


           SocialExchangeDTO  _dto = new SocialExchangeDTO()
            {
               
                Action = moveName.Text,
         
                InfluenceRules = new List<InfluenceRuleDTO>() 
                
            };

            if (AddedObject?.Effects?.Count > 0)
                _dto.Effects = AddedObject.Effects;

            if (AddedObject?.InfluenceRules?.Count > 0)
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

      

        private void button3_Click(object sender, EventArgs e)
        {
          /*  var key = (numericUpDown1.Value).ToString();

            if (AddedObject.Effects.ContainsKey(key))
                AddedObject.Effects[key].Add(textBox1.Text);
            else
            {
                var newList = new List<string>();
                newList.Add(textBox1.Text);
                AddedObject.Effects.Add(key, newList);
            }
            
            Reload();*/
        }

        public void Reload()
        {

            this.dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();

            if (AddedObject?.InfluenceRules != null)
            {
                dataGridView2.DataSource = AddedObject.InfluenceRules;
            }
            if (AddedObject?.Effects != null)
            {
                dataGridView1.DataSource = AddedObject.Effects;
            }



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void AddInfluenceRule_Click(object sender, EventArgs e)
        {

            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);

            
                new AddInfluenceRule(_influenceRuleVm, new InfluenceRuleDTO(), moveName.Text).ShowDialog();

            

          /*  AddedObject.InfluenceRules = _influenceRuleVm.RuleList.ToList();

           
         
            if (AddedObject.InfluenceRules != null)
            {
                foreach (var cond in AddedObject.InfluenceRules)
                {

                    this.listBox1.Items.Add(cond.RuleName);
                }
            }*/ 
            Reload();
        }

        private void EditInfluenceRule_Click(object sender, EventArgs e)
        {

            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);

            if (dataGridView1.SelectedCells.Count == 1)
            {
                InfluenceRule toEdit = (InfluenceRule)dataGridView1.SelectedCells[0].Value;
                new AddInfluenceRule(_influenceRuleVm, toEdit.ToDTO(), moveName.Text).ShowDialog();
            }


            Reload();
        }

        private void RemoveInfluenceRule_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count == 1)
            {
                var toDelete = dataGridView1.SelectedCells[0].Value;
                this.AddedObject.InfluenceRules.Remove(AddedObject.InfluenceRules.Find(x => x.RuleName.ToString() == toDelete.ToString()));

            }


            this.Refresh();
            
        }

        private void AddEffect_Click(object sender, EventArgs e)
        {


           
                new AddConsequence(this._vm, AddedObject).ShowDialog();
            
         
            Reload();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
