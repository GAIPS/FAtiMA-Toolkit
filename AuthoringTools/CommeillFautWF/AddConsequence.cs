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
            var consequenceName = richTextBox1.Text.ToString();
            var affectedVar = richTextBox2.ToString();
            var seResult = richTextBox3.ToString();

            var add = new List<string>();
            if (affectedVar != "")
            add = new List<string>() { affectedVar };
            _dto.Effects.Add(seResult,add );

       //     MessageBox.Show(" uhm " + _dto.Effects.Count );

            Close();
        }
    }
}
