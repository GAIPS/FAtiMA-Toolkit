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


        public MainForm()
        {

            InitializeComponent();
            _socialExchangesVM = new SocialExchangesVM(this);
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

     
        protected override void OnWillSaveAsset(CommeillFautAsset asset)
        {
         Refresh();
            

        }

        private void Refresh()
        {
            OnAssetDataLoaded(this.CurrentAsset);
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
                SocialExchange todelete = CurrentAsset.m_SocialExchanges[todelete_index];

                CurrentAsset.RemoveSocialExchange(todelete);
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
                SocialExchange toEdit = CurrentAsset.m_SocialExchanges[toedit_index];

                new AddSocialExchange(_socialExchangesVM, toEdit).ShowDialog();

            }
            Refresh();
        }



        //    protected override void OnWillSaveAsset(CommeillFautAsset asset)
        //   {
        //      MessageBox.Show("alo alo");
        //     OnAssetDataLoaded(CurrentAsset);
        // }
    }

}

    


      