using System;
using System.Linq;
using CommeillFaut;
using CommeillFaut.DTOs;
using Equin.ApplicationFramework;
using System.Collections;
using GAIPS.AssetEditorTools;
using System.Windows.Forms;

namespace CommeillFautWF
{
    public partial class MainForm : BaseCIFForm
    {
        private ConditionSetView conditions;
        private BindingListView<SocialExchangeDTO> socialExchanges; 

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnAssetDataLoaded(CommeillFautAsset asset)
        {
            socialExchanges = new BindingListView<SocialExchangeDTO>((IList)null);
            gridSocialExchanges.DataSource = this.socialExchanges;
            conditionSetEditorControl.View = conditions;
            conditions = new ConditionSetView();
            conditionSetEditorControl.View = conditions;
            conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            socialExchanges.DataSource = LoadedAsset.GetSocialExchanges().ToList();
            
            EditorTools.HideColumns(gridSocialExchanges, new[] {
                PropertyUtil.GetPropertyName<SocialExchangeDTO>(dto => dto.Id),
                PropertyUtil.GetPropertyName<SocialExchangeDTO>(dto => dto.Conditions)});

            _wasModified = false;
        }

        private void ConditionSetView_OnDataChanged()
        {
            var selectedRule = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(gridSocialExchanges);
            if (selectedRule == null)
                return;
            selectedRule.Conditions = conditions.GetData();
            LoadedAsset.AddOrUpdateExchange(selectedRule);

            SetModified();
        }

        private void buttonAddSE_Click(object sender, EventArgs e)
        {
            var dto = new SocialExchangeDTO()
            {
                Description = "-",
                Name = WellFormedNames.Name.BuildName("SE1"),
                Initiator = WellFormedNames.Name.BuildName("[i]"),
                Target = WellFormedNames.Name.BuildName("[t]"),
            };
            new AddSocialExchange(LoadedAsset, gridSocialExchanges, dto).ShowDialog(this);
            SetModified();
        }

        private void buttonEditSE_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(this.gridSocialExchanges);
            if (rule != null)
            {
                new AddSocialExchange(LoadedAsset, gridSocialExchanges, rule).ShowDialog(this);
                SetModified();
            }
        }

        private void gridSocialExchanges_SelectionChanged(object sender, EventArgs e)
        {
            var se = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(this.gridSocialExchanges);
            if (se != null) conditions.SetData(se.Conditions);
            else conditions.SetData(null);
        }

        private void buttonDuplicateSE_Click(object sender, EventArgs e)
        {
            var r = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(
                this.gridSocialExchanges);

            if (r != null)
            {
                r.Id = Guid.Empty;
                var newRuleId = LoadedAsset.AddOrUpdateExchange(r);
                //EditorTools.RefreshTable( gridSocialExchanges, LoadedAsset.GetSocialExchanges().ToList(), newRuleId);
                SetModified();
            }
        }

        private void buttonRemoveSE_Click(object sender, EventArgs e)
        {
            var selRows = gridSocialExchanges.SelectedRows;
            if (selRows.Count == 0) return;
            foreach (var r in selRows.Cast<DataGridViewRow>())
            {
                var dto = ((ObjectView<SocialExchangeDTO>)r.DataBoundItem).Object;
                LoadedAsset.RemoveSocialExchange(dto.Id);
            }
            //EditorTools.RefreshTable(gridSocialExchanges,                LoadedAsset.GetSocialExchanges().ToList(), Guid.Empty);
            SetModified();
        }

        private void gridSocialExchanges_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonEditSE_Click(sender, e);
        }

        private void gridSocialExchanges_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonAddSE_Click(sender, e);
                    e.Handled = true;
                    break;
                case Keys.D:
                    if (e.Control) this.buttonDuplicateSE_Click(sender, e);
                    break;
                case Keys.Delete:
                    this.buttonRemoveSE_Click(sender, e);
                    break;
            }
        }

       
    }

}

    


      