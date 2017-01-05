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
            this.knowledgeBaseTabPage = new System.Windows.Forms.TabPage();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxPerspective = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.editButton = new System.Windows.Forms.Button();
            this.addBeliefButton = new System.Windows.Forms.Button();
            this.removeBeliefButton = new System.Windows.Forms.Button();
            this.dataGridViewBeliefs = new System.Windows.Forms.DataGridView();
            this.appraisalRulesTagePage = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonEditAppraisalRule = new System.Windows.Forms.Button();
            this.buttonAddAppraisalRule = new System.Windows.Forms.Button();
            this.buttonRemoveAppraisalRule = new System.Windows.Forms.Button();
            this.dataGridViewAppraisalRules = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.conditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
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
            this.knowledgeBaseTabPage.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).BeginInit();
            this.appraisalRulesTagePage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).BeginInit();
            this.groupBox8.SuspendLayout();
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
            this.dynamicPropertyListing.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.dynamicPropertyListing.Controls.Add(this.knowledgeBaseTabPage);
            this.dynamicPropertyListing.Controls.Add(this.appraisalRulesTagePage);
            this.dynamicPropertyListing.Controls.Add(this.emotionDispositionTabPage);
            this.dynamicPropertyListing.Location = new System.Drawing.Point(12, 26);
            this.dynamicPropertyListing.Name = "dynamicPropertyListing";
            this.dynamicPropertyListing.SelectedIndex = 0;
            this.dynamicPropertyListing.Size = new System.Drawing.Size(545, 401);
            this.dynamicPropertyListing.TabIndex = 1;
            this.dynamicPropertyListing.SelectedIndexChanged += new System.EventHandler(this.OnScreenChanged);
            // 
            // knowledgeBaseTabPage
            // 
            this.knowledgeBaseTabPage.Controls.Add(this.groupBox11);
            this.knowledgeBaseTabPage.Controls.Add(this.groupBox10);
            this.knowledgeBaseTabPage.Controls.Add(this.groupBox1);
            this.knowledgeBaseTabPage.Location = new System.Drawing.Point(4, 25);
            this.knowledgeBaseTabPage.Name = "knowledgeBaseTabPage";
            this.knowledgeBaseTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.knowledgeBaseTabPage.Size = new System.Drawing.Size(537, 372);
            this.knowledgeBaseTabPage.TabIndex = 1;
            this.knowledgeBaseTabPage.Text = "Knowledge Base";
            this.knowledgeBaseTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox11
            // 
            this.groupBox11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox11.Controls.Add(this.richTextBoxDescription);
            this.groupBox11.Location = new System.Drawing.Point(6, 303);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(519, 63);
            this.groupBox11.TabIndex = 16;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Description";
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxDescription.CausesValidation = false;
            this.richTextBoxDescription.Location = new System.Drawing.Point(9, 19);
            this.richTextBoxDescription.Multiline = false;
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.Size = new System.Drawing.Size(507, 38);
            this.richTextBoxDescription.TabIndex = 0;
            this.richTextBoxDescription.Text = "";
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.tableLayoutPanel11);
            this.groupBox10.Location = new System.Drawing.Point(3, 3);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(525, 54);
            this.groupBox10.TabIndex = 15;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Perspective";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.Controls.Add(this.textBoxPerspective, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(519, 35);
            this.tableLayoutPanel11.TabIndex = 11;
            // 
            // textBoxPerspective
            // 
            this.textBoxPerspective.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPerspective.Location = new System.Drawing.Point(47, 7);
            this.textBoxPerspective.Name = "textBoxPerspective";
            this.textBoxPerspective.Size = new System.Drawing.Size(469, 20);
            this.textBoxPerspective.TabIndex = 10;
            this.textBoxPerspective.TextChanged += new System.EventHandler(this.textBoxPerspective_TextChanged_1);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 237);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Beliefs";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewBeliefs, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(519, 218);
            this.tableLayoutPanel1.TabIndex = 7;
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(513, 31);
            this.tableLayoutPanel2.TabIndex = 3;
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
            // dataGridViewBeliefs
            // 
            this.dataGridViewBeliefs.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewBeliefs.AllowUserToAddRows = false;
            this.dataGridViewBeliefs.AllowUserToDeleteRows = false;
            this.dataGridViewBeliefs.AllowUserToOrderColumns = true;
            this.dataGridViewBeliefs.AllowUserToResizeRows = false;
            this.dataGridViewBeliefs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewBeliefs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBeliefs.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewBeliefs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBeliefs.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewBeliefs.Location = new System.Drawing.Point(3, 38);
            this.dataGridViewBeliefs.Name = "dataGridViewBeliefs";
            this.dataGridViewBeliefs.ReadOnly = true;
            this.dataGridViewBeliefs.RowHeadersVisible = false;
            this.dataGridViewBeliefs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBeliefs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBeliefs.Size = new System.Drawing.Size(513, 177);
            this.dataGridViewBeliefs.TabIndex = 4;
            this.dataGridViewBeliefs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBeliefs_CellContentClick);
            this.dataGridViewBeliefs.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewBeliefs_CellMouseDoubleClick);
            // 
            // appraisalRulesTagePage
            // 
            this.appraisalRulesTagePage.Controls.Add(this.splitContainer1);
            this.appraisalRulesTagePage.Location = new System.Drawing.Point(4, 25);
            this.appraisalRulesTagePage.Name = "appraisalRulesTagePage";
            this.appraisalRulesTagePage.Padding = new System.Windows.Forms.Padding(3);
            this.appraisalRulesTagePage.Size = new System.Drawing.Size(537, 372);
            this.appraisalRulesTagePage.TabIndex = 3;
            this.appraisalRulesTagePage.Text = "Appraisal Rules";
            this.appraisalRulesTagePage.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
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
            this.splitContainer1.Size = new System.Drawing.Size(531, 366);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 11;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonEditAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonAddAppraisalRule);
            this.groupBox7.Controls.Add(this.buttonRemoveAppraisalRule);
            this.groupBox7.Controls.Add(this.dataGridViewAppraisalRules);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(0, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(531, 183);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Rules";
            // 
            // buttonEditAppraisalRule
            // 
            this.buttonEditAppraisalRule.Location = new System.Drawing.Point(66, 19);
            this.buttonEditAppraisalRule.Name = "buttonEditAppraisalRule";
            this.buttonEditAppraisalRule.Size = new System.Drawing.Size(70, 23);
            this.buttonEditAppraisalRule.TabIndex = 9;
            this.buttonEditAppraisalRule.Text = "Edit";
            this.buttonEditAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonEditAppraisalRule.Click += new System.EventHandler(this.buttonEditAppraisalRule_Click);
            // 
            // buttonAddAppraisalRule
            // 
            this.buttonAddAppraisalRule.Location = new System.Drawing.Point(6, 19);
            this.buttonAddAppraisalRule.Name = "buttonAddAppraisalRule";
            this.buttonAddAppraisalRule.Size = new System.Drawing.Size(54, 23);
            this.buttonAddAppraisalRule.TabIndex = 7;
            this.buttonAddAppraisalRule.Text = "Add";
            this.buttonAddAppraisalRule.UseVisualStyleBackColor = true;
            this.buttonAddAppraisalRule.Click += new System.EventHandler(this.buttonAddAppraisalRule_Click);
            // 
            // buttonRemoveAppraisalRule
            // 
            this.buttonRemoveAppraisalRule.Location = new System.Drawing.Point(142, 19);
            this.buttonRemoveAppraisalRule.Name = "buttonRemoveAppraisalRule";
            this.buttonRemoveAppraisalRule.Size = new System.Drawing.Size(70, 23);
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
            this.dataGridViewAppraisalRules.Location = new System.Drawing.Point(6, 54);
            this.dataGridViewAppraisalRules.Name = "dataGridViewAppraisalRules";
            this.dataGridViewAppraisalRules.ReadOnly = true;
            this.dataGridViewAppraisalRules.RowHeadersVisible = false;
            this.dataGridViewAppraisalRules.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAppraisalRules.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAppraisalRules.Size = new System.Drawing.Size(519, 123);
            this.dataGridViewAppraisalRules.TabIndex = 2;
            this.dataGridViewAppraisalRules.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewAppraisalRules_CellMouseDoubleClick);
            this.dataGridViewAppraisalRules.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAppraisalRules_RowEnter);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.conditionSetEditor);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox8.Location = new System.Drawing.Point(0, 0);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(531, 179);
            this.groupBox8.TabIndex = 10;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Rule Conditions";
            // 
            // conditionSetEditor
            // 
            this.conditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionSetEditor.Location = new System.Drawing.Point(3, 16);
            this.conditionSetEditor.Name = "conditionSetEditor";
            this.conditionSetEditor.Size = new System.Drawing.Size(525, 160);
            this.conditionSetEditor.TabIndex = 0;
            this.conditionSetEditor.View = null;
            // 
            // emotionDispositionTabPage
            // 
            this.emotionDispositionTabPage.Controls.Add(this.tableLayoutPanel8);
            this.emotionDispositionTabPage.Location = new System.Drawing.Point(4, 25);
            this.emotionDispositionTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.emotionDispositionTabPage.Name = "emotionDispositionTabPage";
            this.emotionDispositionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.emotionDispositionTabPage.Size = new System.Drawing.Size(537, 372);
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
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 2;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(531, 366);
            this.tableLayoutPanel8.TabIndex = 2;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(531, 50);
            this.tableLayoutPanel9.TabIndex = 4;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.tableLayoutPanel10);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(300, 50);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Default Values";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 5;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel10.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.comboBoxDefaultDecay, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.comboBoxDefaultThreshold, 4, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel10.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(294, 31);
            this.tableLayoutPanel10.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Decay:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(159, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
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
            this.comboBoxDefaultDecay.Location = new System.Drawing.Point(71, 5);
            this.comboBoxDefaultDecay.Name = "comboBoxDefaultDecay";
            this.comboBoxDefaultDecay.Size = new System.Drawing.Size(62, 21);
            this.comboBoxDefaultDecay.TabIndex = 2;
            this.comboBoxDefaultDecay.SelectedIndexChanged += new System.EventHandler(this.comboBoxDefaultDecay_SelectedIndexChanged);
            // 
            // comboBoxDefaultThreshold
            // 
            this.comboBoxDefaultThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDefaultThreshold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDefaultThreshold.FormattingEnabled = true;
            this.comboBoxDefaultThreshold.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxDefaultThreshold.Location = new System.Drawing.Point(227, 5);
            this.comboBoxDefaultThreshold.Name = "comboBoxDefaultThreshold";
            this.comboBoxDefaultThreshold.Size = new System.Drawing.Size(64, 21);
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
            this.groupBox5.Location = new System.Drawing.Point(0, 50);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(531, 316);
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
            this.dataGridViewEmotionDispositions.Location = new System.Drawing.Point(6, 49);
            this.dataGridViewEmotionDispositions.Name = "dataGridViewEmotionDispositions";
            this.dataGridViewEmotionDispositions.ReadOnly = true;
            this.dataGridViewEmotionDispositions.RowHeadersVisible = false;
            this.dataGridViewEmotionDispositions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmotionDispositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmotionDispositions.Size = new System.Drawing.Size(519, 262);
            this.dataGridViewEmotionDispositions.TabIndex = 14;
            this.dataGridViewEmotionDispositions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewEmotionDispositions_CellMouseDoubleClick);
            // 
            // buttonEditEmotionDisposition
            // 
            this.buttonEditEmotionDisposition.Location = new System.Drawing.Point(66, 20);
            this.buttonEditEmotionDisposition.Name = "buttonEditEmotionDisposition";
            this.buttonEditEmotionDisposition.Size = new System.Drawing.Size(70, 23);
            this.buttonEditEmotionDisposition.TabIndex = 13;
            this.buttonEditEmotionDisposition.Text = "Edit";
            this.buttonEditEmotionDisposition.UseVisualStyleBackColor = true;
            this.buttonEditEmotionDisposition.Click += new System.EventHandler(this.buttonEditEmotionDisposition_Click);
            // 
            // buttonAddEmotionDisposition
            // 
            this.buttonAddEmotionDisposition.Location = new System.Drawing.Point(6, 20);
            this.buttonAddEmotionDisposition.Name = "buttonAddEmotionDisposition";
            this.buttonAddEmotionDisposition.Size = new System.Drawing.Size(54, 23);
            this.buttonAddEmotionDisposition.TabIndex = 11;
            this.buttonAddEmotionDisposition.Text = "Add";
            this.buttonAddEmotionDisposition.UseVisualStyleBackColor = true;
            this.buttonAddEmotionDisposition.Click += new System.EventHandler(this.buttonAddEmotionDisposition_Click);
            // 
            // buttonRemoveEmotionDisposition
            // 
            this.buttonRemoveEmotionDisposition.Location = new System.Drawing.Point(142, 20);
            this.buttonRemoveEmotionDisposition.Name = "buttonRemoveEmotionDisposition";
            this.buttonRemoveEmotionDisposition.Size = new System.Drawing.Size(70, 23);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 440);
            this.Controls.Add(this.dynamicPropertyListing);
            this.EditorName = "Emotional Appraisal Editor";
            this.MinimumSize = new System.Drawing.Size(450, 39);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.Controls.SetChildIndex(this.dynamicPropertyListing, 0);
            this.dynamicPropertyListing.ResumeLayout(false);
            this.knowledgeBaseTabPage.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).EndInit();
            this.appraisalRulesTagePage.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAppraisalRules)).EndInit();
            this.groupBox8.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage knowledgeBaseTabPage;
        private System.Windows.Forms.TabPage appraisalRulesTagePage;
        private System.Windows.Forms.Button addBeliefButton;
        private System.Windows.Forms.Button removeBeliefButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button editButton;
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
        private System.Windows.Forms.DataGridView dataGridViewBeliefs;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox groupBox8;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl conditionSetEditor;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TextBox textBoxPerspective;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
    }
}

