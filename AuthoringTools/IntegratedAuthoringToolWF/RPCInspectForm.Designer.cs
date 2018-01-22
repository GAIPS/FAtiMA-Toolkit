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
            this.buttonTest = new System.Windows.Forms.Button();
            this.textBoxEvents = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridViewDecisions);
            this.groupBox1.Location = new System.Drawing.Point(16, 216);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(773, 222);
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
            this.dataGridViewDecisions.Location = new System.Drawing.Point(4, 19);
            this.dataGridViewDecisions.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridViewDecisions.Name = "dataGridViewDecisions";
            this.dataGridViewDecisions.ReadOnly = true;
            this.dataGridViewDecisions.RowHeadersVisible = false;
            this.dataGridViewDecisions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDecisions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDecisions.Size = new System.Drawing.Size(765, 199);
            this.dataGridViewDecisions.TabIndex = 14;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxEvents);
            this.groupBox2.Location = new System.Drawing.Point(16, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(769, 197);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Events";
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonTest.Location = new System.Drawing.Point(357, 445);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(91, 32);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxEvents
            // 
            this.textBoxEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxEvents.Location = new System.Drawing.Point(3, 18);
            this.textBoxEvents.Multiline = true;
            this.textBoxEvents.Name = "textBoxEvents";
            this.textBoxEvents.Size = new System.Drawing.Size(763, 176);
            this.textBoxEvents.TabIndex = 0;
            // 
            // RPCInspectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 489);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RPCInspectForm";
            this.Text = "RPC Inspect";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewDecisions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxEvents;
        private System.Windows.Forms.Button buttonTest;
    }
}