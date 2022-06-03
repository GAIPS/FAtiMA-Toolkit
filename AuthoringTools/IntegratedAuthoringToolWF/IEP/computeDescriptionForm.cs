using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WellFormedNames;
using IntegratedAuthoringTool;
using EmotionalDecisionMaking;
using GAIPS.Rage;
using Conditions.DTOs;
using ActionLibrary;
using KnowledgeBase;
using EmotionalAppraisal;
using EmotionalAppraisal.DTOs;
using RolePlayCharacter;
using AutobiographicMemory.DTOs;
using IntegratedAuthoringTool.DTOs;

namespace IntegratedAuthoringToolWF.IEP
{
    public partial class ComputeDescriptionForm : Form
    {

        private OutputManager _outputManager;
        ConnectToServerForm _server;
       

        public ComputeDescriptionForm(OutputManager outputManager, ConnectToServerForm server,string description)
        {
            InitializeComponent();
            _outputManager = outputManager;
            _server = server;
            if (description != "")
                this.descriptionText.Text = description;
            else this.descriptionText.Text = "Write your story here...";
            computeStoryButton.Enabled = false;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            if (!_server.connected)
            {
                MessageBox.Show("Please connect to the Wizard Server");
                _server.currentForm = this;
                _server.ShowDialog();
            }

            if (_server.connected)
            {
                this.computeStoryButton.Enabled = true;
                this.ShowDialog();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            var description = "";

            if (descriptionText.Text == null)
            {
                description = "John loves flowers. \n John likes water.";
            }
            else
            {
                description = descriptionText.Text;
            }

            var iepResult = _server.ProcessDescription(description);


            if (iepResult != "")
            {

                var f = new IEPOutputForm(this, _outputManager, iepResult);

                f.ShowDialog();

            }



        }
        private void debugLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void computeDescriptionForm_Load(object sender, EventArgs e)
        {

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void descriptionTextFocus(object sender, EventArgs e)
        {
        
        }


        private void descriptionText_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://fatima-toolkit.eu/using-the-compute-story-tool/");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ComputeDescriptionForm_Shown(object sender, EventArgs e)
        {
            if (_server.connected)
            {
                this.computeStoryButton.Enabled = true;
            }
        }
    }
}
        
