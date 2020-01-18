using ActionLibrary;
using AutobiographicMemory.DTOs;
using CommeillFaut;
using EmotionalAppraisal;
using EmotionalDecisionMaking;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using KnowledgeBase.DTOs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RolePlayCharacter;
using SocialImportance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using Utilities.DataStructures;
using WellFormedNames;
using WorldModel;

namespace IntegratedAuthoringToolWF
{
    public partial class MainForm : Form
    {
        private BindingListView<DialogueStateActionDTO> _dialogs;
        private BindingListView<CharacterNameAndMoodDTO> _characters;

        private WorldModelWF.MainForm _wmForm;
        private WebAPIWF.MainForm _webForm;
        private EmotionalAppraisalWF.MainForm _eaForm;
        private EmotionalDecisionMakingWF.MainForm _edmForm;
        private SocialImportanceWF.MainForm _siForm;
        private CommeillFautWF.MainForm _cifForm;
        private RolePlayCharacterWF.MainForm _rpcForm;

        private IntegratedAuthoringToolAsset _iat;
        private AssetStorage _storage;
        public string _currentScenarioFilePath;

        private IList<RolePlayCharacterAsset> _agentsInSimulation;

        public MainForm()
        {
            InitializeComponent();
            buttonRemoveCharacter.Enabled = false;
            buttonInspect.Enabled = false;

            _iat = new IntegratedAuthoringToolAsset();
            _agentsInSimulation = new List<RolePlayCharacterAsset>();
            _storage = new AssetStorage();
            _webForm = new WebAPIWF.MainForm();
            _webForm.iat = this;
            _eaForm = new EmotionalAppraisalWF.MainForm();
            _edmForm = new EmotionalDecisionMakingWF.MainForm();
            _siForm = new SocialImportanceWF.MainForm();
            _cifForm = new CommeillFautWF.MainForm();
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            OnAssetStorageChange();
            OnAssetDataLoaded(_iat);
        }




        private void OnAssetStorageChange()
        {
            _eaForm.Asset = EmotionalAppraisalAsset.CreateInstance(_storage);
            _edmForm.Asset = EmotionalDecisionMakingAsset.CreateInstance(_storage);
            _siForm.Asset = SocialImportanceAsset.CreateInstance(_storage);
            _cifForm.Asset = CommeillFautAsset.CreateInstance(_storage);
        }

        private void SaveAssetRules()
        {
            _edmForm.Asset.Save();
            _eaForm.Asset.Save();
            _cifForm.Asset.Save();
            _siForm.Asset.Save();
        }

