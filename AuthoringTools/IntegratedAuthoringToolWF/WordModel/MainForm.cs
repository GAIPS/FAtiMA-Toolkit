using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutobiographicMemory;
using WellFormedNames;
using WorldModel;
using WorldModel.DTOs;

namespace WorldModelWF
{
    public partial class MainForm : Form
    {
        private WorldModelAsset _wmAsset;

        public MainForm(WorldModelAsset asset)
        {
            InitializeComponent();
            OnAssetDataLoaded(asset);
        }


        protected void OnAssetDataLoaded(WorldModelAsset asset)
        {
            _wmAsset = asset;

            DataSet DS = new DataSet();

            DataTable table = new DataTable();
            table.TableName = "Table";
            table.Columns.Add("Action");
            table.Columns.Add("Subject");
            table.Columns.Add("Target");
            table.Columns.Add("Priority");

            foreach (var a in asset.GetAllActions())
            {
                var aName = a.Item1;
                table.Rows.Add(aName.GetNTerm(3), aName.GetNTerm(2), aName.GetNTerm(4), a.Item2);
            }

            DS.Tables.Add(table);

            dataGridViewEventTemplates.DataSource = DS;

            this.dataGridViewEventTemplates.DataMember = "Table";

            dataGridViewEventTemplates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (asset.GetAllActions().FirstOrDefault() != null)
            {
                dataGridViewEffects.DataSource = asset.GetAllEventEffects()[asset.GetAllActions().FirstOrDefault().Item1];
                dataGridViewEffects.Columns[0].Visible = false;
                dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (asset.GetAllEventEffects().Count == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                buttonRemoveAttRule.Enabled = false;
                buttonEditAttRule.Enabled = false;
                button4.Enabled = false;
                addEffectDTO.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
                buttonRemoveAttRule.Enabled = true;
                buttonEditAttRule.Enabled = true;
                button4.Enabled = true;
                addEffectDTO.Enabled = true;
            }

            
        }
        

        private void buttonAddEvent_Click(object sender, EventArgs e)
        {
            var ev = new AddOrEditActionTemplateForm(_wmAsset, null);
            ev.ShowDialog(this);
            RefreshEventList();
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {
        }

        private void addEffectDTO_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = _wmAsset.GetAllActions().ElementAt(index);
            var ef = new AddorEditEffect(_wmAsset, eventTemp.Item1, -1, new EffectDTO());
            ef.ShowDialog(this);
            dataGridViewEventTemplates_SelectionChanged(sender, e);
            RefreshEventList();
        }

        private void dataGridViewEventTemplates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_wmAsset.GetAllEventEffects().Count > 0)
            {
               button1.Enabled = true;
                button2.Enabled = true;
                buttonRemoveAttRule.Enabled = true;
                buttonEditAttRule.Enabled = true;
                button4.Enabled = true;
                addEffectDTO.Enabled = true;
            }
        }

        private void RefreshEventList()
        {
            var ds = (DataSet)dataGridViewEventTemplates.DataSource;

            ds.Tables[0].Clear();

            foreach (var a in _wmAsset.GetAllActions())
            {
                var aName = a.Item1;
                var aPriority = a.Item2;
                ds.Tables[0].Rows.Add(aName.GetNTerm(3), aName.GetNTerm(2), aName.GetNTerm(4),aPriority);
            }

               if (_wmAsset.GetAllEventEffects().Count > 0)
            {
               button1.Enabled = true;
                button2.Enabled = true;
                buttonRemoveAttRule.Enabled = true;
                buttonEditAttRule.Enabled = true;
                button4.Enabled = true;
                addEffectDTO.Enabled = true;
            }

            RefreshEffects();
        }

        private void dataGridViewEventTemplates_SelectionChanged(object sender, EventArgs e)
        {
            var index = 0;

            if (dataGridViewEventTemplates.SelectedRows.Count > 0)
                index = dataGridViewEventTemplates.SelectedRows[0].Index;

            dataGridViewEffects.DataSource = null;
            if (_wmAsset != null)
            {
                if (_wmAsset.GetAllEventEffects().Count == 0)
                    return;
                var evt = _wmAsset.GetAllEventEffects().Keys.ElementAt(index);

                if (_wmAsset.GetAllEventEffects()[evt].Count > 0)
                {
                    dataGridViewEffects.DataSource = _wmAsset.GetAllEventEffects().ElementAt(index).Value;
                    dataGridViewEffects.Columns[0].Visible = false;
                    dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                }

                addEffectDTO.Enabled = true;
            }
        }

        public void RefreshEffects()
        {
            var index = 0;

            if (dataGridViewEventTemplates.SelectedRows.Count > 0)
                index = dataGridViewEventTemplates.SelectedRows[0].Index;

            dataGridViewEffects.DataSource = null;
            if (_wmAsset != null)
            {
                if (_wmAsset.GetAllEventEffects().Count == 0)
                    return;
                var evt = _wmAsset.GetAllEventEffects().Keys.ElementAt(index);

                if (_wmAsset.GetAllEventEffects()[evt].Count > 0)
                {
                    dataGridViewEffects.DataSource = _wmAsset.GetAllEventEffects().ElementAt(index).Value;
                    dataGridViewEffects.Columns[0].Visible = false;
                    dataGridViewEffects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Enabled = true;
                }
                else
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                }

                addEffectDTO.Enabled = true;
            }
        }

        private void buttonEditAttRule_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var actionTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;
            var priority = _wmAsset.GetAllActions().ElementAt(index).Item2;

            var ev = new AddOrEditActionTemplateForm(_wmAsset, actionTemp, priority);
            ev.ShowDialog(this);
            
            RefreshEventList();
        }

        private void button2_Click(object sender, EventArgs e) // Edit Effect
        {
            var index2 = -1;

            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var actionTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;

            index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = _wmAsset.GetAllEventEffects()[actionTemp].ElementAt(index2);

            var ef = new AddorEditEffect(_wmAsset, actionTemp, index2, effect.ToDTO());

            ef.ShowDialog(this);

            

            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void buttonRemoveAttRule_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var actionTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;

            _wmAsset.RemoveAction(actionTemp);
            

            RefreshEventList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = _wmAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            _wmAsset.RemoveEffect(eventTemp, effect);
            
            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e) // Duplicate Effect
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;

            var index2 = dataGridViewEffects.SelectedRows[0].Index;

            var effect = _wmAsset.GetAllEventEffects()[eventTemp].ElementAt(index2);

            _wmAsset.AddActionEffect(eventTemp, effect.ToDTO());
            
            dataGridViewEventTemplates_SelectionChanged(sender, e);
        }

        private void dataGridViewEffects_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e) // Duplicate Action
        {
            var index = dataGridViewEventTemplates.SelectedRows[0].Index;

            var eventTemp = _wmAsset.GetAllActions().ElementAt(index).Item1;

            //Cant add events with the same name so I have to rework their variables

            string newActionName = "Duplicate" + eventTemp.GetNTerm(3).ToString();

            
            var newEventTemp = WellFormedNames.Name.BuildName(
                (Name) AMConsts.EVENT,
                (Name) "Action-End",
                eventTemp.GetNTerm(2),
                (Name)newActionName,
                eventTemp.GetNTerm(4));

            var priority = _wmAsset.GetAllActions().ElementAt(index).Item2;

            _wmAsset.addActionTemplate((Name)newEventTemp, priority);

            _wmAsset.AddActionEffects((Name)newEventTemp, _wmAsset.GetAllEventEffects()[eventTemp].ToList());

            

            RefreshEventList();

        }
    }
}