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
                textBoxCooldown.Text = reactionToEdit.Cooldown.ToString();
            }
        }


        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            try
            {
	            float cooldown;
	            if (!float.TryParse(textBoxCooldown.Text, out cooldown))
		            cooldown = 0;

                var newReaction = new ReactionDTO
                {
                    Action = textBoxAction.Text,
                    Target = textBoxTarget.Text,
                    Cooldown = cooldown
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

        private void addOrEditBeliefButton_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void beliefVisibilityComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void beliefNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void beliefValueTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void AddOrEditBeliefForm_Load(object sender, EventArgs e)
        {

        }

    }
}
