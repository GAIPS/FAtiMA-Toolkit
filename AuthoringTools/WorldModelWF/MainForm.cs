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
            var b = 12;

            _wasModified = false;
        }
        #endregion


        private void buttonAddAttRule_Click(object sender, EventArgs e)
        {
           var ev =  new AddOrEditEventTemplateForm();
            ev.ShowDialog(this);
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void addEffectDTO_Click(object sender, EventArgs e)
        {
          var ef = new AddorEditEffect(this._wm, new EffectDTO());
            ef.ShowDialog(this);
        }
        
    }
}
