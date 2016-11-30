using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GAIPS.Rage;
using Utilities;

namespace GAIPS.AssetEditorTools
{
	public abstract partial class BaseAssetForm<T> : Form
		where T : LoadableAsset<T>
	{
		private Func<T> _getExternalAssetInstance = null;
		private bool _wasModified;

		protected BaseAssetForm()
		{
			InitializeComponent();
			BuildToolOptions();
		}

		[Description("The title name of the editor."), Category("Appearance")]
		public string EditorName { get; set; }

		public bool IsEditingOutsideInstance => _getExternalAssetInstance != null;
		public T CurrentAsset { get; private set; }

		protected bool IsLoading { get; private set; }

		private void OnLoad(object sender, EventArgs e)
		{
			UpdateWindowTitle();
		}

		public void SetModified()
		{
			if (_wasModified)
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

		public T SelectAndOpenAssetFromBrowser()
		{
			var path = SelectAssetFileFromBrowser();
			if (path != null)
			{
				try
				{
					return LoadAssetFromFile(path);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"{ex.Message}-{ex.StackTrace}", Resource.LoadFileErrorTitle, MessageBoxButtons.OK,
						MessageBoxIcon.Error);
				}
			}
			return null;
		}

		public T CreateAndSaveEmptyAsset(bool allowOverwrite)
		{
			var sfd = new SaveFileDialog();
			sfd.Filter = GetAssetFileFilters() + "|All Files|*.*";

			if (!allowOverwrite)
				sfd.OverwritePrompt = false;

			do
			{
				if (sfd.ShowDialog() != DialogResult.OK)
					return null;

				if (allowOverwrite)
					break;

				if (!File.Exists(sfd.FileName))
					break;

				MessageBox.Show("Cannot overwrite an existing rpc definition file.", "The file alredy exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				sfd.FileName = string.Empty;
			} while (true);

			var asset = CreateEmptyAsset();
			SaveAssetToFile(asset, sfd.FileName);
			return asset;
		}

		public void CreateNewAsset()
		{
			if (!AssetSaveModified())
				return;

			CurrentAsset = CreateEmptyAsset();
			_getExternalAssetInstance = null;
			_wasModified = false;
			UpdateWindowTitle();

			ReloadEditor();
		}

		public void EditAssetInstance(Func<T> externalAssetResolver)
		{
			var newBool = externalAssetResolver != null;

			newToolStripMenuItem.Enabled = !newBool;
			openToolStripMenuItem.Enabled = !newBool;
			saveAsToolStripMenuItem.Enabled = !newBool;

			_getExternalAssetInstance = externalAssetResolver;
			RequestAssetReload();
		}

		public void ReloadEditor()
		{
			IsLoading = true;
			try
			{
				OnAssetDataLoaded(CurrentAsset);
			}
			finally
			{
				IsLoading = false;
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

			var asset = SelectAndOpenAssetFromBrowser();
			if (asset == null)
				return;

			_getExternalAssetInstance = null;

			CurrentAsset = asset;
			_wasModified = false;
			UpdateWindowTitle();

			ReloadEditor();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveAsset();
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
					return SaveAsset();
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

		#region Build Other ToolStrip Options

		private void BuildToolOptions()
		{
			var methods =
				GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

			var candidates = methods.Select(m => new {att = m.GetCustomAttribute<MenuItemAttribute>(true), method = m})
				.Where(d => d.att != null).ToArray();

			if(candidates.Length<=0)
				return;

			var el = candidates.Select(c =>
			{
				var path = c.att.ItemName.Trim();
				var index = path.LastIndexOf('#');
				Keys keys;
				keys = index < 0 ? default(Keys) : ConvertStringToKeys(path.Substring(index + 1));
				var pathSplit = (index < 0 ? path : path.Substring(0, index)).Split('\\', '/').Select(s => s.Trim());

				return Tuple.Create(pathSplit.ToArray(), keys, c.att.Priority, c.method);
			});

			var entries = BuildToolStripItems(this, el, 0);
			var items = entries.Sort(MenuItemSorter).Select(e => e.Item1).Cast<ToolStripItem>().ToArray();
			menuStrip1.Items.AddRange(items);

			var validationEntries = entries.SelectMany(e => e.Item3).Where(a => a != null).ToArray();
			if (validationEntries.Length > 0)
			{
				menuStrip1.Invalidated += (sender, args) =>
				{
					foreach (var v in validationEntries)
						v();
				};
			}
		}

		private static Keys ConvertStringToKeys(string str)
		{
			var ks =
				str.Split('+')
					.Select(s => s.ToUpperInvariant() == "CTRL" ? "control" : s)
					.Select(s => (Keys) Enum.Parse(typeof (Keys), s, true))
					.ToArray();
			if (ks.Length == 0)
				return default(Keys);

			return ks.Aggregate((k1, k2) => k1 | k2);
		}

		private static int MenuItemSorter(System.Tuple<ToolStripMenuItem, int, IEnumerable<Action>> m1,
			System.Tuple<ToolStripMenuItem, int, IEnumerable<Action>> m2)
		{
			var p = m1.Item2 - m2.Item2;
			if (p != 0)
				return p;

			return string.Compare(m1.Item1.Text, m2.Item1.Text, StringComparison.InvariantCulture);
		}

		private static IEnumerable<System.Tuple<ToolStripMenuItem, int, IEnumerable<Action>>> BuildToolStripItems(
			BaseAssetForm<T> form, IEnumerable<Tuple<string[], Keys, int, MethodInfo>> data, int index)
		{
			var d2 = data.ToArray();

			var group = d2.GroupBy(d => d.Item1[index]).OrderBy(g => g.Key);
			foreach (var g in group)
			{
				ToolStripMenuItem e;
				var priority = 0;
				var validationCallbacks = Enumerable.Empty<Action>();

				var d3 = g.GroupBy(d => d.Item1.Length - index)
					.ToLookup(d => d.Key > 1, d => (IEnumerable<Tuple<string[], Keys, int, MethodInfo>>)g);

				var multi = d3[true].Any();
				var single = d3[false].Any();

				if (multi && single)
					throw new Exception(
						$"Single menu item found on the same menu path as a multi tool. \"{d3[false].SelectMany(d => d).First().Item1.Aggregate((s1, s2) => s1 + "/" + s2)}\"");

				if (single)
				{
					var t = InitializeActionMenuItem(form, g, out priority);
					if (t == null)
						continue;

					e = t.Item1;
					if (t.Item2 != null)
						validationCallbacks = new[] {t.Item2};
				}
				else
				{
					var elements = BuildToolStripItems(form, g, index + 1).ToArray();
					if (elements.Length == 0)
						continue;

					e = new ToolStripMenuItem();
					e.DropDownItems.AddRange(elements.Sort(MenuItemSorter).Select(e2 => e2.Item1).Cast<ToolStripItem>().ToArray());
					priority = 0;

					var callbacks = elements.SelectMany(e2 => e2.Item3).Where(a => a != null).ToArray();
					if (callbacks.Length > 0)
					{
						e.DropDownOpening += (sender, args) =>
						{
							foreach (var v in callbacks)
								v();
						};
					}
				}

				e.Text = g.Key;
				e.AutoSize = true;

				yield return Tuple.Create(e, priority, validationCallbacks);
			}
		}

		private static Tuple<ToolStripMenuItem, Action> InitializeActionMenuItem(BaseAssetForm<T> form,
			IEnumerable<Tuple<string[], Keys, int, MethodInfo>> data, out int priority)
		{
			var formType = form.GetType();

			Delegate actionMethod = null;
			Delegate validationMethod = null;
			priority = -1;
			var shortcut = Keys.None;

			var actionDelegateType = typeof (Action<>).MakeGenericType(formType);
			var validationDelegateType = typeof (Func<,>).MakeGenericType(formType, typeof (bool));

			foreach (var d in data)
			{
				if (actionMethod != null && validationMethod != null)
					break;

				if (actionMethod == null)
				{
					var d2 = Delegate.CreateDelegate(actionDelegateType, null, d.Item4, false);
					if (d2 != null)
					{
						actionMethod = d2;
						priority = d.Item3;
						shortcut = d.Item2;
					}
				}
				else
				{
					var d2 = Delegate.CreateDelegate(validationDelegateType, null, d.Item4, false);
					if (d2 != null)
					{
						validationMethod = d2;
					}
				}
			}

			if (actionMethod == null)
				return null;

			var e = new ToolStripMenuItem {ShortcutKeys = shortcut};

			e.Click += (sender, args) => { actionMethod.DynamicInvoke(form); };

			Action validationAction = null;
			if (validationMethod != null)
			{
				validationAction = () => { e.Enabled = (bool) validationMethod.DynamicInvoke(form); };
			}

			return Tuple.Create(e, validationAction);
		}

		#endregion

		#region Protected Members

		protected void RequestAssetReload()
		{
			if(IsLoading)
				return;

			T originalAsset;
			MemoryStream backup = null;
			DateTime writeDateBackup = DateTime.MinValue;
			string path = null;
			try
			{
				if (_wasModified)
				{
					path = GetAssetCurrentPath(CurrentAsset);
					if (!string.IsNullOrEmpty(path))
					{
						backup = new MemoryStream();
						using (var f = File.OpenRead(path))
						{
							f.CopyTo(backup);
						}
						writeDateBackup = File.GetLastWriteTimeUtc(path);
						SaveAssetToFile(CurrentAsset,path);
					}
				}

				if (IsEditingOutsideInstance)
				{
					originalAsset = _getExternalAssetInstance();
				}
				else if (CurrentAsset != null)
				{
					originalAsset = LoadAssetFromFile(CurrentAsset.AssetFilePath);
				}
				else
					throw new Exception("No defined original asset to be loaded");
			}
			finally
			{
				if (!string.IsNullOrEmpty(path))
				{
					backup.Position = 0;
					using (var f = File.OpenWrite(path))
					{
						backup.CopyTo(f);
					}
					File.SetLastWriteTimeUtc(path,writeDateBackup);
					backup.Dispose();
				}
			}

			CurrentAsset = originalAsset;

			ReloadEditor();
		}

		protected bool SaveAsset()
		{
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