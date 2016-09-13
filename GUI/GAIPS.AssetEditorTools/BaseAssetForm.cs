using System;
using System.ComponentModel;
using System.Windows.Forms;
using GAIPS.Rage;

namespace GAIPS.AssetEditorTools
{
	public abstract partial class BaseAssetForm<T> : Form
		where T: LoadableAsset<T>
	{
		private bool _wasModified;

		[Description("The title name of the editor."),Category("Appearance")]
		public string EditorName{get; set; }

		public bool IsEditingOutsideInstance { get; private set; } = false;
		public T CurrentAsset { get; private set; }

		protected BaseAssetForm()
		{
			InitializeComponent();
		}

		public void SetModified()
		{
			if(_wasModified)
				return;

			_wasModified = true;
			UpdateWindowTitle();
		}

		public string SelectAssetFileFromBrowser()
		{
			var ofd = new OpenFileDialog();
			ofd.Filter = GetAssetFileFilters() + "|All Files|*.*";
			if (ofd.ShowDialog() != DialogResult.OK)
				return null;

			return ofd.FileName;
		}

		public string CreateAndSaveEmptyAsset()
		{
			var sfd = new SaveFileDialog();
			sfd.Filter = GetAssetFileFilters() + "|All Files|*.*";

			if (sfd.ShowDialog() != DialogResult.OK)
				return null;

			var asset = CreateEmptyAsset();
			SaveAssetToFile(asset,sfd.FileName);
			return sfd.FileName;
		}

		public void EditAssetInstance(T asset)
		{
			var newBool = asset != null;

			newToolStripMenuItem.Enabled = !newBool;
			openToolStripMenuItem.Enabled = !newBool;
			saveAsToolStripMenuItem.Enabled = !newBool;

			CurrentAsset = asset;
			IsEditingOutsideInstance = newBool;
			
			OnAssetDataLoaded(asset);

			_wasModified = false;
			UpdateWindowTitle();
		}
		
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateNewAsset();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!AssetSaveModified())
				return;

			var path = SelectAssetFileFromBrowser();
			if (path != null)
				OpenAssetAtPath(path);
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAsset(false);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAssetAs();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void BaseAssetForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = !AssetSaveModified();
		}

		private bool AssetSaveModified()
		{
			if (!_wasModified)
				return true;

			var result = MessageBox.Show("The asset was modified but not saved.\nDo you wish to save now?", "File not saved",
				MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

			switch (result)
			{
				case DialogResult.Yes:
					return SaveAsset(true);
				case DialogResult.No:
					return true;
			}

			return false;
		}

		private void UpdateWindowTitle()
		{
			if (CurrentAsset == null)
			{
				Text = EditorName;
				return;
			}

			var path = GetAssetCurrentPath(CurrentAsset);
			Text = EditorName + (string.IsNullOrEmpty(path) ? string.Empty : $" - {path}") +
			       (_wasModified ? "*" : string.Empty);
		}

		private bool OpenAssetAtPath(string path)
		{
			try
			{
				var a = LoadAssetFromFile(path);
				CurrentAsset = a;
				_wasModified = false;
				UpdateWindowTitle();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}-{ex.StackTrace}", Resource.LoadFileErrorTitle, MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				return false;
			}
			IsEditingOutsideInstance = false;
			OnAssetDataLoaded(CurrentAsset);
			return true;
		}

		public void CreateNewAsset()
		{
			if (!AssetSaveModified())
				return;

			CurrentAsset = CreateEmptyAsset();
			_wasModified = false;
			UpdateWindowTitle();
			IsEditingOutsideInstance = false;
			OnAssetDataLoaded(CurrentAsset);
		}

		#region Protected Members
		
		protected bool SaveAsset(bool force)
		{
			if (!(_wasModified || force))
				return true;

			var path = GetAssetCurrentPath(CurrentAsset);
			if (string.IsNullOrEmpty(path))
				return SaveAssetAs();

			SaveAssetToFile(CurrentAsset, path);
			_wasModified = false;
			UpdateWindowTitle();
			return true;
		}

		protected bool SaveAssetAs()
		{
			var sfd = new SaveFileDialog();
			sfd.Filter = GetAssetFileFilters() + "|All Files|*.*";
			if (sfd.ShowDialog() != DialogResult.OK)
				return false;

			SaveAssetToFile(CurrentAsset, sfd.FileName);
			_wasModified = false;
			UpdateWindowTitle();
			return true;
		}

		#endregion

		#region To Implement

		protected abstract T CreateEmptyAsset();

		protected virtual T LoadAssetFromFile(string path)
		{
			return LoadableAsset<T>.LoadFromFile(path);
		}

		protected void SaveAssetToFile(T asset, string path)
		{
			OnWillSaveAsset(asset);
			asset.SaveToFile(path);
		}

		protected virtual string GetAssetCurrentPath(T asset)
		{
			return asset.AssetFilePath;
		}

		protected abstract string GetAssetFileFilters();

		protected virtual void OnAssetDataLoaded(T asset)
		{
		}

		protected virtual void OnWillSaveAsset(T asset)
		{
			
		}

		#endregion
	}
}