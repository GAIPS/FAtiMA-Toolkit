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
        private ActionRuleDTO _reactionToEdit;

        public AddOrEditReactionForm(EmotionalDecisionMakingAsset edmAsset, ActionRuleDTO reactionToEdit = null)
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
                textBoxLayer.Text = reactionToEdit.Layer;
            }
        }


        private void addOrEditButton_Click(object sender, EventArgs e)
        {
            try
            {
                var newReaction = new ActionRuleDTO
                {
                    Action = textBoxAction.Text,
                    Target = textBoxTarget.Text,
                    Priority = textBoxPriority.Text,
                    Layer = textBoxLayer.Text,
                };

                if (_reactionToEdit != null)
                {
                    _edmAsset.UpdateActionRule(_reactionToEdit, newReaction);
                }
                else
                {
                    _edmAsset.AddActionRule(newReaction);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
	        this.Close();
        }

        private void AddOrEditReactionForm_Load(object sender, EventArgs e)
        {

        }

        private void AddOrEditReactionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
