using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using RolePlayCharacterWF.Properties;

namespace RolePlayCharacterWF
{
	public partial class BaseAssetControl<TAsset,TEditor> : UserControl
		where TAsset: LoadableAsset<TAsset>
		where TEditor: BaseAssetForm<TAsset>
	{
		[Description("Group Label"), Category("Appearance")]
		public string Label {
			get { return groupBox1.Text; }
			set { groupBox1.Text = value; }
		}

		public string Path => _path.Text;

		public TAsset Asset { get; private set; }

		[Category("Action")]
		public event EventHandler OnPathChanged;
		[Category("Action")]
		public event EventHandler OnAssetReload;

		private TEditor _controlForm;

		public BaseAssetControl()
		{
			InitializeComponent();
			_controlForm=(TEditor)Activator.CreateInstance(typeof(TEditor));
		}

		public void SetAsset(TAsset asset)
		{
			Asset = asset;
			_path.Text = asset?.AssetFilePath;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var path = _controlForm.SelectAssetFileFromBrowser();
			if(path==null)
				return;

			try
			{
				LoadableAsset<TAsset>.LoadFromFile(path);
				_path.Text = path;
				OnPathChanged?.Invoke(this, new EventArgs());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_path.Text = string.Empty;
		}

		private void _path_TextChanged(object sender, EventArgs e)
		{
			_editButton.Enabled = !string.IsNullOrEmpty(_path.Text);
		}

		private void _editButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Path))
				return;

			EditAsset();
		}

		private void _createNewButton_Click(object sender, EventArgs e)
		{
			var path = _controlForm.CreateAndSaveEmptyAsset();
			if (path == null)
				return;

			_path.Text = path;
			OnPathChanged?.Invoke(this, new EventArgs());

			EditAsset();
		}

		private void EditAsset()
		{
			ParentForm.Visible = false;

			var lastTime = File.GetLastWriteTimeUtc(Asset.AssetFilePath);
			_controlForm.EditAssetInstance(Asset);
			_controlForm.ShowDialog(ParentForm);

			var currentTime = File.GetLastWriteTimeUtc(Asset.AssetFilePath);
			if (currentTime <= lastTime)
				OnAssetReload?.Invoke(this, new EventArgs());


			ParentForm.Visible = true;
		}
	}
}
