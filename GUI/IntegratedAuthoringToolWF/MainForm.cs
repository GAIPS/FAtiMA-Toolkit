using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using WellFormedNames;
using RolePlayCharacter;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using Utilities;
using Utilities.DataStructures;
using System.Drawing;

namespace IntegratedAuthoringToolWF
{
	public partial class MainForm : BaseIATForm
	{
        
        private BindingListView<GUIDialogStateAction> _playerDialogs;
        private BindingListView<GUIDialogStateAction> _agentDialogs;
        private readonly string PLAYER = IATConsts.PLAYER;
        private readonly string AGENT = IATConsts.AGENT;
        private BindingListView<CharacterSourceDTO> _characterSources;
		private RolePlayCharacterWF.MainForm _rpcForm = new RolePlayCharacterWF.MainForm();

        public MainForm()
		{
			InitializeComponent();
			buttonRemoveCharacter.Enabled = false;
        }
        

        private void RefreshPlayerDialogs()
        {
            _playerDialogs.DataSource = LoadedAsset.GetDialogueActionsBySpeaker(
                PLAYER).Select(d => new GUIDialogStateAction(d)).ToList();
            _playerDialogs.Refresh();
            dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;

        }

        private void RefreshAgentDialogs()
        {
            _agentDialogs.DataSource = LoadedAsset.GetDialogueActionsBySpeaker(
             AGENT).Select(d => new GUIDialogStateAction(d)).ToList();
            _agentDialogs.Refresh();
            dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
        }

       
        protected override void OnAssetDataLoaded(IntegratedAuthoringToolAsset asset)
		{
			textBoxScenarioName.Text = asset.ScenarioName;
			textBoxScenarioDescription.Text = asset.ScenarioDescription;
			_characterSources = new BindingListView<CharacterSourceDTO>(asset.GetAllCharacterSources().ToList());
			dataGridViewCharacters.DataSource = _characterSources;
            _playerDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
            dataGridViewPlayerDialogueActions.DataSource = _playerDialogs;
            RefreshPlayerDialogs();

            _agentDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
            dataGridViewAgentDialogueActions.DataSource = _agentDialogs;
            RefreshAgentDialogs();
        }

		private void buttonCreateCharacter_Click(object sender, EventArgs e)
		{
			var asset = _rpcForm.CreateAndSaveEmptyAsset(false);
			if (asset == null)
				return;
                                   
            LoadedAsset.AddNewCharacterSource(new CharacterSourceDTO() {Source = asset.AssetFilePath});
			_characterSources.DataSource = LoadedAsset.GetAllCharacterSources().ToList();
			_characterSources.Refresh();
			SetModified();

            var rpc = RolePlayCharacterAsset.LoadFromFile(asset.AssetFilePath);
            _rpcForm.EditAssetInstance(() => rpc);
            FormHelper.ShowFormInContainerControl(this.tabControl1.TabPages[1], _rpcForm);
            this.tabControl1.SelectTab(1);
        }

		private void buttonAddCharacter_Click(object sender, EventArgs e)
		{
			var rpc = _rpcForm.SelectAndOpenAssetFromBrowser();
			if (rpc == null)
				return;

			LoadedAsset.AddNewCharacterSource(new CharacterSourceDTO()
			{
				Source =  rpc.AssetFilePath
			});

			_characterSources.DataSource = LoadedAsset.GetAllCharacterSources().ToList();
			_characterSources.Refresh();
			SetModified();
        }

		private void textBoxScenarioName_TextChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			LoadedAsset.ScenarioName = textBoxScenarioName.Text;
			SetModified();
		}

		private void textBoxScenarioDescription_TextChanged(object sender, EventArgs e)
		{
			if (IsLoading)
				return;

			LoadedAsset.ScenarioDescription = textBoxScenarioDescription.Text;
			SetModified();
		}

