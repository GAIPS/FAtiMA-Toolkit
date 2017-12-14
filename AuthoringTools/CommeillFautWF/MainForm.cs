﻿using System;
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
            genericPropertyDataGridControler1.GetColumnByName("Social Exchange")
            genericPropertyDataGridControler1.GetColumnByName("Initiator").Visible = false;
            genericPropertyDataGridControler1.GetColumnByName("Target").Visible = false;
            genericPropertyDataGridControler1.GetColumnByName("Id").Visible = false;
            genericPropertyDataGridControler1.GetColumnByName("Conditions").Visible = false;
            genericPropertyDataGridControler1.GetColumnByName("Priority").Visible = false;
            genericPropertyDataGridControler1.GetColumnByName("InfluenceRule").Visible = false;


            _triggerRules = new TriggerRulesVM(this, asset);
            genericPropertyDataGridControler2.DataController = _triggerRules;
            genericPropertyDataGridControler2.OnSelectionChanged += OnRuleSelectionChanged;
            conditionSetEditorControl1.View = _triggerRules.ConditionSetView;


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


       public override void Refresh()
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

        private void TriggerRulesDataGridController_Load(object sender, EventArgs e)
        {

        }

        private void genericPropertyDataGridControler2_Load(object sender, EventArgs e)
        {

        }
    }

}

    


      