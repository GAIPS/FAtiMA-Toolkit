using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Equin.ApplicationFramework;
using GAIPS.AssetEditorTools;
using GAIPS.Rage;
using IntegratedAuthoringTool;
using IntegratedAuthoringTool.DTOs;
using RolePlayCharacter;
using RolePlayCharacterWF.ViewModels;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class IEPOutputForm : Form
    {
        private ComputeDescriptionForm parent;
        private KnowledgeBaseVM _knowledgeBaseVM;
        private RolePlayCharacterAsset _loadedAsset;

        public IEPOutputForm(ComputeDescriptionForm f, string extrapolations)
        {
            InitializeComponent();
            parent = f;
            this.richTextBox1.Text = extrapolations;
            LoadOutput();
          
           
        }

        private void outputForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadOutput()
        {
          


         }   


        private void button1_Click(object sender, EventArgs e)
        {
            parent.AcceptedOutput(this.richTextBox1.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parent.RejectedOutput();
            this.Close();
        }

    }
}
