using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance.DTOs;

namespace SocialImportanceWF.ViewModels
{
	public class AttributionRuleVM : IDataGridViewController
	{
		private BaseSIForm _parent;
		private Guid _currentlySelected = Guid.Empty;
		private bool m_loading = false;

		public BindingListView<AttributionRuleDTO> RuleList { get; }
		public Guid Selection {
			get { return _currentlySelected; }
			set
			{
				if(_currentlySelected == value)
					return;

				_currentlySelected = value;
				UpdateSelected();
			}
		}

		public ConditionSetView ConditionSetView { get; }

		public AttributionRuleVM(BaseSIForm parent)
		{
			_parent = parent;
			RuleList = new BindingListView<AttributionRuleDTO>((IList)null);
			ConditionSetView = new ConditionSetView();
			ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
		}

		public AttributionRuleDTO CurrentlySelectedRule {
			get
			{
				if (_currentlySelected == Guid.Empty)
					return null;

				var rule = RuleList.FirstOrDefault(r => r.Id == _currentlySelected);
				if (rule == null)
					throw new Exception("Attribution rule not found");

				return rule;
			}
		}

		private void ConditionSetView_OnDataChanged()
		{
			if(m_loading)
				return;

			var rule = CurrentlySelectedRule;

			if (rule==null)
				return;

			rule.Conditions = ConditionSetView.GetData();
			_parent.LoadedAsset.UpdateAttributionRule(rule);
			_parent.SetModified();
		}

		public void Reload()
		{
			m_loading = true;

            var aux = _parent.LoadedAsset.GetAttributionRules().ToList();
            RuleList.DataSource = aux;
            RuleList.Refresh();

			if(CurrentlySelectedRule == null) 
                ConditionSetView.SetData(null);

			m_loading = false;
		}

		public ObjectView<AttributionRuleDTO> AddOrUpdateRule(AttributionRuleDTO dto)
		{
			if (dto.Id == Guid.Empty)
				dto = _parent.LoadedAsset.AddAttributionRule(dto);
			else
				_parent.LoadedAsset.UpdateAttributionRule(dto);

			_parent.SetModified();
			Reload();

			var index = RuleList.Find(PropertyUtil.GetPropertyDescriptor<AttributionRuleDTO>("Id"), dto.Id);
			return RuleList[index];
		}

		private void UpdateSelected()
		{
			if(m_loading)
				return;

			var rule = CurrentlySelectedRule;

			if (rule==null)
			{
				ConditionSetView.SetData(null);
				return;
			}

			m_loading = true;
			ConditionSetView.SetData(rule.Conditions);
			m_loading = false;
		}

		#region Implementation of IDataGridViewController

		public IList GetElements()
		{
			return RuleList;
		}

		public object AddElement()
		{ 
			var dto = new AttributionRuleDTO()
            {
                Description = "-",
                Value = WellFormedNames.Name.BuildName("[v]"),
                Target = WellFormedNames.Name.BuildName("[t]")
            };
			var dialog = new AddOrEditAttributionRuleForm(this, dto);
			dialog.ShowDialog(_parent);
			return dialog.AddedObject;
		}

		public object EditElement(object elementToEdit)
		{
            var dto = (elementToEdit as ObjectView<AttributionRuleDTO>).Object;
        	var dialog = new AddOrEditAttributionRuleForm(this, dto);
		    dialog.ShowDialog(_parent);
            return dialog.AddedObject;
		}


        public object DuplicateElement(object elementToDuplicate)
        {
            var dto = (elementToDuplicate as ObjectView<AttributionRuleDTO>).Object;
            dto.Id = Guid.Empty;
            return AddOrUpdateRule(dto);
        }

        public uint RemoveElements(IEnumerable<object> elementsToRemove)
		{
			uint count = 0;
			foreach (var dto in elementsToRemove.Cast<ObjectView<AttributionRuleDTO>>().Select(v => v.Object))
			{
				try
				{
					_parent.LoadedAsset.RemoveAttributionRuleById(dto.Id);
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
				_parent.SetModified();
			}

			return count;
		}


        #endregion
    }
}