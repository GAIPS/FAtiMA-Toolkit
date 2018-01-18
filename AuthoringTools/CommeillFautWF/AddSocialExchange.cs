using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using CommeillFautWF.ViewModels;
using System;
using System.Windows.Forms;
using WellFormedNames;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangesVM _vm;
        public SocialExchangeDTO AddedObject { get; set; } = null;

        public AddSocialExchange(SocialExchangesVM vm, SocialExchangeDTO seDto)
        {
            InitializeComponent();
            AddedObject = seDto;
            _vm = vm;
            if (seDto.Name != null)
                nameTextBox.Value = seDto.Name;
            if (seDto.Description != null)
                textBoxDescription.Text = seDto.Description;

            buttonAdd.Text = (AddedObject.Id == Guid.Empty) ? "Add" : "Update";
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
                AddedObject.Name = nameTextBox.Value;
                AddedObject.Description = textBoxDescription.Text;
                AddedObject = _vm.AddOrUpdateSocialExchange(AddedObject);
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