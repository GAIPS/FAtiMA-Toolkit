using System;
using System.Linq;
using CommeillFaut;
using CommeillFaut.DTOs;
using Equin.ApplicationFramework;
using System.Collections;
using GAIPS.AssetEditorTools;
using System.Windows.Forms;
using GAIPS.Rage;
using System.IO;

namespace CommeillFautWF
{
    public partial class MainForm : Form
    {
        private ConditionSetView conditions;
        private BindingListView<SocialExchangeDTO> _socialExchangeList;
        private CommeillFautAsset _loadedAsset;
        private AssetStorage _storage;
        private string _currentFilePath;

        public CommeillFautAsset Asset
        {
            get { return _loadedAsset; }
            set { _loadedAsset = value; OnAssetDataLoaded(); }
        }

        public MainForm()
        {
            InitializeComponent();
            _storage = new AssetStorage();
            _loadedAsset = CommeillFautAsset.CreateInstance(_storage);
            OnAssetDataLoaded();
        }

        protected void OnAssetDataLoaded()
        {
           this._socialExchangeList = new BindingListView<SocialExchangeDTO>((IList)null);
			gridSocialExchanges.DataSource = this._socialExchangeList;

            conditions = new ConditionSetView();
            conditionSetEditorControl.View = conditions;
            conditions.OnDataChanged += ConditionSetView_OnDataChanged;

            this._socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
            
            EditorTools.HideColumns(gridSocialExchanges, new[] {
            PropertyUtil.GetPropertyName<SocialExchangeDTO>(dto => dto.Id)});

            if (this._socialExchangeList.Any())
			{
				var ra = _loadedAsset.GetSocialExchange(this._socialExchangeList.First().Id);
				UpdateConditions(ra);
			}
            EditorTools.UpdateFormTitle("CiF-CK", _currentFilePath, this);
        }

        private void ConditionSetView_OnDataChanged()
        {
            var selectedRule = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(gridSocialExchanges);
            if (selectedRule == null)
                return;
            selectedRule.StartingConditions = conditions.GetData();
            _loadedAsset.AddOrUpdateExchange(selectedRule);
            this._socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
            this._socialExchangeList.Refresh();
        }

        private void buttonAddSE_Click(object sender, EventArgs e)
        {
            var dto = new SocialExchangeDTO()
            {
                Description = "-",
                Name = WellFormedNames.Name.BuildName("SE1"),
                Target = WellFormedNames.Name.BuildName("[t]"),
                Id = new Guid(),
                StartingConditions = new Conditions.DTOs.ConditionSetDTO(),
                Steps = "-",
                InfluenceRules = new System.Collections.Generic.List<InfluenceRuleDTO>()
            
            };

           this.auxAddOrUpdateItem(dto);
        }

        private void buttonEditSE_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(this.gridSocialExchanges);
            if (rule != null)
            {
                this.auxAddOrUpdateItem(rule);
            }
        }
        private void auxAddOrUpdateItem(SocialExchangeDTO item)
        {
            var diag = new AddSocialExchange(_loadedAsset, item);
            diag.ShowDialog(this);

           
            if (diag.UpdatedGuid != Guid.Empty)
            {
              //  _socialExchangeList.DataSource = LoadedAsset.GetAllSocialExchanges().ToList();

                    this._socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
            


                EditorTools.HighlightItemInGrid<SocialExchangeDTO>
                    (gridSocialExchanges, diag.UpdatedGuid);
            }
            _socialExchangeList.Refresh();
        }


        private void gridSocialExchanges_SelectionChanged(object sender, EventArgs e)
        {
            var se = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(this.gridSocialExchanges);
            if (se != null) conditions.SetData(se.StartingConditions);
            else conditions.SetData(null);
        }

        private void buttonDuplicateSE_Click(object sender, EventArgs e)
        {
            var r = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(
                this.gridSocialExchanges);

            if (r != null)
            {
                r.Id = Guid.Empty;
                var newRuleId = _loadedAsset.AddOrUpdateExchange(r);
                _socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
                EditorTools.HighlightItemInGrid<SocialExchangeDTO>(gridSocialExchanges, newRuleId);
            }
        }

        private void buttonRemoveSE_Click(object sender, EventArgs e)
        {
            var selRows = gridSocialExchanges.SelectedRows;
            if (selRows.Count == 0) return;
            foreach (var r in selRows.Cast<DataGridViewRow>())
            {
                var dto = ((ObjectView<SocialExchangeDTO>)r.DataBoundItem).Object;
                _loadedAsset.RemoveSocialExchange(dto.Id);
            }
            _socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
            EditorTools.HighlightItemInGrid<SocialExchangeDTO>(gridSocialExchanges, Guid.Empty);
        }

        private void gridSocialExchanges_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                buttonEditSE_Click(sender, e);
            }
        }

        private void gridSocialExchanges_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEditSE_Click(sender, e);
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

        	private void UpdateConditions(SocialExchangeDTO reaction)
		{
			conditions.SetData(reaction?.StartingConditions);
		}

        private void influenceRules_Click(object sender, EventArgs e)
        {
             var selectedRule = EditorTools.GetSelectedDtoFromTable<SocialExchangeDTO>(gridSocialExchanges);
            var newSE = new SocialExchange(selectedRule);
            var diag = new InfluenceRuleInspector(_loadedAsset, newSE);
            diag.ShowDialog(this);
            _loadedAsset.UpdateSocialExchange(newSE.ToDTO);
             _socialExchangeList.DataSource = _loadedAsset.GetAllSocialExchanges().ToList();
        }

        private void gridSocialExchanges_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void filieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = null;
            _storage = new AssetStorage();
            _loadedAsset = CommeillFautAsset.CreateInstance(_storage);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Asset Storage File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                try
                {
                    _currentFilePath = aux;
                    _storage = AssetStorage.FromJson(File.ReadAllText(_currentFilePath));
                    _loadedAsset = CommeillFautAsset.CreateInstance(_storage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath, _storage, _loadedAsset);
            EditorTools.UpdateFormTitle("CiF-CK", _currentFilePath, this);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var old = _currentFilePath;
            _currentFilePath = null;
            _currentFilePath = EditorTools.SaveFileDialog(_currentFilePath, _storage, _loadedAsset);
            if (_currentFilePath == null) _currentFilePath = old;
            EditorTools.UpdateFormTitle("CiF-CK", _currentFilePath, this);

        }
    }
}

    


      