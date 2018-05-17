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

            if (asset == null)
                return;
       
            _wm = asset;



            DataSet DS = new DataSet();

            
            DataTable table = new DataTable();
            table.TableName = "Table";
          //  table.Columns.Add("Type");

            table.Columns.Add("Action");
      
            table.Columns.Add("Subject");
            table.Columns.Add("Target");

            foreach (var ev in asset.GetAllEvents())
            {
                table.Rows.Add(ev.GetNTerm(3), ev.GetNTerm(2), ev.GetNTerm(4));
            }

            DS.Tables.Add(table);

            dataGridViewEventTemplates.DataSource = DS;
          
            this.dataGridViewEventTemplates.DataMember = "Table";

            dataGridViewEventTemplates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (asset.GetAllEvents().FirstOrDefault() != null)
            {
                dataGridViewEffects.DataSource = asset.GetAllEventEffects()[asset.GetAllEvents().FirstOrDefault()];
                dataGridViewEffects.Columns[0].Visible = false;
                dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }


            if (asset.GetAllEventEffects().Count == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                buttonRemoveAttRule.Enabled = false;
                buttonEditAttRule.Enabled = false;
                button4.Enabled = false;
                addEffectDTO.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                buttonRemoveAttRule.Enabled = true;
                buttonEditAttRule.Enabled = true;
                button4.Enabled = true;
                addEffectDTO.Enabled = true;
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
          var ef = new AddorEditEffect(LoadedAsset,eventTemp  , -1, new EffectDTO());
            ef.ShowDialog(this);
            SetModified();
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
               
                ds.Tables[0].Rows.Add(ev.GetNTerm(3), ev.GetNTerm(2), ev.GetNTerm(4));
            }

            RefreshEffects();

        }

        private void dataGridViewEventTemplates_SelectionChanged(object sender, EventArgs e)
        {

            var index = 0;

            if (dataGridViewEventTemplates.SelectedRows.Count > 0)
                index = dataGridViewEventTemplates.SelectedRows[0].Index;


            dataGridViewEffects.DataSource = null;
            if (LoadedAsset != null)
            {
                if (LoadedAsset.GetAllEventEffects().Count == 0)
                    return;
                var evt = LoadedAsset.GetAllEventEffects().Keys.ElementAt(index);

                if (LoadedAsset.GetAllEventEffects()[evt].Count > 0)
                {
                    dataGridViewEffects.DataSource = LoadedAsset.GetAllEventEffects().ElementAt(index).Value;
                    dataGridViewEffects.Columns[0].Visible = false;
                    dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                }

                else{
                        
                            button1.Enabled = false;
                            button2.Enabled = false;
                            button4.Enabled = false;
                    }
                   

                addEffectDTO.Enabled = true;
                }


            
            
        }

        public void RefreshEffects()
        {
            var index = 0;

            if (dataGridViewEventTemplates.SelectedRows.Count > 0)
                index = dataGridViewEventTemplates.SelectedRows[0].Index;


            dataGridViewEffects.DataSource = null;
            if (LoadedAsset != null)
            {
                if (LoadedAsset.GetAllEventEffects().Count == 0)
                    return;
                var evt = LoadedAsset.GetAllEventEffects().Keys.ElementAt(index);

                if (LoadedAsset.GetAllEventEffects()[evt].Count > 0)
                {
                    dataGridViewEffects.DataSource = LoadedAsset.GetAllEventEffects().ElementAt(index).Value;
                    dataGridViewEffects.Columns[0].Visible = false;
                    dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                }

                else{
                        
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                }
                   

                addEffectDTO.Enabled = true;
            }
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
             
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);


            
            var ev = new AddOrEditEventTemplateForm(LoadedAsset, eventTemp);
            ev.ShowDialog(this);
            SetModified();
            RefreshEventList();
        }

        private void button2_Click(object sender, EventArgs e) // Edit Effect
        {
          var  index2 = -1;

            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            var ef = new AddorEditEffect(LoadedAsset, eventTemp  , index2,  effect.ToDTO());

            ef.ShowDialog(this);

            SetModified();

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
               
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);


           LoadedAsset.RemoveEvent(eventTemp);   

            SetModified();

            RefreshEventList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

           LoadedAsset.RemoveEffect(eventTemp, effect);
            SetModified();
            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e) // Duplicate Effect
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = LoadedAsset.GetAllEvents().ElementAt(index);

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = LoadedAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            LoadedAsset.AddEventEffect(eventTemp, effect.ToDTO());
            SetModified();
            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void dataGridViewEffects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
