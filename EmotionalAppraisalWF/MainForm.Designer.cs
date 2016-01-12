namespace EmotionalAppraisalWF
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.appraisalRulesTagePage = new System.Windows.Forms.TabPage();
            this.emotionalStateTabPage = new System.Windows.Forms.TabPage();
            this.knowledgeBaseTabPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.editButton = new System.Windows.Forms.Button();
            this.addBeliefButton = new System.Windows.Forms.Button();
            this.removeBeliefButton = new System.Windows.Forms.Button();
            this.beliefsListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.visibilityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.autobiographicalMemoryTabPage = new System.Windows.Forms.TabPage();
            this.mainMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.knowledgeBaseTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(434, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            this.mainMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mainMenu_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.appraisalRulesTagePage);
            this.tabControl1.Controls.Add(this.emotionalStateTabPage);
            this.tabControl1.Controls.Add(this.knowledgeBaseTabPage);
            this.tabControl1.Controls.Add(this.autobiographicalMemoryTabPage);
            this.tabControl1.Location = new System.Drawing.Point(12, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(410, 367);
            this.tabControl1.TabIndex = 1;
            // 
            // appraisalRulesTagePage
            // 
            this.appraisalRulesTagePage.Location = new System.Drawing.Point(4, 25);
            this.appraisalRulesTagePage.Name = "appraisalRulesTagePage";
            this.appraisalRulesTagePage.Padding = new System.Windows.Forms.Padding(3);
            this.appraisalRulesTagePage.Size = new System.Drawing.Size(402, 338);
            this.appraisalRulesTagePage.TabIndex = 3;
            this.appraisalRulesTagePage.Text = "Appraisal Rules";
            this.appraisalRulesTagePage.UseVisualStyleBackColor = true;
            // 
            // emotionalStateTabPage
            // 
            this.emotionalStateTabPage.Location = new System.Drawing.Point(4, 25);
            this.emotionalStateTabPage.Name = "emotionalStateTabPage";
            this.emotionalStateTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.emotionalStateTabPage.Size = new System.Drawing.Size(402, 338);
            this.emotionalStateTabPage.TabIndex = 0;
            this.emotionalStateTabPage.Text = "Emotional State";
            this.emotionalStateTabPage.UseVisualStyleBackColor = true;
            // 
            // knowledgeBaseTabPage
            // 
            this.knowledgeBaseTabPage.Controls.Add(this.groupBox1);
            this.knowledgeBaseTabPage.Location = new System.Drawing.Point(4, 25);
            this.knowledgeBaseTabPage.Name = "knowledgeBaseTabPage";
            this.knowledgeBaseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.knowledgeBaseTabPage.Size = new System.Drawing.Size(402, 338);
            this.knowledgeBaseTabPage.TabIndex = 1;
            this.knowledgeBaseTabPage.Text = "Knowledge Base";
            this.knowledgeBaseTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 332);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Beliefs";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.beliefsListView, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(390, 313);
            this.tableLayoutPanel1.TabIndex = 7;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.editButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.addBeliefButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.removeBeliefButton, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(0, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(384, 31);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(63, 3);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(70, 23);
            this.editButton.TabIndex = 6;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addBeliefButton
            // 
            this.addBeliefButton.Location = new System.Drawing.Point(3, 3);
            this.addBeliefButton.Name = "addBeliefButton";
            this.addBeliefButton.Size = new System.Drawing.Size(54, 23);
            this.addBeliefButton.TabIndex = 4;
            this.addBeliefButton.Text = "Add";
            this.addBeliefButton.UseVisualStyleBackColor = true;
            this.addBeliefButton.Click += new System.EventHandler(this.addBeliefButton_Click);
            // 
            // removeBeliefButton
            // 
            this.removeBeliefButton.Location = new System.Drawing.Point(139, 3);
            this.removeBeliefButton.Name = "removeBeliefButton";
            this.removeBeliefButton.Size = new System.Drawing.Size(70, 23);
            this.removeBeliefButton.TabIndex = 5;
            this.removeBeliefButton.Text = "Remove";
            this.removeBeliefButton.UseVisualStyleBackColor = true;
            this.removeBeliefButton.Click += new System.EventHandler(this.removeBeliefButton_Click);
            // 
            // beliefsListView
            // 
            this.beliefsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader,
            this.visibilityColumnHeader});
            this.beliefsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.beliefsListView.FullRowSelect = true;
            this.beliefsListView.Location = new System.Drawing.Point(3, 38);
            this.beliefsListView.Name = "beliefsListView";
            this.beliefsListView.Size = new System.Drawing.Size(384, 281);
            this.beliefsListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.beliefsListView.TabIndex = 2;
            this.beliefsListView.UseCompatibleStateImageBehavior = false;
            this.beliefsListView.View = System.Windows.Forms.View.Details;
            this.beliefsListView.SelectedIndexChanged += new System.EventHandler(this.beliefsListView_SelectedIndexChanged);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 107;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Value";
            this.valueColumnHeader.Width = 146;
            // 
            // visibilityColumnHeader
            // 
            this.visibilityColumnHeader.Text = "Visibility";
            this.visibilityColumnHeader.Width = 192;
            // 
            // autobiographicalMemoryTabPage
            // 
            this.autobiographicalMemoryTabPage.Location = new System.Drawing.Point(4, 25);
            this.autobiographicalMemoryTabPage.Name = "autobiographicalMemoryTabPage";
            this.autobiographicalMemoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.autobiographicalMemoryTabPage.Size = new System.Drawing.Size(402, 338);
            this.autobiographicalMemoryTabPage.TabIndex = 2;
            this.autobiographicalMemoryTabPage.Text = "Autobiographical Memory";
            this.autobiographicalMemoryTabPage.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 406);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.MinimumSize = new System.Drawing.Size(450, 39);
            this.Name = "MainForm";
            this.Text = "Emotional Appraisal";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.knowledgeBaseTabPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage emotionalStateTabPage;
        private System.Windows.Forms.TabPage knowledgeBaseTabPage;
        private System.Windows.Forms.TabPage autobiographicalMemoryTabPage;
        private System.Windows.Forms.TabPage appraisalRulesTagePage;
        private System.Windows.Forms.ListView beliefsListView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
        private System.Windows.Forms.Button addBeliefButton;
        private System.Windows.Forms.Button removeBeliefButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader visibilityColumnHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button editButton;
    }
}

