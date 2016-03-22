using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AppraisalRulesVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<AppraisalRuleDTO> AppraisalRules {get;}
        public BindingListView<ConditionDTO> CurrentRuleConditions { get; set; }
        public bool IsRuleSelected { get; set;}

        public AppraisalRulesVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            
            this.AppraisalRules = new BindingListView<AppraisalRuleDTO>(ea.GetAllAppraisalRules().ToList());
            this.CurrentRuleConditions = new BindingListView<ConditionDTO>(new List<ConditionDTO>());
            this.IsRuleSelected = false;
        }

        public void ChangeCurrentRule(AppraisalRuleDTO rule)
        {
            if (rule != null)
            {
                this.CurrentRuleConditions = new BindingListView<ConditionDTO>(rule.Conditions.ToList());
                this.IsRuleSelected = true;
            }
        }

        public void AddAppraisalRule(AppraisalRuleDTO newRule)
        {
            _emotionalAppraisalAsset.AddAppraisalRule(newRule);
            AppraisalRules.DataSource.Add(newRule);
            AppraisalRules.Refresh();
        }


        public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
        {
            _emotionalAppraisalAsset.RemoveAppraisalRules(appraisalRules);
            foreach (var appraisalRuleDto in appraisalRules)
            {
                this.AppraisalRules.DataSource.Remove(appraisalRuleDto);
            }
            AppraisalRules.Refresh();
        }

    }
}
