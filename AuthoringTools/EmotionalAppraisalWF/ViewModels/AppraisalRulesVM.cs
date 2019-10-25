using System;
using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using AutobiographicMemory;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AppraisalRulesVM
    {
        private EmotionalAppraisalAsset _ea;
        public BindingListView<AppraisalRuleDTO> AppraisalRules {get; private set; }
	    public ConditionSetView CurrentRuleConditions { get; }
        public Guid SelectedRuleId { get; set;}

        public static readonly string[] EventTypes = { AMConsts.ACTION_END, AMConsts.ACTION_START, AMConsts.PROPERTY_CHANGE, "*" };

        public AppraisalRulesVM(EmotionalAppraisalAsset ea)
		{
			_ea = ea;
            this.AppraisalRules = new BindingListView<AppraisalRuleDTO>(new List<AppraisalRuleDTO>());
			this.CurrentRuleConditions = new ConditionSetView();
			this.CurrentRuleConditions.OnDataChanged += CurrentRuleConditions_OnDataChanged;
            this.SelectedRuleId = Guid.Empty;
            RefreshData();   
        }

		private void CurrentRuleConditions_OnDataChanged()
		{
			var rule = AppraisalRules.FirstOrDefault(a => a.Id == SelectedRuleId);
			if(rule == null)
				throw new Exception("Unable to alter curretly selected rule.");

			rule.Conditions = CurrentRuleConditions.GetData();
			AddOrUpdateAppraisalRule(rule);
		}

		private void RefreshData()
        {
			this.AppraisalRules.DataSource = _ea.GetAllAppraisalRules().ToList();
			this.AppraisalRules.Refresh();
			if (SelectedRuleId != Guid.Empty)
			{
				CurrentRuleConditions.SetData(_ea.GetAllAppraisalRuleConditions(SelectedRuleId));
			}
			else if (this.AppraisalRules.Count == 0)
			{
				CurrentRuleConditions.SetData(null);
			}
		}

        public void ChangeCurrentRule(AppraisalRuleDTO rule)
        {
            if (rule != null)
            {
                this.SelectedRuleId = rule.Id;
				CurrentRuleConditions.SetData(_ea.GetAllAppraisalRuleConditions(SelectedRuleId));
            }
        }

        public void AddOrUpdateAppraisalRule(AppraisalRuleDTO newRule)
        {
            _ea.AddOrUpdateAppraisalRule(newRule);
            RefreshData();
		}

        public void RemoveAppraisalRules(IEnumerable<AppraisalRuleDTO> appraisalRules)
        {
            _ea.RemoveAppraisalRules(appraisalRules);
            SelectedRuleId = Guid.Empty;
            RefreshData();
		}



     
    }
}
