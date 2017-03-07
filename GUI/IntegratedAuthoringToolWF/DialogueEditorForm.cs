using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Utilities;
using Utilities.DataStructures;

namespace IntegratedAuthoringToolWF
{
    public partial class DialogueEditorForm : Form
    {
	    private MainForm _parentForm;
	    private IntegratedAuthoringToolAsset _iatAsset => _parentForm.CurrentAsset;
        private BindingListView<GUIDialogStateAction> _playerDialogs;
        private BindingListView<GUIDialogStateAction> _agentDialogs;
        private readonly string PLAYER = IATConsts.PLAYER;
        private readonly string AGENT = IATConsts.AGENT;

        private int stateCounter;
        private int totalSize;

        public DialogueEditorForm(MainForm parentForm)
        {
            InitializeComponent();

	        _parentForm = parentForm;

            _playerDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
	        dataGridViewPlayerDialogueActions.DataSource = _playerDialogs;
			RefreshPlayerDialogs();

            _agentDialogs = new BindingListView<GUIDialogStateAction>(new List<GUIDialogStateAction>());
	        dataGridViewAgentDialogueActions.DataSource = _agentDialogs;
            RefreshAgentDialogs();
            stateCounter = 0;
            totalSize = 0;
        }

	    private void RefreshPlayerDialogs()
	    {
			_playerDialogs.DataSource = _iatAsset.GetDialogueActionsBySpeaker(
                PLAYER).Select(d => new GUIDialogStateAction(d)).ToList();
			_playerDialogs.Refresh();
			dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;

		}

	    private void RefreshAgentDialogs()
	    {
			_agentDialogs.DataSource = _iatAsset.GetDialogueActionsBySpeaker(
             AGENT).Select(d => new GUIDialogStateAction(d)).ToList();
			_agentDialogs.Refresh();
			dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
		}

		private void buttonAddPlayerDialogueAction_Click(object sender, EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, true).ShowDialog();
            RefreshPlayerDialogs();
        }

        private void buttonAgentAddDialogAction_Click(object sender, System.EventArgs e)
        {
            new AddOrEditDialogueActionForm(_parentForm, false).ShowDialog();
            RefreshAgentDialogs();
        }

        private void buttonPlayerRemoveDialogueAction_Click(object sender, System.EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewPlayerDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            _iatAsset.RemoveDialogueActions(PLAYER, itemsToRemove);
			RefreshPlayerDialogs();
			_parentForm.SetModified();
        }

