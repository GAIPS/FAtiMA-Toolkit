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
using CommeillFautWF.ViewModels;

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
            this.SocialExchangeBox.Items.Clear();
            this.ExchangeList1.Text = "";

            if (asset?.m_SocialExchanges != null)
            {
                this.ExchangeList1.Text = "Number of Social Exchanges: " + asset.m_SocialExchanges.Count;
                
                foreach (var move in asset.m_SocialExchanges)
                {
                    if(move != null)
                        if(move.ActionName!=null)
                        this.SocialExchangeBox.Items.Add(move.ActionName);
                }
            
                foreach (var move in asset._TriggerRules._triggerRules.Keys)
                {
                    if (move?.RuleName != null)
                        this.TriggerRulesBox.Items.Add(move.RuleName);
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            int todelete_index = SocialExchangeBox.SelectedIndex;
            if (todelete_index >= 0)
            {
                SocialExchange todelete = LoadedAsset.m_SocialExchanges[todelete_index];

                LoadedAsset.RemoveSocialExchange(todelete);
            }
            Refresh();
        }

        private void SocialExchangeBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


            int toedit_index = SocialExchangeBox.SelectedIndex;

            if (toedit_index >= 0)
            {
                SocialExchange toEdit = LoadedAsset.m_SocialExchanges[toedit_index];

            

                new AddSocialExchange(this._socialExchangesVM, toEdit).ShowDialog();

            }
            Refresh();
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

            string toDeleteName = TriggerRulesBox.SelectedItem.ToString();

            if (toDeleteName != "")
            {
               

                LoadedAsset.RemoveTriggerRuleByName(toDeleteName);
            }
            Refresh();
        }

        private void EditTriggerRule_Click(object sender, EventArgs e)
        {

        }



        //    protected override void OnWillSaveAsset(CommeillFautAsset asset)
        //   {
        //      MessageBox.Show("alo alo");
        //     OnAssetDataLoaded(CurrentAsset);
        // }
    }

}

    


      