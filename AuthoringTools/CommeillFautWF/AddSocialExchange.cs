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

        public AddSocialExchange(SocialExchangesVM vm)
        {
            InitializeComponent();
            AddedObject = new SocialExchangeDTO();

            _vm = vm;
        }

        public AddSocialExchange(SocialExchangesVM vm, SocialExchange se)
        {
            InitializeComponent();
            AddedObject = se.ToDTO();
            _vm = vm;
            if (se.Name != null)
                nameTextBox.Value = se.Name;
            if (se.Description != null)
                textBoxDescription.Text = se.Description;

            AddedObject = se.ToDTO();

            button1.Text = (AddedObject.Id == Guid.Empty) ? "Add" : "Update";
        }

   /*     private void NameBox_Click(object sender, EventArgs e)
        {
            SocialExchangeDTO _dto = new SocialExchangeDTO()
            {
                Name = (Name)moveName.Text,
                Description = IntentTextBox.Text,
            };

            try
            {
                AddedObject = _dto;
                _vm.AddSocialMove(_dto);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }*/

        private void AddSocialExchange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}