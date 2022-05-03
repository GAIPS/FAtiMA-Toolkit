
namespace IntegratedAuthoringToolWF.Metabeliefs
{
    partial class MetaBeliefForm
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
            this.dataGridViewAgentInspector = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentInspector)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewAgentInspector
            // 
            this.dataGridViewAgentInspector.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dataGridViewAgentInspector.AllowUserToAddRows = false;
            this.dataGridViewAgentInspector.AllowUserToDeleteRows = false;
            this.dataGridViewAgentInspector.AllowUserToOrderColumns = true;
            this.dataGridViewAgentInspector.AllowUserToResizeRows = false;
            this.dataGridViewAgentInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewAgentInspector.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewAgentInspector.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridViewAgentInspector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAgentInspector.ImeMode = System.Windows.Forms.ImeMode.On;
            this.dataGridViewAgentInspector.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewAgentInspector.Name = "dataGridViewAgentInspector";
            this.dataGridViewAgentInspector.ReadOnly = true;
            this.dataGridViewAgentInspector.RowHeadersVisible = false;
            this.dataGridViewAgentInspector.RowHeadersWidth = 51;
            this.dataGridViewAgentInspector.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewAgentInspector.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewAgentInspector.Size = new System.Drawing.Size(650, 320);
            this.dataGridViewAgentInspector.TabIndex = 10;
            // 
            // MetaBeliefForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 320);
            this.Controls.Add(this.dataGridViewAgentInspector);
            this.Name = "MetaBeliefForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Meta Belief List";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAgentInspector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewAgentInspector;
    }
}