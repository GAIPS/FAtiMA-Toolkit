namespace IntegratedAuthoringToolWF
{
    partial class RPCInspectForm
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
            this.dataGridViewDecisions = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxEvents = new System.Windows.Forms.TextBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridActiveEmotions = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.wfNameActionLayer = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxMood = new System.Windows.Forms.TextBox();
            this.textBoxTick = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridActiveEmotions)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridViewDecisions);
            this.groupBox1.Location = new System.Drawing.Point(16, 348);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(773, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Decide() - Output";
            // 
            // dataGridViewDecisions
            // 
            this.dataGridViewDecisions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewDecisions.AllowUserToAddRows = false;
            this.dataGridViewDecisions.AllowUserToDeleteRows = false;
            this.dataGridViewDecisions.AllowUserToOrderColumns = true;
            this.dataGridViewDecisions.AllowUserToResizeRows = false;
            this.dataGridViewDecisions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewDecisions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewDecisions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDecisions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewDecisions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewDecisions.Location = new System.Drawing.Point(4, 23);
            this.dataGridViewDecisions.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewDecisions.Name = "dataGridViewDecisions";
            this.dataGridViewDecisions.ReadOnly = true;
            this.dataGridViewDecisions.RowHeadersVisible = false;
            this.dataGridViewDecisions.RowHeadersWidth = 51;
            this.dataGridViewDecisions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDecisions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDecisions.Size = new System.Drawing.Size(765, 150);
            this.dataGridViewDecisions.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxEvents);
            this.groupBox2.Location = new System.Drawing.Point(16, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 157);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Events - Input";
            // 
            // textBoxEvents
            // 
            this.textBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxEvents.Location = new System.Drawing.Point(3, 22);
            this.textBoxEvents.Multiline = true;
            this.textBoxEvents.Name = "textBoxEvents";
            this.textBoxEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxEvents.Size = new System.Drawing.Size(763, 132);
            this.textBoxEvents.TabIndex = 0;
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonTest.Location = new System.Drawing.Point(357, 538);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(91, 27);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.dataGridActiveEmotions);
            this.groupBox3.Location = new System.Drawing.Point(16, 175);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(769, 166);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Appraisal - Output";
            // 
            // dataGridActiveEmotions
            // 
            this.dataGridActiveEmotions.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridActiveEmotions.AllowUserToAddRows = false;
            this.dataGridActiveEmotions.AllowUserToDeleteRows = false;
            this.dataGridActiveEmotions.AllowUserToOrderColumns = true;
            this.dataGridActiveEmotions.AllowUserToResizeRows = false;
            this.dataGridActiveEmotions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridActiveEmotions.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridActiveEmotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridActiveEmotions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridActiveEmotions.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridActiveEmotions.Location = new System.Drawing.Point(3, 22);
            this.dataGridActiveEmotions.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridActiveEmotions.Name = "dataGridActiveEmotions";
            this.dataGridActiveEmotions.ReadOnly = true;
            this.dataGridActiveEmotions.RowHeadersVisible = false;
            this.dataGridActiveEmotions.RowHeadersWidth = 51;
            this.dataGridActiveEmotions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridActiveEmotions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridActiveEmotions.Size = new System.Drawing.Size(763, 141);
            this.dataGridActiveEmotions.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 543);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "Action Layer:";
            // 
            // wfNameActionLayer
            // 
            this.wfNameActionLayer.AllowComposedName = true;
            this.wfNameActionLayer.AllowLiteral = true;
            this.wfNameActionLayer.AllowNil = true;
            this.wfNameActionLayer.AllowUniversal = true;
            this.wfNameActionLayer.AllowUniversalLiteral = true;
            this.wfNameActionLayer.AllowVariable = true;
            this.wfNameActionLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.wfNameActionLayer.Location = new System.Drawing.Point(108, 540);
            this.wfNameActionLayer.Name = "wfNameActionLayer";
            this.wfNameActionLayer.OnlyIntOrVariable = false;
            this.wfNameActionLayer.Size = new System.Drawing.Size(152, 26);
            this.wfNameActionLayer.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 543);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Mood:";
            // 
            // textBoxMood
            // 
            this.textBoxMood.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMood.Location = new System.Drawing.Point(726, 540);
            this.textBoxMood.Name = "textBoxMood";
            this.textBoxMood.ReadOnly = true;
            this.textBoxMood.Size = new System.Drawing.Size(59, 26);
            this.textBoxMood.TabIndex = 6;
            // 
            // textBoxTick
            // 
            this.textBoxTick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTick.Location = new System.Drawing.Point(609, 540);
            this.textBoxTick.Name = "textBoxTick";
            this.textBoxTick.ReadOnly = true;
            this.textBoxTick.Size = new System.Drawing.Size(59, 26);
            this.textBoxTick.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(557, 543);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tick:";
            // 
            // RPCInspectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 576);
            this.Controls.Add(this.textBoxTick);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMood);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wfNameActionLayer);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RPCInspectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RPC Inspector";
            this.Load += new System.EventHandler(this.RPCInspectForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridActiveEmotions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewDecisions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxEvents;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridActiveEmotions;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox wfNameActionLayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxMood;
        private System.Windows.Forms.TextBox textBoxTick;
        private System.Windows.Forms.Label label3;
    }
}