namespace IntegratedAuthoringToolWF
{
    partial class MainForm
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
			this.textBoxScenarioName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.dataGridViewCharacters = new System.Windows.Forms.DataGridView();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonAddCharacter = new System.Windows.Forms.Button();
			this.buttonEditCharacter = new System.Windows.Forms.Button();
			this.buttonRemoveCharacter = new System.Windows.Forms.Button();
			this.buttonCreateCharacter = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharacters)).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxScenarioName
			// 
			this.textBoxScenarioName.Location = new System.Drawing.Point(70, 40);
			this.textBoxScenarioName.Name = "textBoxScenarioName";
			this.textBoxScenarioName.Size = new System.Drawing.Size(310, 20);
			this.textBoxScenarioName.TabIndex = 0;
			this.textBoxScenarioName.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 44);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Scenario:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.tableLayoutPanel1);
			this.groupBox1.Location = new System.Drawing.Point(12, 79);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(598, 328);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Characters";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.dataGridViewCharacters, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 309);
			this.tableLayoutPanel1.TabIndex = 15;
			// 
			// dataGridViewCharacters
			// 
			this.dataGridViewCharacters.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.dataGridViewCharacters.AllowUserToAddRows = false;
			this.dataGridViewCharacters.AllowUserToDeleteRows = false;
			this.dataGridViewCharacters.AllowUserToOrderColumns = true;
			this.dataGridViewCharacters.AllowUserToResizeRows = false;
			this.dataGridViewCharacters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dataGridViewCharacters.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
			this.dataGridViewCharacters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewCharacters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewCharacters.ImeMode = System.Windows.Forms.ImeMode.On;
			this.dataGridViewCharacters.Location = new System.Drawing.Point(3, 38);
			this.dataGridViewCharacters.Name = "dataGridViewCharacters";
			this.dataGridViewCharacters.ReadOnly = true;
			this.dataGridViewCharacters.RowHeadersVisible = false;
			this.dataGridViewCharacters.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridViewCharacters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewCharacters.Size = new System.Drawing.Size(586, 268);
			this.dataGridViewCharacters.TabIndex = 13;
			this.dataGridViewCharacters.SelectionChanged += new System.EventHandler(this.dataGridViewCharacters_SelectionChanged);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.buttonCreateCharacter);
			this.flowLayoutPanel1.Controls.Add(this.buttonAddCharacter);
			this.flowLayoutPanel1.Controls.Add(this.buttonEditCharacter);
			this.flowLayoutPanel1.Controls.Add(this.buttonRemoveCharacter);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(586, 29);
			this.flowLayoutPanel1.TabIndex = 14;
			// 
			// buttonAddCharacter
			// 
			this.buttonAddCharacter.Location = new System.Drawing.Point(79, 3);
			this.buttonAddCharacter.Name = "buttonAddCharacter";
			this.buttonAddCharacter.Size = new System.Drawing.Size(70, 23);
			this.buttonAddCharacter.TabIndex = 10;
			this.buttonAddCharacter.Text = "Add";
			this.buttonAddCharacter.UseVisualStyleBackColor = true;
			this.buttonAddCharacter.Click += new System.EventHandler(this.buttonAddCharacter_Click);
			// 
			// buttonEditCharacter
			// 
			this.buttonEditCharacter.Location = new System.Drawing.Point(155, 3);
			this.buttonEditCharacter.Name = "buttonEditCharacter";
			this.buttonEditCharacter.Size = new System.Drawing.Size(70, 23);
			this.buttonEditCharacter.TabIndex = 14;
			this.buttonEditCharacter.Text = "Edit";
			this.buttonEditCharacter.UseVisualStyleBackColor = true;
			this.buttonEditCharacter.Click += new System.EventHandler(this.buttonEditCharacter_Click);
			// 
			// buttonRemoveCharacter
			// 
			this.buttonRemoveCharacter.Location = new System.Drawing.Point(231, 3);
			this.buttonRemoveCharacter.Name = "buttonRemoveCharacter";
			this.buttonRemoveCharacter.Size = new System.Drawing.Size(70, 23);
			this.buttonRemoveCharacter.TabIndex = 11;
			this.buttonRemoveCharacter.Text = "Remove";
			this.buttonRemoveCharacter.UseVisualStyleBackColor = true;
			this.buttonRemoveCharacter.Click += new System.EventHandler(this.buttonRemoveCharacter_Click);
			// 
			// buttonCreateCharacter
			// 
			this.buttonCreateCharacter.Location = new System.Drawing.Point(3, 3);
			this.buttonCreateCharacter.Name = "buttonCreateCharacter";
			this.buttonCreateCharacter.Size = new System.Drawing.Size(70, 23);
			this.buttonCreateCharacter.TabIndex = 15;
			this.buttonCreateCharacter.Text = "Create";
			this.buttonCreateCharacter.UseVisualStyleBackColor = true;
			this.buttonCreateCharacter.Click += new System.EventHandler(this.buttonCreateCharacter_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 419);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxScenarioName);
			this.EditorName = "Integrated Authoring Tool";
			this.Name = "MainForm";
			this.Text = "Integrated Authoring Tool";
			this.Controls.SetChildIndex(this.textBoxScenarioName, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharacters)).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxScenarioName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddCharacter;
        private System.Windows.Forms.Button buttonRemoveCharacter;
        private System.Windows.Forms.DataGridView dataGridViewCharacters;
        private System.Windows.Forms.Button buttonEditCharacter;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonCreateCharacter;
	}
}

