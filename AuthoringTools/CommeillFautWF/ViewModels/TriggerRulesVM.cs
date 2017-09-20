using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CommeillFaut;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;
using CommeillFautWF;
using WellFormedNames;

namespace CommeillFautWF.ViewModels
{
   public class TriggerRulesVM : IDataGridViewController
    {
      
        public readonly BaseCIFForm _mainForm;
        private bool m_loading;
        public CommeillFautAsset _cifAsset => _mainForm.LoadedAsset;
        public BindingListView<TriggerRulesDTO> _TriggerRulesDtos;
        private Guid _currentlySelected = Guid.Empty;

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

        public TriggerRulesVM(BaseCIFForm parent)
        {
            
            _mainForm = parent;
		    _TriggerRulesDtos = new BindingListView<TriggerRulesDTO>((IList)null);
            m_loading = false;
         
        }

        public TriggerRulesDTO CurrentlySelectedRule
        {
            get
            {
                if (_currentlySelected == Guid.Empty)
                    return null;

                var rule = _TriggerRulesDtos.FirstOrDefault(r => r.Equals(_currentlySelected));
                if (rule == null)
                    throw new Exception("Trigger rule not found");

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

        /*    rule._TriggerRules.Keys = ConditionSetView.GetData();
            _SocialExchangeDto.InfluenceRules.Find(x => x.RuleName == rule.RuleName).RuleConditions = rule.RuleConditions;
            _vm._mainForm.SetModified();*/
        }

        public TriggerRulesVM(BaseCIFForm parent, CommeillFautAsset asset)
        {
            _mainForm = parent;

            var uhm = new List<TriggerRulesDTO>();
            uhm.Add(asset._TriggerRules.ToDTO());


            _TriggerRulesDtos = new BindingListView<TriggerRulesDTO>(uhm);

            m_loading = false;
        }



        public void Reload()
        {
            m_loading = true;

     //       foreach (var trig in _cifAsset._TriggerRules._triggerRules)
      //          _TriggerRulesDtos.DataSource.Add(trig);


            _TriggerRulesDtos.Refresh();

     //       ConditionSetView.SetData(null);

            m_loading = false;
        }

        public void AddTriggerRule(InfluenceRuleDTO newRule, string cond)
        {


            _cifAsset._TriggerRules.UpdateTriggerRule(newRule, cond);

            _mainForm.SetModified();
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
            var conds = new List<Conditions.DTOs.ConditionSetDTO>();
            foreach (var cd in rule._TriggerRules.Keys)
                conds.Add(cd.RuleConditions);
            ConditionSetView.SetData(conds.FirstOrDefault());

            m_loading = false;
        }


        public object AddElement()
        {

            var dto = new TriggerRulesDTO();
            var dialog = new AddTriggerRule(this);
            dialog.ShowDialog();
            _mainForm.SetModified();
            _mainForm.Refresh();

            Reload();
            return dialog.AddedObject;

        }

        public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
        {
            List<object> result = new List<object>();
            foreach (var dto in elementsToEdit.Cast<ObjectView<TriggerRulesDTO>>().Select(v => v.Object))
            {
                try
                {
                    var dialog = new AddTriggerRule(this, dto);
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
            foreach (var dto in elementsToRemove.Cast<ObjectView<TriggerRulesDTO>>().Select(v => v.Object))
            {
                try
                {
                    RemoveTriggerRuleByObject(dto);

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
                _mainForm.SetModified();
            }

            return count;
        }

        public void RemoveTriggerRuleByObject(TriggerRulesDTO toRem)
        {
            var se = _cifAsset._TriggerRules._triggerRules.FirstOrDefault(a => a.Equals( toRem));
          
            _cifAsset.RemoveTriggerRule(se.Key);
            Reload();
        }

        public IList GetElements()
        {
            return _TriggerRulesDtos;
        }


    }
}


