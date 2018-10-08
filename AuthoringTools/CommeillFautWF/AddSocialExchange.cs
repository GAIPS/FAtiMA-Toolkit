using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using GAIPS.AssetEditorTools;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangeDTO dto;
        private CommeillFautAsset asset;
        public Guid UpdatedGuid { get; private set; }

        public AddSocialExchange(CommeillFautAsset asset, SocialExchangeDTO dto)
        {
            InitializeComponent();

            this.dto = dto;
            this.asset = asset;

            //Validators 
            EditorTools.AllowOnlyGroundedLiteral(nameTextBox);
            EditorTools.AllowOnlyVariable(wfNameTarget);

            nameTextBox.Value = dto.Name;
            textBoxDescription.Text = dto.Description;
            wfNameTarget.Value = dto.Target;


            stepsTextBox.Text = dto.Steps;

            
            buttonAdd.Text = (dto.Id == Guid.Empty) ? "Add" : "Update";
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
                dto.Name = nameTextBox.Value;
                dto.Description = textBoxDescription.Text;
                dto.Steps = stepsTextBox.Text;
                dto.Target = wfNameTarget.Value;
                dto.Id = asset.AddOrUpdateExchange(dto);
                UpdatedGuid = dto.Id;
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