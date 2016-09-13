using System;
using System.Collections.Generic;
using System.Linq;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;

namespace EmotionalAppraisalWF.ViewModels
{
    public class AppraisalRulesVM
    {
	    private readonly BaseEAForm _mainForm;
	    private EmotionalAppraisalAsset _emotionalAppraisalAsset => _mainForm.CurrentAsset;

        public BindingListView<AppraisalRuleDTO> AppraisalRules {get; private set; }
	    public ConditionSetView CurrentRuleConditions { get; }
        public Guid SelectedRuleId { get; set;}

		public AppraisalRulesVM(BaseEAForm form)
		{
			_mainForm = form;
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
			this.AppraisalRules.DataSource = _emotionalAppraisalAsset.GetAllAppraisalRules().ToList();
			this.AppraisalRules.Refresh();
			if (SelectedRuleId != Guid.Empty)
			{
				CurrentRuleConditions.SetData(_emotionalAppraisalAsset.GetAllAppraisalRuleConditions(SelectedRuleId));
			}
			else if (this.AppraisalRules.Count == 0)
			{
				CurrentRuleConditions.SetData(null);
			}
			_mainForm.SetModified();
		}

        public void ChangeCurrentRule(AppraisalRuleDTO rule)
        {
            if (rule != null)
            {
                this.SelectedRuleId = rule.Id;
				CurrentRuleConditions.SetData(_emotionalAppraisalAsset.GetAllAppraisalRuleConditions(SelectedRuleId));
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
    }
}
