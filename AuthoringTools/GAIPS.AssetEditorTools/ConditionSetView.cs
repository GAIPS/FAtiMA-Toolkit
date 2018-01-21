using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Conditions;
using Conditions.DTOs;
using Equin.ApplicationFramework;

namespace GAIPS.AssetEditorTools
{
	public class ConditionSetView
	{
		public static readonly LogicalQuantifier[] QuantifierTypes = (LogicalQuantifier[])Enum.GetValues(typeof (LogicalQuantifier));

		public event Action OnRefresh;
		public event Action OnDataChanged;

		public bool HasData { get; private set; }
		public BindingListView<ConditionHolder> Conditions { get; }
		public LogicalQuantifier Quantifier { get; private set; }

		private bool _loading = false;

		public ConditionSetView()
		{
			Conditions = new BindingListView<ConditionHolder>((IList)null);
		}

		public void SetData(ConditionSetDTO dto)
		{
			_loading = true;

			Conditions.DataSource = null;
			Quantifier = LogicalQuantifier.Existential;
			HasData = dto!=null;

			if (HasData)
			{
				if (dto.ConditionSet != null)
					Conditions.DataSource = dto.ConditionSet.Select(s => (ConditionHolder)s).ToList();
				else
					Conditions.DataSource = new List<ConditionHolder>();

				Quantifier = dto.Quantifier;
			}

			_loading = false;

			OnRefresh?.Invoke();
		}

		public ConditionSetDTO GetData()
		{
			return new ConditionSetDTO() {ConditionSet = Conditions.Count == 0?null:Conditions.Select(c => c.Condition).ToArray(),Quantifier = Quantifier};
		}

		public void AddNewDefaultCondition()
		{
			if(!HasData)
				return;

			((IList<ConditionHolder>)Conditions.DataSource).Add((ConditionHolder)"Bel([x]) = True");
			Conditions.Refresh();
			OnDataChanged?.Invoke();
			OnRefresh?.Invoke();
		}

		public void RemoveConditionAt(int index)
		{
			if(!HasData)
				return;

			Conditions.DataSource.RemoveAt(index);
			Conditions.Refresh();
			OnDataChanged?.Invoke();
			OnRefresh?.Invoke();
		}

		public void SetQuantifier(LogicalQuantifier value)
		{
			Quantifier = value;
			OnDataChanged?.Invoke();
			OnRefresh?.Invoke();
		}

		private void AssetBounds(int index)
		{
			if(index>=0 || index < Conditions.Count)
				return;

			throw new IndexOutOfRangeException();
		}

		public void ChangeConditionAtIndex(int index, string newCondition)
		{
			Conditions[index].Object.Condition = newCondition;
			Conditions.Refresh();
			OnDataChanged?.Invoke();
			OnRefresh?.Invoke();
		}

		public void MoveCondition(int index, int moveAmount)
		{
			AssetBounds(index);

			var newIndex = index + moveAmount;
			AssetBounds(index);

			var l = Conditions.DataSource;
			var obj = l[index];
			l.RemoveAt(index);
			l.Insert(newIndex,obj);

			Conditions.Refresh();
			OnDataChanged?.Invoke();
			OnRefresh?.Invoke();
		}
	}
}