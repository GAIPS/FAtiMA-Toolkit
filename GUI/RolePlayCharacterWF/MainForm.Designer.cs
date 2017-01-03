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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCharacterName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxCharacterBody = new System.Windows.Forms.TextBox();
            this.textBoxCharacterVoice = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.siAssetControl1 = new RolePlayCharacterWF.SIAssetControl();
            this.edmAssetControl1 = new RolePlayCharacterWF.EDMAssetControl();
            this.eaAssetControl1 = new RolePlayCharacterWF.EAAssetControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(566, 387);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterName, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterBody, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxCharacterVoice, 1, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(560, 80);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Voice:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterName.Location = new System.Drawing.Point(61, 3);
            this.textBoxCharacterName.Name = "textBoxCharacterName";
            this.textBoxCharacterName.Size = new System.Drawing.Size(496, 20);
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
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Body:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxCharacterBody
            // 
            this.textBoxCharacterBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterBody.Location = new System.Drawing.Point(61, 29);
            this.textBoxCharacterBody.Name = "textBoxCharacterBody";
            this.textBoxCharacterBody.Size = new System.Drawing.Size(496, 20);
            this.textBoxCharacterBody.TabIndex = 4;
            this.textBoxCharacterBody.TextChanged += new System.EventHandler(this.textBoxCharacterBody_TextChanged);
            // 
            // textBoxCharacterVoice
            // 
            this.textBoxCharacterVoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCharacterVoice.Location = new System.Drawing.Point(61, 56);
            this.textBoxCharacterVoice.Name = "textBoxCharacterVoice";
            this.textBoxCharacterVoice.Size = new System.Drawing.Size(496, 20);
            this.textBoxCharacterVoice.TabIndex = 10;
            this.textBoxCharacterVoice.TextChanged += new System.EventHandler(this.textBoxCharacterVoice_TextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.siAssetControl1, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.edmAssetControl1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.eaAssetControl1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 89);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(560, 306);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // siAssetControl1
            // 
            this.siAssetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siAssetControl1.Label = "Social Importance";
            this.siAssetControl1.Location = new System.Drawing.Point(3, 207);
            this.siAssetControl1.Name = "siAssetControl1";
            this.siAssetControl1.Size = new System.Drawing.Size(554, 96);
            this.siAssetControl1.TabIndex = 9;
            this.siAssetControl1.OnPathChanged += new System.EventHandler(this.siAssetControl1_OnPathChanged);
            // 
            // edmAssetControl1
            // 
            this.edmAssetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edmAssetControl1.Label = "Emotional Decision Making";
            this.edmAssetControl1.Location = new System.Drawing.Point(3, 105);
            this.edmAssetControl1.Name = "edmAssetControl1";
            this.edmAssetControl1.Size = new System.Drawing.Size(554, 96);
            this.edmAssetControl1.TabIndex = 10;
            this.edmAssetControl1.OnPathChanged += new System.EventHandler(this.edmAssetControl1_OnPathChanged);
            // 
            // eaAssetControl1
            // 
            this.eaAssetControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eaAssetControl1.Label = "Emotional Appraisal";
            this.eaAssetControl1.Location = new System.Drawing.Point(3, 3);
            this.eaAssetControl1.Name = "eaAssetControl1";
            this.eaAssetControl1.Size = new System.Drawing.Size(554, 96);
            this.eaAssetControl1.TabIndex = 11;
            this.eaAssetControl1.OnPathChanged += new System.EventHandler(this.eaAssetControl1_OnPathChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(566, 411);
            this.Controls.Add(this.tableLayoutPanel1);
            this.EditorName = "Role Play Character Editor";
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "MainForm";
            this.Text = "";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
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
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private SIAssetControl siAssetControl1;
		private EDMAssetControl edmAssetControl1;
		private EAAssetControl eaAssetControl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCharacterVoice;
    }
}

