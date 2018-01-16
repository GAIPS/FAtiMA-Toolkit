using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RolePlayCharacter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using Utilities.DataStructures;

namespace IntegratedAuthoringToolWF
{
    public partial class MainForm : BaseIATForm
    {
        private BindingListView<DialogueStateActionDTO> _dialogs;
        private readonly string PLAYER = IATConsts.PLAYER;
        private readonly string AGENT = IATConsts.AGENT;
        private BindingListView<CharacterSourceDTO> _characterSources;
        private RolePlayCharacterWF.MainForm _rpcForm = new RolePlayCharacterWF.MainForm();

        public MainForm()
        {
            InitializeComponent();
            buttonRemoveCharacter.Enabled = false;
        }

        private void RefreshDialogs()
        {
            _dialogs.DataSource = LoadedAsset.GetAllDialogueActions().Select(d => d.ToDTO()).ToList();
            _dialogs.Refresh();
            dataGridViewDialogueActions.Columns["Id"].Visible = false;
            dataGridViewDialogueActions.Columns["UtteranceId"].Visible = false;
        }

        protected override void OnAssetDataLoaded(IntegratedAuthoringToolAsset asset)
        {
            textBoxScenarioName.Text = asset.ScenarioName;
            textBoxScenarioDescription.Text = asset.ScenarioDescription;
            _characterSources = new BindingListView<CharacterSourceDTO>(asset.GetAllCharacterSources().ToList());
            dataGridViewCharacters.DataSource = _characterSources;
            _dialogs = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
            dataGridViewDialogueActions.DataSource = _dialogs;
            RefreshDialogs();
        }

        private void buttonCreateCharacter_Click(object sender, EventArgs e)
        {
            _rpcForm = new RolePlayCharacterWF.MainForm();
            var asset = _rpcForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;

            var rpcAsset = RolePlayCharacterAsset.LoadFromFile(asset.AssetFilePath);

            FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[1], _rpcForm);
            this.tabControlIAT.SelectTab(1);
            _rpcForm.LoadedAsset = rpcAsset;

            LoadedAsset.AddNewCharacterSource(new CharacterSourceDTO() { Source = asset.AssetFilePath });
            _characterSources.DataSource = LoadedAsset.GetAllCharacterSources().ToList();
            _characterSources.Refresh();
            SetModified();
        }

        private void buttonAddCharacter_Click(object sender, EventArgs e)
        {
            _rpcForm = new RolePlayCharacterWF.MainForm();
            var rpc = _rpcForm.SelectAndOpenAssetFromBrowser();
            if (rpc == null)
                return;

            LoadedAsset.AddNewCharacterSource(new CharacterSourceDTO()
            {
                Source = rpc.AssetFilePath
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
                var character = ((ObjectView<CharacterSourceDTO>)dataGridViewCharacters.SelectedRows[i].DataBoundItem).Object;
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

        [MenuItem("About", Priority = int.MaxValue)]
        private void ShowAbout()
        {
            var form = new AboutForm();
            form.ShowDialog(this);
        }

        #endregion About

        private void dataGridViewCharacters_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewCharacters.SelectedRows.Count > 0)
            {
                var rpcSource = ((ObjectView<CharacterSourceDTO>)dataGridViewCharacters.SelectedRows[0].DataBoundItem).Object;
                var rpc = RolePlayCharacterAsset.LoadFromFile(rpcSource.Source);
                _rpcForm.Close();
                _rpcForm = new RolePlayCharacterWF.MainForm();
                _rpcForm.LoadedAsset = rpc;
                FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[1], _rpcForm);
                this.tabControlIAT.SelectTab(1);

                buttonRemoveCharacter.Enabled = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void groupBoxDialogueEditor_Enter(object sender, EventArgs e)
        {
        }

        private void buttonAddDialogueAction_Click_1(object sender, EventArgs e)
        {
            new AddOrEditDialogueActionForm(this).ShowDialog(this);
            RefreshDialogs();
        }

        private void buttonEditDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(this, true, item.Id).ShowDialog(this);
                RefreshDialogs();
            }
        }

