using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace GAIPS.AssetEditorTools
{
	public abstract partial class BaseAssetForm<T> : Form
	{
		private bool _wasModified;

		[Description("The title name of the editor."),Category("Appearance")]
		public string EditorName{get; set; }

		public T CurrentAsset { get; private set; }

		protected BaseAssetForm()
		{
			InitializeComponent();
		}

		public void SetModified()
		{
			_wasModified = true;
			UpdateWindowTitle();
		}

		private void BaseAssetForm_Shown(object sender, EventArgs e)
		{
			if(DesignMode)
				return;

			string[] args = Environment.GetCommandLineArgs();
			if (args.Length <= 1)
			{
				CreateNewAsset();
				return;
			}

			var fileName = args[1];
			try
			{
				var fullPath = Path.GetFullPath(fileName);
				if (File.Exists(fullPath))
				{
					if (!OpenAssetAtPath(fullPath))
					{
						CreateNewAsset();
					}
				}
				else
				{
					CreateNewAsset();
					SaveAssetToFile(CurrentAsset, fullPath);
					_wasModified = false;
					UpdateWindowTitle();
				}
			}
			catch (Exception)
			{
				MessageBox.Show(
					$"\"{fileName}\" is not a valid url path.\nPlease check the path parameter being passed to this application.",
					"Invalid file path format", MessageBoxButtons.OK, MessageBoxIcon.Error);

				CreateNewAsset();
				return;
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CreateNewAsset();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!AssetSaveModified())
				return;

			var ofd = new OpenFileDialog();
			ofd.Filter = GetAssetFileFilters() + "|All Files|*.*";
			if (ofd.ShowDialog() != DialogResult.OK)
				return;

			OpenAssetAtPath(ofd.FileName);
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
			LoadAssetData(CurrentAsset);
			return true;
		}

		#region Protected Members

		protected void CreateNewAsset()
		{
			if (!AssetSaveModified())
				return;

			CurrentAsset = CreateEmptyAsset();
			_wasModified = false;
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