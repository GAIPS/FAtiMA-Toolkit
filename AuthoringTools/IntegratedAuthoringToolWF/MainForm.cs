using ActionLibrary;
using ActionLibrary.DTOs;
using AutobiographicMemory.DTOs;
using CommeillFaut;
using EmotionalAppraisal;
using EmotionalAppraisalWF;
using EmotionalDecisionMaking;
using EmotionalDecisionMakingWF;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using IntegratedAuthoringToolWF.IEP;
using KnowledgeBase.DTOs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using RolePlayCharacter;
using RolePlayCharacterWF;
using SocialImportance;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Utilities;
using Utilities.DataStructures;
using WellFormedNames;

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

        public bool showImbalances = true;
      //  private string tooltip = "";
        private int step;
        private int MaxStep = 5;
        public int authorExpertiseLevel = 0;
        

        public MainForm()
        {
            InitializeComponent();
            buttonRemoveCharacter.Enabled = false;
            buttonInspect.Enabled = false;
            step = 0;
            _iat = new IntegratedAuthoringToolAsset();
            _agentsInSimulation = new List<RolePlayCharacterAsset>();
            _storage = new AssetStorage();
            _webForm = new WebAPIWF.MainForm();
            _webForm.iat = this;
            _eaForm = new EmotionalAppraisalWF.MainForm(this);
            _edmForm = new EmotionalDecisionMakingWF.MainForm(this);
            _edmForm.AddedRuleEvent += AddedRule;
            _siForm = new SocialImportanceWF.MainForm();
            _cifForm = new CommeillFautWF.MainForm();
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
            initAssistant();
            OnAssetStorageChange();
            OnAssetDataLoaded(_iat);
            tabControlIAT.SelectedIndexChanged += TabIndexChanged_Handler;
            tabControlAssetEditor.SelectedIndexChanged += TabIndexChanged_Handler;
            debugLabel.Text = "";
            
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
            AssistantHandler();

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




        private void importStoryButton_Click(object sender, EventArgs e)
        {
            var story = new ComputeDescriptionForm(_iat, _storage);
            story.ShowDialog();
            OnAssetDataLoaded(_iat);
            OnAssetStorageChange();
        }

        private void loadTemplateScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String storageString = "[\"EmotionalAppraisalAsset\",{\"root\":{\"classId\":0,\"Description\":null,\"AppraisalRules\":{\"AppraisalWeight\":1,\"Rules\":[{\"EventName\":\"Event(Action-End, SELF, Speak([cs], [ns], *, *), John)\",\"Conditions\":{\"Set\":[]},\"AppraisalVariables\":{\"AppraisalVariables\":[{\"Name\":\"Praiseworthiness\",\"Value\":-5,\"Target\":\"SELF\"}]}},{\"EventName\":\"Event(Action-End, SELF, Speak([cs], [ns], *, *), Charlie)\",\"Conditions\":{\"Set\":[]},\"AppraisalVariables\":{\"AppraisalVariables\":[{\"Name\":\"Praiseworthiness\",\"Value\":-5,\"Target\":\"SELF\"}]}},{\"EventName\":\"Event(Action-End, SELF, Do, John)\",\"Conditions\":{\"Set\":[]},\"AppraisalVariables\":{\"AppraisalVariables\":[{\"Name\":\"Praiseworthiness\",\"Value\":-5,\"Target\":\"John\"}]}},{\"EventName\":\"Event(Action-End, SELF, Do, Charlie)\",\"Conditions\":{\"Set\":[]},\"AppraisalVariables\":{\"AppraisalVariables\":[{\"Name\":\"Praiseworthiness\",\"Value\":-6,\"Target\":\"Chalie\"}]}},{\"EventName\":\"Event(Property-Change, SELF, Hello(World), [t])\",\"Conditions\":{\"Set\":[]},\"AppraisalVariables\":{\"AppraisalVariables\":[{\"Name\":\"Praiseworthiness\",\"Value\":6,\"Target\":\"[t]\"}]}}]}},\"types\":[{\"TypeId\":0,\"ClassName\":\"EmotionalAppraisal.EmotionalAppraisalAsset, EmotionalAppraisal, Version=1.4.1.0, Culture=neutral, PublicKeyToken=null\"}]},\"EmotionalDecisionMakingAsset\",{\"root\":{\"classId\":0,\"ActionTendencies\":[{\"Action\":\"Speak([cs], [ns], [mean], [style])\",\"Target\":\"[t]\",\"Layer\":\"-\",\"Conditions\":{\"Set\":[\"DialogueState([t]) = [cs]\",\"Has(Floor) = SELF\",\"ValidDialogue([cs], [ns], [mean], [style]) = True\"]},\"Priority\":1},{\"Action\":\"Speak([cs], [ns], [mean], Rude)\",\"Target\":\"[t]\",\"Layer\":\"-\",\"Conditions\":{\"Set\":[\"DialogueState([t]) = [cs]\",\"ValidDialogue([cs], [ns], [mean], Rude) = True\",\"Has(Floor) = SELF\",\"Mood(SELF) < 0\"]},\"Priority\":5},{\"Action\":\"Speak([cs], [ns], [mean], Polite)\",\"Target\":\"[t]\",\"Layer\":\"-\",\"Conditions\":{\"Set\":[\"DialogueState([t]) = [cs]\",\"ValidDialogue([cs], [ns], [mean], Polite) = True\",\"Has(Floor) = SELF\",\"Mood(SELF) < 0\"]},\"Priority\":5}]},\"types\":[{\"TypeId\":0,\"ClassName\":\"EmotionalDecisionMaking.EmotionalDecisionMakingAsset, EmotionalDecisionMaking, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null\"}]},\"SocialImportanceAsset\",{\"root\":{\"classId\":0,\"AttributionRules\":[{\"RuleName\":\"Good Mood\",\"Target\":\"[t]\",\"Value\":10,\"Conditions\":{\"Set\":[\"Mood(SELF) > 0\"]}},{\"RuleName\":\"Close Friends\",\"Target\":\"[t]\",\"Value\":20,\"Conditions\":{\"Set\":[\"CloseFriends([t]) = True\"]}},{\"RuleName\":\"TalktTo\",\"Target\":\"[t]\",\"Value\":40,\"Conditions\":{\"Set\":[\"EventId(Action-End, [t], Speak(*, *, *, *), SELF) != -1\"]}}]},\"types\":[{\"TypeId\":0,\"ClassName\":\"SocialImportance.SocialImportanceAsset, SocialImportance, Version=1.5.0.0, Culture=neutral, PublicKeyToken=null\"}]},\"CommeillFautAsset\",{\"root\":{\"classId\":0,\"SocialExchanges\":[]},\"types\":[{\"TypeId\":0,\"ClassName\":\"CommeillFaut.CommeillFautAsset, CommeillFaut, Version=1.7.0.0, Culture=neutral, PublicKeyToken=null\"}]}]";

            String scenarioString = "{\"root\":{\"classId\":0,\"ScenarioName\":\"Example\",\"Description\":\"A short conversation between the Player and a Character named Charlie. Charlie discovers that there is a major conspiracy within the company he works in. \",\"Dialogues\":[{\"CurrentState\":\"Leave\",\"NextState\":\"End\",\"Meaning\":\"-\",\"Style\":\"-\",\"Utterance\":\"Alright, goodbye\",\"UtteranceId\":\"TTS-CCF7877090E49659FB15B89D80C365A7\"},{\"CurrentState\":\"Greeting\",\"NextState\":\"Order\",\"Meaning\":\"-\",\"Style\":\"Polite\",\"Utterance\":\"How do you do?\",\"UtteranceId\":\"TTS-E191BADF9DAD8B0EDC942E4A6DFC8D64\"},{\"CurrentState\":\"Greeting\",\"NextState\":\"Leave\",\"Meaning\":\"-\",\"Style\":\"VeryRude\",\"Utterance\":\"Not you again\",\"UtteranceId\":\"TTS-4DDAA9A4B302A0E4E9DEE292CDB9481D\"},{\"CurrentState\":\"Order\",\"NextState\":\"OrderResponse\",\"Meaning\":\"Hamburger\",\"Style\":\"-\",\"Utterance\":\"Yes, I would like a burger please\",\"UtteranceId\":\"TTS-6D501E2FE494013994B75A7225E5FA29\"},{\"CurrentState\":\"Greeting\",\"NextState\":\"Order\",\"Meaning\":\"-\",\"Style\":\"Polite\",\"Utterance\":\"How can I help you?\",\"UtteranceId\":\"TTS-C425F01490F94A3080B9922108A78C33\"},{\"CurrentState\":\"Start\",\"NextState\":\"Greeting\",\"Meaning\":\"-\",\"Style\":\"Rude\",\"Utterance\":\"Hey\",\"UtteranceId\":\"TTS-6057F13C496ECF7FD777CEB9E79AE285\"},{\"CurrentState\":\"Start\",\"NextState\":\"Greeting\",\"Meaning\":\"-\",\"Style\":\"Polite\",\"Utterance\":\"Good Afternoon\",\"UtteranceId\":\"TTS-91145E15F72DF3A48A9E83CAE7E3BED7\"},{\"CurrentState\":\"Order\",\"NextState\":\"OrderResponse\",\"Meaning\":\"Pizza\",\"Style\":\"-\",\"Utterance\":\"Yes, I would like a Pizza please\",\"UtteranceId\":\"TTS-E2A8BDFAB9C5D5B8A5CD9A67F8C08155\"}],\"Characters\":[{\"KnowledgeBase\":{\"Perspective\":\"Charlie\",\"Knowledge\":{\"SELF\":{\"Has(Floor)\":\"Charlie, 1\",\"DialogueState(Player)\":\"Start, 1\",\"AM(Charlie)\":\"True, 1\",\"CloseFriends(Player)\":\"False, 1\"}}},\"BodyName\":\"Male\",\"VoiceName\":\"Male\",\"EmotionalState\":{\"Mood\":4,\"initialTick\":0,\"EmotionalPool\":[],\"AppraisalConfiguration\":{\"HalfLifeDecayConstant\":0.5,\"EmotionInfluenceOnMoodFactor\":0.3,\"MoodInfluenceOnEmotionFactor\":0.3,\"MinimumMoodValueForInfluencingEmotions\":0.5,\"EmotionalHalfLifeDecayTime\":15,\"MoodHalfLifeDecayTime\":60}},\"AutobiographicMemory\":{\"Tick\":0,\"records\":[]},\"OtherAgents\":{\"dictionary\":[]},\"Goals\":[{\"Name\":\"Survive\",\"Significance\":5,\"Likelihood\":0.5}]},{\"KnowledgeBase\":{\"Perspective\":\"Player\",\"Knowledge\":{\"SELF\":{\"Has(Floor)\":\"Charlie, 1\",\"DialogueState(Charlie)\":\"Start, 1\"}}},\"BodyName\":null,\"VoiceName\":null,\"EmotionalState\":{\"Mood\":-3,\"initialTick\":0,\"EmotionalPool\":[],\"AppraisalConfiguration\":{\"HalfLifeDecayConstant\":0.5,\"EmotionInfluenceOnMoodFactor\":0.3,\"MoodInfluenceOnEmotionFactor\":0.3,\"MinimumMoodValueForInfluencingEmotions\":0.5,\"EmotionalHalfLifeDecayTime\":15,\"MoodHalfLifeDecayTime\":60}},\"AutobiographicMemory\":{\"Tick\":0,\"records\":[]},\"OtherAgents\":{\"dictionary\":[]},\"Goals\":[{\"Name\":\"Survive\",\"Significance\":5,\"Likelihood\":0.2}]}],\"WorldModel\":{\"Effects\":{\"dictionary\":[{\"key\":\"Event(Action-End, [s], Speak(*, [ns], *, *), [t])\",\"value\":[{\"PropertyName\":\"DialogueState([s])\",\"NewValue\":\"[ns]\",\"ObserverAgent\":\"[t]\"},{\"PropertyName\":\"Has(Floor)\",\"NewValue\":\"[t]\",\"ObserverAgent\":\"*\"},{\"PropertyName\":\"DialogueState([s])\",\"NewValue\":\"[ns]\",\"ObserverAgent\":\"Player\"}]}]},\"Priorities\":{\"dictionary\":[{\"key\":\"Event(Action-End, [s], Speak(*, [ns], *, *), [t])\",\"value\":1}]}}},\"types\":[{\"TypeId\":0,\"ClassName\":\"IntegratedAuthoringTool.IntegratedAuthoringToolAsset, IntegratedAuthoringTool, Version=1.7.0.0, Culture=neutral, PublicKeyToken=null\"}]}";

            this._storage = AssetStorage.FromJson(storageString);
            // Now that I have gotten the string for sure I can load the IAT
            this._iat = IntegratedAuthoringToolAsset.FromJson(scenarioString, _storage);
            OnAssetDataLoaded(_iat);
            OnAssetStorageChange();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPageDialogue_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void buttonNewAssetStorage_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Creates a new Asset Storage. \n This is the file where the cognitive rules of the scenario will be stored in.", buttonNewAssetStorage);
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://fatima-toolkit.eu/2-integrated-authoring-tool/");
        }




        #region Authoring Assistant

        Dictionary<Tuple<int, int>, string> assistantDescription;

        // Initialize All things Authoring Assistant
        private void initAssistant()
        {
           // assistantStatetoAction = new Dictionary<Tuple<int, int>, System.Action<object, EventArgs>>();
            assistantDescription = new Dictionary<Tuple<int, int>, string>();

            // 0 Expertise level, No Characters
            assistantDescription.Add(new Tuple<int, int>(0, 0),
                "Characters are agents that have a name, beliefs and actions");

            // 0 Expertise level, Characters have no beliefs
            assistantDescription.Add(new Tuple<int, int>(0, 1),
               "Every character should have beliefs that represent their knowledge regarding the world and themselves" +
               "For examples: Is(Hungry)=True , Has(Money)=5");

            // 0 Expertise level, No Characters
            assistantDescription.Add(new Tuple<int, int>(0, 2),
               "The action space of all the characters can be defined in the Emotional Decision Making component" +
               " A Decision Rule defines the action name, its priority, if it belongs to a particular layer and its target");

            // 0 Expertise level, No Characters
            assistantDescription.Add(new Tuple<int, int>(0, 3),
               "FAtiMA-Toolkit uses a hybrid approach combining both dialogue trees and dialogue states. Using a special Speak action the dialogue manager chooses the applicable dialogue from the pool defined here");

            // 0 Expertise level, No Characters
            assistantDescription.Add(new Tuple<int, int>(0, 4),
                "FAtiMA is based on the Ortony, Clore and Collins's (OCC) Model of emotions." +
               "Emotions represented valenced reactions to events in the world. They are generated by an appraisal process.");


        }

        private void TabIndexChanged_Handler(object sender, EventArgs e)
        {
            AssistantHandler();
        }

        private void AddedRule(object sender, EventArgs e)
        {

            if (!showImbalances)
                return;

            var edmRulesCount = _edmForm.Asset.GetAllActionRules().Count();
            var eaRulesCount = _eaForm.Asset.GetAllAppraisalRules().Count();
            ActionLibrary.DTOs.ActionRuleDTO rule = this._edmForm.latestAddedRule;

            if (edmRulesCount > eaRulesCount * 2 + 1)
            {
                string actionName = rule.Action.ToString();
                    //+ " towards " + rule.Target.ToString();
                var addEmotForm = new AddEmotionalReactionForm(actionName);

                var result = addEmotForm.ShowDialog();
               


                if (result == DialogResult.Yes)
                {
                    AddEmotionalReaction(rule, addEmotForm.targetEmotion, addEmotForm.subjectEmotion);
                   
                }

                if (addEmotForm.neverShowAgain)
                    showImbalances = false;
            }
        }

        private void AddEmotionalReaction(ActionRuleDTO rule, EmotionalAppraisal.OCCModel.OCCEmotionType targetEmotion, EmotionalAppraisal.OCCModel.OCCEmotionType subjectEmotion )
        {
            tabControlAssetEditor.SelectedIndex = 0;

            _eaForm.AddAppraisalRulewithEmotions(rule, targetEmotion, subjectEmotion);

        }

        // Main Handler of the Assistant, if something happens call this function
        public void AssistantHandler()
        {
            AssistantReasoner();
        }

        // According to the authors expertise level what should be the focus of the assistant
        public void AssistantReasoner()
        {
            if(authorExpertiseLevel == 0)
            {
                TutorialManager();
            }

            else if (authorExpertiseLevel == 1)
            {
                ScenarioBasicReasoner();
            }

            else if (authorExpertiseLevel == 2)
            {
                ScenarioExpertReasoner();
            }
        }

        // Update the Text within the reasoner to take into account the current state of the scenario
        private void UpdateLabel()
        {

            switch (step)
            {
                case 0:
                    button1.Text = "Add Character";
                    indexLabel.Text = "FAtiMA Characters";
                    break;
                case 1:
                    button1.Text = "Add Beliefs";
                    indexLabel.Text = "Agent's Internal State";
                    break;
                case 2:
                    button1.Text = "Add Actions";
                    indexLabel.Text = "Emotional Decison Making";
                    break;
                case 3:
                    button1.Text = "Add Dialogue";
                    indexLabel.Text = "Dialogues";
                    break;
                case 4:
                    button1.Text = "Add Appraisal Rule";
                    indexLabel.Text = "Emotional Appraisal";
                    break;
            }
            this.assistantTextBox.Text = assistantDescription[Tuple.Create(authorExpertiseLevel, step)];
            indexLabel.Text += ":";

        }

        // If users have no expertise make sure the tutorial is being followed
        public void TutorialManager()
        {
            if(_iat.Characters.Count() == 0)
            {
                step = 0;
            }
            else if(CalculateAverageBeliefs() < 1)
            {
                step = 1;
            }
            else if (_edmForm.dataGridViewReactiveActions.Rows.Count < 1)
            {
                step = 2;
            }
            else if (_iat.GetAllDialogueActions().Count() < 1)
            {
                step = 3;
            }
            else if (_eaForm._appraisalRulesVM.AppraisalRules.Count() < 1)
            {
                step = 4;
            }
            UpdateLabel();
        }

        // What to present to authors if they have a basic understanding of FAtiMA
        public void ScenarioBasicReasoner()
        {

        }

        // What to present to authors if they have an expert level of understanding of FAtiMA
        public void ScenarioExpertReasoner()
        {          
            var edmRulesCount = this._edmForm._loadedAsset.GetAllActionRules().Count();
            var eaRulesCount = this._eaForm.Asset.GetAllAppraisalRules().Count();


            if (edmRulesCount > eaRulesCount + 1)
              MessageBox.Show("Would you like to add an emotion effect to this action?");
            
        }

        // Auxiliary Function to help analyse the scenario
        public float CalculateAverageBeliefs()
        {
            var beliefTotal = 0;

            foreach(var c in _iat.Characters)
            {
                beliefTotal += c.GetAllBeliefs().Count();
            }

            return beliefTotal / _iat.Characters.Count();

        }


        // When the button task button is clicked 
        private void button1_Click(object sender, EventArgs e)
        {
            if(authorExpertiseLevel == 1)
            switch (step)
            {
                case 0:  // No characters
                    buttonAddCharacter_Click(sender, e);
                    this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("Characters");
                    break;

                case 1: // No beliefs

                    if (dataGridViewCharacters.SelectedRows.Count == 0)
                    {
                        dataGridViewCharacters.Rows[0].Selected = true;
                    }
                    if (this._rpcForm != null)
                    {
                        this._rpcForm.SelectedTab = 0;
                        new AddOrEditBeliefForm(this._rpcForm._knowledgeBaseVM).ShowDialog();
                        this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("Beliefs");
                    }
                    break;

                case 2: // No decision rules
                    this.tabControlIAT.SelectedIndex = 1;
                    this.tabControlAssetEditor.SelectedIndex = 1;
                    this._edmForm.buttonAddReaction_Click(sender, e);
                    this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("DecisionMaking");
                    break;

                case 3: // No dialogues
                    this.tabControlIAT.SelectedIndex = 0;
                    this.buttonAddDialogueAction_Click_1(sender, e);
                    this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("Dialogues");
                    break;

                case 4: // No appraisal rules
                    this.tabControlIAT.SelectedIndex = 1;
                    this.tabControlAssetEditor.SelectedIndex = 0;
                    this._eaForm.buttonAddAppraisalRule_Click(sender, e);
                    this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("AppraisalRules");
                    break;

                default:
                    this.assistantTextBox.Text = AuthorAssistant.GetTipByKey("Default");
                    break;

            }

            AssistantHandler();

        }




        private void assistantTextBox_TextChanged(object sender, EventArgs e)
        {

        }
      

        private void nextPicture_Click(object sender, EventArgs e)
        {
            if (this.step < MaxStep - 1)
                this.step += 1;
            else this.step = 0;

            UpdateLabel();

        }

        private void helpPicture_Click_1(object sender, EventArgs e)
        {
            if (step == 3)
                // Navigate to a URL.
                System.Diagnostics.Process.Start("https://fatima-toolkit.eu/9-dialogue-manager/");

            else if (step == 4)
                // Navigate to a URL.
                System.Diagnostics.Process.Start("https://fatima-toolkit.eu/5-emotional-appraisal/");

            else if (step == 2)
                // Navigate to a URL.
                System.Diagnostics.Process.Start("https://fatima-toolkit.eu/6-emotional-decision-making/");


            else                // Navigate to a URL.
                System.Diagnostics.Process.Start("https://fatima-toolkit.eu/2-integrated-authoring-tool/");
        }

        private void Assistant_Enter(object sender, EventArgs e)
        {

        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void assistantTextBox_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter_1(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Assistant.Hide();
        }

        private void authoringAssistantToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assistant.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.step > 0)
                this.step -= 1;
            else this.step = MaxStep - 1;

            UpdateLabel();
        }

        private void nonExperiencedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorExpertiseLevel = 0;
            AssistantHandler();
        }

        private void someExperienceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorExpertiseLevel = 1;
            AssistantHandler();
        }

        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            authorExpertiseLevel = 2;
            AssistantHandler();
        }

        #endregion Authoring Assistant
    }
}