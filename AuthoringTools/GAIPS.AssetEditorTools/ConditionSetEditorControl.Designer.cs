namespace GAIPS.AssetEditorTools
{
	partial class ConditionSetEditorControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.add_button = new System.Windows.Forms.Button();
            this.remove_button = new System.Windows.Forms.Button();
            this.quantifierComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.moveDown_button = new System.Windows.Forms.Button();
            this.moveUp_button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 492);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.24096F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.add_button, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.remove_button, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.quantifierComboBox, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(830, 36);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // add_button
            // 
            this.add_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.add_button.Location = new System.Drawing.Point(3, 3);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(118, 30);
            this.add_button.TabIndex = 0;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // remove_button
            // 
            this.remove_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remove_button.Location = new System.Drawing.Point(127, 3);
            this.remove_button.Name = "remove_button";
            this.remove_button.Size = new System.Drawing.Size(118, 30);
            this.remove_button.TabIndex = 2;
            this.remove_button.Text = "Remove";
            this.remove_button.UseVisualStyleBackColor = true;
            this.remove_button.Click += new System.EventHandler(this.remove_button_Click);
            // 
            // quantifierComboBox
            // 
            this.quantifierComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.quantifierComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.quantifierComboBox.FormattingEnabled = true;
            this.quantifierComboBox.Location = new System.Drawing.Point(666, 3);
            this.quantifierComboBox.Name = "quantifierComboBox";
            this.quantifierComboBox.Size = new System.Drawing.Size(161, 21);
            this.quantifierComboBox.TabIndex = 3;
            this.quantifierComboBox.SelectedIndexChanged += new System.EventHandler(this.quantifierComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(586, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 4;
            this.label1.Text = "Quantifier:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel3.Controls.Add(this.dataView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(830, 456);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // dataView
            // 
            this.dataView.AllowUserToAddRows = false;
            this.dataView.AllowUserToDeleteRows = false;
            this.dataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataView.Location = new System.Drawing.Point(3, 3);
            this.dataView.MultiSelect = false;
            this.dataView.Name = "dataView";
            this.dataView.RowHeadersVisible = false;
            this.dataView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.Size = new System.Drawing.Size(774, 450);
            this.dataView.TabIndex = 1;
            this.dataView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataView_CellBeginEdit);
            this.dataView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataView_CellEndEdit);
            this.dataView.SelectionChanged += new System.EventHandler(this.dataView_SelectionChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.moveDown_button, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.moveUp_button, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(780, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(50, 456);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // moveDown_button
            // 
            this.moveDown_button.Dock = System.Windows.Forms.DockStyle.Top;
            this.moveDown_button.Location = new System.Drawing.Point(3, 231);
            this.moveDown_button.Name = "moveDown_button";
            this.moveDown_button.Size = new System.Drawing.Size(44, 67);
            this.moveDown_button.TabIndex = 1;
            this.moveDown_button.Text = "Move Down";
            this.moveDown_button.UseVisualStyleBackColor = true;
            this.moveDown_button.Click += new System.EventHandler(this.moveDown_button_Click);
            // 
            // moveUp_button
            // 
            this.moveUp_button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.moveUp_button.Location = new System.Drawing.Point(3, 158);
            this.moveUp_button.Name = "moveUp_button";
            this.moveUp_button.Size = new System.Drawing.Size(44, 67);
            this.moveUp_button.TabIndex = 0;
            this.moveUp_button.Text = "Move Up";
            this.moveUp_button.UseVisualStyleBackColor = true;
            this.moveUp_button.Click += new System.EventHandler(this.moveUp_button_Click);
            // 
            // ConditionSetEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ConditionSetEditorControl";
            this.Size = new System.Drawing.Size(830, 492);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button remove_button;
		private System.Windows.Forms.Button add_button;
		private System.Windows.Forms.DataGridView dataView;
		private System.Windows.Forms.ComboBox quantifierComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button moveDown_button;
		private System.Windows.Forms.Button moveUp_button;
	}
}
