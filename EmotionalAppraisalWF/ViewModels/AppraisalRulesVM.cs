using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AppraisalRulesVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<AppraisalRuleDTO> AppraisalRules {get;}
        public BindingListView<ConditionDTO> CurrentRuleConditions { get; set; }

        public AppraisalRulesVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            
            this.AppraisalRules = new BindingListView<AppraisalRuleDTO>(ea.GetAllAppraisalRules().ToList());
            this.CurrentRuleConditions = new BindingListView<ConditionDTO>(new List<ConditionDTO>());
        }

        public void ChangeCurrentRule(AppraisalRuleDTO rule)
        {
            if (rule != null)
            {
                this.CurrentRuleConditions = new BindingListView<ConditionDTO>(rule.Conditions.ToList());
            }
        }

        public void AddAppraisalRule(AppraisalRuleDTO newRule)
        {
            _emotionalAppraisalAsset.AddAppraisalRule(newRule);
            AppraisalRules.DataSource.Add(newRule);
            AppraisalRules.Refresh();
        }
        
    }
}
