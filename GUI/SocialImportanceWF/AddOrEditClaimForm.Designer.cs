namespace SocialImportanceWF
{
	partial class AddOrEditClaimForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this._clamSI = new GAIPS.AssetEditorTools.TypedTextBoxes.UInt32FieldBox();
			this.label2 = new System.Windows.Forms.Label();
			this._actionTemplateField = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this._button = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this._clamSI);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this._actionTemplateField);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(384, 119);
			this.splitContainer1.SplitterDistance = 90;
			this.splitContainer1.TabIndex = 0;
			// 
			// _clamSI
			// 
			this._clamSI.Location = new System.Drawing.Point(145, 54);
			this._clamSI.Name = "_clamSI";
			this._clamSI.Size = new System.Drawing.Size(227, 20);
			this._clamSI.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Claim Social Importance:";
			// 
			// _actionTemplateField
			// 
			this._actionTemplateField.AllowProperty = true;
			this._actionTemplateField.AllowUniversal = true;
			this._actionTemplateField.AllowVariable = true;
			this._actionTemplateField.Location = new System.Drawing.Point(16, 29);
			this._actionTemplateField.Name = "_actionTemplateField";
			this._actionTemplateField.Size = new System.Drawing.Size(356, 20);
			this._actionTemplateField.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(87, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Action Template:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.Controls.Add(this._button, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 25);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// _button
			// 
			this._button.Dock = System.Windows.Forms.DockStyle.Fill;
			this._button.Location = new System.Drawing.Point(131, 3);
			this._button.Name = "_button";
			this._button.Size = new System.Drawing.Size(122, 19);
			this._button.TabIndex = 0;
			this._button.Text = "_button";
			this._button.UseVisualStyleBackColor = true;
			this._button.Click += new System.EventHandler(this.OnButton_Click);
			// 
			// AddOrEditClaimForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 119);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddOrEditClaimForm";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button _button;
		private System.Windows.Forms.Label label2;
		private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox _actionTemplateField;
		private GAIPS.AssetEditorTools.TypedTextBoxes.UInt32FieldBox _clamSI;
	}
}