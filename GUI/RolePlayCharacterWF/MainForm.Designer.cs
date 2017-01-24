using SocialImportance;

namespace RolePlayCharacterWF
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.moodGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.moodValueLabel = new System.Windows.Forms.Label();
            this.moodTrackBar = new System.Windows.Forms.TrackBar();
            this.emotionGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonEditEmotion = new System.Windows.Forms.Button();
            this.addEmotionButton = new System.Windows.Forms.Button();
            this.buttonRemoveEmotion = new System.Windows.Forms.Button();
            this.emotionsDataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonDuplicateEventRecord = new System.Windows.Forms.Button();
            this.dataGridViewAM = new System.Windows.Forms.DataGridView();
            this.buttonEditEvent = new System.Windows.Forms.Button();
            this.buttonAddEventRecord = new System.Windows.Forms.Button();
            this.buttonRemoveEventRecord = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.StartTickField = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.eaAssetControl1 = new RolePlayCharacterWF.EAAssetControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.edmAssetControl1 = new RolePlayCharacterWF.EDMAssetControl();
            this.siAssetControl1 = new RolePlayCharacterWF.SIAssetControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCharacterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCharacterBody = new System.Windows.Forms.TextBox();
            this.textBoxCharacterVoice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.moodGroupBox.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moodTrackBar)).BeginInit();
            this.emotionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.emotionsDataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAM)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartTickField)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(513, 441);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(3, 89);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(507, 349);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(499, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Emotional State";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.emotionGroupBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.moodGroupBox, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(493, 311);
            this.tableLayoutPanel3.TabIndex = 2;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // moodGroupBox
            // 
            this.moodGroupBox.Controls.Add(this.tableLayoutPanel7);
            this.moodGroupBox.Location = new System.Drawing.Point(3, 3);
            this.moodGroupBox.Name = "moodGroupBox";
            this.moodGroupBox.Size = new System.Drawing.Size(487, 58);
            this.moodGroupBox.TabIndex = 14;
            this.moodGroupBox.TabStop = false;
            this.moodGroupBox.Text = "Mood";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel7.Controls.Add(this.moodValueLabel, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.moodTrackBar, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(481, 39);
            this.tableLayoutPanel7.TabIndex = 12;
            // 
            // moodValueLabel
            // 
            this.moodValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.moodValueLabel.AutoSize = true;
            this.moodValueLabel.Location = new System.Drawing.Point(434, 13);
            this.moodValueLabel.Name = "moodValueLabel";
            this.moodValueLabel.Size = new System.Drawing.Size(44, 13);
            this.moodValueLabel.TabIndex = 5;
            this.moodValueLabel.Text = "0";
            this.moodValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // moodTrackBar
            // 
            this.moodTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.moodTrackBar.Location = new System.Drawing.Point(3, 3);
            this.moodTrackBar.Minimum = -10;
            this.moodTrackBar.Name = "moodTrackBar";
            this.moodTrackBar.Size = new System.Drawing.Size(425, 33);
            this.moodTrackBar.TabIndex = 11;
            this.moodTrackBar.Scroll += new System.EventHandler(this.moodTrackBar_Scroll);
            // 
            // emotionGroupBox
            // 
            this.emotionGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emotionGroupBox.Controls.Add(this.buttonEditEmotion);
            this.emotionGroupBox.Controls.Add(this.addEmotionButton);
            this.emotionGroupBox.Controls.Add(this.buttonRemoveEmotion);
            this.emotionGroupBox.Controls.Add(this.emotionsDataGridView);
            this.emotionGroupBox.Location = new System.Drawing.Point(3, 67);
            this.emotionGroupBox.Name = "emotionGroupBox";
            this.emotionGroupBox.Size = new System.Drawing.Size(487, 241);
            this.emotionGroupBox.TabIndex = 1;
            this.emotionGroupBox.TabStop = false;
            this.emotionGroupBox.Text = "Emotions";
            // 
            // buttonEditEmotion
            // 
            this.buttonEditEmotion.Location = new System.Drawing.Point(66, 19);
            this.buttonEditEmotion.Name = "buttonEditEmotion";
            this.buttonEditEmotion.Size = new System.Drawing.Size(70, 23);
            this.buttonEditEmotion.TabIndex = 9;
            this.buttonEditEmotion.Text = "Edit";
            this.buttonEditEmotion.UseVisualStyleBackColor = true;
            this.buttonEditEmotion.Click += new System.EventHandler(this.buttonEditEmotion_Click);
            // 
            // addEmotionButton
            // 
            this.addEmotionButton.Location = new System.Drawing.Point(6, 19);
            this.addEmotionButton.Name = "addEmotionButton";
            this.addEmotionButton.Size = new System.Drawing.Size(54, 23);
            this.addEmotionButton.TabIndex = 7;
            this.addEmotionButton.Text = "Add";
            this.addEmotionButton.UseVisualStyleBackColor = true;
            this.addEmotionButton.Click += new System.EventHandler(this.addEmotionButton_Click_1);
            // 
            // buttonRemoveEmotion
            // 
            this.buttonRemoveEmotion.Location = new System.Drawing.Point(142, 19);
            this.buttonRemoveEmotion.Name = "buttonRemoveEmotion";
            this.buttonRemoveEmotion.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveEmotion.TabIndex = 8;
            this.buttonRemoveEmotion.Text = "Remove";
            this.buttonRemoveEmotion.UseVisualStyleBackColor = true;
            this.buttonRemoveEmotion.Click += new System.EventHandler(this.buttonRemoveEmotion_Click_1);
            // 
            // emotionsDataGridView
            // 
            this.emotionsDataGridView.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.emotionsDataGridView.AllowUserToAddRows = false;
            this.emotionsDataGridView.AllowUserToDeleteRows = false;
            this.emotionsDataGridView.AllowUserToOrderColumns = true;
            this.emotionsDataGridView.AllowUserToResizeRows = false;
            this.emotionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.emotionsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.emotionsDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.emotionsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.emotionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.emotionsDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.emotionsDataGridView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.emotionsDataGridView.Location = new System.Drawing.Point(6, 54);
            this.emotionsDataGridView.Name = "emotionsDataGridView";
            this.emotionsDataGridView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.emotionsDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.emotionsDataGridView.RowHeadersVisible = false;
            this.emotionsDataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.emotionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.emotionsDataGridView.Size = new System.Drawing.Size(475, 181);
            this.emotionsDataGridView.TabIndex = 2;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel4);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(499, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Autobiographical Memory";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.groupBox9, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(6, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 206F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(487, 314);
            this.tableLayoutPanel4.TabIndex = 0;
            this.tableLayoutPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel4_Paint);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonDuplicateEventRecord);
            this.groupBox4.Controls.Add(this.dataGridViewAM);
            this.groupBox4.Controls.Add(this.buttonEditEvent);
            this.groupBox4.Controls.Add(this.buttonAddEventRecord);
            this.groupBox4.Controls.Add(this.buttonRemoveEventRecord);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(481, 248);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Event Records";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // buttonDuplicateEventRecord
            // 
            this.buttonDuplicateEventRecord.Location = new System.Drawing.Point(142, 20);
            this.buttonDuplicateEventRecord.Name = "buttonDuplicateEventRecord";
            this.buttonDuplicateEventRecord.Size = new System.Drawing.Size(70, 23);
            this.buttonDuplicateEventRecord.TabIndex = 11;
            this.buttonDuplicateEventRecord.Text = "Duplicate";
            this.buttonDuplicateEventRecord.UseVisualStyleBackColor = true;
            this.buttonDuplicateEventRecord.Click += new System.EventHandler(this.buttonDuplicateEventRecord_Click);
            // 
            // dataGridViewAM
            // 
            this.dataGridViewAM.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAM.AllowUserToAddRows = false;
            this.dataGridViewAM.AllowUserToDeleteRows = false;
            this.dataGridViewAM.AllowUserToOrderColumns = true;
            this.dataGridViewAM.AllowUserToResizeRows = false;
            this.dataGridViewAM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAM.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAM.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewAM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAM.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewAM.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAM.Location = new System.Drawing.Point(3, 49);
            this.dataGridViewAM.Name = "dataGridViewAM";
            this.dataGridViewAM.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAM.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewAM.RowHeadersVisible = false;
            this.dataGridViewAM.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAM.Size = new System.Drawing.Size(472, 193);
            this.dataGridViewAM.TabIndex = 10;
            // 
            // buttonEditEvent
            // 
            this.buttonEditEvent.Location = new System.Drawing.Point(66, 20);
            this.buttonEditEvent.Name = "buttonEditEvent";
            this.buttonEditEvent.Size = new System.Drawing.Size(70, 23);
            this.buttonEditEvent.TabIndex = 9;
            this.buttonEditEvent.Text = "Edit";
            this.buttonEditEvent.UseVisualStyleBackColor = true;
            this.buttonEditEvent.Click += new System.EventHandler(this.buttonEditEvent_Click);
            // 
            // buttonAddEventRecord
            // 
            this.buttonAddEventRecord.Location = new System.Drawing.Point(6, 20);
            this.buttonAddEventRecord.Name = "buttonAddEventRecord";
            this.buttonAddEventRecord.Size = new System.Drawing.Size(54, 23);
            this.buttonAddEventRecord.TabIndex = 7;
            this.buttonAddEventRecord.Text = "Add";
            this.buttonAddEventRecord.UseVisualStyleBackColor = true;
            this.buttonAddEventRecord.Click += new System.EventHandler(this.buttonAddEventRecord_Click);
            // 
            // buttonRemoveEventRecord
            // 
            this.buttonRemoveEventRecord.Location = new System.Drawing.Point(218, 20);
            this.buttonRemoveEventRecord.Name = "buttonRemoveEventRecord";
            this.buttonRemoveEventRecord.Size = new System.Drawing.Size(70, 23);
            this.buttonRemoveEventRecord.TabIndex = 8;
            this.buttonRemoveEventRecord.Text = "Remove";
            this.buttonRemoveEventRecord.UseVisualStyleBackColor = true;
            this.buttonRemoveEventRecord.Click += new System.EventHandler(this.buttonRemoveEventRecord_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.tableLayoutPanel6);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(481, 54);
            this.groupBox9.TabIndex = 15;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Time";
            this.groupBox9.Enter += new System.EventHandler(this.groupBox9_Enter);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.StartTickField, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(472, 35);
            this.tableLayoutPanel6.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Starting Tick:";
            // 
            // StartTickField
            // 
            this.StartTickField.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.StartTickField.Location = new System.Drawing.Point(79, 7);
            this.StartTickField.Maximum = new decimal(new int[] {
            -1,
            -1,
            0,
            0});
            this.StartTickField.Name = "StartTickField";
            this.StartTickField.Size = new System.Drawing.Size(90, 20);
            this.StartTickField.TabIndex = 12;
            this.StartTickField.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StartTickField.ThousandsSeparator = true;
            this.StartTickField.ValueChanged += new System.EventHandler(this.StartTickField_ValueChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.eaAssetControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(499, 320);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Emotional Appraisal";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // eaAssetControl1
            // 
            this.eaAssetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eaAssetControl1.Label = "Emotional Appraisal";
            this.eaAssetControl1.Location = new System.Drawing.Point(0, 0);
            this.eaAssetControl1.Name = "eaAssetControl1";
            this.eaAssetControl1.Size = new System.Drawing.Size(499, 320);
            this.eaAssetControl1.TabIndex = 12;
            this.eaAssetControl1.Load += new System.EventHandler(this.eaAssetControl1_Load);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tableLayoutPanel5);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(499, 320);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Decision Making";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.edmAssetControl1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.siAssetControl1, 0, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(493, 308);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // edmAssetControl1
            // 
            this.edmAssetControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edmAssetControl1.Label = "Emotional Decision Making";
            this.edmAssetControl1.Location = new System.Drawing.Point(3, 3);
            this.edmAssetControl1.Name = "edmAssetControl1";
            this.edmAssetControl1.Size = new System.Drawing.Size(487, 148);
            this.edmAssetControl1.TabIndex = 11;
            this.edmAssetControl1.Load += new System.EventHandler(this.edmAssetControl1_Load);
            // 
            // siAssetControl1
            // 
            this.siAssetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siAssetControl1.Label = "Social Importance";
            this.siAssetControl1.Location = new System.Drawing.Point(3, 157);
            this.siAssetControl1.Name = "siAssetControl1";
            this.siAssetControl1.Size = new System.Drawing.Size(487, 148);
            this.siAssetControl1.TabIndex = 12;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterBody, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterVoice, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(507, 80);
            this.tableLayoutPanel2.TabIndex = 0;
            this.tableLayoutPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel2_Paint);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Voice:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterName.Location = new System.Drawing.Point(77, 3);
            this.textBoxCharacterName.Name = "textBoxCharacterName";
            this.textBoxCharacterName.Size = new System.Drawing.Size(370, 20);
            this.textBoxCharacterName.TabIndex = 3;
            this.textBoxCharacterName.TextChanged += new System.EventHandler(this.textBoxCharacterName_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Body:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxCharacterBody
            // 
            this.textBoxCharacterBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterBody.Location = new System.Drawing.Point(77, 29);
            this.textBoxCharacterBody.Name = "textBoxCharacterBody";
            this.textBoxCharacterBody.Size = new System.Drawing.Size(370, 20);
            this.textBoxCharacterBody.TabIndex = 4;
            this.textBoxCharacterBody.TextChanged += new System.EventHandler(this.textBoxCharacterBody_TextChanged);
            // 
            // textBoxCharacterVoice
            // 
            this.textBoxCharacterVoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterVoice.Location = new System.Drawing.Point(77, 56);
            this.textBoxCharacterVoice.Name = "textBoxCharacterVoice";
            this.textBoxCharacterVoice.Size = new System.Drawing.Size(370, 20);
            this.textBoxCharacterVoice.TabIndex = 10;
            this.textBoxCharacterVoice.TextChanged += new System.EventHandler(this.textBoxCharacterVoice_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(513, 465);
            this.Controls.Add(this.tableLayoutPanel1);
            this.EditorName = "Role Play Character Editor";
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "MainForm";
            this.Text = "";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.moodGroupBox.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moodTrackBar)).EndInit();
            this.emotionGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.emotionsDataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAM)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StartTickField)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxCharacterName;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxCharacterBody;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCharacterVoice;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private EAAssetControl eaAssetControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private SIAssetControl siAssetControl1;
        private EDMAssetControl edmAssetControl1;
        private System.Windows.Forms.GroupBox emotionGroupBox;
        private System.Windows.Forms.Button buttonEditEmotion;
        private System.Windows.Forms.Button addEmotionButton;
        private System.Windows.Forms.Button buttonRemoveEmotion;
        private System.Windows.Forms.DataGridView emotionsDataGridView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox moodGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label moodValueLabel;
        private System.Windows.Forms.TrackBar moodTrackBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.NumericUpDown StartTickField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonDuplicateEventRecord;
        private System.Windows.Forms.DataGridView dataGridViewAM;
        private System.Windows.Forms.Button buttonEditEvent;
        private System.Windows.Forms.Button buttonAddEventRecord;
        private System.Windows.Forms.Button buttonRemoveEventRecord;
    }
}

