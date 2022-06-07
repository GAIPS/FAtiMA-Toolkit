﻿
namespace IntegratedAuthoringToolWF.IEP
{
    partial class IEPOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IEPOutputForm));
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.OutputInformation = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.rPCConstsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.scenarioTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CharacterTab = new System.Windows.Forms.TabControl();
            this.Characters = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewGoals = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewBeliefs = new System.Windows.Forms.DataGridView();
            this.internalCharacterView = new System.Windows.Forms.DataGridView();
            this.CogRules = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridViewEmotions = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewReactiveActions = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.rPCConstsBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.CharacterTab.SuspendLayout();
            this.Characters.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCharacterView)).BeginInit();
            this.CogRules.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmotions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).BeginInit();
            this.SuspendLayout();
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // OutputInformation
            // 
            this.OutputInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputInformation.Location = new System.Drawing.Point(3, 16);
            this.OutputInformation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OutputInformation.Name = "OutputInformation";
            this.OutputInformation.Size = new System.Drawing.Size(893, 38);
            this.OutputInformation.TabIndex = 19;
            this.OutputInformation.Text = "The resulting output of the Information Extraction Pipeline can be seen below.  T" +
    "he output is sepperated by \"special symbols\" that we then use to efficiently par" +
    "se the information.\r\n";
            this.OutputInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.OutputInformation.Click += new System.EventHandler(this.OutputInformation_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::IntegratedAuthoringToolWF.Properties.Resources.icons8_system_information_40;
            this.button1.Location = new System.Drawing.Point(382, 374);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 43);
            this.button1.TabIndex = 1;
            this.button1.Text = "    Add to current Scenario";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rPCConstsBindingSource
            // 
            this.rPCConstsBindingSource.DataSource = typeof(RolePlayCharacter.RPCConsts);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.OutputInformation);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(908, 94);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(781, 30);
            this.label1.TabIndex = 20;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(5, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(908, 356);
            this.splitContainer1.SplitterDistance = 94;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer2.Size = new System.Drawing.Size(908, 258);
            this.splitContainer2.SplitterDistance = 324;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.scenarioTextBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 258);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output (edit this to your liking)";
            // 
            // scenarioTextBox
            // 
            this.scenarioTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scenarioTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.scenarioTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scenarioTextBox.Location = new System.Drawing.Point(6, 18);
            this.scenarioTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scenarioTextBox.Name = "scenarioTextBox";
            this.scenarioTextBox.Size = new System.Drawing.Size(309, 230);
            this.scenarioTextBox.TabIndex = 2;
            this.scenarioTextBox.Text = "";
            this.scenarioTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CharacterTab);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 258);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Result";
            // 
            // CharacterTab
            // 
            this.CharacterTab.Controls.Add(this.Characters);
            this.CharacterTab.Controls.Add(this.CogRules);
            this.CharacterTab.Controls.Add(this.tabPage1);
            this.CharacterTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharacterTab.Location = new System.Drawing.Point(3, 16);
            this.CharacterTab.Name = "CharacterTab";
            this.CharacterTab.SelectedIndex = 0;
            this.CharacterTab.Size = new System.Drawing.Size(574, 239);
            this.CharacterTab.TabIndex = 18;
            // 
            // Characters
            // 
            this.Characters.Controls.Add(this.groupBox4);
            this.Characters.Location = new System.Drawing.Point(4, 22);
            this.Characters.Name = "Characters";
            this.Characters.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Characters.Size = new System.Drawing.Size(566, 213);
            this.Characters.TabIndex = 0;
            this.Characters.Text = "Characters";
            this.Characters.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.dataGridViewGoals);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dataGridViewBeliefs);
            this.groupBox4.Controls.Add(this.internalCharacterView);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(560, 207);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Characters";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
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
            this.dataGridViewGoals.Location = new System.Drawing.Point(6, 141);
            this.dataGridViewGoals.Name = "dataGridViewGoals";
            this.dataGridViewGoals.ReadOnly = true;
            this.dataGridViewGoals.RowHeadersVisible = false;
            this.dataGridViewGoals.RowHeadersWidth = 51;
            this.dataGridViewGoals.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewGoals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGoals.Size = new System.Drawing.Size(209, 59);
            this.dataGridViewGoals.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
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
            this.dataGridViewBeliefs.Location = new System.Drawing.Point(230, 26);
            this.dataGridViewBeliefs.Name = "dataGridViewBeliefs";
            this.dataGridViewBeliefs.ReadOnly = true;
            this.dataGridViewBeliefs.RowHeadersVisible = false;
            this.dataGridViewBeliefs.RowHeadersWidth = 51;
            this.dataGridViewBeliefs.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewBeliefs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewBeliefs.Size = new System.Drawing.Size(324, 174);
            this.dataGridViewBeliefs.TabIndex = 17;
            this.dataGridViewBeliefs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBeliefs_CellContentClick);
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
            this.internalCharacterView.Location = new System.Drawing.Point(6, 19);
            this.internalCharacterView.Name = "internalCharacterView";
            this.internalCharacterView.ReadOnly = true;
            this.internalCharacterView.RowHeadersVisible = false;
            this.internalCharacterView.RowHeadersWidth = 51;
            this.internalCharacterView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.internalCharacterView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.internalCharacterView.Size = new System.Drawing.Size(209, 103);
            this.internalCharacterView.TabIndex = 16;
            this.internalCharacterView.SelectionChanged += new System.EventHandler(this.internalCharacterView_SelectionChanged);
            // 
            // CogRules
            // 
            this.CogRules.Controls.Add(this.label5);
            this.CogRules.Controls.Add(this.dataGridViewEmotions);
            this.CogRules.Controls.Add(this.label4);
            this.CogRules.Controls.Add(this.dataGridViewReactiveActions);
            this.CogRules.Location = new System.Drawing.Point(4, 22);
            this.CogRules.Name = "CogRules";
            this.CogRules.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.CogRules.Size = new System.Drawing.Size(566, 214);
            this.CogRules.TabIndex = 1;
            this.CogRules.Text = "Cognitive Rules";
            this.CogRules.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Emotional Appraisal Rules";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dataGridViewEmotions
            // 
            this.dataGridViewEmotions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewEmotions.AllowUserToAddRows = false;
            this.dataGridViewEmotions.AllowUserToDeleteRows = false;
            this.dataGridViewEmotions.AllowUserToOrderColumns = true;
            this.dataGridViewEmotions.AllowUserToResizeRows = false;
            this.dataGridViewEmotions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewEmotions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewEmotions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewEmotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmotions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewEmotions.Location = new System.Drawing.Point(7, 116);
            this.dataGridViewEmotions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewEmotions.Name = "dataGridViewEmotions";
            this.dataGridViewEmotions.RowHeadersVisible = false;
            this.dataGridViewEmotions.RowHeadersWidth = 51;
            this.dataGridViewEmotions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewEmotions.ShowCellToolTips = false;
            this.dataGridViewEmotions.Size = new System.Drawing.Size(551, 92);
            this.dataGridViewEmotions.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Action Rules";
            // 
            // dataGridViewReactiveActions
            // 
            this.dataGridViewReactiveActions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewReactiveActions.AllowUserToAddRows = false;
            this.dataGridViewReactiveActions.AllowUserToDeleteRows = false;
            this.dataGridViewReactiveActions.AllowUserToOrderColumns = true;
            this.dataGridViewReactiveActions.AllowUserToResizeRows = false;
            this.dataGridViewReactiveActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReactiveActions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewReactiveActions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewReactiveActions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReactiveActions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewReactiveActions.Location = new System.Drawing.Point(8, 20);
            this.dataGridViewReactiveActions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewReactiveActions.Name = "dataGridViewReactiveActions";
            this.dataGridViewReactiveActions.RowHeadersVisible = false;
            this.dataGridViewReactiveActions.RowHeadersWidth = 51;
            this.dataGridViewReactiveActions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewReactiveActions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReactiveActions.ShowCellToolTips = false;
            this.dataGridViewReactiveActions.Size = new System.Drawing.Size(552, 92);
            this.dataGridViewReactiveActions.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(566, 214);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Dialogues";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::IntegratedAuthoringToolWF.Properties.Resources.right_arrow1;
            this.button3.Location = new System.Drawing.Point(45, 373);
            this.button3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 51);
            this.button3.TabIndex = 4;
            this.button3.Text = "Process Output";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::IntegratedAuthoringToolWF.Properties.Resources.icons8_system_report_40;
            this.button2.Location = new System.Drawing.Point(648, 374);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(224, 43);
            this.button2.TabIndex = 5;
            this.button2.Text = "    Cancel Operation";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // IEPOutputForm
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(914, 427);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(528, 412);
            this.Name = "IEPOutputForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Story to Scenario Wizard Output";
            this.Load += new System.EventHandler(this.outputForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rPCConstsBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.CharacterTab.ResumeLayout(false);
            this.Characters.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGoals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBeliefs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.internalCharacterView)).EndInit();
            this.CogRules.ResumeLayout(false);
            this.CogRules.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmotions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReactiveActions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource rPCConstsBindingSource;
        private System.Windows.Forms.Label OutputInformation;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox scenarioTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView internalCharacterView;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewBeliefs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewGoals;
        private System.Windows.Forms.TabControl CharacterTab;
        private System.Windows.Forms.TabPage Characters;
        private System.Windows.Forms.TabPage CogRules;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataGridView dataGridViewReactiveActions;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.DataGridView dataGridViewEmotions;
        private System.Windows.Forms.TabPage tabPage1;
    }
}