using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using SocialImportance.DTOs;

namespace SocialImportanceWF.ViewModels
{
	public class ConferralsVM: IDataGridViewController
	{
		private bool m_loading = false;
		private Guid _selected = Guid.Empty;
		private BaseSIForm _parent;
		private BindingListView<ConferralDTO> _conferralsList = new BindingListView<ConferralDTO>((IList)null);

		public ConditionSetView ConditionsView { get;} = new ConditionSetView();

		public ConferralsVM(BaseSIForm parent)
		{
			_parent = parent;

			ConditionsView.OnDataChanged += ConditionSetView_OnDataChanged;
		}

		private void ConditionSetView_OnDataChanged()
		{
			if (m_loading)
				return;

			if (_selected == Guid.Empty)
				return;

			var conferral = _conferralsList.FirstOrDefault(c => c.Id == _selected);

			conferral.Conditions = ConditionsView.GetData();
			_parent.LoadedAsset.UpdateConferralData(conferral);
			_parent.SetModified();
		}

		public void Reload()
		{
			m_loading = true;

			_conferralsList.DataSource = _parent.LoadedAsset.GetConferrals().ToList();
			_conferralsList.Refresh();

			ConditionsView.SetData(null);

			m_loading = false;
		}

		public ObjectView<ConferralDTO> AddOrUpdateConferral(ConferralDTO dto)
		{
			if (dto.Id == Guid.Empty)
				dto = _parent.LoadedAsset.AddConferral(dto);
			else
				_parent.LoadedAsset.UpdateConferralData(dto);

			_parent.SetModified();
			Reload();

			var index = _conferralsList.Find(PropertyUtil.GetPropertyDescriptor<ConferralDTO>("Id"), dto.Id);
			return _conferralsList[index];
		}

		public void SetSelectedCondition(Guid id)
		{
			_selected = id;
			var conferral = _conferralsList.FirstOrDefault(c => c.Id == _selected);
			ConditionsView.SetData(conferral?.Conditions);
		}

		#region Implementation of IDataGridViewController

		public IList GetElements()
		{
			return _conferralsList;
		}

		public object AddElement()
		{
			var dialog = new AddOrEditConferralForm(this, null);
			dialog.ShowDialog(_parent);
			return dialog.AddedObject;
		}

		public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
		{
			List<object> result = new List<object>();
			foreach (var dto in elementsToEdit.Cast<ObjectView<ConferralDTO>>().Select(v => v.Object))
			{
				try
				{
					var dialog = new AddOrEditConferralForm(this, dto);
					dialog.ShowDialog(_parent);
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
			foreach (var dto in elementsToRemove.Cast<ObjectView<ConferralDTO>>().Select(v => v.Object))
			{
				try
				{
					_parent.LoadedAsset.RemoveConferralById(dto.Id);
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