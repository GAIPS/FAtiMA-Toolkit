
namespace IntegratedAuthoringToolWF.IEP
{
    partial class GenerateBeliefsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateBeliefsForm));
            this.inputBox = new System.Windows.Forms.GroupBox();
            this.descriptionText = new System.Windows.Forms.RichTextBox();
            this.outputBox = new System.Windows.Forms.GroupBox();
            this.scenarioTextBox = new System.Windows.Forms.RichTextBox();
            this.internalCharacterView = new System.Windows.Forms.DataGridView();
            this.resultingScenarioGroupBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewGoals = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewBeliefs = new System.Windows.Forms.DataGridView();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.processInputButton = new System.Windows.Forms.Button();
            this.processOutputButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.inputBox.SuspendLayout();
            this.outputBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.internalCharacterView)).BeginInit();
            this.resultingScenarioGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).BeginInit();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Controls.Add(this.descriptionText);
            this.inputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.Location = new System.Drawing.Point(12, 12);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(245, 108);
            this.inputBox.TabIndex = 2;
            this.inputBox.TabStop = false;
            this.inputBox.Text = "Input (start here)";
            // 
            // descriptionText
            // 
            this.descriptionText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.descriptionText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.descriptionText.Location = new System.Drawing.Point(6, 19);
            this.descriptionText.Margin = new System.Windows.Forms.Padding(15, 16, 15, 16);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.descriptionText.Size = new System.Drawing.Size(228, 83);
            this.descriptionText.TabIndex = 0;
            this.descriptionText.Text = "John is a student";
            // 
            // outputBox
            // 
            this.outputBox.Controls.Add(this.scenarioTextBox);
            this.outputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputBox.Location = new System.Drawing.Point(415, 12);
            this.outputBox.Name = "outputBox";
            this.outputBox.Size = new System.Drawing.Size(267, 113);
            this.outputBox.TabIndex = 4;
            this.outputBox.TabStop = false;
            this.outputBox.Text = "Wizard Output (edit this to your liking)";
            // 
            // scenarioTextBox
            // 
            this.scenarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scenarioTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scenarioTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scenarioTextBox.Location = new System.Drawing.Point(6, 18);
            this.scenarioTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.scenarioTextBox.Name = "scenarioTextBox";
            this.scenarioTextBox.Size = new System.Drawing.Size(267, 90);
            this.scenarioTextBox.TabIndex = 2;
            this.scenarioTextBox.Text = "";
            // 
            // internalCharacterView
            // 
            this.internalCharacterView.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.internalCharacterView.AllowUserToAddRows = false;
            this.internalCharacterView.AllowUserToDeleteRows = false;
            this.internalCharacterView.AllowUserToOrderColumns = true;
            this.internalCharacterView.AllowUserToResizeRows = false;
            this.internalCharacterView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.internalCharacterView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.internalCharacterView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.internalCharacterView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.internalCharacterView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.internalCharacterView.Location = new System.Drawing.Point(6, 46);
            this.internalCharacterView.Name = "internalCharacterView";
            this.internalCharacterView.ReadOnly = true;
            this.internalCharacterView.RowHeadersVisible = false;
            this.internalCharacterView.RowHeadersWidth = 51;
            this.internalCharacterView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.internalCharacterView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.internalCharacterView.Size = new System.Drawing.Size(193, 58);
            this.internalCharacterView.TabIndex = 16;
            this.internalCharacterView.SelectionChanged += new System.EventHandler(this.internalCharacterView_SelectionChanged);
            // 
            // resultingScenarioGroupBox
            // 
            this.resultingScenarioGroupBox.Controls.Add(this.label1);
            this.resultingScenarioGroupBox.Controls.Add(this.label3);
            this.resultingScenarioGroupBox.Controls.Add(this.dataGridViewGoals);
            this.resultingScenarioGroupBox.Controls.Add(this.label2);
            this.resultingScenarioGroupBox.Controls.Add(this.dataGridViewBeliefs);
            this.resultingScenarioGroupBox.Controls.Add(this.internalCharacterView);
            this.resultingScenarioGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultingScenarioGroupBox.Location = new System.Drawing.Point(18, 125);
            this.resultingScenarioGroupBox.Name = "resultingScenarioGroupBox";
            this.resultingScenarioGroupBox.Size = new System.Drawing.Size(477, 164);
            this.resultingScenarioGroupBox.TabIndex = 18;
            this.resultingScenarioGroupBox.TabStop = false;
            this.resultingScenarioGroupBox.Text = "Resulting Scenario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Goals:";
            // 
            // dataGridViewGoals
            // 
            this.dataGridViewGoals.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewGoals.AllowUserToAddRows = false;
            this.dataGridViewGoals.AllowUserToDeleteRows = false;
            this.dataGridViewGoals.AllowUserToOrderColumns = true;
            this.dataGridViewGoals.AllowUserToResizeRows = false;
            this.dataGridViewGoals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewGoals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGoals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGoals.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewGoals.Location = new System.Drawing.Point(10, 126);
            this.dataGridViewGoals.Name = "dataGridViewGoals";
            this.dataGridViewGoals.ReadOnly = true;
            this.dataGridViewGoals.RowHeadersVisible = false;
            this.dataGridViewGoals.RowHeadersWidth = 51;
            this.dataGridViewGoals.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGoals.Size = new System.Drawing.Size(189, 32);
            this.dataGridViewGoals.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 18;
            this.label2.Text = "Beliefs:";
            // 
            // dataGridViewBeliefs
            // 
            this.dataGridViewBeliefs.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewBeliefs.AllowUserToAddRows = false;
            this.dataGridViewBeliefs.AllowUserToDeleteRows = false;
            this.dataGridViewBeliefs.AllowUserToOrderColumns = true;
            this.dataGridViewBeliefs.AllowUserToResizeRows = false;
            this.dataGridViewBeliefs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBeliefs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBeliefs.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewBeliefs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBeliefs.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewBeliefs.Location = new System.Drawing.Point(209, 36);
            this.dataGridViewBeliefs.Name = "dataGridViewBeliefs";
            this.dataGridViewBeliefs.ReadOnly = true;
            this.dataGridViewBeliefs.RowHeadersVisible = false;
            this.dataGridViewBeliefs.RowHeadersWidth = 51;
            this.dataGridViewBeliefs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBeliefs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBeliefs.Size = new System.Drawing.Size(252, 122);
            this.dataGridViewBeliefs.TabIndex = 17;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Image = global::IntegratedAuthoringToolWF.Properties.Resources.icons8_system_report_40;
            this.cancelButton.Location = new System.Drawing.Point(359, 309);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(211, 43);
            this.cancelButton.TabIndex = 20;
            this.cancelButton.Text = "      Cancel";
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.acceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptButton.Image = global::IntegratedAuthoringToolWF.Properties.Resources.icons8_system_information_40;
            this.acceptButton.Location = new System.Drawing.Point(110, 309);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(2);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(224, 43);
            this.acceptButton.TabIndex = 19;
            this.acceptButton.Text = "    Add to current Scenario";
            this.acceptButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // processInputButton
            // 
            this.processInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processInputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processInputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processInputButton.Image = global::IntegratedAuthoringToolWF.Properties.Resources.icons8_divisa_circulada_à_direita_40;
            this.processInputButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.processInputButton.Location = new System.Drawing.Point(274, 40);
            this.processInputButton.Margin = new System.Windows.Forms.Padding(2);
            this.processInputButton.Name = "processInputButton";
            this.processInputButton.Size = new System.Drawing.Size(136, 67);
            this.processInputButton.TabIndex = 22;
            this.processInputButton.Text = "Process  Input";
            this.processInputButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.processInputButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.processInputButton.UseVisualStyleBackColor = true;
            this.processInputButton.Click += new System.EventHandler(this.processInputButton_Click);
            // 
            // processOutputButton
            // 
            this.processOutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.processOutputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.processOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processOutputButton.Image = global::IntegratedAuthoringToolWF.Properties.Resources.enter__1_;
            this.processOutputButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.processOutputButton.Location = new System.Drawing.Point(517, 150);
            this.processOutputButton.Margin = new System.Windows.Forms.Padding(2);
            this.processOutputButton.Name = "processOutputButton";
            this.processOutputButton.Size = new System.Drawing.Size(131, 100);
            this.processOutputButton.TabIndex = 23;
            this.processOutputButton.Text = "Process   Output";
            this.processOutputButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.processOutputButton.UseVisualStyleBackColor = true;
            this.processOutputButton.Click += new System.EventHandler(this.processOutputButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 21;
            this.label1.Text = "Characters:";
            // 
            // GenerateBeliefsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 363);
            this.Controls.Add(this.processOutputButton);
            this.Controls.Add(this.processInputButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.resultingScenarioGroupBox);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.inputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GenerateBeliefsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Generate Beliefs Wizard";
            this.Load += new System.EventHandler(this.GenerateBeliefsForm_Load);
            this.inputBox.ResumeLayout(false);
            this.outputBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.internalCharacterView)).EndInit();
            this.resultingScenarioGroupBox.ResumeLayout(false);
            this.resultingScenarioGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox inputBox;
        private System.Windows.Forms.RichTextBox descriptionText;
        private System.Windows.Forms.GroupBox outputBox;
        private System.Windows.Forms.RichTextBox scenarioTextBox;
        private System.Windows.Forms.DataGridView internalCharacterView;
        private System.Windows.Forms.GroupBox resultingScenarioGroupBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewGoals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewBeliefs;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button processInputButton;
        private System.Windows.Forms.Button processOutputButton;
        private System.Windows.Forms.Label label1;
    }
}