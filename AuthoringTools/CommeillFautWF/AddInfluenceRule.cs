using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommeillFautWF.Properties;
using GAIPS.AssetEditorTools;
using CommeillFaut;
using CommeillFaut.DTOs;


namespace CommeillFautWF
{
    public partial class AddInfluenceRule : Form
    {

        private ConditionSetView conditions;
        private InfluenceRuleDTO _dto;
        private SocialExchange SE;
        public Guid UpdatedGuid { get; private set; }

        public AddInfluenceRule(SocialExchange _se, InfluenceRuleDTO dto)
        {
            InitializeComponent();

            _dto = dto;
            SE = _se;

            conditions = new ConditionSetView();
            conditionSetEditorControl.View = conditions;
            //conditions.OnDataChanged += ConditionSetView_OnDataChanged;
            conditions.SetData(dto.Rule);

            if(dto != null)
            {
                valueFieldBox.Value = dto.Value;

                if(dto.Mode != null)
                modeNameField.Value = dto.Mode;
            }

              buttonAdd.Text = (dto.Id == Guid.Empty) ? "Add" : "Update";
        
        }

     

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            var condSet = new Conditions.ConditionSet();

           
          var guid = new Guid();

            if(  _dto.Id != Guid.Empty)
                guid = _dto.Id;

            SE.AddInfluenceRule(new InfluenceRuleDTO(){
                Id = guid,
                Rule = conditions.GetData(),
                Value = valueFieldBox.Value,
                Mode = modeNameField.Value
            }
            );

            Close();

      
        }

        private void AddInfluenceRule_Load(object sender, EventArgs e)
        {

        }

        private void conditionSetEditorControl1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
