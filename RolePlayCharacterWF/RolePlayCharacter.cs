﻿using System;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        private void emotionalAppraisalSelected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void emotionalAppraisalToSelect_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
    }
}