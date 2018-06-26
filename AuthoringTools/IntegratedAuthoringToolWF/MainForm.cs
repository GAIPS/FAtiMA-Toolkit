using ActionLibrary;
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
using GAIPS.Rage;
using Utilities;
using Utilities.DataStructures;
using WorldModel;
using WorldModel.DTOs;


namespace IntegratedAuthoringToolWF
{
    public partial class MainForm : BaseIATForm
    {
        private BindingListView<DialogueStateActionDTO> _dialogs;

        private readonly string PLAYER = IATConsts.PLAYER;
        private readonly string AGENT = IATConsts.AGENT;
        private BindingListView<CharacterSourceDTO> _characterSources;
        private RolePlayCharacterWF.MainForm _rpcForm = new RolePlayCharacterWF.MainForm();
        private WorldModelWF.MainForm _wmForm = new WorldModelWF.MainForm();
        private WorldModelSourceDTO _wmSource = new WorldModelSourceDTO();

        private int currentRPCTabIndex;

        public MainForm()
        {
            InitializeComponent();
            buttonRemoveCharacter.Enabled = false;
            buttonInspect.Enabled = false;
        }

        private void RefreshDialogs()
        {
            _dialogs.DataSource = LoadedAsset.GetAllDialogueActions().ToList();
            EditorTools.HideColumns(dataGridViewDialogueActions, new[]
                {
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.Id),
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.UtteranceId),
                }
            );

