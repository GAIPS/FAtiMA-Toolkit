using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using CommeillFaut.DTOs;


namespace CommeillFautWF.ViewModels
{
	public class InfluenceRuleVM : IDataGridViewController
	{
		private BaseCIFForm _parent;
	    private SocialExchangesVM currentSocialMove;
		private Guid _currentlySelected = Guid.Empty;
		private bool m_loading = false;

		public BindingListView<InfluenceRuleDTO> RuleList { get; }
		public Guid Selection {
			get { return _currentlySelected; }
			set
			{
				if(_currentlySelected == value)
					return;

				_currentlySelected = value;
				//UpdateSelected();
			}
		}

		public ConditionSetView ConditionSetView { get; }

		public InfluenceRuleVM(BaseCIFForm parent)
		{
			_parent = parent;
			RuleList = new BindingListView<InfluenceRuleDTO>((IList)null);
			ConditionSetView = new ConditionSetView();
			ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
		}
        public InfluenceRuleVM(SocialExchangesVM _current)
        {
            currentSocialMove = _current;
            RuleList = new BindingListView<InfluenceRuleDTO>((IList)null);
            ConditionSetView = new ConditionSetView();
            ConditionSetView.OnDataChanged += ConditionSetView_OnDataChanged;
        }


        public InfluenceRuleDTO CurrentlySelectedRule {
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


            //rule.Conditions = ConditionSetView.GetData();
			//_parent.CurrentAsset.UpdateAttributionRule(rule);
			_parent.SetModified();
		}

		public void Reload()
		{
			m_loading = true;

		//	RuleList.DataSource = _parent.CurrentAsset.GetAttributionRules().ToList();
			RuleList.Refresh();

	//		ConditionSetView.SetData(null);

			m_loading = false;
		}

		public ObjectView<InfluenceRuleDTO> AddOrUpdateInfluenceRule(InfluenceRuleDTO dto)
		{
		    if (dto.Id == Guid.Empty)
		       currentSocialMove.AddInfluenceRule(dto);
			else
				currentSocialMove.AddInfluenceRule(dto);

			_parent.SetModified();
			Reload();

			var index = RuleList.Find(PropertyUtil.GetPropertyDescriptor<InfluenceRuleDTO>("Id"), dto.Id);
			return RuleList[index];
		}

	/*	private void UpdateSelected()
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
		} */

		#region Implementation of IDataGridViewController

		public IList GetElements()
		{
			return RuleList;
		}

		public object AddElement()
		{ 
			var dto = new InfluenceRuleDTO() { RuleName = "New Attribution Rule", Value = 1, Target = "-" };
			var dialog = new AddInfluenceRule(currentSocialMove, dto);
			dialog.ShowDialog(_parent);
			return dialog.AddedObject;
		}

		public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
		{
			List<object> result = new List<object>();
			foreach (var dto in elementsToEdit.Cast<ObjectView<InfluenceRuleDTO>>().Select(v => v.Object))
			{
				try
				{
					var dialog = new AddInfluenceRule(currentSocialMove, dto);
					dialog.ShowDialog(_parent);
					if (dialog.AddedObject!=null)
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
				//	_parent.CurrentAsset.RemoveInfluenceRule(dto.Id);
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