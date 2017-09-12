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

      

        public SocialExchangeDTO AddedObject { get; private set; } = null;

        public AddSocialExchange(SocialExchangesVM vm)
        {

            InitializeComponent();
            AddedObject = new SocialExchangeDTO();
           
            _vm = vm;
    
            AddedObject.InfluenceRules = new List<InfluenceRuleDTO>();
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

            genericPropertyDataGridControler1.DataController = _influenceRuleVm;
            genericPropertyDataGridControler1.OnSelectionChanged += OnRuleSelectionChanged;

            conditionSetEditorControl1.View = _influenceRuleVm.ConditionSetView;

            button1.Text = (AddedObject.Id == Guid.Empty) ? "Add" : "Update";

            if (AddedObject.Effects != null)
                dataGridView1.DataSource = AddedObject.Effects;

         
        }

        private void NameBox_Click(object sender, EventArgs e)
        {


           SocialExchangeDTO  _dto = new SocialExchangeDTO()
            {
               
                Action = moveName.Text,
                Intent = IntentTextBox.Text,
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

        private void OnRuleSelectionChanged()
        {
            var obj = genericPropertyDataGridControler1.CurrentlySelected;
            if (obj == null)
            {
                _influenceRuleVm.Selection = Guid.Empty;
                return;
            }

            var dto = ((ObjectView<InfluenceRuleDTO>)obj).Object;
            _influenceRuleVm.Selection = dto.Id;
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

        private void AddInfluenceRule_Click(object sender, EventArgs e)
        {

            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);

            
                new AddOrEditInfluenceRuleForm(_influenceRuleVm, new InfluenceRuleDTO()).ShowDialog();

           
        }

        private void EditInfluenceRule_Click(object sender, EventArgs e)
        {

            _influenceRuleVm = new InfluenceRuleVM(_vm, AddedObject);

            if (dataGridView1.SelectedCells.Count == 1)
            {
                InfluenceRule toEdit = (InfluenceRule)dataGridView1.SelectedCells[0].Value;
                new AddInfluenceRule(_influenceRuleVm, toEdit.ToDTO(), moveName.Text).ShowDialog();
            }

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
    }
}
