using System;
using System.Windows.Forms;
using EmotionalDecisionMaking;
using ActionLibrary.DTOs;
using EmotionalDecisionMakingWF.Properties;

namespace EmotionalDecisionMakingWF
{
    public partial class AddOrEditReactionForm : Form
    {
        private EmotionalDecisionMakingAsset _edmAsset;
        private ActionDefinitionDTO _reactionToEdit;

        public AddOrEditReactionForm(EmotionalDecisionMakingAsset edmAsset, ActionDefinitionDTO reactionToEdit = null)
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
                textBoxPriority.Text = reactionToEdit.Priority;
                textBoxType.Text = reactionToEdit.Type;
            }
        }


        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            try
            {

                var newReaction = new ActionDefinitionDTO
                {
                    Action = textBoxAction.Text,
                    Target = textBoxTarget.Text,
                    Priority = textBoxPriority.Text,
                    Type = textBoxType.Text,
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
