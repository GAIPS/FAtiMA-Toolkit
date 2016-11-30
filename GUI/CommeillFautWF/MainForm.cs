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

namespace CommeillFautWF
{
    public partial class MainForm : BaseCIFForm
    {
        //    private AttributionRuleVM _attributionRulesVM;
        //   private ClaimsVM _claimsVM;
        //   private ConferralsVM _conferralsVM;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnAssetDataLoaded(CommeillFautAsset asset) 
        {

            label1.Text = CurrentAsset.ToString();
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
              
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AddSocialExchange(this.CurrentAsset).ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
    }

}

    


      