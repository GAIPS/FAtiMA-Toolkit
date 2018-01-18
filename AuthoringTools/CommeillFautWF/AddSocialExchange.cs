using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using CommeillFautWF.ViewModels;
using Equin.ApplicationFramework;
using System;
using System.Windows.Forms;
using WellFormedNames;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangesVM _vm;
        
        public ObjectView<SocialExchangeDTO> AddedObject { get; set; } = null;

        public AddSocialExchange(SocialExchangesVM vm, SocialExchangeDTO seDto)
        {
            InitializeComponent();
            _vm = vm;
            if (seDto.Name != null)
                nameTextBox.Value = seDto.Name;
            if (seDto.Description != null)
                textBoxDescription.Text = seDto.Description;

            buttonAdd.Text = (seDto.Id == Guid.Empty) ? "Add" : "Update";
        }


        private void AddSocialExchange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddedObject.Object.Name = nameTextBox.Value;
                AddedObject.Object.Description = textBoxDescription.Text;
                _vm.AddOrUpdateSocialExchange(AddedObject.Object);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSocialExchange_Load(object sender, EventArgs e)
        {

        }
    }
}