		private void buttonRemoveCharacter_Click(object sender, EventArgs e)
		{
			IList<int> charactersToRemove = new List<int>();
			for (var i = 0; i < dataGridViewCharacters.SelectedRows.Count; i++)
			{
				var character = ((ObjectView<CharacterSourceDTO>) dataGridViewCharacters.SelectedRows[i].DataBoundItem).Object;
				Form f;
				charactersToRemove.Add(character.Id);
			}

			LoadedAsset.RemoveCharacters(charactersToRemove);
			_characterSources.DataSource = LoadedAsset.GetAllCharacterSources().ToList();
			_characterSources.Refresh();
            _rpcForm.Close();
			SetModified();
            dataGridViewCharacters.ClearSelection();
        }

		#region About

		[MenuItem("About",Priority = int.MaxValue)]
		private void ShowAbout()
		{
			var form = new AboutForm();
			form.ShowDialog();
		}

		#endregion

		private void dataGridViewCharacters_SelectionChanged(object sender, EventArgs e)
		{
            if(dataGridViewCharacters.SelectedRows.Count > 0)
            {
                var rpcSource = ((ObjectView<CharacterSourceDTO>)dataGridViewCharacters.SelectedRows[0].DataBoundItem).Object;
                var rpc = RolePlayCharacterAsset.LoadFromFile(rpcSource.Source);
                _rpcForm.Close();
                _rpcForm = new RolePlayCharacterWF.MainForm();
                _rpcForm.EditAssetInstance(() => rpc);
                FormHelper.ShowFormInContainerControl(this.tabControl1.TabPages[1], _rpcForm);
                this.tabControl1.SelectTab(1);
                
                buttonRemoveCharacter.Enabled = true;
            }
		}

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBoxDialogueEditor_Enter(object sender, EventArgs e)
        {

        }


        private void buttonAgentAddDialogAction_Click(object sender, EventArgs e)
        {
            new AddOrEditDialogueActionForm(this, false).ShowDialog();
            RefreshAgentDialogs();
        }

        private void buttonAddPlayerDialogueAction_Click_1(object sender, EventArgs e)
        {
            new AddOrEditDialogueActionForm(this, true).ShowDialog();
            RefreshPlayerDialogs();
        }

