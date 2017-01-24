using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommeillFaut.DTOs;
using CommeillFautWF.ViewModels;

namespace CommeillFautWF
{
    public partial class AddTriggerRule : Form
    {

        private InfluenceRuleDTO dto;

        public TriggerRulesVM Vm { get; private set; }

        public AddTriggerRule(TriggerRulesVM vm)
        {
            Vm = vm;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void AddTriggerRule_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            var socialDTO = new SocialExchangeDTO();

         var go =   new AddOrEditInfluenceRuleForm(new InfluenceRuleVM(new SocialExchangesVM(new BaseCIFForm()), socialDTO), new InfluenceRuleDTO()).ShowDialog();

            if (socialDTO.InfluenceRules != null)
            {
                dto = socialDTO.InfluenceRules.First();

               }
            
        }

        private void button1_Click(object sender, EventArgs e)

        {
            if (dto != null)
            {
            
                string condition = "" + textBox1.Text + "(" + textBox2.Text + "," + textBox3.Text + "," + numericUpDown1.Value + ")";

                Vm.AddTriggerRule(dto, condition);

                MessageBox.Show("Added Trigger Rule " + dto.RuleName + " effect: " + condition);

            }

            Close();
        }
    }
}
