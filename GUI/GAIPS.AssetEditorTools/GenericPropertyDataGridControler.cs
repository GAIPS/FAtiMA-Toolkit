using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;

namespace GAIPS.AssetEditorTools
{
	public partial class GenericPropertyDataGridControler : UserControl
	{
		private IDataGridViewController _controller;

		public event Action OnSelectionChanged;
		
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IDataGridViewController DataController { get { return _controller; }
			set
			{
				_controller = value;
				_dataView.DataSource = _controller.GetElements();
				_dataView.ClearSelection();
				Refresh();
			}
		}

		public bool AllowMuliSelect
		{
			get { return _dataView.MultiSelect; }
			set { _dataView.MultiSelect = value; }
		}

		public object CurrentlySelected
		{
			get { return SelectedObjects.FirstOrDefault(); }
		}

		public IEnumerable<object> SelectedObjects {
			get
			{
				if(_dataView.SelectedRows.Count>0)
					return _dataView.SelectedRows.Cast<DataGridViewRow>().Select(v => v.DataBoundItem);

				return Enumerable.Empty<ObjectView<object>>();
			}
		}

		public GenericPropertyDataGridControler()
		{
			InitializeComponent();
			Refresh();
			_dataView.SelectionChanged += OnSelectionChangedEventHandler;
		}

		public void ClearSelection()
		{
			_dataView.ClearSelection();
			UpdateButtons();
		}

		public DataGridViewColumn GetColumnByName(string columnName)
		{
			return _dataView.Columns[columnName];
		}

		private void OnSelectionChangedEventHandler(object sender, System.EventArgs e)
		{
			OnSelectionChanged?.Invoke();
			UpdateButtons();
		}

		private void OnAddButton_Click(object sender, EventArgs e)
		{
			var elm = _controller?.AddElement();
			if (elm != null)
			{
				_dataView.ClearSelection();
				foreach (DataGridViewRow r in _dataView.Rows)
				{
					var b = r.DataBoundItem == elm;
					if (r.Selected != b)
						r.Selected = b;
				}
				Refresh();
			}
		}

		private void OnEditButton_Click(object sender, EventArgs e)
		{
			var sucessfullyEdited = _controller?.EditElements(SelectedObjects);
			if (sucessfullyEdited != null)
			{
				var set = new HashSet<object>(sucessfullyEdited);
				if (set.Count > 0)
				{
					_dataView.ClearSelection();
					foreach (DataGridViewRow r in _dataView.Rows)
					{
						var b = set.Contains(r.DataBoundItem);
						if (r.Selected != b)
							r.Selected = b;
					}
					Refresh();
				}
			}
		}

		private void OnRemoveButton_Click(object sender, EventArgs e)
		{
			if (_controller != null && _controller.RemoveElements(SelectedObjects) > 0)
			{
				_dataView.ClearSelection();
				Refresh();
			}
		}

		private void Refresh()
		{
			UpdateButtons();
			_dataView.Refresh();
		}

		private void UpdateButtons()
		{
			_editButton.Enabled = _removeButton.Enabled = SelectedObjects.Any();
		}
	}
}
