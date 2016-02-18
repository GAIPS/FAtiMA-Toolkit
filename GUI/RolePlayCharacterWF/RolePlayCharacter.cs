using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisalWF;
using System.IO;

namespace RolePlayCharacterWF
{
    public partial class RolePlayCharacter : Form
    {

        private string _nameFileNPC;
        private string _descriptionNPC;

        private ListView _emotionalAppraisalSelectionView;


        private ListView.SelectedListViewItemCollection _emotionalAppraisalSelected;
        private ListView.SelectedListViewItemCollection _emotionalAppraisalToSelected;

        private ListView.SelectedListViewItemCollection _emotionalDecisionMakingSelected;
        private ListView.SelectedListViewItemCollection _emotionalDecisionMakingToSelected;

        public string NameFileNPC
        {
            get { return _nameFileNPC; }

            set { _nameFileNPC = value; }
        }

        public string DescriptionNPC
        {
            get { return _descriptionNPC; }

            set { _descriptionNPC = value; }
        }

        public RolePlayCharacter()
        {
            InitializeComponent();
        }

        private void addNewEA_Click(object sender, EventArgs e)
        {
            var emotionalAppraisalWF = new EmotionalAppraisalWF.MainForm();
            emotionalAppraisalWF.ShowDialog();
        }

        private void editEA_Click(object sender, EventArgs e)
        {
            var emotionalAppraisalWF = new EmotionalAppraisalWF.MainForm();
            emotionalAppraisalWF.ShowDialog();
        }

        private void addNewGroupButton_Click(object sender, EventArgs e)
        {

        }

        private void editGroupButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteGroupButton_Click(object sender, EventArgs e)
        {

        }

        private void PopulateListBoxView(ListView lsv, string folder, string fileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(folder);
            FileInfo[] files = dinfo.GetFiles(fileType);
            foreach (FileInfo f in files)
            {
                lsv.Items.Add(f.Name);
            }
        }

        private void PopulateEmotionalAppraisalList()
        {
            PopulateListBoxView(emotionalAppraisalView, "", "");
        }

        private void PopulateEmotionalAppraisalAvailableList()
        {
            PopulateListBoxView(emotionalAppraisalSelectionView, "", "");
        }

        private void PopulateEmotionalDecisionMaKing()
        {

            PopulateListBoxView(emotionalDecisionMakingView, "", ""); 
        }

        private void PopulateEmotionalDecisionMakingAvailable()
        {
            PopulateListBoxView(emotionalDecisionMakingAvailableView, "", "");
        }

        private void descriptionTextBox_TextChanged(object sender, EventArgs e)
        {
            _descriptionNPC = descriptionTextBox.Text;
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            _nameFileNPC = nameTextBox.Text;
        }

        private void emotionalAppraisalSelectionView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionalAppraisalSelectionView = emotionalAppraisalSelectionView;
        }

        private void emotionalDecisionMakingView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_emotionalAppraisalToSelected == null)
                _emotionalAppraisalToSelected = emotionalAppraisalSelectionView.SelectedItems;
            else
                emotionalAppraisalSelectionView.Items.Remove(emotionalAppraisalSelectionView.SelectedItems[0]);
        }

        private void emotionalAppraisalView_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_emotionalAppraisalSelected == null)
                _emotionalAppraisalSelected = emotionalAppraisalView.SelectedItems;
            else
                emotionalAppraisalView.Items.Remove(emotionalAppraisalView.SelectedItems[0]);
        }

        private void emotionalDecisionMakingAvailableView_SelectedIndexChanged(object sender, EventArgs e)
        {
            _emotionalDecisionMakingToSelected = emotionalDecisionMakingAvailableView.SelectedItems ;
        }

        private void deleteEA_Click(object sender, EventArgs e)
        {
            if(_emotionalAppraisalToSelected != null)
                emotionalAppraisalSelectionView.Items.Remove(emotionalAppraisalSelectionView.SelectedItems[0]);
        }

        private void deleteEDM_Click(object sender, EventArgs e)
        {
            if (_emotionalDecisionMakingToSelected != null)
                emotionalDecisionMakingAvailableView.Items.Remove(emotionalDecisionMakingAvailableView.SelectedItems[0]);
        }

        private void addNewEDM_Click(object sender, EventArgs e)
        {

        }

        private void editEDM_Click(object sender, EventArgs e)
        {

        }
    }
}
