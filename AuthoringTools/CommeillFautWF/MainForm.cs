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
using CommeillFautWF.ViewModels;
using Equin.ApplicationFramework;

namespace CommeillFautWF
{
    public partial class MainForm : BaseCIFForm
    {
        private SocialExchangesVM _socialExchangesVM;
        //   private ClaimsVM _claimsVM;
        //   private ConferralsVM _conferralsVM;

        private TriggerRulesVM _triggerRules;


        public MainForm()
        {

            InitializeComponent();
            _socialExchangesVM = new SocialExchangesVM(this);
            _triggerRules = new TriggerRulesVM(this);

        }

        protected override void OnAssetDataLoaded(CommeillFautAsset asset)
        {
            _socialExchangesVM = new SocialExchangesVM(this, asset);
            genericPropertyDataGridControler1.DataController = _socialExchangesVM;
            genericPropertyDataGridControler1.OnSelectionChanged += OnRuleSelectionChanged;


          
            if (asset?._TriggerRules._triggerRules != null)
            {
                dataGridView2.DataSource = asset._TriggerRules._triggerRules;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddSocialExchange(_socialExchangesVM).ShowDialog();

            Refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }




        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }


        private void Refresh()
        {
            OnAssetDataLoaded(this.LoadedAsset);
        }

      

       

        private void SocialExchangeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void TriggerRulesBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddTriggerRule_Click(object sender, EventArgs e)
        {
            new AddTriggerRule(this._triggerRules).ShowDialog();

            Refresh();
        }

        private void DeleteTriggerRule_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count == 1)
            {
                var toDelete = (InfluenceRuleDTO)dataGridView2.SelectedCells[0].Value;
                this.LoadedAsset._TriggerRules.RemoveTriggerRule(LoadedAsset._TriggerRules._triggerRules.ToList().Find(x => x.Key.RuleName.ToString() == toDelete.RuleName).Key);

            }


            this.Refresh();
            SetModified();
        }

        private void EditTriggerRule_Click(object sender, EventArgs e)
        {

            var toedit_index = dataGridView2.SelectedCells[0].RowIndex;

            if (toedit_index >= 0)
            {
                var toEdit = LoadedAsset._TriggerRules._triggerRules.ElementAt(toedit_index);
                new AddTriggerRule(new TriggerRulesVM(this)).ShowDialog();

            }
            Refresh();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void genericPropertyDataGridControler1_Load(object sender, EventArgs e)
        {

        }

        private void AddClick(object sender, EventArgs e)
        {

            new AddSocialExchange(this._socialExchangesVM).ShowDialog();

            Refresh();

        }

        private void OnRuleSelectionChanged()
        {
            var obj = genericPropertyDataGridControler1.CurrentlySelected;
            if (obj == null)
            {
                _socialExchangesVM.Selection = Guid.Empty;
                return;
            }

            var dto = ((ObjectView<SocialExchangeDTO>)obj).Object;
            _socialExchangesVM.Selection = dto.Id;
        }


        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void genericPropertyDataGridControler1_Load_1(object sender, EventArgs e)
        {

        }



        //    protected override void OnWillSaveAsset(CommeillFautAsset asset)
        //   {
        //      MessageBox.Show("alo alo");
        //     OnAssetDataLoaded(CurrentAsset);
        // }
    }

}

    


      