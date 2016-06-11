using System;
using System.Windows.Forms;

namespace WFHelperLib
{
	public abstract partial class BaseAssetForm<T> : Form
	{
		private string _defaultWindowTitle;
		private bool _wasModified = false;

		protected BaseAssetForm()
		{
			InitializeComponent();
			_defaultWindowTitle = Text;
		}

		private void BaseAssetForm_Load(object sender, EventArgs e)
		{
			CreateNewAsset();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateNewAsset();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if(!AssetSaveModified())
				return;

			var ofd = new OpenFileDialog();
			ofd.Filter = GetAssetFileFilters()+"|All Files|*.*";
			if(ofd.ShowDialog() != DialogResult.OK)
				return;

			try
			{
				var a = LoadAssetFromFile(ofd.FileName);
				CurrentAsset = a;
				_wasModified = false;
				UpdateWindowTitle();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}-{ex.StackTrace}", Resource.LoadFileErrorTitle, MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}

			LoadAssetData(CurrentAsset);
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
			var path = GetAssetCurrentPath(CurrentAsset);
			Text = _defaultWindowTitle + (string.IsNullOrEmpty(path) ? string.Empty : $" - {path}") + (_wasModified?"*":string.Empty);
		}

		#region Protected Members

		protected T CurrentAsset { get; private set; }

		protected void CreateNewAsset()
		{
			if(!AssetSaveModified())
				return;

			CurrentAsset = CreateEmptyAsset();
			_wasModified = true;
			UpdateWindowTitle();
			LoadAssetData(CurrentAsset);
		}

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

		protected abstract T LoadAssetFromFile(string path);

		protected abstract void SaveAssetToFile(T asset, string path);

		protected abstract string GetAssetCurrentPath(T asset);

		protected abstract string GetAssetFileFilters();

		protected virtual void LoadAssetData(T asset)
		{
		}

		#endregion
	}
}
