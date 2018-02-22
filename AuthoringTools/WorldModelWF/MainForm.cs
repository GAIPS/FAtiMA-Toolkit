using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisal.DTOs;
using GAIPS.AssetEditorTools;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelWF
{
    public partial class MainForm : BaseWorldModelForm
    {
        private WorldModelAsset _wm;
        
        public MainForm()
        {
            InitializeComponent();
        }


        #region Overrides of BaseAssetForm<WorldModelAsset>

        protected override void OnAssetDataLoaded(WorldModelAsset asset)
        {
            _wm = asset;


            DataSet DS = new DataSet();

            
            DataTable table = new DataTable();
            table.TableName = "Table";
            table.Columns.Add("Type");
            table.Columns.Add("Subject");
            table.Columns.Add("Action");
            table.Columns.Add("Target");

            foreach (var ev in asset.GetAllEvents())
            {
                table.Rows.Add(ev.GetNTerm(1),ev.GetNTerm(2), ev.GetNTerm(3), ev.GetNTerm(4));
            }

            DS.Tables.Add(table);

            dataGridViewEventTemplates.DataSource = DS;
            this.dataGridViewEventTemplates.DataMember = "Table";

            dataGridViewEffects.DataSource = asset.GetAllEventEffects().SelectMany(x=>x.Value).ToList();
            dataGridViewEffects.Columns[0].Visible = false;


            _wasModified = false;
        }
        #endregion


        private void buttonAddEvent_Click(object sender, EventArgs e)
        {


        
                var ev = new AddOrEditEventTemplateForm(LoadedAsset, null);
                ev.ShowDialog(this);
            dataGridViewEventTemplates.Refresh();


        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void addEffectDTO_Click(object sender, EventArgs e)
        {
          var ef = new AddorEditEffect(LoadedAsset, new EffectDTO());
            ef.ShowDialog(this);

            dataGridViewEffects.Refresh();
        }

        private void dataGridViewEventTemplates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
