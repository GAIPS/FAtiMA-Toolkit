using System;
using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using KnowledgeBase.Conditions;
using KnowledgeBase.DTOs.Conditions;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AppraisalRulesVM
    {
        private EmotionalAppraisalAsset _emotionalAppraisalAsset;

        public BindingListView<AppraisalRuleDTO> AppraisalRules {get; private set; }
        public BindingListView<string> CurrentRuleConditions { get; set; }
        public Guid SelectedRuleId { get; set;}

        public string[] QuantifierTypes = Enum.GetNames(typeof(LogicalQuantifier));

		public AppraisalRulesVM(EmotionalAppraisalAsset ea)
        {
            _emotionalAppraisalAsset = ea;
            this.AppraisalRules = new BindingListView<AppraisalRuleDTO>(new List<AppraisalRuleDTO>());
            this.CurrentRuleConditions = new BindingListView<string>(new List<string>());
            this.SelectedRuleId = Guid.Empty;
            RefreshData();   
        }

        private void RefreshData()
        {
            this.AppraisalRules.DataSource = _emotionalAppraisalAsset.GetAllAppraisalRules().ToList();
            this.AppraisalRules.Refresh();
            if (SelectedRuleId != Guid.Empty)
            {
	            this.CurrentRuleConditions.DataSource =
		            _emotionalAppraisalAsset.GetAllAppraisalRuleConditions(SelectedRuleId).ConditionSet;

            }else if (this.AppraisalRules.Count == 0)
            {
                this.CurrentRuleConditions.DataSource = new List<string>();
            }
            this.CurrentRuleConditions.Refresh();
        }

        public void ChangeCurrentRule(AppraisalRuleDTO rule)
        {
            if (rule != null)
            {
                this.SelectedRuleId = rule.Id;
	            this.CurrentRuleConditions.DataSource =
		            _emotionalAppraisalAsset.GetAllAppraisalRuleConditions(SelectedRuleId).ConditionSet;
                this.CurrentRuleConditions.Refresh();
            }
        }

        public void AddOrUpdateAppraisalRule(AppraisalRuleDTO newRule)
        {
            _emotionalAppraisalAsset.AddOrUpdateAppraisalRule(newRule);
            RefreshData();
        }

        public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
        {
            _emotionalAppraisalAsset.RemoveAppraisalRules(appraisalRules);
            SelectedRuleId = Guid.Empty;
            RefreshData();
        }

        public void AddCondition(string newCondition)
        {
            _emotionalAppraisalAsset.AddAppraisalRuleCondition(SelectedRuleId, newCondition);
            RefreshData();
        }

        public void UpdateCondition(string oldCondition, string updatedCondition)
        {
            _emotionalAppraisalAsset.RemoveAppraisalRuleCondition(SelectedRuleId, oldCondition);
            _emotionalAppraisalAsset.AddAppraisalRuleCondition(SelectedRuleId, updatedCondition);
            RefreshData();
        }
        
        public void RemoveConditions(IList<String> conditionsToRemove)
        {
            foreach (var condition in conditionsToRemove)
            {
                _emotionalAppraisalAsset.RemoveAppraisalRuleCondition(SelectedRuleId, condition);
            }
            RefreshData();
        }
    }
}
