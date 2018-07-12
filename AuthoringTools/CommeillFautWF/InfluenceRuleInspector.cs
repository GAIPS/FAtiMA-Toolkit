using System;
using System.Linq;
using System.Windows.Forms;
using CommeillFaut.DTOs;
using CommeillFaut;
using System.Collections;
using GAIPS.AssetEditorTools;
using System.Windows.Forms;
using Equin.ApplicationFramework;

namespace CommeillFautWF
{
    public partial class InfluenceRuleInspector : Form
    {

        private BindingListView<InfluenceRuleDTO> _influenceRuleList; 

        
        private SocialExchange dto;
        private CommeillFautAsset asset;
        public Guid UpdatedGuid { get; private set; }

        public InfluenceRuleInspector()
        {
            InitializeComponent();
        }

          public InfluenceRuleInspector(CommeillFautAsset asset, SocialExchange dto)
        {
            InitializeComponent();

            this._influenceRuleList = new BindingListView<InfluenceRuleDTO>((IList)null);
            gridInfluenceRules.DataSource = this._influenceRuleList;
            

            this.dto = dto;
            this.asset = asset;

            
         
            this._influenceRuleList.DataSource = dto.InfluenceRules.Select(x=>x.ToDTO()).ToList();

            EditorTools.HideColumns(gridInfluenceRules, new string[]{"Id" });

            label3.Text =dto.Name.ToString();
        }

        private void buttonAddInfluenceRule_Click(object sender, EventArgs e)
        {
              var dto = new InfluenceRuleDTO()
            {
              
               Rule = new Conditions.DTOs.ConditionSetDTO(),
               Value = 0
            
            };
              this.auxAddOrUpdateItem(dto);
        }

           private void auxAddOrUpdateItem(InfluenceRuleDTO item)
        {
            var diag = new AddInfluenceRule(dto, item);
            diag.ShowDialog(this);
           
                      this._influenceRuleList.DataSource = dto.InfluenceRules.Select(x=>x.ToDTO()).ToList();
            


                EditorTools.HighlightItemInGrid<InfluenceRuleDTO>
                    (gridInfluenceRules, diag.UpdatedGuid);
        

           
          this._influenceRuleList.Refresh();
          
        }

        private void InfluenceRuleInspector_Load(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void buttonEditInfluenceRule_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<InfluenceRuleDTO>(this.gridInfluenceRules);
            if (rule != null)
            {
                this.auxAddOrUpdateItem(rule);
            }
        }

        private void buttonDuplicateInfluenceRule_Click(object sender, EventArgs e)
        {
              var r = EditorTools.GetSelectedDtoFromTable<InfluenceRuleDTO>(
                this.gridInfluenceRules);

            if (r != null)
            {
                r.Id = Guid.Empty;
                var newRuleId = dto.AddInfluenceRule(r);

                  this._influenceRuleList.DataSource = dto.InfluenceRules.Select(x=>x.ToDTO()).ToList();
            
                EditorTools.HighlightItemInGrid<SocialExchangeDTO>(gridInfluenceRules, newRuleId);
            }


            this._influenceRuleList.Refresh();
        }

           private void gridInfluenceRules_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                buttonEditInfluenceRule_Click(sender, e);
            }
        }

        private void buttonRemoveInfluenceRule_Click(object sender, EventArgs e)
        {
            var ids = gridInfluenceRules.SelectedRows.Cast<DataGridViewRow>()
		        .Select(r => ((ObjectView<InfluenceRuleDTO>) r.DataBoundItem).Object.Id).ToList();

            foreach(var i in ids)
            {
            dto.InfluenceRules.Remove( dto.InfluenceRules.Find(x=>x.Id == i));
            }
			
     this._influenceRuleList.DataSource = dto.InfluenceRules.Select(x=>x.ToDTO()).ToList();

            _influenceRuleList.Refresh();
          
       
        }
    }
}