        private void RefreshDialogs()
        {
            _dialogs.DataSource = _iat.GetAllDialogueActions().ToList();
            EditorTools.HideColumns(dataGridViewDialogueActions, new[]
                {
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.Id),
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.UtteranceId),
                }
            );
        }

        public void RefreshCharacters()
        {
            _characters.DataSource = _iat.Characters.Select(c =>
                        new CharacterNameAndMoodDTO
                        {
                            Name = c.CharacterName.ToString(),
                            Mood = c.Mood
                        }).ToList();
            dataGridViewCharacters.ClearSelection();
            if (_rpcForm != null)
            {
                _rpcForm.Close();
                _rpcForm = null;
            }
        }



        protected void OnAssetDataLoaded(IntegratedAuthoringToolAsset asset)
        {
            textBoxScenarioName.Text = asset.ScenarioName;
            textBoxScenarioDescription.Text = asset.ScenarioDescription;
            _dialogs = new BindingListView<DialogueStateActionDTO>(new List<DialogueStateActionDTO>());
            _characters = new BindingListView<CharacterNameAndMoodDTO>(new List<CharacterNameAndMoodDTO>());
            dataGridViewCharacters.DataSource = _characters;
            dataGridViewDialogueActions.DataSource = _dialogs;


            //ResetSimulator
            richTextBoxChat.Clear();
            buttonContinue.Enabled = false;
            textBoxTick.Text = "";
            comboBoxPlayerRpc.Items.Clear();
            comboBoxPlayerRpc.Items.Add("-");
            comboBoxPlayerRpc.SelectedItem = "-";
      

            searchCheckList.Items.Clear();
            searchCheckList.Items.Add("CurrentState", true);
            searchCheckList.Items.Add("NextState", false);
            searchCheckList.Items.Add("Meaning", false);
            searchCheckList.Items.Add("Style", false);
            searchCheckList.Items.Add("Utterance", false);

            _wmForm = new WorldModelWF.MainForm(_iat.WorldModel);
            FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[3], _wmForm);
            FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[5], _webForm);

            FormHelper.ShowFormInContainerControl(this.tabControlAssetEditor.TabPages[0], _eaForm);
            FormHelper.ShowFormInContainerControl(this.tabControlAssetEditor.TabPages[1], _edmForm);
            FormHelper.ShowFormInContainerControl(this.tabControlAssetEditor.TabPages[2], _siForm);
            FormHelper.ShowFormInContainerControl(this.tabControlAssetEditor.TabPages[3], _cifForm);

            if (_rpcForm != null)
            {
                _rpcForm.Close();
                _rpcForm = null;
            }

            RefreshDialogs();
            RefreshCharacters();
        }


        private void buttonAddCharacter_Click(object sender, EventArgs e)
        {
            new AddCharacterForm(_iat).ShowDialog(this);
            RefreshCharacters();
        }

        private void textBoxScenarioName_TextChanged(object sender, EventArgs e)
        {
            _iat.ScenarioName = textBoxScenarioName.Text;
        }

        private void textBoxScenarioDescription_TextChanged(object sender, EventArgs e)
        {
            _iat.ScenarioDescription = textBoxScenarioDescription.Text;
        }

        private void buttonRemoveCharacter_Click(object sender, EventArgs e)
        {
            var charactersToRemove = new List<string>();
            for (var i = 0; i < dataGridViewCharacters.SelectedRows.Count; i++)
            {
                var character = ((ObjectView<CharacterNameAndMoodDTO>)dataGridViewCharacters.SelectedRows[i].DataBoundItem)
                    .Object;
                charactersToRemove.Add(character.Name);
            }
            _iat.RemoveCharacters(charactersToRemove);
            if (!_iat.Characters.Any())
            {
                buttonRemoveCharacter.Enabled = false;
                buttonInspect.Enabled = false;
            }

            RefreshCharacters();

            if (_rpcForm != null)
            {
                _rpcForm.Close();
                _rpcForm = null;
            }

            comboBoxPlayerRpc.SelectedItem = "-";
        }

        private void dataGridViewCharacters_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var selectedRPC = EditorTools.GetSelectedDtoFromTable<CharacterNameAndMoodDTO>(dataGridViewCharacters);

            if (selectedRPC != null && _rpcForm != null && _rpcForm.Asset.CharacterName.ToString() == selectedRPC.Name)
            {
                return;
            }

            if (selectedRPC != null)
            {
                var rpc = _iat.Characters.Where(r => r.CharacterName.ToString() == selectedRPC.Name).FirstOrDefault();
                int selectedRPCTab = 0;
                if (_rpcForm != null)
                {
                    selectedRPCTab = _rpcForm.SelectedTab;
                    _rpcForm.Close();
                }
                _rpcForm = new RolePlayCharacterWF.MainForm();
                _rpcForm.OnNameChangeEvent += this.OnRPCNameChange;
                _rpcForm.OnMoodChangeEvent += this.OnRPCMoodChange;

                _rpcForm.OnAssetDataLoaded();
                _rpcForm.Asset = rpc;
                FormHelper.ShowFormInContainerControl(this.tabControlIAT.TabPages[2], _rpcForm);
                this.tabControlIAT.SelectTab(2);
                _rpcForm.SelectedTab = selectedRPCTab;
                buttonInspect.Enabled = true;
                buttonRemoveCharacter.Enabled = true;
            }
        }

        private void OnRPCNameChange(string newName)
        {
            dataGridViewCharacters.SelectedRows[0].Cells[0].Value = newName;
        }

        private void OnRPCMoodChange(float newMood)
        {
            dataGridViewCharacters.SelectedRows[0].Cells[1].Value = newMood;
        }

        #region About

        [MenuItem("About", Priority = int.MaxValue)]
        private void ShowAbout()
        {
            var form = new AboutForm();
            form.ShowDialog(this);
        }

        #endregion About


        private void MainForm_Load(object sender, EventArgs e)
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
            var diag = new AddOrEditDialogueActionForm(_iat, item);
            diag.ShowDialog(this);
            if (diag.UpdatedGuid != Guid.Empty)
            {
                _dialogs.DataSource = _iat.GetAllDialogueActions().ToList();
                EditorTools.HighlightItemInGrid<DialogueStateActionDTO>
                    (dataGridViewDialogueActions, diag.UpdatedGuid);
            }
        }

        private void buttonDuplicateDialogueAction_Click(object sender, EventArgs e)
        {
            if (dataGridViewDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewDialogueActions.SelectedRows[0]
                    .DataBoundItem).Object;

                var newDialogueAction = new DialogueStateActionDTO
                {
                    CurrentState = item.CurrentState,
                    NextState = item.NextState,
                    Meaning = item.Meaning,
                    Style = item.Style,
                    Utterance = item.Utterance
                };
                _iat.AddDialogAction(newDialogueAction);
                RefreshDialogs();
            }
        }

        private void buttonRemoveDialogueAction_Click(object sender, EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<DialogueStateActionDTO>)dataGridViewDialogueActions.SelectedRows[i]
                    .DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }

            _iat.RemoveDialogueActions(itemsToRemove);
            RefreshDialogs();
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
                _iat.RemoveDialogueActions(_iat.GetAllDialogueActions());

                foreach (var d in dialogs)
                    _iat.AddDialogAction(d);
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
                ExportWorkSheet(excelDoc, "Dialogs", _iat.GetAllDialogueActions());
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

        private void buttonTTS_Click(object sender, EventArgs e)
        {
            var dialogs = _iat.GetAllDialogueActions().ToArray();
            var t = new TextToSpeechForm(dialogs);
            t.Show(this);
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            var dfsearch = new DFSearch<string>(state =>
                _iat.GetDialogueActionsByState(state).Select(dto => dto.NextState));
            dfsearch.InitializeSearch(IATConsts.INITIAL_DIALOGUE_STATE);
            dfsearch.FullSearch();

            int unreachableStatesCount = 0;
            int totalStates = 0;
            string unreachableStatesDescription = "The following Dialogue States are not reachable: \n[";

            foreach (var dAction in _iat.GetAllDialogueActions().GroupBy(da => da.CurrentState)
                .Select(group => group.First()))
            {
                totalStates++;
                if (dfsearch.Closed.SearchInClosed(new NodeRecord<string>() { node = dAction.CurrentState.ToString() }) ==
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

            IntegratedAuthoringToolAsset loadedIAT = _iat;

            List<RolePlayCharacterAsset> rpcList = _iat.Characters.ToList();

            List<WellFormedNames.Name> _eventList = new List<WellFormedNames.Name>();

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
                        double timePercentage = ((double)lastFor / (double)timestamp) * 100;
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
            var rpcSource = EditorTools.GetSelectedDtoFromTable<CharacterNameAndMoodDTO>(dataGridViewCharacters);
            if (rpcSource != null)
            {
               new RPCInspectForm(_iat, _storage, rpcSource.Name).Show(this);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (_iat.Characters.Count() == 0)
            {
                EditorTools.WriteText(richTextBoxChat, "Error: The character list is empty.", Color.Red, true);
                return;
            }

            _agentsInSimulation = _iat.Characters.Select(c => CloneHelper.Clone(c)).ToList();
            foreach (var c in _agentsInSimulation)
            {
                c.LoadAssociatedAssets(_storage);
                _iat.BindToRegistry(c.DynamicPropertiesRegistry);
            }

            richTextBoxChat.Clear();
            listBoxPlayerDialogues.DataSource = new List<string>();
            comboBoxAgChat.DataSource = _agentsInSimulation.Select(a => a.CharacterName.ToString()).ToList();
            listBoxPlayerActions.DataSource = new List<string>();
            comboBoxAgentView.SelectedIndex = 0;

            EditorTools.WriteText(richTextBoxChat, "Characters were loaded with success (" + DateTime.Now + ")",
                Color.Blue, true);
            var enterEvents = new List<Name>();
            foreach (var ag in _agentsInSimulation)
            {
                enterEvents.Add(EventHelper.ActionEnd(ag.CharacterName.ToString(), "Enters", "-"));
            }

            foreach (var ag in _agentsInSimulation)
            {
                ag.Perceive(enterEvents);

                EditorTools.WriteText(richTextBoxChat, ag.CharacterName + " enters.", Color.Black, false);
                EditorTools.WriteText(richTextBoxChat,
                    " (" + ag.GetInternalStateString() + " | " + ag.GetSIRelationsString() + ")", Color.DarkRed, true);
            }

            EditorTools.WriteText(richTextBoxChat, "", Color.Black, true);

            buttonContinue.Enabled = true;
            textBoxTick.Text = _agentsInSimulation.ElementAt(0).Tick.ToString();
            this.buttonContinue_Click(sender, e);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            foreach (var ag in _agentsInSimulation)
            {
                if (ag.CharacterName == (WellFormedNames.Name.BuildName(comboBoxPlayerRpc.SelectedItem)))
                {
                    ag.Update();
                    continue;
                }

                var decisions = ag.Decide();
                if (decisions.Any())
                {
                    var action = decisions.First();
                    if (action.Key.ToString() == IATConsts.DIALOG_ACTION_KEY)
                    {
                        string error;
                        var diag = _iat.GetDialogAction(action, out error);
                        if (error != null)
                        {
                            EditorTools.WriteText(richTextBoxChat, ag.CharacterName + " : " + error, Color.Red, false);
                        }
                        EditorTools.WriteText(richTextBoxChat,
                            ag.CharacterName + " To " + action.Target + " : ", Color.ForestGreen, true);
                        EditorTools.WriteText(richTextBoxChat, ag.ProcessWithBeliefs(diag.Utterance), Color.Black, false);

                        EditorTools.WriteText(richTextBoxChat,
                            " (" + ag.GetInternalStateString() + " | " + ag.GetSIRelationsString() + ")" + "\n", Color.DarkRed,
                            true);
                        var toString = "Speak(" + diag.CurrentState + "," + diag.NextState + "," + diag.Meaning + "," + diag.Style + ")";
                        var ev = EventHelper.ActionEnd(ag.CharacterName.ToString(), toString, action.Target.ToString());
                        HandleEffects(new[] { ev });
                    }
                    else
                    {
                        if (action.Target != WellFormedNames.Name.NIL_SYMBOL)
                        {
                            EditorTools.WriteText(richTextBoxChat,
                                ag.CharacterName + " Performs To " + action.Target + ":" + action, Color.Blue, true);
                        }
                        else
                        {
                            EditorTools.WriteText(richTextBoxChat,
                                ag.CharacterName + " Performs: " + action, Color.Blue, true);

                        }
                        var ev = EventHelper.ActionEnd(ag.CharacterName, action.Name, action.Target);
                        HandleEffects(new[] { ev });
                    }
                    EditorTools.WriteText(richTextBoxChat, "", Color.Black, true);
                }

                ag.Update();
            }


            //Update the ListBoxes with the new player options
            UpdatePlayerActionOptions();

            //Event triggers
            HandleEventTriggers();

            //Assumption: All agents have the same tick
            textBoxTick.Text = _agentsInSimulation.ElementAt(0).Tick.ToString();
            if (_agentsInSimulation.Count() > 0)
                comboBoxAgentView_SelectedIndexChanged(sender, e); // update the agent inspector views;
        }

        private IEnumerable<IAction> playerDialogueOptions;
        private IEnumerable<IAction> playerNotDialogueOptions;

        private string playerRPCName;


        private void UpdatePlayerActionOptions()
        {
            if (playerRPCName == WellFormedNames.Name.NIL_STRING)
            {
                listBoxPlayerDialogues.DataSource = new List<string>();
                listBoxPlayerActions.DataSource = new List<string>();
                return;
            }

            var allPlayerActionOptions = _agentsInSimulation.Where(c => c.CharacterName.ToString() == playerRPCName).First().Decide();

            this.playerDialogueOptions = allPlayerActionOptions.Where(a => a.Key.ToString() == IATConsts.DIALOG_ACTION_KEY);
            this.playerNotDialogueOptions = allPlayerActionOptions.Where(a => a.Key.ToString() != IATConsts.DIALOG_ACTION_KEY);

            float maxDialoguePriority = -1;
            float maxNonDialoguePriority = -1;

            if (playerDialogueOptions.Any())
            {
                maxDialoguePriority = playerDialogueOptions.Max(x => x.Utility);
                playerDialogueOptions = playerDialogueOptions.Where(x => x.Utility == maxDialoguePriority);
            }
            else
            {
                listBoxPlayerDialogues.DataSource = new List<string>();
            }

            if (playerNotDialogueOptions.Any())
            {
                maxNonDialoguePriority = playerNotDialogueOptions.Max(x => x.Utility);
                playerNotDialogueOptions = playerNotDialogueOptions.Where(x => x.Utility == maxNonDialoguePriority);
            }
            else
            {
                listBoxPlayerActions.DataSource = new List<string>();
            }

            if (maxDialoguePriority == maxNonDialoguePriority)
            {
                DeterminePlayerActionOptions();
                DeterminePlayerDialogueOptions();
            }
            else if (maxDialoguePriority > maxNonDialoguePriority)
            {
                DeterminePlayerDialogueOptions();
                listBoxPlayerActions.DataSource = new List<string>();
            }
            else
            {
                DeterminePlayerActionOptions();
                listBoxPlayerDialogues.DataSource = new List<string>();
            }
        }


        private void DeterminePlayerActionOptions()
        {
            List<string> result = new List<string>();
            List<IAction> aux = new List<IAction>();
            foreach (var a in this.playerNotDialogueOptions)
            {
                aux.Add(a);
                if (a.Target != WellFormedNames.Name.NIL_SYMBOL)
                {
                    result.Add("To " + a.Target + " : " + a.Name);
                }
                else
                {
                    result.Add("" + a.Name);
                }

            }
            listBoxPlayerActions.DataSource = result;
            listBoxPlayerActions.ClearSelected();
            playerNotDialogueOptions = aux;
        }

        private void DeterminePlayerDialogueOptions()
        {
            List<string> result = new List<string>();
            List<IAction> extendedList = new List<IAction>();

            RolePlayCharacterAsset playerRPC = _agentsInSimulation.Where(c => c.CharacterName.ToString() == comboBoxPlayerRpc.SelectedItem).FirstOrDefault();
            foreach (var a in playerDialogueOptions)
            {
                var diags = _iat.GetDialogueActions(a.Parameters[0], a.Parameters[1], a.Parameters[2], a.Parameters[3]);

                if (diags.Count() == 0)
                {
                    EditorTools.WriteText(richTextBoxChat, playerRPCName.ToString() +
                        " : " + " could not find any matching dialogue for action " + a.Name, Color.Red, true);
                }
                else if (a.Target != WellFormedNames.Name.NIL_SYMBOL)
                {
                    foreach (var d in diags)
                    {
                        extendedList.Add(a);
                        result.Add("To " + a.Target + " : \"" + playerRPC.ProcessWithBeliefs(d.Utterance) + "\"");
                    }
                }
                else
                {
                    foreach (var d in diags)
                    {
                        extendedList.Add(a);
                        result.Add(playerRPC.ProcessWithBeliefs(d.Utterance));
                    }
                }
            }
            playerDialogueOptions = extendedList;
            listBoxPlayerDialogues.DataSource = result;
            listBoxPlayerDialogues.ClearSelected();
        }


        private void comboBoxEventType_SelectedIndexChanged(object sender, EventArgs e)
        {
            playerRPCName = comboBoxPlayerRpc.SelectedItem.ToString();
        }

        private void listBoxPlayerDialogues_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var idx = listBoxPlayerDialogues.SelectedIndex;
            var item = listBoxPlayerDialogues.SelectedItem;

            if (item == null) return;

            var action = playerDialogueOptions.ElementAt(idx);
            EditorTools.WriteText(richTextBoxChat, playerRPCName + " Says " + listBoxPlayerDialogues.SelectedItem.ToString() + "\n", Color.Blue, true);

            var ev = EventHelper.ActionEnd(playerRPCName, action.Name.ToString(), action.Target.ToString());

            this.HandleEffects(new[] { ev });

            this.buttonContinue_Click(sender, e);
        }

        private void HandleEffects(Name[] eventList)
        {

            foreach (var ev in eventList)
            {
                var actor = ev.GetNTerm(2);


                if (effectTickBox.Checked)
                    EditorTools.WriteText(richTextBoxChat,
                          "Event Registered " + ev + "\n", Color.Brown, true);

                foreach (var a in _agentsInSimulation)
                {
                    a.Perceive(ev);
                }
                var effects = _iat.WorldModel.Simulate(new[] { ev });
                string toWrite = "";
                toWrite += "Effects: \n";

                Dictionary<string, List<string>> observerAgents = new Dictionary<string, List<string>>();

                foreach (var eff in effects)
                {
                    foreach (var a in _agentsInSimulation)
                    {
                        string newValue = "";


                        if (eff.ObserverAgent == a.CharacterName || eff.ObserverAgent.ToString() == "*")
                        {
                            if (eff.NewValue.IsComposed) //New Value is a Dynamic Property
                            {
                                newValue = a.GetBeliefValue(eff.NewValue.ToString());
                            }
                            else
                            {
                                newValue = eff.NewValue.ToString();
                            }

                            var evt = EventHelper.PropertyChange(eff.PropertyName.ToString(), newValue, actor.ToString());

                            if (!observerAgents.ContainsKey(a.CharacterName.ToString()))
                            {
                                observerAgents.Add(a.CharacterName.ToString(), new List<string>() { evt.GetNTerm(3).ToString() });
                            }
                            else observerAgents[a.CharacterName.ToString()].Add(evt.GetNTerm(3).ToString());

                            a.Perceive(evt);
                        }
                    }

                }

                foreach (var o in observerAgents)
                {
                    toWrite += o.Key + ": ";

                    foreach (var e in o.Value)
                    {
                        var value = _agentsInSimulation.FirstOrDefault(x => x.CharacterName.ToString() == o.Key).GetBeliefValue(e);
                        toWrite += e + " = " + value + ", ";
                    }
                    toWrite = toWrite.Substring(0, toWrite.Length - 2);
                    toWrite += "\n";
                }



                if (effects.Any())
                {
                    textBoxBelChat_TextChanged(null, null);
                }

                if (effectTickBox.Checked)
                    EditorTools.WriteText(richTextBoxChat,
                    toWrite, Color.Black, true);
            }

        }

        private void textBoxBelChat_TextChanged(object sender, EventArgs e)
        {
            if (textBoxBelChat.Text != string.Empty)
            {
                buttonEvalBelief.Enabled = true;
            }
            else
            {
                buttonEvalBelief.Enabled = false;
            }
        }

        private void comboBoxAgChat_SelectedValueChanged(object sender, EventArgs e)
        {
            textBoxBelChat_TextChanged(sender, e);
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {
        }

        private void HandleEventTriggers()
        {
            if (_iat.eventTriggers == null)
                _iat.eventTriggers = new EventTriggers();

            var events = _iat.eventTriggers.ComputeTriggersList(_agentsInSimulation.ToList());

            if (events.Count() == 0)
                return;

            var toWrite = "";

            toWrite += "Events Triggered: \n";
            foreach (var ev in events)
            {
                toWrite += ev.ToString() + "\n";
            }


            if (checkBox1.Checked)
                EditorTools.WriteText(richTextBoxChat,
                    toWrite, Color.CornflowerBlue, true);

            HandleEffects(events.ToArray());
        }

        private void displayGraph_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<string>> states = new Dictionary<string, List<string>>();

            foreach (var d in _iat.GetAllDialogueActions())
            {
                if (states.ContainsKey(d.CurrentState))
                {
                    states[d.CurrentState].Add(d.NextState);
                }
                else
                    states.Add(d.CurrentState, new List<string>() { d.NextState });
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
                    else writer += "Any" + "->" + "Any" + "\n";
                }
            }

            writer += "}";

            Bitmap bit = Run(writer);
            var image = new ImageForm(bit);
            image.Show();

            //     Graphics.DrawImage(bit, 60, 10);
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

            try
            {
                using (Stream bmpStream = System.IO.File.Open(output + ".jpg", System.IO.FileMode.Open))
                {
                    Image image = Image.FromStream(bmpStream);
                    bitmap = new Bitmap(image);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            File.Delete(output);
            File.Delete(output + ".jpg");
            return bitmap;
        }

        private void DialogueSearchBox_TextChanged(object sender, EventArgs e)
        {

            var text = searchDialogueBox.Text.ToString();

            var dialogs = _iat.GetAllDialogueActions().ToList();
            bool cs = false;
            bool ns = false;
            bool mn = false;
            bool sty = false;
            bool utterance = false;

            if (searchCheckList.GetItemCheckState(0) == CheckState.Checked)
            {
                cs = true;
            }
            if (searchCheckList.GetItemCheckState(1) == CheckState.Checked)
            {
                ns = true;
            }
            if (searchCheckList.GetItemCheckState(2) == CheckState.Checked)
            {
                mn = true;
            }
            if (searchCheckList.GetItemCheckState(3) == CheckState.Checked)
            {
                sty = true;
            }

            if (searchCheckList.GetItemCheckState(4) == CheckState.Checked)
            {
                utterance = true;
            }


            if (text != "" && text != "-" && text != "*")
            {

                dialogs = new List<DialogueStateActionDTO>();
                if (cs)
                    dialogs.AddRange(_iat.GetAllDialogueActions().ToList().FindAll(x => x.CurrentState.ToLower().ToString().Contains(text.ToLower())));
                if (ns)
                    dialogs.AddRange(_iat.GetAllDialogueActions().ToList().FindAll(x => x.NextState.ToLower().ToString().Contains(text.ToLower())));
                if (mn)
                    dialogs.AddRange(_iat.GetAllDialogueActions().ToList().FindAll(x => x.Meaning.ToLower().ToString().Contains(text.ToLower())));
                if (sty)
                    dialogs.AddRange(_iat.GetAllDialogueActions().ToList().FindAll(x => x.Style.ToLower().ToString().Contains(text.ToLower())));
                if (utterance)
                    dialogs.AddRange(_iat.GetAllDialogueActions().ToList().FindAll(x => x.Utterance.ToLower().ToString().Contains(text.ToLower())));
            }

            _dialogs.DataSource = dialogs;

            EditorTools.HideColumns(dataGridViewDialogueActions, new[]
                {
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.Id),
                    PropertyUtil.GetPropertyName<DialogueStateActionDTO>(d => d.UtteranceId),
                }
            );

            _dialogs.Refresh();
        }

        private void dataGridViewDialogueActions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int currentRowIndex = dataGridViewDialogueActions.CurrentCell.RowIndex;
            int currentColumnIndex = dataGridViewDialogueActions.CurrentCell.ColumnIndex;

        }

        private void dataGridViewDialogueActions_Leave(object sender, EventArgs e)
        {

        }

        private void dataGridViewDialogueActions_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBoxPlayerActions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        private void listBoxPlayerActions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var idx = listBoxPlayerActions.SelectedIndex;
            var item = listBoxPlayerActions.SelectedItem;
            if (item == null) return;

            var chosenAction = playerNotDialogueOptions.ElementAt(idx);

            if (chosenAction.Target != WellFormedNames.Name.NIL_SYMBOL)
            {
                EditorTools.WriteText(richTextBoxChat,
                playerRPCName + " Performs " + listBoxPlayerActions.SelectedItem.ToString() + "\n", Color.Blue, true);
            }
            else
            {
                EditorTools.WriteText(richTextBoxChat,
                playerRPCName + " Performs : " + listBoxPlayerActions.SelectedItem.ToString() + "\n", Color.Blue, true);
            }

            var ev = EventHelper.ActionEnd(playerRPCName, chosenAction.Name.ToString(), chosenAction.Target.ToString());

            try
            {
                this.HandleEffects(new[] { ev });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.buttonContinue_Click(sender, e);
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxAgChat_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.comboBoxAgentView_SelectedIndexChanged(sender, e);
        }

        private void comboBoxAgentView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_agentsInSimulation != null && _agentsInSimulation.Count() > 0)
            {
                var selectedRPCName = (string)comboBoxAgChat.SelectedItem;

                var rpc = _agentsInSimulation.Where(c => c.CharacterName.ToString() == selectedRPCName).FirstOrDefault();

                var selectedView = (string)comboBoxAgentView.SelectedItem;
                if (selectedView.EqualsIgnoreCase("Knowledge Base"))
                {
                    dataGridViewAgentInspector.DataSource = new BindingListView<BeliefDTO>(rpc.GetAllBeliefs().ToList());

                }
                else if (selectedView.EqualsIgnoreCase("Autobiographical Memory"))
                {
                    dataGridViewAgentInspector.DataSource = new BindingListView<EventDTO>(rpc.EventRecords.ToList());
                }
                else if (selectedView.EqualsIgnoreCase("Emotional State"))
                {
                    dataGridViewAgentInspector.DataSource = new BindingListView<EmotionalAppraisal.DTOs.EmotionDTO>(rpc.GetAllActiveEmotions().ToList());
                }
                else if (selectedView.EqualsIgnoreCase("Goals"))
                {
                    dataGridViewAgentInspector.DataSource = new BindingListView<EmotionalAppraisal.DTOs.GoalDTO>(rpc.GetAllGoals().ToList());
                }
                else if (selectedView.EqualsIgnoreCase("Dynamic Properties"))
                {
                    dataGridViewAgentInspector.DataSource = new BindingListView<DynamicPropertyDTO>(rpc.GetAllDynamicProperties().ToList());
                }

                dataGridViewAgentInspector.Refresh();
            }
        }

        private void buttonEvalBelief_Click(object sender, EventArgs e)
        {
            var selectedRPCName = (string)comboBoxAgChat.SelectedItem;

            if (string.IsNullOrWhiteSpace(textBoxBelChat.Text) ||
                string.IsNullOrWhiteSpace(selectedRPCName))
            {
                textBoxValChat.Text = "-";
                return;
            }

            var rpc = _agentsInSimulation.Where(c => c.CharacterName.ToString() == selectedRPCName).FirstOrDefault();
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

                //Special case for the Tell Dynamic Property
                if (textBoxBelChat.Text.StartsWith("Tell("))
                    comboBoxAgentView_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                textBoxValChat.Text = ex.Message;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog(this);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void buttonNewAssetStorage_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _storage = new AssetStorage();
                OnAssetStorageChange();
                File.WriteAllText(sfd.FileName, _storage.ToJson());
                textBoxPathAssetStorage.Text = sfd.FileName;
            }
        }

        private void buttonOpenAssetStorage_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Asset Storage File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                AssetStorage storage;
                try
                {
                    _storage = AssetStorage.FromJson(File.ReadAllText(aux));
                    OnAssetStorageChange();
                    textBoxPathAssetStorage.Text = aux;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _storage = new AssetStorage();
                    OnAssetStorageChange();
                    textBoxPathAssetStorage.Text = string.Empty;
                }

            }
        }

        private void saveAssetStorageButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxPathAssetStorage.Text))
            {
                var sfd = new SaveFileDialog();
                sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveAssetRules();
                    File.WriteAllText(sfd.FileName, _storage.ToJson());
                    textBoxPathAssetStorage.Text = sfd.FileName;
                }
            }
            else
            {
                SaveAssetRules();
                File.WriteAllText(textBoxPathAssetStorage.Text, _storage.ToJson());
            }
        }


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _iat = new IntegratedAuthoringToolAsset();
            OnAssetDataLoaded(_iat);
            _currentScenarioFilePath = null;
            EditorTools.UpdateFormTitle("FAtiMA Authoring Tool", string.Empty, this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aux = EditorTools.OpenFileDialog("Scenario File (*.json)|*.json|All Files|*.*");
            if (aux != null)
            {
                try
                {
                    _iat = IntegratedAuthoringToolAsset.FromJson(File.ReadAllText(aux), _storage);
                    OnAssetDataLoaded(_iat);
                    EditorTools.UpdateFormTitle("FAtiMA Authoring Tool", aux, this);
                    _currentScenarioFilePath = aux;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSaveAsAssetStorage_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, _storage.ToJson());
                textBoxPathAssetStorage.Text = sfd.FileName;
            }
        }

        void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                var sfd = new SaveFileDialog();
                if (string.IsNullOrEmpty(textBoxPathAssetStorage.Text))
                {
                    sfd = new SaveFileDialog();
                    sfd.Filter = "Asset Storage File (*.json)|*.json|All Files|*.*";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveAssetRules();
                        File.WriteAllText(sfd.FileName, _storage.ToJson());
                        textBoxPathAssetStorage.Text = sfd.FileName;
                    }
                }
                else
                {
                    SaveAssetRules();
                    File.WriteAllText(textBoxPathAssetStorage.Text, _storage.ToJson());
                }

                sfd.Filter = "Scenario File (*.json)|*.json|All Files|*.*";
                if (_currentScenarioFilePath != null)
                {
                    File.WriteAllText(_currentScenarioFilePath, _iat.ToJson());
                }
                else
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, _iat.ToJson());
                        _currentScenarioFilePath = sfd.FileName;
                    }
                }
                EditorTools.UpdateFormTitle("FAtiMA Authoring Tool", _currentScenarioFilePath, this);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Scenario File (*.json)|*.json|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, _iat.ToJson());
                _currentScenarioFilePath = sfd.FileName;
                EditorTools.UpdateFormTitle("FAtiMA Authoring Tool", _currentScenarioFilePath, this);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.Filter = "Scenario File (*.json)|*.json|All Files|*.*";
            if (_currentScenarioFilePath != null)
            {
                File.WriteAllText(_currentScenarioFilePath, _iat.ToJson());
            }
            else
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, _iat.ToJson());
                    _currentScenarioFilePath = sfd.FileName;
                }
            }
            EditorTools.UpdateFormTitle("FAtiMA Authoring Tool", _currentScenarioFilePath, this);
        }

        private void dataGridViewCharacters_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewCharacters_Sorted(object sender, EventArgs e)
        {
            dataGridViewCharacters.ClearSelection();
            if (_rpcForm != null)
            {
                _rpcForm.Close();
                _rpcForm = null;
            }
        }

        private void comboBoxPlayerRpc_Click(object sender, EventArgs e)
        {
            comboBoxPlayerRpc.Items.Clear();
            comboBoxPlayerRpc.Items.Add("-");
            if (_iat.Characters.Any())
            {
                comboBoxPlayerRpc.Items.AddRange(_iat.Characters.Select(x => x.CharacterName.ToString()).ToArray());
            }
            comboBoxPlayerRpc.SelectedIndex = 0;
        }

        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {

        }


        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            // Determine if text has changed in the textbox by comparing to original text.

            // Display a MsgBox asking the user to save changes or abort.
            DialogResult = MessageBox.Show("Do you want to save changes?", "FAtiMA Toolkit",
               MessageBoxButtons.YesNoCancel);

            if (DialogResult == DialogResult.Yes)
            {
                // Cancel the Closing event from closing the form.
                if (_currentScenarioFilePath != null)
                {
                    File.WriteAllText(_currentScenarioFilePath, _iat.ToJson());
                }
                else
                {
                    var sfd = new SaveFileDialog();
                    sfd.Filter = "Scenario File (*.json)|*.json|All Files|*.*";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(sfd.FileName, _iat.ToJson());
                        _currentScenarioFilePath = sfd.FileName;
                    }
                }

                SaveAssetRules();
              
                //e.Cancel = true;
                // Call method to save file...
            }
            else if (DialogResult == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}