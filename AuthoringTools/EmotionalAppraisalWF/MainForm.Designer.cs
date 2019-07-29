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
            this.components = new System.ComponentModel.Container();
            this.dynamicPropertyListing = new System.Windows.Forms.TabControl();
            this.appraisalRulesTagePage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonAppVariables = new System.Windows.Forms.Button();
            this.buttonDuplicateAppraisalRule = new System.Windows.Forms.Button();
            this.buttonEditAppraisalRule = new System.Windows.Forms.Button();
            this.buttonAddAppraisalRule = new System.Windows.Forms.Button();
            this.buttonRemoveAppraisalRule = new System.Windows.Forms.Button();
            this.dataGridViewAppraisalRules = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.dataGridViewGoals = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonEditGoal = new System.Windows.Forms.Button();
            this.emotionDispositionTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxDefaultDecay = new System.Windows.Forms.ComboBox();
            this.comboBoxDefaultThreshold = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridViewEmotionDispositions = new System.Windows.Forms.DataGridView();
            this.buttonEditEmotionDisposition = new System.Windows.Forms.Button();
            this.buttonAddEmotionDisposition = new System.Windows.Forms.Button();
            this.buttonRemoveEmotionDisposition = new System.Windows.Forms.Button();
            this.decayErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.emotionListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.mainFormBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dynamicPropertyListing.SuspendLayout();
            this.appraisalRulesTagePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).BeginInit();
            this.emotionDispositionTabPage.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmotionDispositions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.decayErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionListItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dynamicPropertyListing
            // 
            this.dynamicPropertyListing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dynamicPropertyListing.Controls.Add(this.appraisalRulesTagePage);
            this.dynamicPropertyListing.Controls.Add(this.tabPage1);
            this.dynamicPropertyListing.Controls.Add(this.emotionDispositionTabPage);
            this.dynamicPropertyListing.Location = new System.Drawing.Point(16, 32);
            this.dynamicPropertyListing.Margin = new System.Windows.Forms.Padding(4);
            this.dynamicPropertyListing.Name = "dynamicPropertyListing";
            this.dynamicPropertyListing.SelectedIndex = 0;
            this.dynamicPropertyListing.Size = new System.Drawing.Size(885, 492);
            this.dynamicPropertyListing.TabIndex = 1;
            // 
            // appraisalRulesTagePage
            // 
            this.appraisalRulesTagePage.Controls.Add(this.splitContainer1);
            this.appraisalRulesTagePage.Location = new System.Drawing.Point(4, 25);
            this.appraisalRulesTagePage.Margin = new System.Windows.Forms.Padding(4);
            this.appraisalRulesTagePage.Name = "appraisalRulesTagePage";
            this.appraisalRulesTagePage.Padding = new System.Windows.Forms.Padding(4);
            this.appraisalRulesTagePage.Size = new System.Drawing.Size(877, 463);
            this.appraisalRulesTagePage.TabIndex = 3;
            this.appraisalRulesTagePage.Text = "Appraisal Rules";
            this.appraisalRulesTagePage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox8);
            this.splitContainer1.Size = new System.Drawing.Size(869, 455);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 11;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonAppVariables);
            this.groupBox7.Controls.Add(this.buttonDuplicateAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonEditAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonAddAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonRemoveAppraisalRule);
            this.groupBox7.Controls.Add(this.dataGridViewAppraisalRules);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox7.Size = new System.Drawing.Size(869, 225);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Rules";
            this.groupBox7.Enter += new System.EventHandler(this.GroupBox7_Enter);
            // 
            // buttonAppVariables
            // 
            this.buttonAppVariables.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAppVariables.Location = new System.Drawing.Point(404, 18);
            this.buttonAppVariables.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAppVariables.Name = "buttonAppVariables";
            this.buttonAppVariables.Size = new System.Drawing.Size(170, 25);
            this.buttonAppVariables.TabIndex = 11;
            this.buttonAppVariables.Text = "Appraisal Variables";
            this.buttonAppVariables.UseVisualStyleBackColor = true;
            this.buttonAppVariables.Click += new System.EventHandler(this.buttonAppVariables_Click);
            // 
            // buttonDuplicateAppraisalRule
            // 
            this.buttonDuplicateAppraisalRule.Location = new System.Drawing.Point(185, 18);
            this.buttonDuplicateAppraisalRule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDuplicateAppraisalRule.Name = "buttonDuplicateAppraisalRule";
            this.buttonDuplicateAppraisalRule.Size = new System.Drawing.Size(96, 25);
            this.buttonDuplicateAppraisalRule.TabIndex = 10;
            this.buttonDuplicateAppraisalRule.Text = "Duplicate";
            this.buttonDuplicateAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonDuplicateAppraisalRule.Click += new System.EventHandler(this.buttonDuplicateAppraisalRule_Click);
            // 
            // buttonEditAppraisalRule
            // 
            this.buttonEditAppraisalRule.Location = new System.Drawing.Point(98, 18);
            this.buttonEditAppraisalRule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonEditAppraisalRule.Name = "buttonEditAppraisalRule";
            this.buttonEditAppraisalRule.Size = new System.Drawing.Size(81, 25);
            this.buttonEditAppraisalRule.TabIndex = 9;
            this.buttonEditAppraisalRule.Text = "Edit";
            this.buttonEditAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonEditAppraisalRule.Click += new System.EventHandler(this.buttonEditAppraisalRule_Click);
            // 
            // buttonAddAppraisalRule
            // 
            this.buttonAddAppraisalRule.Location = new System.Drawing.Point(6, 18);
            this.buttonAddAppraisalRule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAddAppraisalRule.Name = "buttonAddAppraisalRule";
            this.buttonAddAppraisalRule.Size = new System.Drawing.Size(85, 25);
            this.buttonAddAppraisalRule.TabIndex = 7;
            this.buttonAddAppraisalRule.Text = "Add";
            this.buttonAddAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonAddAppraisalRule.Click += new System.EventHandler(this.buttonAddAppraisalRule_Click);
            // 
            // buttonRemoveAppraisalRule
            // 
            this.buttonRemoveAppraisalRule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemoveAppraisalRule.Location = new System.Drawing.Point(288, 18);
            this.buttonRemoveAppraisalRule.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRemoveAppraisalRule.Name = "buttonRemoveAppraisalRule";
            this.buttonRemoveAppraisalRule.Size = new System.Drawing.Size(110, 25);
            this.buttonRemoveAppraisalRule.TabIndex = 8;
            this.buttonRemoveAppraisalRule.Text = "Remove";
            this.buttonRemoveAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonRemoveAppraisalRule.Click += new System.EventHandler(this.buttonRemoveAppraisalRule_Click);
            // 
            // dataGridViewAppraisalRules
            // 
            this.dataGridViewAppraisalRules.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAppraisalRules.AllowUserToAddRows = false;
            this.dataGridViewAppraisalRules.AllowUserToDeleteRows = false;
            this.dataGridViewAppraisalRules.AllowUserToOrderColumns = true;
            this.dataGridViewAppraisalRules.AllowUserToResizeRows = false;
            this.dataGridViewAppraisalRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAppraisalRules.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAppraisalRules.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAppraisalRules.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAppraisalRules.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAppraisalRules.Location = new System.Drawing.Point(8, 49);
            this.dataGridViewAppraisalRules.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewAppraisalRules.Name = "dataGridViewAppraisalRules";
            this.dataGridViewAppraisalRules.ReadOnly = true;
            this.dataGridViewAppraisalRules.RowHeadersVisible = false;
            this.dataGridViewAppraisalRules.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAppraisalRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAppraisalRules.Size = new System.Drawing.Size(857, 170);
            this.dataGridViewAppraisalRules.TabIndex = 2;
            this.dataGridViewAppraisalRules.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAppraisalRules_CellContentClick);
            this.dataGridViewAppraisalRules.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAppraisalRules_CellMouseDoubleClick);
            this.dataGridViewAppraisalRules.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAppraisalRules_RowEnter);
            this.dataGridViewAppraisalRules.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewAppraisalRules_KeyDown);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.conditionSetEditor);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox8.Size = new System.Drawing.Size(869, 225);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Rule Conditions";
            // 
            // conditionSetEditor
            // 
            this.conditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSetEditor.Location = new System.Drawing.Point(4, 18);
            this.conditionSetEditor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.conditionSetEditor.Name = "conditionSetEditor";
            this.conditionSetEditor.Size = new System.Drawing.Size(861, 204);
            this.conditionSetEditor.TabIndex = 0;
            this.conditionSetEditor.View = null;
            this.conditionSetEditor.Load += new System.EventHandler(this.conditionSetEditor_Load);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(877, 463);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Goals";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonRemove);
            this.groupBox1.Controls.Add(this.dataGridViewGoals);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.buttonEditGoal);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(866, 449);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // buttonRemove
            // 
            this.buttonRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRemove.Location = new System.Drawing.Point(164, 14);
            this.buttonRemove.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemove.MinimumSize = new System.Drawing.Size(90, 27);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(90, 27);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // dataGridViewGoals
            // 
            this.dataGridViewGoals.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewGoals.AllowUserToAddRows = false;
            this.dataGridViewGoals.AllowUserToDeleteRows = false;
            this.dataGridViewGoals.AllowUserToOrderColumns = true;
            this.dataGridViewGoals.AllowUserToResizeRows = false;
            this.dataGridViewGoals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewGoals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewGoals.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewGoals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGoals.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewGoals.Location = new System.Drawing.Point(7, 49);
            this.dataGridViewGoals.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewGoals.Name = "dataGridViewGoals";
            this.dataGridViewGoals.ReadOnly = true;
            this.dataGridViewGoals.RowHeadersVisible = false;
            this.dataGridViewGoals.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGoals.Size = new System.Drawing.Size(851, 392);
            this.dataGridViewGoals.TabIndex = 2;
            this.dataGridViewGoals.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewGoals_CellContentClick);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(7, 14);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 27);
            this.button2.TabIndex = 7;
            this.button2.Text = "Add";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonAddGoal_Click);
            // 
            // buttonEditGoal
            // 
            this.buttonEditGoal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEditGoal.Location = new System.Drawing.Point(81, 14);
            this.buttonEditGoal.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditGoal.Name = "buttonEditGoal";
            this.buttonEditGoal.Size = new System.Drawing.Size(75, 27);
            this.buttonEditGoal.TabIndex = 9;
            this.buttonEditGoal.Text = "Edit";
            this.buttonEditGoal.UseVisualStyleBackColor = true;
            this.buttonEditGoal.Click += new System.EventHandler(this.buttonEditGoal_Click);
            // 
            // emotionDispositionTabPage
            // 
            this.emotionDispositionTabPage.Controls.Add(this.tableLayoutPanel8);
            this.emotionDispositionTabPage.Location = new System.Drawing.Point(4, 25);
            this.emotionDispositionTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.emotionDispositionTabPage.Name = "emotionDispositionTabPage";
            this.emotionDispositionTabPage.Padding = new System.Windows.Forms.Padding(4);
            this.emotionDispositionTabPage.Size = new System.Drawing.Size(877, 463);
            this.emotionDispositionTabPage.TabIndex = 4;
            this.emotionDispositionTabPage.Text = "Emotion Dispositions";
            this.emotionDispositionTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(869, 455);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(869, 62);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel10);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(400, 62);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Default Values";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 5;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.comboBoxDefaultDecay, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.comboBoxDefaultThreshold, 4, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 19);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(392, 39);
            this.tableLayoutPanel10.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 11);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "Decay:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(213, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "Threshold:";
            // 
            // comboBoxDefaultDecay
            // 
            this.comboBoxDefaultDecay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDefaultDecay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultDecay.FormattingEnabled = true;
            this.comboBoxDefaultDecay.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDefaultDecay.Location = new System.Drawing.Point(95, 9);
            this.comboBoxDefaultDecay.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDefaultDecay.Name = "comboBoxDefaultDecay";
            this.comboBoxDefaultDecay.Size = new System.Drawing.Size(83, 24);
            this.comboBoxDefaultDecay.TabIndex = 2;
            this.comboBoxDefaultDecay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultDecay_SelectedIndexChanged);
            // 
            // comboBoxDefaultThreshold
            // 
            this.comboBoxDefaultThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDefaultThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultThreshold.FormattingEnabled = true;
            this.comboBoxDefaultThreshold.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDefaultThreshold.Location = new System.Drawing.Point(304, 9);
            this.comboBoxDefaultThreshold.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDefaultThreshold.Name = "comboBoxDefaultThreshold";
            this.comboBoxDefaultThreshold.Size = new System.Drawing.Size(84, 24);
            this.comboBoxDefaultThreshold.TabIndex = 3;
            this.comboBoxDefaultThreshold.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultThreshold_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dataGridViewEmotionDispositions);
            this.groupBox5.Controls.Add(this.buttonEditEmotionDisposition);
            this.groupBox5.Controls.Add(this.buttonAddEmotionDisposition);
            this.groupBox5.Controls.Add(this.buttonRemoveEmotionDisposition);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 62);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(869, 393);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Emotion Dispositions";
            // 
            // dataGridViewEmotionDispositions
            // 
            this.dataGridViewEmotionDispositions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewEmotionDispositions.AllowUserToAddRows = false;
            this.dataGridViewEmotionDispositions.AllowUserToDeleteRows = false;
            this.dataGridViewEmotionDispositions.AllowUserToOrderColumns = true;
            this.dataGridViewEmotionDispositions.AllowUserToResizeRows = false;
            this.dataGridViewEmotionDispositions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmotionDispositions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmotionDispositions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEmotionDispositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmotionDispositions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewEmotionDispositions.Location = new System.Drawing.Point(8, 60);
            this.dataGridViewEmotionDispositions.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewEmotionDispositions.Name = "dataGridViewEmotionDispositions";
            this.dataGridViewEmotionDispositions.ReadOnly = true;
            this.dataGridViewEmotionDispositions.RowHeadersVisible = false;
            this.dataGridViewEmotionDispositions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmotionDispositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmotionDispositions.Size = new System.Drawing.Size(853, 330);
            this.dataGridViewEmotionDispositions.TabIndex = 14;
            this.dataGridViewEmotionDispositions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEmotionDispositions_CellMouseDoubleClick);
            // 
            // buttonEditEmotionDisposition
            // 
            this.buttonEditEmotionDisposition.Location = new System.Drawing.Point(88, 25);
            this.buttonEditEmotionDisposition.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditEmotionDisposition.Name = "buttonEditEmotionDisposition";
            this.buttonEditEmotionDisposition.Size = new System.Drawing.Size(93, 28);
            this.buttonEditEmotionDisposition.TabIndex = 13;
            this.buttonEditEmotionDisposition.Text = "Edit";
            this.buttonEditEmotionDisposition.UseVisualStyleBackColor = true;
            this.buttonEditEmotionDisposition.Click += new System.EventHandler(this.buttonEditEmotionDisposition_Click);
            // 
            // buttonAddEmotionDisposition
            // 
            this.buttonAddEmotionDisposition.Location = new System.Drawing.Point(8, 25);
            this.buttonAddEmotionDisposition.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddEmotionDisposition.Name = "buttonAddEmotionDisposition";
            this.buttonAddEmotionDisposition.Size = new System.Drawing.Size(72, 28);
            this.buttonAddEmotionDisposition.TabIndex = 11;
            this.buttonAddEmotionDisposition.Text = "Add";
            this.buttonAddEmotionDisposition.UseVisualStyleBackColor = true;
            this.buttonAddEmotionDisposition.Click += new System.EventHandler(this.buttonAddEmotionDisposition_Click);
            // 
            // buttonRemoveEmotionDisposition
            // 
            this.buttonRemoveEmotionDisposition.Location = new System.Drawing.Point(189, 25);
            this.buttonRemoveEmotionDisposition.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveEmotionDisposition.Name = "buttonRemoveEmotionDisposition";
            this.buttonRemoveEmotionDisposition.Size = new System.Drawing.Size(93, 28);
            this.buttonRemoveEmotionDisposition.TabIndex = 12;
            this.buttonRemoveEmotionDisposition.Text = "Remove";
            this.buttonRemoveEmotionDisposition.UseVisualStyleBackColor = true;
            this.buttonRemoveEmotionDisposition.Click += new System.EventHandler(this.buttonRemoveEmotionDisposition_Click);
            // 
            // decayErrorProvider
            // 
            this.decayErrorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 540);
            this.Controls.Add(this.dynamicPropertyListing);
            this.EditorName = "Emotional Appraisal Editor";
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(595, 47);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.dynamicPropertyListing, 0);
            this.dynamicPropertyListing.ResumeLayout(false);
            this.appraisalRulesTagePage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).EndInit();
            this.emotionDispositionTabPage.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmotionDispositions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.decayErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emotionListItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainFormBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl dynamicPropertyListing;
        private System.Windows.Forms.TabPage appraisalRulesTagePage;
        private System.Windows.Forms.TabPage emotionDispositionTabPage;
        private System.Windows.Forms.BindingSource mainFormBindingSource;
        private System.Windows.Forms.BindingSource emotionListItemBindingSource;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGridViewEmotionDispositions;
        private System.Windows.Forms.Button buttonEditEmotionDisposition;
        private System.Windows.Forms.Button buttonAddEmotionDisposition;
        private System.Windows.Forms.Button buttonRemoveEmotionDisposition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxDefaultDecay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxDefaultThreshold;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonEditAppraisalRule;
        private System.Windows.Forms.Button buttonAddAppraisalRule;
        private System.Windows.Forms.Button buttonRemoveAppraisalRule;
        private System.Windows.Forms.DataGridView dataGridViewAppraisalRules;
        private System.Windows.Forms.ErrorProvider decayErrorProvider;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox8;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditor;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button buttonDuplicateAppraisalRule;
        private System.Windows.Forms.Button buttonAppVariables;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEditGoal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.DataGridView dataGridViewGoals;
    }
}

