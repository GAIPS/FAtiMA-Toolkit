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

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangeDTO _dto;
        private CommeillFautAsset _cif;

        public SocialExchangeDTO AddedObject { get; private set; } = null;

        public AddSocialExchange(CommeillFautAsset asset)
        {
            InitializeComponent();
            _cif = asset;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
           Close();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {

            MessageBox.Show(moveName.Name);
            _cif.ToString();
            try
            {
                MessageBox.Show(moveName.Text);
                var newMove = new SocialExchangeDTO()
                {
                   
                    SocialExchangeName = moveName.Text

                };
               
                _cif.AddSocialExchange(newMove);

            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.Close();
        }

        private void AddSocialExchange_Load(object sender, EventArgs e)
        {

        }

        private void SocialExchangeNameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