        private void buttonDuplicateDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewDialogueActions.SelectedRows[0].DataBoundItem).Object;

                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = item.CurrentState,
                    NextState = item.NextState,
                    Meaning = item.Meaning,
                    Style = item.Style,
                    Utterance = item.Utterance
                };
                LoadedAsset.AddDialogAction(newDialogueAction);
                RefreshDialogs();
            }
        }

        private void buttonRemoveDialogueAction_Click(object sender, EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            LoadedAsset.RemoveDialogueActions(itemsToRemove);
            RefreshDialogs();
            this.SetModified();
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
                var dialogs = ImportWorkSheet(excelDoc, "Dialogs").ToArray();

                //Clear all actions from the asset
                LoadedAsset.RemoveDialogueActions(LoadedAsset.GetAllDialogueActions().Select(d => d.ToDTO()));

                foreach (var d in dialogs)
                    LoadedAsset.AddDialogAction(d);
            }

            RefreshDialogs();
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
                    Meaning = rowValues[2],
                    Style = rowValues[3],
                    Utterance = rowValues[4]
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
                ExportWorkSheet(excelDoc, "Dialogs", LoadedAsset.GetAllDialogueActions().Select(d => d.ToDTO()));
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

                header.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = "Current State";
                worksheet.Cells[2, 2].Value = "Next State";
                worksheet.Cells[2, 3].Value = "Meaning";
                worksheet.Cells[2, 4].Value = "Style";
                worksheet.Cells[2, 5].Value = "Utterance";
            }

            worksheet.View.FreezePanes(3, 1);

            var cellIndex = 3;
            foreach (var d in dialogActions)
            {
                var line = worksheet.Cells[cellIndex, 1, cellIndex, 5];
                line.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                worksheet.Cells[cellIndex, 1].Value = d.CurrentState;
                worksheet.Cells[cellIndex, 2].Value = d.NextState;
                worksheet.Cells[cellIndex, 3].Value = d.Meaning;
                worksheet.Cells[cellIndex, 4].Value = d.Style;
                worksheet.Cells[cellIndex, 5].Value = d.Utterance;

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

            LoadedAsset.RemoveDialogueActions(LoadedAsset.GetAllDialogueActions().Select(d => d.ToDTO()));

            int stateCounter = 0;
            var lines = File.ReadAllLines(fileName.FullName);
            var totalSize = lines.Length;

            foreach (var line in lines)
            {
                var add = GenerateDialogueActionFromLine(line, totalSize, ref stateCounter);
                LoadedAsset.AddDialogAction(add);
            }

            RefreshDialogs();
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
                NextState = nextState,
                Utterance = result[0],
            };

            return add;
        }

        private void buttonTTS_Click(object sender, EventArgs e)
        {
            var dialogs = LoadedAsset.GetAllDialogueActions().Select(d => d.ToDTO()).ToArray();
            var t = new TextToSpeechForm(dialogs);
            t.Show(this);
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var dfsearch = new DFSearch<string>(state => LoadedAsset.GetDialogueActionsByState(state).Select(dto => dto.NextState));
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

        private void CalculateEmotions(object sender, EventArgs e)
        {
            Dictionary<string, Dictionary<string, int>> emotionList = new Dictionary<string, Dictionary<string, int>>();

            IntegratedAuthoringToolAsset loadedIAT = this.LoadedAsset;

            List<RolePlayCharacterAsset> rpcList = new List<RolePlayCharacterAsset>();

            List<WellFormedNames.Name> _eventList = new List<WellFormedNames.Name>();

            foreach (var rpc in loadedIAT.GetAllCharacterSources())
            {
                var actor = RolePlayCharacterAsset.LoadFromFile(rpc.Source); ;

                actor.LoadAssociatedAssets();

                loadedIAT.BindToRegistry(actor.DynamicPropertiesRegistry);

                rpcList.Add(actor);
            }

            foreach (var actor in rpcList)
            {
                foreach (var anotherActor in rpcList)
                {
                    if (actor != anotherActor)
                    {
                        var changed = new[] { EventHelper.ActionEnd(anotherActor.CharacterName.ToString(), "Enters", "Room") };
                        actor.Perceive(changed);
                    }
                }

                emotionList.Add(actor.CharacterName.ToString(), new Dictionary<string, int>());
                // actor.SaveToFile("../../../Tests/" + actor.CharacterName + "-output1" + ".rpc");
            }

            string validationMessage = "";

            var rpcActions = new Dictionary<ActionLibrary.IAction, RolePlayCharacterAsset>();

            var act = rpcList.FirstOrDefault().Decide();

            if (act == null)
            {
                foreach (var r in rpcList)
                {
                    act = r.Decide();
                    if (act != null)
                    {
                        rpcActions.Add(act.FirstOrDefault(), r);
                        break;
                    }
                }
            }

            int timestamp = 0;

            while (rpcActions.Keys != null)
            {  // Stopping condition kinda shaky
                rpcActions = new Dictionary<ActionLibrary.IAction, RolePlayCharacterAsset>();

                foreach (var rpc in rpcList)
                {
                    act = rpc.Decide();

                    if (act.FirstOrDefault() == null) continue;

                    foreach (var action in act)
                    {
                        rpcActions.Add(action, rpc);
                    }
                }

                // COPY the rpc list without linked refereces

                var newList = new List<RolePlayCharacterAsset>();  // mmm is the new list linked to the other? to be tested

                foreach (var action in rpcActions)
                {
                    // COPY the rpc list without linked refereces

                    var newListAux = new List<RolePlayCharacterAsset>(rpcList);  // mmm is the new list linked to the other? to be tested

                    foreach (var rpctoPerceive in newListAux)
                    {
                        _eventList = new List<WellFormedNames.Name>();

                        if (rpctoPerceive.CharacterName != action.Value.CharacterName)
                        {
                            if (action.Key.Name.ToString().Contains("Speak") && action.Key.Target == rpctoPerceive.CharacterName)
                                _eventList.Add(EventHelper.PropertyChange("DialogueState(" + action.Value.CharacterName.ToString() + ")", action.Key.Parameters.ElementAt(1).ToString(), action.Value.CharacterName.ToString()));

                            _eventList.Add(EventHelper.ActionEnd(action.Value.CharacterName.ToString(), action.Key.Name.ToString(), action.Key.Target.ToString()));

                            rpctoPerceive.Perceive(_eventList);

                            newList.Add(rpctoPerceive);
                        }
                    }
                }

                rpcList.Clear();
                rpcList = newList;

                foreach (var rpc in rpcList)
                {
                    foreach (var emot in rpc.GetAllActiveEmotions())
                    {
                        if (!emotionList[rpc.CharacterName.ToString()].Keys.Contains(emot.Type))
                            emotionList[rpc.CharacterName.ToString()].Add(emot.Type, (timestamp + 1));
                    }
                }
                timestamp++;
                if (timestamp > 1) break;
            }

            if (emotionList.Count > 0)
            {
                validationMessage = "Simulation result:                " + "\n";

                foreach (var rpc in emotionList.Keys)
                {
                    validationMessage += rpc + " felt: " + "\n";

                    foreach (var emot in emotionList[rpc])
                    {
                        var lastFor = timestamp - emot.Value;
                        double timePercentage = ((double)lastFor / (double)timestamp) * 100;
                        validationMessage += "Emotion: " + emot.Key + " ( " + timePercentage + "% of total scenario time)\n";
                    }
                }
            }
            else
            {
                validationMessage += "No emotions detected";
            }

            MessageBox.Show(validationMessage);
        }

        private List<EmotionalAppraisal.DTOs.EmotionDTO> updateEmotionList(List<EmotionalAppraisal.DTOs.EmotionDTO> b, List<RolePlayCharacterAsset> rpcList)
        {
            foreach (var rpc in rpcList)
            {
                //     rpc.Perceive(EventHelper.ActionEnd("SELF" d.ToString(), );

                foreach (var e in rpc.GetAllActiveEmotions())
                    b.Add(e);
            }

            return b;
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
            if (_rpcForm.LoadedAsset != null)
            {
                _rpcForm.SaveAssetToFile(_rpcForm.LoadedAsset, _rpcForm.LoadedAsset.AssetFilePath);
                _rpcForm.SaveSubAssets();
                _rpcForm.ClearModified();
            }
            SaveAsset();
        }

        protected override void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rpcForm.LoadedAsset != null)
            {
                _rpcForm.SaveAssetToFile(_rpcForm.LoadedAsset, _rpcForm.LoadedAsset.AssetFilePath);
                _rpcForm.SaveSubAssets();
                _rpcForm.ClearModified();
            }
            SaveAssetAs();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void computeEmotions_Click(object sender, EventArgs e)
        {
            CalculateEmotions(sender, e);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void dataGridViewDialogueActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewDialogueActions_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewDialogueActions_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1) //exclude header cells
            {
                this.buttonEditDialogueAction_Click(sender, e);
            }
        }

        private void dataGridViewDialogueActions_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void dataGridViewDialogueActions_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    this.buttonEditDialogueAction_Click(sender, e);
                    e.Handled = true;
                    break;

                case Keys.D:
                    if (e.Control) this.buttonDuplicateDialogueAction_Click(sender, e);
                    break;

                case Keys.Delete:
                    this.buttonRemoveDialogueAction_Click(sender, e);
                    break;
            }
        }
    }
}