        private void buttonAgentRemoveDialogAction_Click(object sender, System.EventArgs e)
        {
            IList<Guid> itemsToRemove = new List<Guid>();
            for (int i = 0; i < dataGridViewAgentDialogueActions.SelectedRows.Count; i++)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[i].DataBoundItem).Object;
                itemsToRemove.Add(item.Id);
            }
            _iatAsset.RemoveDialogueActions(AGENT, itemsToRemove);
			RefreshAgentDialogs();
			_parentForm.SetModified();
		}

        private void buttonPlayerEditDialogueAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewPlayerDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewPlayerDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, true, item.Id).ShowDialog();
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
                _iatAsset.AddPlayerDialogAction(newDialogueAction);
                RefreshPlayerDialogs();
            }
        }

        private void buttonAgentEditDialogAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, false, item.Id).ShowDialog();
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
                _iatAsset.AddAgentDialogAction(newDialogueAction);
                RefreshAgentDialogs();
            }
        }

        private void textToSpeachToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dialogs = _iatAsset.GetDialogueActionsBySpeaker(AGENT).ToArray();
			var t = new TextToSpeechForm(dialogs);
			t.Show(this);
		}

		private void importToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ImportFromExcel();
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ExportToExcel();
		}

	    #region Import/Export Data to Excel

	    private void ImportFromExcel()
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

				//Clear all actions
				ClearAllDialogActions(PLAYER);
				ClearAllDialogActions(AGENT);

			    foreach (var d in playerDialogs)
					_iatAsset.AddPlayerDialogAction(d);

			    foreach (var d in agentDialogs)
					_iatAsset.AddAgentDialogAction(d);
			}

			RefreshPlayerDialogs();
			RefreshAgentDialogs();
		}

	    private void ClearAllDialogActions(string speaker)
	    {
		    _iatAsset.RemoveDialogueActions(speaker,_iatAsset.GetDialogueActionsBySpeaker(speaker).ToArray());
	    }

	    private static IEnumerable<DialogueStateActionDTO> ImportWorkSheet(ExcelPackage package, string workSheetName)
	    {
		    var worksheet = package.Workbook.Worksheets[workSheetName];
			if(worksheet==null)
				throw new Exception($"Could not find worksheet with the name \"{workSheetName}\"");

		    var lastRow = worksheet.Dimension.End.Row;
			var cells = worksheet.Cells;
			for (int i = 3; i <= lastRow; i++)
			{
				var rowValues = Enumerable.Range(1, 5).Select(j => cells[i, j].Text).ToArray();
				if(rowValues.All(string.IsNullOrEmpty))
					continue;

				var value = new DialogueStateActionDTO()
				{
					CurrentState = rowValues[0],
					NextState = rowValues[1],
					Utterance = rowValues[2],
					Meaning = rowValues[3].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray(),
					Style = rowValues[4].Split(',').Select(s => s.Trim()).Where(s=>!string.IsNullOrEmpty(s)).ToArray()
				};

				yield return value;
			}
	    }

	    private void ExportToExcel()
	    {
		    var sfd = new SaveFileDialog();
		    sfd.Filter = "Excel Workbook|*.xlsx";
		    if (sfd.ShowDialog() != DialogResult.OK)
			    return;

			var fileName = new FileInfo(sfd.FileName);
		    using (var excelDoc = new ExcelPackage())
		    {
				ExportWorkSheet(excelDoc,"Player Dialogs", _iatAsset.GetDialogueActionsBySpeaker(PLAYER));
                ExportWorkSheet(excelDoc, "Agent Dialogs", _iatAsset.GetDialogueActionsBySpeaker(AGENT));
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

			worksheet.View.FreezePanes(3,1);

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


        #endregion

        private void dataGridViewPlayerDialogueActions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void validateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dfsearch = new DFSearch<string>(state => _iatAsset.GetAllDialogueActionsByState(state).Select(dto => dto.NextState));
            dfsearch.InitializeSearch(IATConsts.INITIAL_DIALOGUE_STATE);
            dfsearch.FullSearch();

            int unreachableStatesCount = 0;
            int totalStates = 0;
            string unreachableStatesDescription = "The following Dialogue States are not reachable: \n[";

            foreach(var dAction in _iatAsset.GetAllDialogueActions().GroupBy(da => da.CurrentState).Select(group => group.First()))
            {
                totalStates++;
                if(dfsearch.Closed.SearchInClosed(new NodeRecord<string>() { node = dAction.CurrentState.ToString() })==null)
                {
                    unreachableStatesCount++;
                    unreachableStatesDescription += dAction.CurrentState + ", ";
                }
            }

            unreachableStatesDescription = unreachableStatesDescription.Remove(unreachableStatesDescription.Length - 2);
            unreachableStatesDescription += "]";


            string validationMessage;

            if(unreachableStatesCount > 0)
            {
                validationMessage = "Reachability: " + (totalStates - unreachableStatesCount)*100/totalStates + "%\n"+ unreachableStatesDescription;
            }
            else
            {
                validationMessage = "All Dialogue States are reachable!";
            }

            //get the dead ends
            validationMessage += "\n\nEnd States:\n[" + string.Join(",",dfsearch.End.ToArray()) + "]";

            MessageBox.Show(validationMessage);
        }

        private void importFromtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ImportFromTxt();
        }


        private void ImportFromTxt()
        {

            var ofd = new OpenFileDialog();
            ofd.Filter = "Text File|*.txt";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var fileName = new FileInfo(ofd.FileName);
            File.SetAttributes(fileName.DirectoryName, FileAttributes.Normal);

            ClearAllDialogActions(PLAYER);
            ClearAllDialogActions(AGENT);
            stateCounter = 0;
            totalSize = 0;

            var lines = File.ReadAllLines(fileName.FullName);
            totalSize = lines.Length;

          

            foreach (var line in lines) 
            {
                if (line.Contains("P:"))
                {
                    var add = getPlayerDialogueAction(line);
                    _iatAsset.AddPlayerDialogAction(add);
                }

                else if (line.Contains("A:"))
                {


                    var add = getAgentDialogueAction(line);
                    _iatAsset.AddAgentDialogAction(add);

                }
            }

            RefreshPlayerDialogs();
            RefreshAgentDialogs();
        }



        private DialogueStateActionDTO getPlayerDialogueAction(string line)
        {

            line = line.Replace("P:", "");
            char[] delimitedchars = {'\n'};
            line = line.Trim();

            var result = line.Split(delimitedchars);
            string currentState = "";
            string nextState = "";
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

        private DialogueStateActionDTO getAgentDialogueAction(string line)
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
                Meaning = new string[1],
                NextState = nextState,
                Utterance = result[0],
                Style = new string[1]

            };

            return add;
        }
    }
}
