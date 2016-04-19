namespace IntegratedAuthoringToolWF
{
    partial class DialogueEditorForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonEditCharacter = new System.Windows.Forms.Button();
            this.dataGridViewDialogActions = new System.Windows.Forms.DataGridView();
            this.buttonAddCharacter = new System.Windows.Forms.Button();
            this.buttonRemoveCharacter = new System.Windows.Forms.Button();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDialogActions)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonEditCharacter);
            this.groupBox1.Controls.Add(this.dataGridViewDialogActions);
            this.groupBox1.Controls.Add(this.buttonAddCharacter);
            this.groupBox1.Controls.Add(this.buttonRemoveCharacter);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 393);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dialogue Actions";
            // 
            // buttonEditCharacter
            // 
            this.buttonEditCharacter.Location = new System.Drawing.Point(66, 19);
            this.buttonEditCharacter.Name = "buttonEditCharacter";
            this.buttonEditCharacter.Size = new System.Drawing.Size(54, 23);
            this.buttonEditCharacter.TabIndex = 14;
            this.buttonEditCharacter.Text = "Edit";
            this.buttonEditCharacter.UseVisualStyleBackColor = true;
            // 
            // dataGridViewDialogActions
            // 
            this.dataGridViewDialogActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewDialogActions.AllowUserToAddRows = false;
            this.dataGridViewDialogActions.AllowUserToDeleteRows = false;
            this.dataGridViewDialogActions.AllowUserToOrderColumns = true;
            this.dataGridViewDialogActions.AllowUserToResizeRows = false;
            this.dataGridViewDialogActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDialogActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDialogActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewDialogActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDialogActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewDialogActions.Location = new System.Drawing.Point(6, 51);
            this.dataGridViewDialogActions.Name = "dataGridViewDialogActions";
            this.dataGridViewDialogActions.ReadOnly = true;
            this.dataGridViewDialogActions.RowHeadersVisible = false;
            this.dataGridViewDialogActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDialogActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDialogActions.Size = new System.Drawing.Size(478, 336);
            this.dataGridViewDialogActions.TabIndex = 13;
            // 
            // buttonAddCharacter
            // 
            this.buttonAddCharacter.Location = new System.Drawing.Point(6, 19);
            this.buttonAddCharacter.Name = "buttonAddCharacter";
            this.buttonAddCharacter.Size = new System.Drawing.Size(54, 23);
            this.buttonAddCharacter.TabIndex = 10;
            this.buttonAddCharacter.Text = "Add";
            this.buttonAddCharacter.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveCharacter
            // 
            this.buttonRemoveCharacter.Location = new System.Drawing.Point(126, 19);
            this.buttonRemoveCharacter.Name = "buttonRemoveCharacter";
            this.buttonRemoveCharacter.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveCharacter.TabIndex = 11;
            this.buttonRemoveCharacter.Text = "Remove";
            this.buttonRemoveCharacter.UseVisualStyleBackColor = true;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(514, 24);
            this.mainMenu.TabIndex = 16;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "&Import";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            // 
            // DialogueEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 432);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.groupBox1);
            this.Name = "DialogueEditorForm";
            this.Text = "Dialogue Editor";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDialogActions)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEditCharacter;
        private System.Windows.Forms.DataGridView dataGridViewDialogActions;
        private System.Windows.Forms.Button buttonAddCharacter;
        private System.Windows.Forms.Button buttonRemoveCharacter;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    }
}