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
	public class InfluenceRuleVM : IDataGridViewController
	{
	    public SocialExchangesVM _vm;
		private Guid _currentlySelected = Guid.Empty;
        private bool m_loading = false;
	    public SocialExchangeDTO _SocialExchangeDto;

        public BindingListView<InfluenceRuleDTO> RuleList { get; }
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
            RuleList = new BindingListView<InfluenceRuleDTO>((IList)null);

            if(dto?.InfluenceRules != null)

                foreach (var rule in _SocialExchangeDto.InfluenceRules)
                {
                    RuleList.DataSource = _SocialExchangeDto.InfluenceRules;
                }

            ConditionSetView = new ConditionSetView();
            ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
		

		}

        public InfluenceRuleDTO CurrentlySelectedRule
        {
            get
            {
                if (_currentlySelected == Guid.Empty)
                    return null;

                var rule = RuleList.FirstOrDefault(r => r.Id == _currentlySelected);
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
            _SocialExchangeDto.InfluenceRules.Find(x => x.RuleName == rule.RuleName).RuleConditions =rule.RuleConditions;
            _vm._mainForm.SetModified();
        }


        public void Reload()
        {
            m_loading = true;

           RuleList.DataSource = _SocialExchangeDto.InfluenceRules;
            RuleList.Refresh();

            ConditionSetView.SetData(null);

            m_loading = false;
        }

        public ObjectView<InfluenceRuleDTO> AddOrUpdateInfluenceRule(InfluenceRuleDTO dto)
        {

            if (dto.Id == Guid.Empty)
            {
                var at = new InfluenceRule(dto);
                dto = at.ToDTO();
            }

         

            if(_SocialExchangeDto.InfluenceRules == null)
                _SocialExchangeDto.InfluenceRules = new List<InfluenceRuleDTO>();
            if (_SocialExchangeDto.InfluenceRules.Find(x => x.RuleName == dto.RuleName) != null)
            {
                _SocialExchangeDto.InfluenceRules.Remove(_SocialExchangeDto.InfluenceRules.Find(x => x.RuleName == dto.RuleName));
                _SocialExchangeDto.InfluenceRules.Add(dto);
            }
                    
             else   _SocialExchangeDto.InfluenceRules.Add(dto);
          
            _vm._mainForm.SetModified();
            Reload();


            var index = RuleList.Find(PropertyUtil.GetPropertyDescriptor<InfluenceRuleDTO>("Id"), dto.Id);

            return RuleList[index];

        }


        private void UpdateSelected()
        {
            if (m_loading)
                return;

            var rule = CurrentlySelectedRule;

            if (rule == null)
            {
                ConditionSetView.SetData(null);
                return;
            }

            m_loading = true;
            ConditionSetView.SetData(rule.RuleConditions);
            m_loading = false;
        }


        #region Implementation of IDataGridViewController

        public IList GetElements()
        {
            return RuleList;
        }

        public object AddElement()
        {
          
            var dto = new InfluenceRuleDTO();
            var dialog = new AddOrEditInfluenceRuleForm(this, dto);
            dto.RuleConditions = new ConditionSetDTO();

            dialog.ShowDialog();
            return dialog.AddedObject;

        }

        public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
        {
            List<object> result = new List<object>();
            foreach (var dto in elementsToEdit.Cast<ObjectView<InfluenceRuleDTO>>().Select(v => v.Object))
            {
                try
                {
                    var dialog = new AddOrEditInfluenceRuleForm(this, dto);
                   dialog.ShowDialog();
                    if (dialog.AddedObject != null)
                        result.Add(dialog.AddedObject);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return result;
        }

        public uint RemoveElements(IEnumerable<object> elementsToRemove)
        {
            uint count = 0;
            foreach (var dto in elementsToRemove.Cast<ObjectView<InfluenceRuleDTO>>().Select(v => v.Object))
            {
                try
                {
                    RemoveInfluenceRuleById(dto.Id);
                    count++;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (count > 0)
            {
                Reload();
                _vm._mainForm.SetModified();
            }

            return count;
        }

        public void RemoveInfluenceRuleById(Guid id)
        {
            var rule = RuleList.FirstOrDefault(a => a.Id == id);
            if (rule == null)
                throw new Exception("Influence rule not found");

           
        }

        #endregion
    }
}