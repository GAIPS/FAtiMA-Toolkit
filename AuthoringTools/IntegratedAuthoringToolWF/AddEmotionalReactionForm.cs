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

namespace IntegratedAuthoringToolWF
{
    public partial class AddEmotionalReactionForm : Form
    {
        public bool neverShowAgain;
        public OCCEmotionType targetEmotion, subjectEmotion;
        public AddEmotionalReactionForm(string actionName)
        {
            InitializeComponent();
            targetEmotionBox.DataSource = OCCEmotionType.Types.ToList();
            subjectEmotionBox.DataSource = OCCEmotionType.Types.ToList();
            actionText.Text = actionName;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            neverShowAgain = checkBox1.Checked;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
           

            if (targetEmotionBox.SelectedItem != null)
                targetEmotion = OCCEmotionType.Parse((string)targetEmotionBox.SelectedItem);

            if (subjectEmotionBox.SelectedItem != null)
                subjectEmotion = OCCEmotionType.Parse((string)subjectEmotionBox.SelectedItem);
        }
    }
}
