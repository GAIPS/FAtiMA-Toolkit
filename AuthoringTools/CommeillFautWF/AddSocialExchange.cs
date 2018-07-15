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

            if(dto.Steps.Count>0){
            foreach(var s in dto.Steps)
            stepsTextBox.Text += s.ToString() + ", ";

            stepsTextBox.Text = stepsTextBox.Text.ToString().Remove(stepsTextBox.Text.Length - 2);

            }
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
            var steps = stepsTextBox.Text.ToString().Split(',');
            
            List<WellFormedNames.Name> _steps = new List<WellFormedNames.Name>();

            foreach(var s in steps)
            {
                var text = s.ToString().Trim();
                if(text != "" && text != null && text != " ")
                _steps.Add((WellFormedNames.Name)text);
            }

            try
            {
                dto.Name = nameTextBox.Value;
                dto.Description = textBoxDescription.Text;
                dto.Steps = _steps;
                dto.Target = wfNameTarget.Value;
                UpdatedGuid = asset.AddOrUpdateExchange(dto);
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