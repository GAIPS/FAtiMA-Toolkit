using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using CommeillFaut;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;
using Conditions.DTOs;


namespace CommeillFautWF.ViewModels
{
	public class InfluenceRuleVM 
	{
	    public SocialExchangesVM _vm;
		private Guid _currentlySelected = Guid.Empty;
        private bool m_loading = false;
	    public SocialExchangeDTO _SocialExchangeDto;

        public InfluenceRuleDTO Rule { get; }
        public Guid Selection
        {
            get { return _currentlySelected; }
            set
            {
                if (_currentlySelected == value)
                    return;

                _currentlySelected = value;
                UpdateSelected();
            }
        }

        public ConditionSetView ConditionSetView { get; }


        public InfluenceRuleVM(SocialExchangesVM vm, SocialExchangeDTO dto)
        {
            _vm = vm;
          
            _SocialExchangeDto = dto;
            Rule = new InfluenceRuleDTO();
           
            if (dto?.InfluenceRule != null)

                Rule = dto.InfluenceRule;

            ConditionSetView = new ConditionSetView();
            ConditionSetView.SetData(Rule.RuleConditions);

            //      ConditionSetView.Conditions.AddNew();
//            ConditionSetView.Conditions.DataSource = dto.InfluenceRule.RuleConditions.ConditionSet;

  //          ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
		

		}

        public InfluenceRuleDTO CurrentlySelectedRule
        {
            get
            {
                if (_currentlySelected == Guid.Empty)
                    return null;

                var rule = Rule;
                if (rule == null)
                    throw new Exception("Influence rule not found");

                return rule;
            }
        }


        private void ConditionSetView_OnDataChanged()
        {
            if (m_loading)
                return;

            var rule = CurrentlySelectedRule;

            if (rule == null)
                return;

            rule.RuleConditions = ConditionSetView.GetData();
            _SocialExchangeDto.InfluenceRule.RuleConditions =rule.RuleConditions;
            _vm._mainForm.SetModified();
        }


        public void Reload()
        {
            m_loading = true;

            ConditionSetView.SetData(null);

            m_loading = false;
        }

     

        private void UpdateSelected()
        {
           

            m_loading = true;
            ConditionSetView.SetData(Rule.RuleConditions);
            m_loading = false;
        }

    }
}