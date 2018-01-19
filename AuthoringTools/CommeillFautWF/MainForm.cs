using System;
using CommeillFaut;
using CommeillFautWF.ViewModels;

namespace CommeillFautWF
{
    public partial class MainForm : BaseCIFForm
    {
        private SocialExchangesVM _socialExchangesVM;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnAssetDataLoaded(CommeillFautAsset asset)
        {
            _socialExchangesVM = new SocialExchangesVM(this);
            /*socialExchangesDataView.DataController = _socialExchangesVM;
            socialExchangesDataView.OnSelectionChanged += OnRuleSelectionChanged;
            socialExchangesDataView.GetColumnByName("Initiator").Visible = false;
            socialExchangesDataView.GetColumnByName("Target").Visible = false;
            socialExchangesDataView.GetColumnByName("Id").Visible = false;
            socialExchangesDataView.GetColumnByName("Conditions").Visible = false;*/
        }

        public override void Refresh()
        {
            OnAssetDataLoaded(this.LoadedAsset);
        }

        private void OnRuleSelectionChanged()
        {
            /*var obj = socialExchangesDataView.CurrentlySelected;
            if (obj == null)
            {
                _socialExchangesVM.Selection = Guid.Empty;
                return;
            }

            var dto = ((ObjectView<SocialExchangeDTO>)obj).Object;
            _socialExchangesVM.Selection = dto.Id;*/
        }

        private void socialExchangesDataView_Load(object sender, EventArgs e)
        {

        }
    }

}

    


      