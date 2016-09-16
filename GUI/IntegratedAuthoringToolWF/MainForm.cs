using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF
{
	public partial class MainForm : BaseIATForm
	{
		private BindingListView<CharacterSourceDTO> _characterSources;
		private readonly RolePlayCharacterWF.MainForm _rpcForm = new RolePlayCharacterWF.MainForm();

		private Dictionary<string,Form> _openedForms = new Dictionary<string, Form>();

		public MainForm()
		{
			InitializeComponent();
		}

		protected override void OnAssetDataLoaded(IntegratedAuthoringToolAsset asset)
		{
			textBoxScenarioName.Text = asset.ScenarioName;
			_characterSources = new BindingListView<CharacterSourceDTO>(asset.GetAllCharacterSources().ToList());
			dataGridViewCharacters.DataSource = _characterSources;
		}

		private void buttonAddCharacter_Click(object sender, EventArgs e)
		{
			var rpc = _rpcForm.SelectAndOpenAssetFromBrowser();
			if (rpc == null)
				return;

			CurrentAsset.AddCharacter(rpc);
			_characterSources.DataSource = CurrentAsset.GetAllCharacterSources().ToList();
			_characterSources.Refresh();
			SetModified();
		}

		private void textBoxScenarioName_TextChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			CurrentAsset.ScenarioName = textBoxScenarioName.Text;
			SetModified();
		}

		private void buttonRemoveCharacter_Click(object sender, EventArgs e)
		{
			IList<string> charactersToRemove = new List<string>();
			for (var i = 0; i < dataGridViewCharacters.SelectedRows.Count; i++)
			{
				var character = ((ObjectView<CharacterSourceDTO>) dataGridViewCharacters.SelectedRows[i].DataBoundItem).Object;
				Form f;
				if (_openedForms.TryGetValue(character.Name,out f))
				{
					var r = MessageBox.Show($"\"{character.Name}\" is currently being edited.\nDo you wish to remove it?", "Warning",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question);
					if (r == DialogResult.No)
						continue;

					f.Close();
				}
				
				charactersToRemove.Add(character.Name);
			}

			CurrentAsset.RemoveCharacters(charactersToRemove);
			_characterSources.DataSource = CurrentAsset.GetAllCharacterSources().ToList();
			_characterSources.Refresh();
			SetModified();
		}

		private void buttonEditCharacter_Click(object sender, EventArgs e)
		{
			for (var i = 0; i < dataGridViewCharacters.SelectedRows.Count; i++)
			{
				var character = ((ObjectView<CharacterSourceDTO>) dataGridViewCharacters.SelectedRows[i].DataBoundItem).Object;
				if(_openedForms.ContainsKey(character.Name))
					continue;

				var form = new RolePlayCharacterWF.MainForm();
				form.Closed += (o, args) =>
				{
					_openedForms.Remove(character.Name);
					ReloadEditor();
				};
				form.EditAssetInstance(() => CurrentAsset.InstantiateCharacterAsset(character.Name));
				_openedForms.Add(character.Name,form);
				form.Show();
			}
		}

		#region Toolbar Options

		private bool _dialogEditorIsShowing;

		[MenuItem("Tools/Show Dialog Editor #ctrl+D")]
		private void ShowToolbar()
		{
			var d = new DialogueEditorForm(CurrentAsset);
			_dialogEditorIsShowing = true;
			d.Closed += (sender, args) => { _dialogEditorIsShowing = false; };
			d.Show();
		}

		[MenuItem("Tools/Show Dialog Editor")]
		private bool ShowToolbar_Validation()
		{
			return !_dialogEditorIsShowing;
		}

		#endregion
	}
}