using CommeillFaut;
using CommeillFaut.DTOs;
using CommeillFautWF.Properties;
using GAIPS.AssetEditorTools;
using System;
using System.Linq;
using System.Windows.Forms;

namespace CommeillFautWF
{
    public partial class AddSocialExchange : Form
    {
        private SocialExchangeDTO dto;
        private CommeillFautAsset asset;
        private DataGridView table;

        public AddSocialExchange(CommeillFautAsset asset, DataGridView table,
          SocialExchangeDTO dto)
        {
            InitializeComponent();

            this.dto = dto;
            this.asset = asset;
            this.table = table;

            //Validators (TODO)
            nameTextBox.Value = dto.Name;
            textBoxDescription.Text = dto.Description;
            wfNameInitiator.Value = dto.Initiator;
            wfNameTarget.Value = dto.Target;

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
                dto.Target = wfNameTarget.Value;
                dto.Initiator = wfNameInitiator.Value;
                var id = asset.AddOrUpdateExchange(dto);
                EditorTools.RefreshTable(table, asset.GetSocialExchanges().ToList(), id);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}