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
    public partial class AddConsequence : Form
    {

        private SocialExchangeDTO _dto;
        private SocialExchangesVM _vm;

        public AddConsequence(SocialExchangesVM vm, SocialExchangeDTO dto)
        {
            InitializeComponent();
           
            _dto = dto;
            _vm = vm;

       
        }

        public AddConsequence(SocialExchangesVM vm, SocialExchangeDTO dto, List<string> obj)
        {
            InitializeComponent();

            _dto = dto;
            _vm = vm;
            richTextBox1.Text = obj.ElementAt(0);
            richTextBox2.Text = obj.ElementAt(1);
            richTextBox3.Text = obj.ElementAt(2);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var consequenceName = richTextBox1.Text;
            var affectedVar = richTextBox3.Text;
            var seResult = richTextBox2.Text;

            var add = new List<string>();
            if (affectedVar != "")
                add = new List<string>() { consequenceName, affectedVar };
            _dto.Effects.Add(seResult,add );

            Close();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
