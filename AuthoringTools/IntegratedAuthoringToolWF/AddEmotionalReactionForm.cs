using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmotionalAppraisal.OCCModel;
using Serilog;

namespace IntegratedAuthoringToolWF
{
    public partial class AddEmotionalReactionForm : Form
    {
        public bool neverShowAgain;
        public OCCEmotionType targetEmotion, subjectEmotion;
       
        public AddEmotionalReactionForm(List<string> actionNames)
        {
            InitializeComponent();
            var emotionList = new List<string>();
            emotionList.Add("None");
            emotionList.AddRange(OCCEmotionType.Types.ToList());

            var emotionList2 = new List<string>();
            emotionList2.Add("None");
            emotionList2.AddRange(OCCEmotionType.Types.ToList());

            // We need to create 2 different objects otherwise they will pair up
            targetEmotionBox.DataSource = emotionList;
            subjectEmotionBox.DataSource = emotionList2;
            this.actionComboBox.DataSource = actionNames;
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = !groupBox1.Visible;
        }

        private void AddEmotionalReactionForm_Load(object sender, EventArgs e)
        {

        }

        private void acceptButton_Click(object sender, EventArgs e)
        {          

            if (targetEmotionBox.SelectedItem != null && targetEmotionBox.SelectedItem.ToString() != "None")
                targetEmotion = OCCEmotionType.Parse((string)targetEmotionBox.SelectedItem);

            if (subjectEmotionBox.SelectedItem != null && subjectEmotionBox.SelectedItem.ToString() != "None")
                subjectEmotion = OCCEmotionType.Parse((string)subjectEmotionBox.SelectedItem);

        }
    }
}
