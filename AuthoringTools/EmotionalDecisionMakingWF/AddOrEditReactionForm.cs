using System;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using EmotionalDecisionMaking.DTOs;
using EmotionalDecisionMakingWF.Properties;

namespace EmotionalDecisionMakingWF
{
    public partial class AddOrEditReactionForm : Form
    {
        private EmotionalDecisionMakingAsset _edmAsset;
        private ReactionDTO _reactionToEdit;

        public AddOrEditReactionForm(EmotionalDecisionMakingAsset edmAsset, ReactionDTO reactionToEdit = null)
        {
            InitializeComponent();

            _edmAsset = edmAsset;
            _reactionToEdit = reactionToEdit;
            
            if (reactionToEdit != null)
            {
                this.Text = Resources.EditReactionFormTitle;
                this.addOrEditButton.Text = Resources.UpdateButtonLabel;
                textBoxAction.Text = reactionToEdit.Action;
                textBoxTarget.Text = reactionToEdit.Target;
                textBoxPriority.Text = reactionToEdit.Priority.ToString();
            }
        }


        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            try
            {
	            float priority;
	            if (!float.TryParse(textBoxPriority.Text, out priority))
		            priority = 0;

                var newReaction = new ReactionDTO
                {
                    Action = textBoxAction.Text,
                    Target = textBoxTarget.Text,
                    Priority = priority.ToString()
                };

                if (_reactionToEdit != null)
                {
                    _edmAsset.UpdateReaction(_reactionToEdit, newReaction);
                }
                else
                {
                    _edmAsset.AddReaction(newReaction);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
	        this.Close();
        }

    }
}
