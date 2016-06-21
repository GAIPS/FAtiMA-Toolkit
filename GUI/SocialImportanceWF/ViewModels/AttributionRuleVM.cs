using System.Collections;
using System.Linq;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using KnowledgeBase.DTOs.Conditions;
using SocialImportance.DTOs;

namespace SocialImportanceWF.ViewModels
{
	public class AttributionRuleVM
	{
		private BaseSIForm _parent;
		private int _currentlySelected = -1;
		private bool m_loading = false;

		public BindingListView<AttributionRuleDTO> RuleList { get; }
		public int Selection {
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

		private void ConditionSetView_OnDataChanged()
		{
			if(m_loading)
				return;

			if(_currentlySelected<0)
				return;

			var rule = RuleList[_currentlySelected].Object;
			rule.Conditions = ConditionSetView.GetData();
			_parent.CurrentAsset.UpdateAttributionRule(rule);
			_parent.SetModified();
		}

		public void Reload()
		{
			m_loading = true;

			RuleList.DataSource = _parent.CurrentAsset.GetAttributionRules().ToList();
			RuleList.Refresh();

			ConditionSetView.SetData(null);

			m_loading = false;
		}

		public void CreateNewAttributionRule()
		{
			var rule = new AttributionRuleDTO() {RuleName = "New Attribution Rule", Value = 1};
			
			var outRule = _parent.CurrentAsset.AddAttributionRule(rule);
			RuleList.DataSource.Add(outRule);
			RuleList.Refresh();
			_parent.SetModified();
		}

		private void UpdateSelected()
		{
			if(m_loading)
				return;

			if (_currentlySelected < 0)
			{
				ConditionSetView.SetData(null);
				return;
			}

			var rule = RuleList[_currentlySelected].Object;
			m_loading = true;
			ConditionSetView.SetData(rule.Conditions);
			m_loading = false;
		}
	}
}