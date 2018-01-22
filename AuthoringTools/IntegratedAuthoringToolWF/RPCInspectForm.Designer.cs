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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridViewDecisions);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(773, 389);
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
            this.dataGridViewDecisions.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dataGridViewDecisions.Name = "dataGridViewDecisions";
            this.dataGridViewDecisions.ReadOnly = true;
            this.dataGridViewDecisions.RowHeadersVisible = false;
            this.dataGridViewDecisions.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewDecisions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDecisions.Size = new System.Drawing.Size(765, 366);
            this.dataGridViewDecisions.TabIndex = 14;
            // 
            // RPCInspectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 418);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "RPCInspectForm";
            this.Text = "RPC Inspect";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDecisions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewDecisions;
    }
}