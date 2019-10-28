namespace RolePlayCharacterWF
{
    partial class AddOrEditGoalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxGoalName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.floatFieldBoxSignificance = new GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox();
            this.label2 = new System.Windows.Forms.Label();
            this.floatFieldBoxLikelihood = new GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox();
            this.buttonAddOrEditGoal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxGoalName
            // 
            this.textBoxGoalName.AllowComposedName = true;
            this.textBoxGoalName.AllowLiteral = true;
            this.textBoxGoalName.AllowNil = true;
            this.textBoxGoalName.AllowUniversal = true;
            this.textBoxGoalName.AllowUniversalLiteral = true;
            this.textBoxGoalName.AllowVariable = true;
            this.textBoxGoalName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGoalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGoalName.Location = new System.Drawing.Point(36, 68);
            this.textBoxGoalName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxGoalName.Name = "textBoxGoalName";
            this.textBoxGoalName.OnlyIntOrVariable = false;
            this.textBoxGoalName.Size = new System.Drawing.Size(338, 22);
            this.textBoxGoalName.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 45;
            this.label4.Text = "Name:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Significance:";
            // 
            // floatFieldBoxSignificance
            // 
            this.floatFieldBoxSignificance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floatFieldBoxSignificance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floatFieldBoxSignificance.HasBounds = false;
            this.floatFieldBoxSignificance.Location = new System.Drawing.Point(36, 148);
            this.floatFieldBoxSignificance.Margin = new System.Windows.Forms.Padding(4);
            this.floatFieldBoxSignificance.MaxValue = 0F;
            this.floatFieldBoxSignificance.MinValue = 0F;
            this.floatFieldBoxSignificance.Name = "floatFieldBoxSignificance";
            this.floatFieldBoxSignificance.Size = new System.Drawing.Size(338, 22);
            this.floatFieldBoxSignificance.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 20);
            this.label2.TabIndex = 49;
            this.label2.Text = "Likelihood:";
            // 
            // floatFieldBoxLikelihood
            // 
            this.floatFieldBoxLikelihood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.floatFieldBoxLikelihood.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.floatFieldBoxLikelihood.HasBounds = false;
            this.floatFieldBoxLikelihood.Location = new System.Drawing.Point(36, 235);
            this.floatFieldBoxLikelihood.Margin = new System.Windows.Forms.Padding(4);
            this.floatFieldBoxLikelihood.MaxValue = 0F;
            this.floatFieldBoxLikelihood.MinValue = 0F;
            this.floatFieldBoxLikelihood.Name = "floatFieldBoxLikelihood";
            this.floatFieldBoxLikelihood.Size = new System.Drawing.Size(338, 22);
            this.floatFieldBoxLikelihood.TabIndex = 50;
            this.floatFieldBoxLikelihood.TextChanged += new System.EventHandler(this.floatFieldBoxLikelihood_TextChanged);
            // 
            // buttonAddOrEditGoal
            // 
            this.buttonAddOrEditGoal.Location = new System.Drawing.Point(139, 291);
            this.buttonAddOrEditGoal.Name = "buttonAddOrEditGoal";
            this.buttonAddOrEditGoal.Size = new System.Drawing.Size(119, 30);
            this.buttonAddOrEditGoal.TabIndex = 51;
            this.buttonAddOrEditGoal.Text = "Add";
            this.buttonAddOrEditGoal.UseVisualStyleBackColor = true;
            this.buttonAddOrEditGoal.Click += new System.EventHandler(this.buttonAddOrEditGoal_Click);
            // 
            // AddOrEditGoalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 342);
            this.Controls.Add(this.buttonAddOrEditGoal);
            this.Controls.Add(this.floatFieldBoxLikelihood);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.floatFieldBoxSignificance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxGoalName);
            this.Controls.Add(this.label4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximumSize = new System.Drawing.Size(421, 381);
            this.MinimumSize = new System.Drawing.Size(421, 381);
            this.Name = "AddOrEditGoalForm";
            this.Text = "Add Goal";
            this.Load += new System.EventHandler(this.AddOrEditGoalForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditGoalForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxGoalName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox floatFieldBoxSignificance;
        private System.Windows.Forms.Label label2;
        private GAIPS.AssetEditorTools.TypedTextBoxes.FloatFieldBox floatFieldBoxLikelihood;
        private System.Windows.Forms.Button buttonAddOrEditGoal;
    }
}