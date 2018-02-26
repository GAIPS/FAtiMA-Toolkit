namespace IntegratedAuthoringToolWF
{
    partial class MainForm
    {
      

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBoxScenarioName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewCharacters = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonCreateCharacter = new System.Windows.Forms.Button();
            this.buttonAddCharacter = new System.Windows.Forms.Button();
            this.buttonRemoveCharacter = new System.Windows.Forms.Button();
            this.buttonInspect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxScenarioDescription = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlIAT = new System.Windows.Forms.TabControl();
            this.tabPageDialogue = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewDialogueActions = new System.Windows.Forms.DataGridView();
            this.buttonPlayerDuplicateDialogueAction = new System.Windows.Forms.Button();
            this.buttonPlayerEditDialogueAction = new System.Windows.Forms.Button();
            this.buttonAddPlayerDialogueAction = new System.Windows.Forms.Button();
            this.buttonPlayerRemoveDialogueAction = new System.Windows.Forms.Button();
            this.buttonValidate = new System.Windows.Forms.Button();
            this.buttonTTS = new System.Windows.Forms.Button();
            this.buttonImportTxt = new System.Windows.Forms.Button();
            this.buttonImportExcel = new System.Windows.Forms.Button();
            this.buttonExportExcel = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxValChat = new System.Windows.Forms.TextBox();
            this.textBoxBelChat = new System.Windows.Forms.TextBox();
            this.comboBoxAgChat = new System.Windows.Forms.ComboBox();
            this.buttonContinue = new System.Windows.Forms.Button();
            this.textBoxTick = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.listBoxPlayerDialogues = new System.Windows.Forms.ListBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.openEAButton = new System.Windows.Forms.Button();
            this.clearEAButton = new System.Windows.Forms.Button();
            this.pathTextBoxEA = new System.Windows.Forms.TextBox();
            this.createNewEAButton = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharacters)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlIAT.SuspendLayout();
            this.tabPageDialogue.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDialogueActions)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxScenarioName
            // 
            this.textBoxScenarioName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScenarioName.Location = new System.Drawing.Point(93, 20);
            this.textBoxScenarioName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxScenarioName.Name = "textBoxScenarioName";
            this.textBoxScenarioName.Size = new System.Drawing.Size(351, 26);
            this.textBoxScenarioName.TabIndex = 0;
            this.textBoxScenarioName.TextChanged += new System.EventHandler(this.textBoxScenarioName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Scenario:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(19, 272);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(425, 413);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 23);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 386);
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
            this.dataGridViewCharacters.Location = new System.Drawing.Point(4, 50);
            this.dataGridViewCharacters.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewCharacters.Name = "dataGridViewCharacters";
            this.dataGridViewCharacters.ReadOnly = true;
            this.dataGridViewCharacters.RowHeadersVisible = false;
            this.dataGridViewCharacters.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCharacters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCharacters.Size = new System.Drawing.Size(409, 332);
            this.dataGridViewCharacters.TabIndex = 13;
            this.dataGridViewCharacters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCharacters_CellContentClick);
            this.dataGridViewCharacters.SelectionChanged += new System.EventHandler(this.dataGridViewCharacters_SelectionChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.buttonCreateCharacter);
            this.flowLayoutPanel1.Controls.Add(this.buttonAddCharacter);
            this.flowLayoutPanel1.Controls.Add(this.buttonRemoveCharacter);
            this.flowLayoutPanel1.Controls.Add(this.buttonInspect);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(409, 38);
            this.flowLayoutPanel1.TabIndex = 14;
            // 
            // buttonCreateCharacter
            // 
            this.buttonCreateCharacter.Location = new System.Drawing.Point(4, 4);
            this.buttonCreateCharacter.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCreateCharacter.Name = "buttonCreateCharacter";
            this.buttonCreateCharacter.Size = new System.Drawing.Size(93, 30);
            this.buttonCreateCharacter.TabIndex = 2;
            this.buttonCreateCharacter.Text = "Create";
            this.buttonCreateCharacter.UseVisualStyleBackColor = true;
            this.buttonCreateCharacter.Click += new System.EventHandler(this.buttonCreateCharacter_Click);
            // 
            // buttonAddCharacter
            // 
            this.buttonAddCharacter.Location = new System.Drawing.Point(105, 4);
            this.buttonAddCharacter.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddCharacter.Name = "buttonAddCharacter";
            this.buttonAddCharacter.Size = new System.Drawing.Size(93, 30);
            this.buttonAddCharacter.TabIndex = 3;
            this.buttonAddCharacter.Text = "Add";
            this.buttonAddCharacter.UseVisualStyleBackColor = true;
            this.buttonAddCharacter.Click += new System.EventHandler(this.buttonAddCharacter_Click);
            // 
            // buttonRemoveCharacter
            // 
            this.buttonRemoveCharacter.Location = new System.Drawing.Point(206, 4);
            this.buttonRemoveCharacter.Margin = new System.Windows.Forms.Padding(4);
            this.buttonRemoveCharacter.Name = "buttonRemoveCharacter";
            this.buttonRemoveCharacter.Size = new System.Drawing.Size(93, 30);
            this.buttonRemoveCharacter.TabIndex = 4;
            this.buttonRemoveCharacter.Text = "Remove";
            this.buttonRemoveCharacter.UseVisualStyleBackColor = true;
            this.buttonRemoveCharacter.Click += new System.EventHandler(this.buttonRemoveCharacter_Click);
            // 
            // buttonInspect
            // 
            this.buttonInspect.Location = new System.Drawing.Point(307, 4);
            this.buttonInspect.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInspect.Name = "buttonInspect";
            this.buttonInspect.Size = new System.Drawing.Size(93, 30);
            this.buttonInspect.TabIndex = 5;
            this.buttonInspect.Text = "Inspect";
            this.buttonInspect.UseVisualStyleBackColor = true;
            this.buttonInspect.Click += new System.EventHandler(this.buttonInspect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Description:";
            // 
            // textBoxScenarioDescription
            // 
            this.textBoxScenarioDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScenarioDescription.Location = new System.Drawing.Point(20, 101);
            this.textBoxScenarioDescription.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxScenarioDescription.Multiline = true;
            this.textBoxScenarioDescription.Name = "textBoxScenarioDescription";
            this.textBoxScenarioDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxScenarioDescription.Size = new System.Drawing.Size(424, 163);
            this.textBoxScenarioDescription.TabIndex = 1;
            this.textBoxScenarioDescription.TextChanged += new System.EventHandler(this.textBoxScenarioDescription_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBoxScenarioName);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxScenarioDescription);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1MinSize = 466;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlIAT);
            this.splitContainer1.Size = new System.Drawing.Size(1304, 685);
            this.splitContainer1.SplitterDistance = 466;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 5;
            // 
            // tabControlIAT
            // 
            this.tabControlIAT.Controls.Add(this.tabPageDialogue);
            this.tabControlIAT.Controls.Add(this.tabPage2);
            this.tabControlIAT.Controls.Add(this.tabPage1);
            this.tabControlIAT.Controls.Add(this.tabPage3);
            this.tabControlIAT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlIAT.Location = new System.Drawing.Point(0, 0);
            this.tabControlIAT.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlIAT.Name = "tabControlIAT";
            this.tabControlIAT.SelectedIndex = 0;
            this.tabControlIAT.Size = new System.Drawing.Size(833, 685);
            this.tabControlIAT.TabIndex = 20;
            this.tabControlIAT.TabStop = false;
            // 
            // tabPageDialogue
            // 
            this.tabPageDialogue.Controls.Add(this.groupBox2);
            this.tabPageDialogue.Controls.Add(this.buttonValidate);
            this.tabPageDialogue.Controls.Add(this.buttonTTS);
            this.tabPageDialogue.Controls.Add(this.buttonImportTxt);
            this.tabPageDialogue.Controls.Add(this.buttonImportExcel);
            this.tabPageDialogue.Controls.Add(this.buttonExportExcel);
            this.tabPageDialogue.Location = new System.Drawing.Point(4, 27);
            this.tabPageDialogue.Margin = new System.Windows.Forms.Padding(5);
            this.tabPageDialogue.Name = "tabPageDialogue";
            this.tabPageDialogue.Padding = new System.Windows.Forms.Padding(5);
            this.tabPageDialogue.Size = new System.Drawing.Size(825, 654);
            this.tabPageDialogue.TabIndex = 0;
            this.tabPageDialogue.Text = "Dialogue Editor";
            this.tabPageDialogue.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridViewDialogueActions);
            this.groupBox2.Controls.Add(this.buttonPlayerDuplicateDialogueAction);
            this.groupBox2.Controls.Add(this.buttonPlayerEditDialogueAction);
            this.groupBox2.Controls.Add(this.buttonAddPlayerDialogueAction);
            this.groupBox2.Controls.Add(this.buttonPlayerRemoveDialogueAction);
            this.groupBox2.Location = new System.Drawing.Point(8, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(808, 597);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dialogue Actions";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // dataGridViewDialogueActions
            // 
            this.dataGridViewDialogueActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewDialogueActions.AllowUserToAddRows = false;
            this.dataGridViewDialogueActions.AllowUserToDeleteRows = false;
            this.dataGridViewDialogueActions.AllowUserToOrderColumns = true;
            this.dataGridViewDialogueActions.AllowUserToResizeRows = false;
            this.dataGridViewDialogueActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDialogueActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDialogueActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewDialogueActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDialogueActions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridViewDialogueActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewDialogueActions.Location = new System.Drawing.Point(8, 76);
            this.dataGridViewDialogueActions.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewDialogueActions.Name = "dataGridViewDialogueActions";
            this.dataGridViewDialogueActions.ReadOnly = true;
            this.dataGridViewDialogueActions.RowHeadersVisible = false;
            this.dataGridViewDialogueActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDialogueActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDialogueActions.Size = new System.Drawing.Size(800, 513);
            this.dataGridViewDialogueActions.TabIndex = 14;
            this.dataGridViewDialogueActions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDialogueActions_CellContentClick);
            this.dataGridViewDialogueActions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDialogueActions_CellContentDoubleClick);
            this.dataGridViewDialogueActions.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewDialogueActions_CellMouseDoubleClick);
            this.dataGridViewDialogueActions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewDialogueActions_KeyDown);
            this.dataGridViewDialogueActions.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridViewDialogueActions_KeyPress);
            // 
            // buttonPlayerDuplicateDialogueAction
            // 
            this.buttonPlayerDuplicateDialogueAction.Location = new System.Drawing.Point(168, 25);
            this.buttonPlayerDuplicateDialogueAction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlayerDuplicateDialogueAction.Name = "buttonPlayerDuplicateDialogueAction";
            this.buttonPlayerDuplicateDialogueAction.Size = new System.Drawing.Size(93, 30);
            this.buttonPlayerDuplicateDialogueAction.TabIndex = 7;
            this.buttonPlayerDuplicateDialogueAction.Text = "Duplicate";
            this.buttonPlayerDuplicateDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerDuplicateDialogueAction.Click += new System.EventHandler(this.buttonDuplicateDialogueAction_Click);
            // 
            // buttonPlayerEditDialogueAction
            // 
            this.buttonPlayerEditDialogueAction.Location = new System.Drawing.Point(88, 25);
            this.buttonPlayerEditDialogueAction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlayerEditDialogueAction.Name = "buttonPlayerEditDialogueAction";
            this.buttonPlayerEditDialogueAction.Size = new System.Drawing.Size(72, 30);
            this.buttonPlayerEditDialogueAction.TabIndex = 6;
            this.buttonPlayerEditDialogueAction.Text = "Edit";
            this.buttonPlayerEditDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerEditDialogueAction.Click += new System.EventHandler(this.buttonEditDialogueAction_Click);
            // 
            // buttonAddPlayerDialogueAction
            // 
            this.buttonAddPlayerDialogueAction.Location = new System.Drawing.Point(8, 25);
            this.buttonAddPlayerDialogueAction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonAddPlayerDialogueAction.Name = "buttonAddPlayerDialogueAction";
            this.buttonAddPlayerDialogueAction.Size = new System.Drawing.Size(72, 30);
            this.buttonAddPlayerDialogueAction.TabIndex = 5;
            this.buttonAddPlayerDialogueAction.Text = "Add";
            this.buttonAddPlayerDialogueAction.UseVisualStyleBackColor = true;
            this.buttonAddPlayerDialogueAction.Click += new System.EventHandler(this.buttonAddDialogueAction_Click_1);
            // 
            // buttonPlayerRemoveDialogueAction
            // 
            this.buttonPlayerRemoveDialogueAction.Location = new System.Drawing.Point(269, 25);
            this.buttonPlayerRemoveDialogueAction.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlayerRemoveDialogueAction.Name = "buttonPlayerRemoveDialogueAction";
            this.buttonPlayerRemoveDialogueAction.Size = new System.Drawing.Size(93, 30);
            this.buttonPlayerRemoveDialogueAction.TabIndex = 8;
            this.buttonPlayerRemoveDialogueAction.Text = "Remove";
            this.buttonPlayerRemoveDialogueAction.UseVisualStyleBackColor = true;
            this.buttonPlayerRemoveDialogueAction.Click += new System.EventHandler(this.buttonRemoveDialogueAction_Click);
            // 
            // buttonValidate
            // 
            this.buttonValidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonValidate.Location = new System.Drawing.Point(514, 613);
            this.buttonValidate.Margin = new System.Windows.Forms.Padding(4);
            this.buttonValidate.Name = "buttonValidate";
            this.buttonValidate.Size = new System.Drawing.Size(143, 30);
            this.buttonValidate.TabIndex = 19;
            this.buttonValidate.Text = "Validate";
            this.buttonValidate.UseVisualStyleBackColor = true;
            this.buttonValidate.Click += new System.EventHandler(this.buttonValidate_Click);
            // 
            // buttonTTS
            // 
            this.buttonTTS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTTS.Location = new System.Drawing.Point(355, 613);
            this.buttonTTS.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTTS.Name = "buttonTTS";
            this.buttonTTS.Size = new System.Drawing.Size(151, 30);
            this.buttonTTS.TabIndex = 18;
            this.buttonTTS.Text = "Text-To-Speech";
            this.buttonTTS.UseVisualStyleBackColor = true;
            this.buttonTTS.Click += new System.EventHandler(this.buttonTTS_Click);
            // 
            // buttonImportTxt
            // 
            this.buttonImportTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImportTxt.Location = new System.Drawing.Point(232, 613);
            this.buttonImportTxt.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImportTxt.Name = "buttonImportTxt";
            this.buttonImportTxt.Size = new System.Drawing.Size(115, 30);
            this.buttonImportTxt.TabIndex = 17;
            this.buttonImportTxt.Text = "Import Txt";
            this.buttonImportTxt.UseVisualStyleBackColor = true;
            this.buttonImportTxt.Click += new System.EventHandler(this.buttonImportTxt_Click);
            // 
            // buttonImportExcel
            // 
            this.buttonImportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImportExcel.Location = new System.Drawing.Point(8, 613);
            this.buttonImportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonImportExcel.Name = "buttonImportExcel";
            this.buttonImportExcel.Size = new System.Drawing.Size(101, 30);
            this.buttonImportExcel.TabIndex = 15;
            this.buttonImportExcel.Text = "Import Excel";
            this.buttonImportExcel.UseVisualStyleBackColor = true;
            this.buttonImportExcel.Click += new System.EventHandler(this.buttonImportExcel_Click);
            // 
            // buttonExportExcel
            // 
            this.buttonExportExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExportExcel.Location = new System.Drawing.Point(117, 613);
            this.buttonExportExcel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExportExcel.Name = "buttonExportExcel";
            this.buttonExportExcel.Size = new System.Drawing.Size(107, 30);
            this.buttonExportExcel.TabIndex = 16;
            this.buttonExportExcel.Text = "Export Excel";
            this.buttonExportExcel.UseVisualStyleBackColor = true;
            this.buttonExportExcel.Click += new System.EventHandler(this.buttonExportExcel_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.tabPage2.Size = new System.Drawing.Size(825, 654);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Role Play Character Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(825, 654);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Simulator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.buttonContinue);
            this.groupBox3.Controls.Add(this.textBoxTick);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.buttonStart);
            this.groupBox3.Controls.Add(this.richTextBoxChat);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(819, 648);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chat Simulator";
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textBoxValChat);
            this.groupBox5.Controls.Add(this.textBoxBelChat);
            this.groupBox5.Controls.Add(this.comboBoxAgChat);
            this.groupBox5.Location = new System.Drawing.Point(6, 383);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(804, 58);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Belief Inspector";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(588, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 19);
            this.label6.TabIndex = 5;
            this.label6.Text = "Val:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(225, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Bel:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ag:";
            // 
            // textBoxValChat
            // 
            this.textBoxValChat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValChat.Location = new System.Drawing.Point(624, 25);
            this.textBoxValChat.Name = "textBoxValChat";
            this.textBoxValChat.ReadOnly = true;
            this.textBoxValChat.Size = new System.Drawing.Size(174, 26);
            this.textBoxValChat.TabIndex = 2;
            // 
            // textBoxBelChat
            // 
            this.textBoxBelChat.Location = new System.Drawing.Point(265, 25);
            this.textBoxBelChat.Name = "textBoxBelChat";
            this.textBoxBelChat.Size = new System.Drawing.Size(317, 26);
            this.textBoxBelChat.TabIndex = 1;
            this.textBoxBelChat.TextChanged += new System.EventHandler(this.textBoxBelChat_TextChanged);
            // 
            // comboBoxAgChat
            // 
            this.comboBoxAgChat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAgChat.FormattingEnabled = true;
            this.comboBoxAgChat.Location = new System.Drawing.Point(43, 24);
            this.comboBoxAgChat.Name = "comboBoxAgChat";
            this.comboBoxAgChat.Size = new System.Drawing.Size(173, 26);
            this.comboBoxAgChat.TabIndex = 0;
            this.comboBoxAgChat.SelectedValueChanged += new System.EventHandler(this.comboBoxAgChat_SelectedValueChanged);
            // 
            // buttonContinue
            // 
            this.buttonContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonContinue.Enabled = false;
            this.buttonContinue.Location = new System.Drawing.Point(247, 606);
            this.buttonContinue.Name = "buttonContinue";
            this.buttonContinue.Size = new System.Drawing.Size(93, 30);
            this.buttonContinue.TabIndex = 6;
            this.buttonContinue.Text = "Continue";
            this.buttonContinue.UseVisualStyleBackColor = true;
            this.buttonContinue.Click += new System.EventHandler(this.buttonContinue_Click);
            // 
            // textBoxTick
            // 
            this.textBoxTick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxTick.Location = new System.Drawing.Point(160, 610);
            this.textBoxTick.Name = "textBoxTick";
            this.textBoxTick.ReadOnly = true;
            this.textBoxTick.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTick.Size = new System.Drawing.Size(61, 26);
            this.textBoxTick.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 613);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tick:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.listBoxPlayerDialogues);
            this.groupBox4.Location = new System.Drawing.Point(6, 447);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(804, 139);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Select Dialogue:";
            // 
            // listBoxPlayerDialogues
            // 
            this.listBoxPlayerDialogues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPlayerDialogues.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxPlayerDialogues.FormattingEnabled = true;
            this.listBoxPlayerDialogues.ItemHeight = 22;
            this.listBoxPlayerDialogues.Location = new System.Drawing.Point(3, 18);
            this.listBoxPlayerDialogues.Name = "listBoxPlayerDialogues";
            this.listBoxPlayerDialogues.Size = new System.Drawing.Size(798, 92);
            this.listBoxPlayerDialogues.TabIndex = 2;
            this.listBoxPlayerDialogues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxPlayerDialogues_MouseDoubleClick);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(12, 606);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(93, 30);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxChat.BackColor = System.Drawing.Color.White;
            this.richTextBoxChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxChat.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxChat.Location = new System.Drawing.Point(9, 21);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.Size = new System.Drawing.Size(801, 356);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.Text = "";
            this.richTextBoxChat.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox7);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(825, 654);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "World Model";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.openEAButton);
            this.groupBox6.Controls.Add(this.clearEAButton);
            this.groupBox6.Controls.Add(this.pathTextBoxEA);
            this.groupBox6.Controls.Add(this.createNewEAButton);
            this.groupBox6.Location = new System.Drawing.Point(3, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(813, 98);
            this.groupBox6.TabIndex = 15;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Source";
            // 
            // openEAButton
            // 
            this.openEAButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.openEAButton.Location = new System.Drawing.Point(103, 39);
            this.openEAButton.Name = "openEAButton";
            this.openEAButton.Size = new System.Drawing.Size(85, 34);
            this.openEAButton.TabIndex = 15;
            this.openEAButton.Text = "Open";
            this.openEAButton.UseVisualStyleBackColor = true;
            this.openEAButton.Click += new System.EventHandler(this.openEAButton_Click);
            // 
            // clearEAButton
            // 
            this.clearEAButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.clearEAButton.Location = new System.Drawing.Point(202, 39);
            this.clearEAButton.Name = "clearEAButton";
            this.clearEAButton.Size = new System.Drawing.Size(85, 34);
            this.clearEAButton.TabIndex = 16;
            this.clearEAButton.Text = "Clear";
            this.clearEAButton.UseVisualStyleBackColor = true;
            // 
            // pathTextBoxEA
            // 
            this.pathTextBoxEA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBoxEA.Location = new System.Drawing.Point(293, 44);
            this.pathTextBoxEA.Name = "pathTextBoxEA";
            this.pathTextBoxEA.ReadOnly = true;
            this.pathTextBoxEA.Size = new System.Drawing.Size(508, 26);
            this.pathTextBoxEA.TabIndex = 13;
            // 
            // createNewEAButton
            // 
            this.createNewEAButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.createNewEAButton.Location = new System.Drawing.Point(6, 39);
            this.createNewEAButton.Name = "createNewEAButton";
            this.createNewEAButton.Size = new System.Drawing.Size(85, 34);
            this.createNewEAButton.TabIndex = 18;
            this.createNewEAButton.Text = "New";
            this.createNewEAButton.UseVisualStyleBackColor = true;
            this.createNewEAButton.Click += new System.EventHandler(this.createNewEAButton_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Location = new System.Drawing.Point(1, 107);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(815, 544);
            this.groupBox7.TabIndex = 16;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "World Model Asset";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1304, 713);
            this.Controls.Add(this.splitContainer1);
            this.EditorName = "Integrated Authoring Tool";
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1214, 549);
            this.Name = "MainForm";
            this.Text = "Integrated Authoring Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCharacters)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlIAT.ResumeLayout(false);
            this.tabPageDialogue.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDialogueActions)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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

        private System.Windows.Forms.TextBox textBoxScenarioName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddCharacter;
        private System.Windows.Forms.Button buttonRemoveCharacter;
        private System.Windows.Forms.DataGridView dataGridViewCharacters;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonCreateCharacter;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxScenarioDescription;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonImportExcel;
        private System.Windows.Forms.Button buttonTTS;
        private System.Windows.Forms.Button buttonExportExcel;
        private System.Windows.Forms.Button buttonValidate;
        private System.Windows.Forms.Button buttonImportTxt;
        private System.Windows.Forms.TabControl tabControlIAT;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPageDialogue;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonPlayerDuplicateDialogueAction;
        private System.Windows.Forms.Button buttonPlayerEditDialogueAction;
        private System.Windows.Forms.DataGridView dataGridViewDialogueActions;
        private System.Windows.Forms.Button buttonAddPlayerDialogueAction;
        private System.Windows.Forms.Button buttonPlayerRemoveDialogueAction;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonInspect;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.ListBox listBoxPlayerDialogues;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxTick;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonContinue;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxValChat;
        private System.Windows.Forms.TextBox textBoxBelChat;
        private System.Windows.Forms.ComboBox comboBoxAgChat;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button openEAButton;
        private System.Windows.Forms.Button clearEAButton;
        private System.Windows.Forms.TextBox pathTextBoxEA;
        private System.Windows.Forms.Button createNewEAButton;
        private System.Windows.Forms.GroupBox groupBox7;
    }
}