        private void buttonPlayerEditDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(this, true, item.Id).ShowDialog();
                RefreshPlayerDialogs();
            }
        }

        private void buttonPlayerDuplicateDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;

                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = item.CurrentState,
                    NextState = item.NextState,
                    Meaning = item.Meaning.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Style = item.Style.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Utterance = item.Utterance
                };
                LoadedAsset.AddPlayerDialogAction(newDialogueAction);
                RefreshPlayerDialogs();
            }
        }

        private void buttonPlayerRemoveDialogueAction_Click(object sender, EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewPlayerDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
             LoadedAsset.RemoveDialogueActions(PLAYER, itemsToRemove);
             RefreshPlayerDialogs();
             this.SetModified();
        }

        private void buttonAgentEditDialogAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(this, false, item.Id).ShowDialog();
                RefreshAgentDialogs();
            }
        }

        private void buttonAgentDuplicateDialogueAction_Click(object sender, EventArgs e)
        {
             if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;

                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = item.CurrentState,
                    NextState = item.NextState,
                    Meaning = item.Meaning.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Style = item.Style.Split(',').Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Utterance = item.Utterance
                };
                LoadedAsset.AddAgentDialogAction(newDialogueAction);
                RefreshAgentDialogs();
            }
        }

        private void buttonAgentRemoveDialogAction_Click(object sender, EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewAgentDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            LoadedAsset.RemoveDialogueActions(AGENT, itemsToRemove);
            RefreshAgentDialogs();
            SetModified();
        }

        private void buttonImportExcel_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Excel Workbook|*.xlsx";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileName = new FileInfo(ofd.FileName);
            using (var excelDoc = new ExcelPackage(fileName))
            {
                var playerDialogs = ImportWorkSheet(excelDoc, "Player Dialogs").ToArray();
                var agentDialogs = ImportWorkSheet(excelDoc, "Agent Dialogs").ToArray();

                //Clear all actions from the asset
                LoadedAsset.RemoveDialogueActions(PLAYER, LoadedAsset.GetDialogueActionsBySpeaker(PLAYER).ToArray());
                LoadedAsset.RemoveDialogueActions(AGENT, LoadedAsset.GetDialogueActionsBySpeaker(AGENT).ToArray());

                foreach (var d in playerDialogs)
                    LoadedAsset.AddPlayerDialogAction(d);

                foreach (var d in agentDialogs)
                    LoadedAsset.AddAgentDialogAction(d);
            }

            RefreshPlayerDialogs();
            RefreshAgentDialogs();
        }

        private static IEnumerable<DialogueStateActionDTO> ImportWorkSheet(ExcelPackage package, string workSheetName)
        {
            var worksheet = package.Workbook.Worksheets[workSheetName];
            if (worksheet == null)
                throw new Exception($"Could not find worksheet with the name \"{workSheetName}\"");

            var lastRow = worksheet.Dimension.End.Row;
            var cells = worksheet.Cells;
            for (int i = 3; i <= lastRow; i++)
            {
                var rowValues = Enumerable.Range(1, 5).Select(j => cells[i, j].Text).ToArray();
                if (rowValues.All(string.IsNullOrEmpty))
                    continue;

                var value = new DialogueStateActionDTO()
                {
                    CurrentState = rowValues[0],
                    NextState = rowValues[1],
                    Utterance = rowValues[2],
                    Meaning = rowValues[3].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray(),
                    Style = rowValues[4].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray()
                };

                yield return value;
            }
        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Excel Workbook|*.xlsx";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            var fileName = new FileInfo(sfd.FileName);
            using (var excelDoc = new ExcelPackage())
            {
                ExportWorkSheet(excelDoc, "Player Dialogs", LoadedAsset.GetDialogueActionsBySpeaker(PLAYER));
                ExportWorkSheet(excelDoc, "Agent Dialogs", LoadedAsset.GetDialogueActionsBySpeaker(AGENT));
                excelDoc.SaveAs(fileName);
            }
        }


        private static void ExportWorkSheet(ExcelPackage package, string workSheetName, IEnumerable<DialogueStateActionDTO> dialogActions)
        {
            var worksheet = package.Workbook.Worksheets.Add(workSheetName);

            //Worksheet Header
            {
                var header = worksheet.Cells[1, 1, 1, 5];
                header.Merge = true;
                header.Value = workSheetName;
                header.Style.Border.BorderAround(ExcelBorderStyle.Thick);
                header.Style.Font.Bold = true;
                header.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                header.Style.Font.Color.SetColor(Color.White);
                header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(Color.Black);
            }

            //Column Headers
            {
                var header = worksheet.Cells[2, 1, 2, 5];
                header.Style.Font.Bold = true;
                var border = header.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                header.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = "Current State";
                worksheet.Cells[2, 2].Value = "Next State";
                worksheet.Cells[2, 3].Value = "Utterance";
                worksheet.Cells[2, 4].Value = "Meanings";
                worksheet.Cells[2, 5].Value = "Styles";
            }

            worksheet.View.FreezePanes(3, 1);

            var cellIndex = 3;
            foreach (var d in dialogActions)
            {
                var line = worksheet.Cells[cellIndex, 1, cellIndex, 5];
                var border = line.Style.Border;
                border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                line.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Cells[cellIndex, 1].Value = d.CurrentState;
                worksheet.Cells[cellIndex, 2].Value = d.NextState;
                worksheet.Cells[cellIndex, 3].Value = d.Utterance;
                worksheet.Cells[cellIndex, 4].Value = d.Meaning.AggregateToString(", ");
                worksheet.Cells[cellIndex, 5].Value = d.Style.AggregateToString(", ");

                cellIndex++;
            }

            worksheet.Cells[2, 1, cellIndex, 5].AutoFilter = true;

            for (int i = 1; i < 6; i++)
            {
                worksheet.Column(i).AutoFit();
            }
        }


        private void buttonImportTxt_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Text File|*.txt";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileName = new FileInfo(ofd.FileName);
            File.SetAttributes(fileName.DirectoryName, FileAttributes.Normal);

            LoadedAsset.RemoveDialogueActions(PLAYER, LoadedAsset.GetDialogueActionsBySpeaker(PLAYER).ToArray());
            LoadedAsset.RemoveDialogueActions(AGENT, LoadedAsset.GetDialogueActionsBySpeaker(AGENT).ToArray());

            int stateCounter = 0;
            var lines = File.ReadAllLines(fileName.FullName);
            var totalSize = lines.Length;

            foreach (var line in lines)
            {
                if (line.Contains("P:"))
                {
                    var auxLine = line.Replace("P:", "");
                    var add = GenerateDialogueActionFromLine(auxLine, totalSize, ref stateCounter);
                    LoadedAsset.AddPlayerDialogAction(add);
                }
                else if (line.Contains("A:"))
                {
                    var auxLine = line.Replace("A:", "");
                    var add = GenerateDialogueActionFromLine(auxLine, totalSize, ref stateCounter);
                    LoadedAsset.AddAgentDialogAction(add);

                }
            }

            RefreshPlayerDialogs();
            RefreshAgentDialogs();
        }

        private DialogueStateActionDTO GenerateDialogueActionFromLine(string line, int totalSize, ref int stateCounter)
        {
            line = line.Replace("A:", "");
            char[] delimitedchars = { '\n' };
            line = line.Trim();

            var result = line.Split(delimitedchars);
            var currentState = "";
            var nextState = "";
            
            if (stateCounter == 0)
            {
                currentState = IATConsts.INITIAL_DIALOGUE_STATE;
                stateCounter += 1;
                nextState = "S" + stateCounter;
            }
            else
            {
                currentState = "S" + stateCounter;
                stateCounter += 1;
                nextState = "S" + stateCounter;
            }
            if (stateCounter == totalSize)
                nextState = "End";

            var add = new DialogueStateActionDTO()
            {
                CurrentState = currentState,
                Meaning = new string[1],
                NextState = nextState,
                Utterance = result[0],
                Style = new string[1]

            };

            return add;
        }

        private void buttonTTS_Click(object sender, EventArgs e)
        {
            var dialogs = LoadedAsset.GetDialogueActionsBySpeaker(AGENT).ToArray();
            var t = new TextToSpeechForm(dialogs);
            t.Show(this);
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var dfsearch = new DFSearch<string>(state => LoadedAsset.GetAllDialogueActionsByState(state).Select(dto => dto.NextState));
            dfsearch.InitializeSearch(IATConsts.INITIAL_DIALOGUE_STATE);
            dfsearch.FullSearch();

            int unreachableStatesCount = 0;
            int totalStates = 0;
            string unreachableStatesDescription = "The following Dialogue States are not reachable: \n[";

            foreach (var dAction in LoadedAsset.GetAllDialogueActions().GroupBy(da => da.CurrentState).Select(group => group.First()))
            {
                totalStates++;
                if (dfsearch.Closed.SearchInClosed(new NodeRecord<string>() { node = dAction.CurrentState.ToString() }) == null)
                {
                    unreachableStatesCount++;
                    unreachableStatesDescription += dAction.CurrentState + ", ";
                }
            }

            unreachableStatesDescription = unreachableStatesDescription.Remove(unreachableStatesDescription.Length - 2);
            unreachableStatesDescription += "]";


            string validationMessage;

            if (unreachableStatesCount > 0)
            {
                validationMessage = "Reachability: " + (totalStates - unreachableStatesCount) * 100 / totalStates + "%\n" + unreachableStatesDescription;
            }
            else
            {
                validationMessage = "All Dialogue States are reachable!";
            }

            //get the dead ends
            validationMessage += "\n\nEnd States:\n[" + string.Join(",", dfsearch.End.ToArray()) + "]";

            MessageBox.Show(validationMessage);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewCharacters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        protected override void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _rpcForm.Close();
            _rpcForm = new RolePlayCharacterWF.MainForm();
            
            CreateNewAsset();
        }


        protected override void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewCharacters.ClearSelection();
            _rpcForm.Close();
            Close();
        }

        protected override void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _rpcForm.LoadedAsset?.Save();
            _rpcForm.ClearModified();
            SaveAsset();
        }

        protected override void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _rpcForm.LoadedAsset?.Save();
            _rpcForm.ClearModified();
            SaveAssetAs();
        }

    }
}