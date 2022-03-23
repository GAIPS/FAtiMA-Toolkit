using EmotionalAppraisal.DTOs;
using EmotionalAppraisal.OCCModel;
using EmotionalAppraisalWF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmotionalAppraisalWF
{
    public partial class OCCSimplex : Form
    {
        AppraisalRulesVM ruleVM;
        AppraisalRuleDTO appraisalRule;
        public OCCSimplex(AppraisalRulesVM _vm, AppraisalRuleDTO _appraisalRule)
        {
            InitializeComponent();
            emotionBox.DataSource = OCCEmotionType.Types;
            ruleVM = _vm;
            appraisalRule = _appraisalRule;

        }

        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            var emo = this.emotionBox.Text;

            var appVars = OCCEmotionType.getVariableFromEmotion(emo);

            List<AppraisalVariableDTO> appVarList = new List<AppraisalVariableDTO>();

            foreach(var appraisalVariable in appVars)
            {
               AppraisalVariableDTO dto = new AppraisalVariableDTO()
                {
                    Name = appraisalVariable.appraisalVariable,
                    Value = WellFormedNames.Name.BuildName(appraisalVariable.intensity),
                    Target = WellFormedNames.Name.BuildName(appraisalVariable.target)
                };

                appVarList.Add(dto);
            }



            appraisalRule.AppraisalVariables.appraisalVariables.AddRange(appVarList);
            ruleVM.AddOrUpdateAppraisalRule(appraisalRule);
            
            Close();
        }
    }
}
