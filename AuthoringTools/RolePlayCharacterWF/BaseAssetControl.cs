using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using AssetPackage;
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

		[Category("Action")]
		public event EventHandler OnPathChanged;

		private TEditor _controlForm;
		private Func<TAsset> _assetRequester;
		private TEditor _activeForm=null;

		public BaseAssetControl()
		{
			InitializeComponent();
			_controlForm=(TEditor)Activator.CreateInstance(typeof(TEditor));
		}

		public void SetAsset(string assetPath, Func<TAsset> assetRequester)
		{
			_assetRequester = assetRequester;
			_path.Text = assetPath;
      
        }

		private void button1_Click(object sender, EventArgs e)
		{
			var path = _controlForm.SelectAssetFileFromBrowser();
            
			if(path==null)
				return;
         
			try
			{
				var asset = LoadableAsset<TAsset>.LoadFromFile(path);
				_path.Text = asset.AssetFilePath;
				OnPathChanged?.Invoke(this, new EventArgs());
                EditAsset();
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

           
        }

		private void _clearButton_Click(object sender, EventArgs e)
		{
			_path.Text = string.Empty;
			OnPathChanged?.Invoke(this, new EventArgs());
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
			var asset = _controlForm.CreateAndSaveEmptyAsset(false);
			if (asset == null)
				return;

			_path.Text = asset.AssetFilePath;
			OnPathChanged?.Invoke(this, new EventArgs());

			EditAsset();
		}

		private void EditAsset()
		{
			_clearButton.Enabled = false;
			_createNewButton.Enabled = false;
			_setButton.Enabled = false;
			_editButton.Enabled = false;

			_activeForm = (TEditor)Activator.CreateInstance(typeof(TEditor));
			_activeForm.EditAssetInstance(_assetRequester);

			_activeForm.Closed += (sender, args) =>
			{
				_clearButton.Enabled = true;
				_createNewButton.Enabled = true;
				_setButton.Enabled = true;
				_editButton.Enabled = true;

				_activeForm = null;
			};

			_activeForm.Show();
		}
	}
}
