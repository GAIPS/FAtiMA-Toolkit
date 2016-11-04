using System;
using System.Collections;
using System.Collections.Generic;
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

namespace IntegratedAuthoringToolWF
{
    public partial class DialogueEditorForm : Form
    {
	    private MainForm _parentForm;
	    private IntegratedAuthoringToolAsset _iatAsset => _parentForm.CurrentAsset;
        private BindingListView<GUIDialogStateAction> _playerDialogs;
        private BindingListView<GUIDialogStateAction> _agentDialogs;

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
        }

	    private void RefreshPlayerDialogs()
	    {
			_playerDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL).Select(d => new GUIDialogStateAction(d)).ToList();
			_playerDialogs.Refresh();
			dataGridViewPlayerDialogueActions.Columns["Id"].Visible = false;
		}

	    private void RefreshAgentDialogs()
	    {
			_agentDialogs.DataSource = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).Select(d => new GUIDialogStateAction(d)).ToList();
			_agentDialogs.Refresh();
			dataGridViewAgentDialogueActions.Columns["Id"].Visible = false;
		}

		private void buttonAddPlayerDialogueAction_Click(object sender, System.EventArgs e)
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
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.PLAYER, itemsToRemove);
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
            _iatAsset.RemoveDialogueActions(IntegratedAuthoringToolAsset.AGENT, itemsToRemove);
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

        private void buttonAgentEditDialogAction_Click(object sender, System.EventArgs e)
        {
            if (dataGridViewAgentDialogueActions.SelectedRows.Count == 1)
            {
                var item = ((ObjectView<GUIDialogStateAction>)dataGridViewAgentDialogueActions.SelectedRows[0].DataBoundItem).Object;
                new AddOrEditDialogueActionForm(_parentForm, false, item.Id).ShowDialog();
                RefreshAgentDialogs();
            }
        }

		private void textToSpeachToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			var dialogs = _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToArray();
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
				ClearAllDialogActions(IntegratedAuthoringToolAsset.PLAYER);
				ClearAllDialogActions(IntegratedAuthoringToolAsset.AGENT);

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
		    _iatAsset.RemoveDialogueActions(speaker,_iatAsset.GetDialogueActions(speaker, WellFormedNames.Name.UNIVERSAL_SYMBOL).ToArray());
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
				ExportWorkSheet(excelDoc,"Player Dialogs", _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.PLAYER, WellFormedNames.Name.UNIVERSAL_SYMBOL));
				ExportWorkSheet(excelDoc, "Agent Dialogs", _iatAsset.GetDialogueActions(IntegratedAuthoringToolAsset.AGENT, WellFormedNames.Name.UNIVERSAL_SYMBOL));

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
	}
}
