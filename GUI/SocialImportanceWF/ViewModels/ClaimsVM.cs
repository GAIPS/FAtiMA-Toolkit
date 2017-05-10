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
	public class ClaimsVM: IDataGridViewController
	{
		private BaseSIForm _parent;
		private bool m_loading;

		public BindingListView<ClaimDTO> ClaimList { get; }

		public ClaimsVM(BaseSIForm parent)
		{
			_parent = parent;
			ClaimList = new BindingListView<ClaimDTO>((IList)null);
			m_loading = false;
		}

		public void Reload()
		{
			m_loading = true;

			ClaimList.DataSource = _parent.LoadedAsset.GetClaims().ToList();
			ClaimList.Refresh();

			m_loading = false;
		}

		public ObjectView<ClaimDTO> AddClaim(ClaimDTO dto)
		{
			_parent.LoadedAsset.AddClaim(dto);

			ClaimList.DataSource = _parent.LoadedAsset.GetClaims().ToList();
			ClaimList.Refresh();
			_parent.SetModified();

			var index = ClaimList.Find(PropertyUtil.GetPropertyDescriptor<ClaimDTO>("ActionTemplate"), dto.ActionTemplate);
			return ClaimList[index];
		}

		public ObjectView<ClaimDTO> ReplaceClaim(ClaimDTO oldClaim, ClaimDTO newClaim)
		{
			_parent.LoadedAsset.ReplaceClaim(oldClaim,newClaim);

			ClaimList.DataSource = _parent.LoadedAsset.GetClaims().ToList();
			ClaimList.Refresh();
			_parent.SetModified();

			var index = ClaimList.Find(PropertyUtil.GetPropertyDescriptor<ClaimDTO>("ActionTemplate"), newClaim.ActionTemplate);
			return ClaimList[index];
		}

		#region Implementation of IDataGridViewController

		public IList GetElements()
		{
			return ClaimList;
		}

		public object AddElement()
		{
			var dialog = new AddOrEditClaimForm(this, null);
			dialog.ShowDialog(_parent);
			return dialog.AddedObject;
		}

		public IEnumerable<object> EditElements(IEnumerable<object> elementsToEdit)
		{
			List<object> result = new List<object>();
			foreach (var dto in elementsToEdit.Cast<ObjectView<ClaimDTO>>().Select(v => v.Object))
			{
				try
				{
					var dialog = new AddOrEditClaimForm(this, dto);
					dialog.ShowDialog(_parent);
					if(dialog.AddedObject!=null)
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
			foreach (var dto in elementsToRemove.Cast<ObjectView<ClaimDTO>>().Select(v => v.Object))
			{
				try
				{
					if (_parent.LoadedAsset.RemoveClaim(dto.ActionTemplate))
						count++;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (count > 0)
			{
				ClaimList.DataSource = _parent.LoadedAsset.GetClaims().ToList();
				ClaimList.Refresh();
				_parent.SetModified();
			}

			return count;
		}

		#endregion
	}
}