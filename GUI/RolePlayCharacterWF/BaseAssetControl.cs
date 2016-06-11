using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GAIPS.Rage;
using RolePlayCharacterWF.Properties;

namespace RolePlayCharacterWF
{
	public partial class BaseAssetControl<T> : UserControl
		where T: LoadableAsset<T>
	{
		[Description("Group Label"), Category("Appearance")]
		public string Label {
			get { return groupBox1.Text; }
			set { groupBox1.Text = value; }
		}

		[Description("Open File Filters"),Category("Behavior")]
		public string Filters { get; set; }

		[Description("Relative Path to the respective Asset Editor"), Category("Behavior")]
		public string AssetEditorExecutablePath { get; set; }

		public string Path
		{
			get { return _path.Text; }
			set { _path.Text = value; }
		}

		[Category("Action")]
		public event EventHandler OnPathChanged;

		public BaseAssetControl()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var ofd = new OpenFileDialog();
			ofd.Filter = Filters;
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				try
				{
					var asset = LoadableAsset<T>.LoadFromFile(LocalStorageProvider.Instance, ofd.FileName);
					_path.Text = ofd.FileName;
					OnPathChanged?.Invoke(this,new EventArgs());
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "-" + ex.StackTrace, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
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

			Process.Start(AssetEditorExecutablePath, $"\"{Path}\"");
		}

		private void _createNewButton_Click(object sender, EventArgs e)
		{
			var sfd = new SaveFileDialog();
			sfd.Filter = Filters;
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				_path.Text = sfd.FileName;
				OnPathChanged?.Invoke(this, new EventArgs());
				Process.Start(AssetEditorExecutablePath, $"\"{Path}\"");
			}
		}
	}
}
