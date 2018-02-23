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

            if (asset.GetAllEvents().FirstOrDefault() != null)
            {
                dataGridViewEffects.DataSource = asset.GetAllEventEffects()[asset.GetAllEvents().FirstOrDefault()];
                dataGridViewEffects.Columns[0].Visible = false;
            }

            _wasModified = false;
        }
        #endregion


        private void buttonAddEvent_Click(object sender, EventArgs e)
        {


        
                var ev = new AddOrEditEventTemplateForm(LoadedAsset, null);
                ev.ShowDialog(this);
            RefreshEventList();


        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void addEffectDTO_Click(object sender, EventArgs e)
        {
        
         var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);
          var ef = new AddorEditEffect(LoadedAsset,eventTemp  ,new EffectDTO());
            ef.ShowDialog(this);

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void dataGridViewEventTemplates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void RefreshEventList()
        {
            var ds = (DataSet)dataGridViewEventTemplates.DataSource;

            ds.Tables[0].Clear();


            foreach (var ev in LoadedAsset.GetAllEvents())
            {
               
                ds.Tables[0].Rows.Add(ev.GetNTerm(1),ev.GetNTerm(2), ev.GetNTerm(3), ev.GetNTerm(4));
            }

        }

        private void dataGridViewEventTemplates_SelectionChanged(object sender, EventArgs e)
        {

            var index = 0;

            if (dataGridViewEventTemplates.SelectedRows.Count > 0)
                index = dataGridViewEventTemplates.SelectedRows[0].Index;


            dataGridViewEffects.DataSource = null;
            if (LoadedAsset != null)
            {
                if (LoadedAsset.GetAllEventEffects().Count > 0)
                {
                    dataGridViewEffects.DataSource = LoadedAsset.GetAllEventEffects().ElementAt(index).Value;
                    dataGridViewEffects.Columns[0].Visible = false;
                }
            }
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
             
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);


            
            var ev = new AddOrEditEventTemplateForm(LoadedAsset, eventTemp);
            ev.ShowDialog(this);
            RefreshEventList();
        }

        private void button2_Click(object sender, EventArgs e) // Edit Effect
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            var ef = new AddorEditEffect(LoadedAsset,eventTemp  , effect.ToDTO());

            ef.ShowDialog(this);

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
               
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);


           LoadedAsset.RemoveEvent(eventTemp);   
         
            RefreshEventList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

           LoadedAsset.RemoveEffect(eventTemp, effect);

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e) // Duplicate Effect
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            LoadedAsset.AddEventEffect(eventTemp, effect.ToDTO());

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }
    }
}