            EditorTools.HideColumns(dataGridViewCharacters, new[]
                {
                    PropertyUtil.GetPropertyName<CharacterSourceDTO>(s => s.Source),
                }
            );

        }

        protected override void OnAssetDataLoaded(IntegratedAuthoringToolAsset asset)
        {
            textBoxScenarioName.Text = asset.ScenarioName;
            textBoxScenarioDescription.Text = asset.ScenarioDescription;
            _characterSources = new BindingListView<CharacterSourceDTO>(asset.GetAllCharacterSources().ToList());
            _wmSource = asset.GetWorldModelSource();
            dataGridViewCharacters.DataSource = _characterSources;
            _dialogs = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
            dataGridViewDialogueActions.DataSource = _dialogs;


            if (_wmSource != null)
            {
                if (_wmSource.Source != ""){
                    pathTextBoxWorldModel.Text = _wmSource.Source;
                LoadWorldModelForm();

            }
        }

        //ResetSimulator
            richTextBoxChat.Clear();
            buttonContinue.Enabled = false;
            textBoxTick.Text = "";


            

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
            this.currentRPCTabIndex = 1;
            _rpcForm.LoadedAsset = rpcAsset;

            LoadedAsset.AddNewCharacterSource(new CharacterSourceDTO() {Source = asset.AssetFilePath});
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
                var character = ((ObjectView<CharacterSourceDTO>) dataGridViewCharacters.SelectedRows[i].DataBoundItem)
                    .Object;
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
            var rpcSource = EditorTools.GetSelectedDtoFromTable<CharacterSourceDTO>(dataGridViewCharacters);
            if (rpcSource != null)
            {
                var rpc = RolePlayCharacterAsset.LoadFromFile(rpcSource.Source);
                var selectedRPCTab = _rpcForm.SelectedTab;
                _rpcForm.Close();
                _rpcForm = new RolePlayCharacterWF.MainForm();
                _rpcForm.LoadedAsset = rpc;
                FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[1], _rpcForm);
                this.tabControlIAT.SelectTab(1);
                _rpcForm.SelectedTab = selectedRPCTab;
                buttonRemoveCharacter.Enabled = true;
                buttonInspect.Enabled = true;
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
            this.auxAddOrUpdateItem(new DialogueStateActionDTO());
        }

        private void buttonEditDialogueAction_Click(object sender, EventArgs e)
        {
            var rule = EditorTools.GetSelectedDtoFromTable<DialogueStateActionDTO>(this.dataGridViewDialogueActions);
            if (rule != null)
            {
                this.auxAddOrUpdateItem(rule);
            }
        }

        private void auxAddOrUpdateItem(DialogueStateActionDTO item)
        {
            var diag = new AddOrEditDialogueActionForm(LoadedAsset, item);
            diag.ShowDialog(this);
            if (diag.UpdatedGuid != Guid.Empty)
            {
                _dialogs.DataSource = LoadedAsset.GetAllDialogueActions().ToList();
                EditorTools.HighlightItemInGrid<DialogueStateActionDTO>
                    (dataGridViewDialogueActions, diag.UpdatedGuid);
            }

            SetModified();
        }


        private void buttonDuplicateDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>) dataGridViewDialogueActions.SelectedRows[0]
                    .DataBoundItem).Object;

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
                var item = ((ObjectView<DialogueStateActionDTO>) dataGridViewDialogueActions.SelectedRows[i]
                    .DataBoundItem).Object;
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
                LoadedAsset.RemoveDialogueActions(LoadedAsset.GetAllDialogueActions());

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
                ExportWorkSheet(excelDoc, "Dialogs", LoadedAsset.GetAllDialogueActions());
                excelDoc.SaveAs(fileName);
            }
        }

        private static void ExportWorkSheet(ExcelPackage package, string workSheetName,
            IEnumerable<DialogueStateActionDTO> dialogActions)
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

            LoadedAsset.RemoveDialogueActions(LoadedAsset.GetAllDialogueActions());

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
            char[] delimitedchars = {'\n'};
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
            var dialogs = LoadedAsset.GetAllDialogueActions().ToArray();
            var t = new TextToSpeechForm(dialogs);
            t.Show(this);
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var dfsearch = new DFSearch<string>(state =>
                LoadedAsset.GetDialogueActionsByState(state).Select(dto => dto.NextState));
            dfsearch.InitializeSearch(IATConsts.INITIAL_DIALOGUE_STATE);
            dfsearch.FullSearch();

            int unreachableStatesCount = 0;
            int totalStates = 0;
            string unreachableStatesDescription = "The following Dialogue States are not reachable: \n[";

            foreach (var dAction in LoadedAsset.GetAllDialogueActions().GroupBy(da => da.CurrentState)
                .Select(group => group.First()))
            {
                totalStates++;
                if (dfsearch.Closed.SearchInClosed(new NodeRecord<string>() {node = dAction.CurrentState.ToString()}) ==
                    null)
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
                validationMessage = "Reachability: " + (totalStates - unreachableStatesCount) * 100 / totalStates +
                                    "%\n" + unreachableStatesDescription;
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
                var actor = RolePlayCharacterAsset.LoadFromFile(rpc.Source);
                ;

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
                        var changed = new[]
                            {EventHelper.ActionEnd(anotherActor.CharacterName.ToString(), "Enters", "Room")};
                        actor.Perceive(changed);
                    }
                }

                emotionList.Add(actor.CharacterName.ToString(), new Dictionary<string, int>());
                // actor.SaveToFile("../../../Tests/" + actor.CharacterName + "-output1" + ".rpc");
            }

            string validationMessage = "";

            var rpcActions = new Dictionary<IAction, RolePlayCharacterAsset>();

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
            {
                // Stopping condition kinda shaky
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

                var newList =
                    new List<RolePlayCharacterAsset>(); // mmm is the new list linked to the other? to be tested

                foreach (var action in rpcActions)
                {
                    // COPY the rpc list without linked refereces

                    var newListAux =
                        new List<RolePlayCharacterAsset>(
                            rpcList); // mmm is the new list linked to the other? to be tested

                    foreach (var rpctoPerceive in newListAux)
                    {
                        _eventList = new List<WellFormedNames.Name>();

                        if (rpctoPerceive.CharacterName != action.Value.CharacterName)
                        {
                            if (action.Key.Name.ToString().Contains("Speak") &&
                                action.Key.Target == rpctoPerceive.CharacterName)
                                _eventList.Add(EventHelper.PropertyChange(
                                    "DialogueState(" + action.Value.CharacterName.ToString() + ")",
                                    action.Key.Parameters.ElementAt(1).ToString(),
                                    action.Value.CharacterName.ToString()));

                            _eventList.Add(EventHelper.ActionEnd(action.Value.CharacterName.ToString(),
                                action.Key.Name.ToString(), action.Key.Target.ToString()));

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
                        double timePercentage = ((double) lastFor / (double) timestamp) * 100;
                        validationMessage += "Emotion: " + emot.Key + " ( " + timePercentage +
                                             "% of total scenario time)\n";
                    }
                }
            }
            else
            {
                validationMessage += "No emotions detected";
            }

            MessageBox.Show(validationMessage);
        }

        private List<EmotionalAppraisal.DTOs.EmotionDTO> updateEmotionList(List<EmotionalAppraisal.DTOs.EmotionDTO> b,
            List<RolePlayCharacterAsset> rpcList)
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
           
            pathTextBoxWorldModel.Text = null; 
            _wmForm.Close();
            _wmForm = new WorldModelWF.MainForm();
            
            CreateNewAsset();
        }

        protected override void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridViewCharacters.ClearSelection();
            _rpcForm.Close();
            _wmForm.Close();
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
            if(_wmForm.LoadedAsset != null)
                _wmForm.SaveAssetToFile(_wmForm.LoadedAsset, _wmForm.LoadedAsset.AssetFilePath );

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
            if(_wmForm.LoadedAsset != null)
                _wmForm.SaveAssetToFile(_wmForm.LoadedAsset, _wmForm.LoadedAsset.AssetFilePath );

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

        private void buttonInspect_Click(object sender, EventArgs e)
        {
            _rpcForm.LoadedAsset.Save();
            var rpcSource = EditorTools.GetSelectedDtoFromTable<CharacterSourceDTO>(dataGridViewCharacters);
            if (rpcSource != null)
            {
                new RPCInspectForm(LoadedAsset, rpcSource.Source).Show(this);
            }
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private List<RolePlayCharacterAsset> agentsInChat;


        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.saveToolStripMenuItem_Click(sender, e);
            }
            catch (Exception ex)
            {
                EditorTools.WriteText(richTextBoxChat, "The IAT asset must be saved", Color.Red, true);
                return;
            }

            var sources = LoadedAsset.GetAllCharacterSources();
            if (sources.Count() <= 1)
            {
                EditorTools.WriteText(richTextBoxChat, "At least two characters are needed.", Color.Red, true);
                return;
            }

            agentsInChat = new List<RolePlayCharacterAsset>();
            foreach (var s in sources)
            {
                var rpc = RolePlayCharacterAsset.LoadFromFile(s.Source);
                rpc.LoadAssociatedAssets();
                LoadedAsset.BindToRegistry(rpc.DynamicPropertiesRegistry);
                if (rpc.CharacterName.ToString().ToLower().Contains("player"))
                {
                    rpc.IsPlayer = true;
                }

                agentsInChat.Add(rpc);
            }

            richTextBoxChat.Clear();
            listBoxPlayerDialogues.DataSource = new List<string>();
            comboBoxAgChat.DataSource = agentsInChat.Select(a => a.CharacterName.ToString()).ToList();

            EditorTools.WriteText(richTextBoxChat, "Characters were loaded with success (" + DateTime.Now + ")",
                Color.Blue, true);
            var enterEvents = new List<WellFormedNames.Name>();
            foreach (var ag in agentsInChat)
            {
                enterEvents.Add(EventHelper.ActionEnd(ag.CharacterName.ToString(), "Enters", "-"));
            }

            foreach (var ag in agentsInChat)
            {
                ag.Perceive(enterEvents);

                EditorTools.WriteText(richTextBoxChat, ag.CharacterName + " enters the chat.", Color.Black, false);
                EditorTools.WriteText(richTextBoxChat,
                    " (" + ag.GetInternalStateString() + " | " + ag.GetSIRelationsString() + ")", Color.DarkRed, true);
            }

            EditorTools.WriteText(richTextBoxChat, "", Color.Black, true);


            buttonContinue.Enabled = true;
            textBoxTick.Text = agentsInChat[0].Tick.ToString();
            this.buttonContinue_Click(sender, e);
        }



        private void buttonContinue_Click(object sender, EventArgs e)
        {
            foreach (var ag in agentsInChat)
            {
                if (ag.IsPlayer) continue;
                var decisions = ag.Decide().Where(a => a.Key.ToString() == IATConsts.DIALOG_ACTION_KEY);
                if (decisions.Any())
                {
                    var action = decisions.First();
                    string error;
                    var diag = LoadedAsset.GetDialogAction(action, out error);
                    if (error != null)
                    {
                        EditorTools.WriteText(richTextBoxChat, ag.CharacterName + " : " + error, Color.Red, true);
                    }
                    else if (this.ValidateTarget(action, ag.CharacterName.ToString()))
                    {
                        auxHandlePropertyChangesForDialogAction(ag.CharacterName.ToString(), action,
                            diag.NextState.ToString());

                        EditorTools.WriteText(richTextBoxChat,
                            ag.CharacterName + " To " + action.Target + " : ", Color.ForestGreen, false);

                        EditorTools.WriteText(richTextBoxChat, diag.Utterance, Color.Black, false);

                        EditorTools.WriteText(richTextBoxChat,
                            " (" + ag.GetInternalStateString() + " | " + ag.GetSIRelationsString() + ")", Color.DarkRed,
                            true);


                    }
                }

                ag.Update();
            }

            EditorTools.WriteText(richTextBoxChat, "", Color.Black, true);

            var playerRPC = agentsInChat.First(a => a.IsPlayer == true);
            var playerDecisions = playerRPC.Decide().Where(a => a.Key.ToString() == IATConsts.DIALOG_ACTION_KEY);
            if (playerDecisions.Any())
            {
                this.determinePlayerDialogueOptions(playerDecisions, playerRPC.CharacterName.ToString());
            }
            else
            {
                listBoxPlayerDialogues.DataSource = new List<string>();
            }

            playerRPC.Update();

            //Assumption: All agents have the same tick
            textBoxTick.Text = agentsInChat[0].Tick.ToString();
        }

        private List<IAction> playerActions;

        private void determinePlayerDialogueOptions(IEnumerable<IAction> actions, string playerCharName)
        {
            playerActions = new List<IAction>();
            string error;
            foreach (var a in actions)
            {
                var diag = LoadedAsset.GetDialogAction(a, out error);
                if (error != null)
                {
                    EditorTools.WriteText(richTextBoxChat, playerCharName + " : " + error, Color.Red, true);
                }
                else if (this.ValidateTarget(a, playerCharName))
                {
                    playerActions.Add(a);
                }
            }

            listBoxPlayerDialogues.DataSource = playerActions.Select(x => "To " + x.Target + " : " +
                                                                          LoadedAsset.GetDialogAction(x, out error)
                                                                              .Utterance).ToList();
        }


        private bool ValidateTarget(IAction action, string actor)
        {
            var targetAgent = agentsInChat.FirstOrDefault(x => x.CharacterName == action.Target);
            if (targetAgent == null)
            {
                EditorTools.WriteText(richTextBoxChat,
                    actor + " : Invalid Target '" + action.Target + "' for " + action, Color.Red, true);
                return false;
            }
            else return true;
        }

        private void listBoxPlayerDialogues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var idx = listBoxPlayerDialogues.SelectedIndex;
            if (idx == -1) return;

            var act = playerActions[idx];

            EditorTools.WriteText(richTextBoxChat,
                "Player " + listBoxPlayerDialogues.SelectedItem.ToString(), Color.Blue, true);

            var playerRPC = agentsInChat.First(x => x.IsPlayer);
            string error;
            var dialog = LoadedAsset.GetDialogAction(act, out error);
           // this.auxHandlePropertyChangesForDialogAction(playerRPC.CharacterName.ToString(), act, dialog.NextState);

            this.HandleEffects(act, playerRPC);

            this.buttonContinue_Click(sender, e);
        }


        private void HandleEffects(IAction action, RolePlayCharacterAsset actor)
        {
            if(LoadedAsset.m_worldModelSource.Source == null)
                return;

            var wm = WorldModelAsset.LoadFromFile(LoadedAsset.m_worldModelSource.Source);

               var events = new List<WellFormedNames.Name>();

            var target = action.Target;
             events.Add(EventHelper.ActionEnd(actor.CharacterName.ToString(), action.ToString(),target.ToString() ));

               var effects = wm.Simulate(events);

            string toWrite ="";
            toWrite += "Effects: \n";

            Dictionary<string,string> observerAgents = new Dictionary<string, string>();

             foreach (var eff in effects )
        {

            var ef = eff.ToPropertyChangeEvent();
           
            foreach(var a in agentsInChat){

            if(eff.ObserverAgent == a.CharacterName){

                 
               if(!observerAgents.ContainsKey(a.CharacterName.ToString())){
                            
                            observerAgents.Add(a.CharacterName.ToString(), ef.GetNTerm(3).ToString() + " = " + ef.GetNTerm(4).ToString());

                        }   else observerAgents[a.CharacterName.ToString()] += ", " + ef.GetNTerm(3).ToString() + " = " + ef.GetNTerm(4).ToString();
               
                a.Perceive(ef);
                        
                        
                        }
            }
            
            }
                 foreach(var o in observerAgents)
            {
                toWrite += o.Key + ": " + o.Value + "\n";
            }


              EditorTools.WriteText(richTextBoxChat,
              toWrite, Color.Black, true);
         

        }
        private void auxHandlePropertyChangesForDialogAction(string actor, IAction action, string nextState)
        {
            foreach (var agent in agentsInChat)
            {
                var evt = EventHelper.ActionEnd(
                    actor,
                    action.Name.ToString(),
                    action.Target.ToString());

                agent.Perceive(evt);

                evt = EventHelper.PropertyChange(
                    IATConsts.HAS_FLOOR_PROPERTY,
                    action.Target.ToString(),
                    actor);

                agent.Perceive(evt);
            }


            if (nextState != WellFormedNames.Name.NIL_STRING)
            {
                var targetAgent = agentsInChat.FirstOrDefault(x => x.CharacterName == action.Target);
                targetAgent.Perceive(
                    EventHelper.PropertyChange(
                        string.Format(IATConsts.DIALOGUE_STATE_PROPERTY, actor),
                        nextState,
                        actor
                    ));

                var actorAgent = agentsInChat.FirstOrDefault(x => x.CharacterName.ToString() == actor);
                actorAgent.Perceive(
                    EventHelper.PropertyChange(
                        string.Format(IATConsts.DIALOGUE_STATE_PROPERTY, targetAgent.CharacterName.ToString()),
                        nextState,
                        actor
                    ));
            }
        }

        private void textBoxBelChat_TextChanged(object sender, EventArgs e)
        {
            var selectedRPCName = (string) comboBoxAgChat.SelectedItem;

            if (string.IsNullOrWhiteSpace(textBoxBelChat.Text) ||
                string.IsNullOrWhiteSpace(selectedRPCName))
            {
                textBoxValChat.Text = "-";
                return;
            }

            var rpc = agentsInChat.Where(c => c.CharacterName.ToString() == selectedRPCName).FirstOrDefault();
            try
            {
                var name = WellFormedNames.Name.BuildName(textBoxBelChat.Text);
                if (name.IsGrounded && name.IsComposed)
                {
                    var bel = rpc.GetBeliefValue(name.ToString());
                    textBoxValChat.Text = bel;
                }
                else
                {
                    textBoxValChat.Text = "-";
                }
            }
            catch (Exception ex)
            {
                textBoxValChat.Text = ex.Message;
            }
        }

        private void comboBoxAgChat_SelectedValueChanged(object sender, EventArgs e)
        {
            textBoxBelChat_TextChanged(sender, e);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void createNewWorldModelButton_Click(object sender, EventArgs e)
        {

            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the IAT asset");
                return;
            }

            _wmForm = new WorldModelWF.MainForm();

            var asset = _wmForm.CreateAndSaveEmptyAsset(false);
            if (asset == null)
                return;
            LoadedAsset.m_worldModelSource = new WorldModelSourceDTO();
            LoadedAsset.m_worldModelSource.Source = asset.AssetFilePath;
            LoadedAsset.m_worldModelSource.RelativePath =
                LoadableAsset<WorldModelAsset>.ToRelativePath(LoadedAsset.AssetFilePath,
                    asset.AssetFilePath);
            SetModified();
            ReloadEditor();

           
        }

        private void openWorldModelButton_Click(object sender, EventArgs e)
        {
            if (LoadedAsset.AssetFilePath == null)
            {
                MessageBox.Show("You must first save the IAT asset");
                return;
            }
            _wmForm = new WorldModelWF.MainForm();
            var asset = _wmForm.SelectAndOpenAssetFromBrowser();
            if (asset == null)
                return;

            LoadedAsset.m_worldModelSource = new WorldModelSourceDTO();

            LoadedAsset.m_worldModelSource.RelativePath =
                LoadableAsset<WorldModelAsset>.ToRelativePath(LoadedAsset.AssetFilePath,
                    asset.AssetFilePath);
            LoadedAsset.m_worldModelSource.Source = asset.AssetFilePath;

            SetModified();
            ReloadEditor();
        }



        private void LoadWorldModelForm()
        {


            var wm = WorldModelAsset.LoadFromFile(LoadedAsset.m_worldModelSource.Source);
         
            _wmForm = new WorldModelWF.MainForm();
          _wmForm.LoadedAsset = wm;

            this.pathTextBoxWorldModel.Text = LoadableAsset<WorldModelAsset>.ToRelativePath(LoadedAsset.AssetFilePath, this.LoadedAsset.m_worldModelSource.Source);

            _wmForm.Refresh();
            FormHelper.ShowFormInContainerControl(groupBox7, _wmForm);
        }

        private void pathTextBoxEA_TextChanged(object sender, EventArgs e)
        {

        }

        private void clearWorldModelButton_Click(object sender, EventArgs e)
        {
            LoadedAsset.m_worldModelSource = null;
            pathTextBoxWorldModel.Text = null; 
            SetModified();
            _wmForm.Refresh();
            _wmForm.Hide();
        }

        private void displayGraph_Click(object sender, EventArgs e)
        {

            Dictionary<string, List<string>> states = new Dictionary<string, List<string>>();
          
            foreach (var d in LoadedAsset.GetAllDialogueActions())
            {
                if (states.ContainsKey(d.CurrentState))
                {
                 states[d.CurrentState].Add(d.NextState);   
                }
                else 
                states.Add(d.CurrentState, new List<string>(){d.NextState});
            }

      

            string writer = "";
       

            writer += "digraph { \n";
            writer += "node[fontsize=10, labelloc = \"t\", labeljust = \"l\"]; \n";
         
                foreach (var s in states.Keys)
                {
                        foreach (var ns in states[s])
                        {
                    if (s != "-")
                        writer += s + "->" + ns + "\n";
                    else if (ns != "-")
                        writer += "Any" + "->" + ns + "\n";
                    else  writer += "Any" + "->" + "Any" + "\n";
                        }

                }

                writer+= "}";
      

           Bitmap bit =  Run(writer);
          var image =  new ImageForm(bit);
          image.Show();

            //      Graphics.DrawImage(bit, 60, 10);

        }

        public static Bitmap Run(string dot)
        {
            string executable = @".\external\dot.exe";
            string output = @".\external\tempgraph";
            File.WriteAllText(output, dot);

            System.Diagnostics.Process process = new System.Diagnostics.Process();

            // Stop the process from opening a new window
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            // Setup executable and parameters
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = string.Format(@"{0} -Tjpg -O", output);

            // Go
            process.Start();
            // and wait dot.exe to complete and exit
            process.WaitForExit();
            Bitmap bitmap = null; ;

            try{
            using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
            {
                Image image = Image.FromStream(bmpStream);
                bitmap = new Bitmap(image);
            } }
            catch (Exception e)
            {
              MessageBox.Show("Error: " + e.Message);
            }
            File.Delete(output);
           File.Delete(output + ".jpg");
            return bitmap;
        }


    }
}