namespace GAIPS.AssetEditorTools.DynamicPropertiesWindow
{
	partial class DynamicPropertyDisplayer
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
            this._dynamicPropertiesListView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._dynamicPropertiesListView)).BeginInit();
            this.SuspendLayout();
            // 
            // _dynamicPropertiesListView
            // 
            this._dynamicPropertiesListView.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this._dynamicPropertiesListView.AllowUserToAddRows = false;
            this._dynamicPropertiesListView.AllowUserToDeleteRows = false;
            this._dynamicPropertiesListView.AllowUserToOrderColumns = true;
            this._dynamicPropertiesListView.AllowUserToResizeRows = false;
            this._dynamicPropertiesListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._dynamicPropertiesListView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dynamicPropertiesListView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._dynamicPropertiesListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._dynamicPropertiesListView.DefaultCellStyle = dataGridViewCellStyle2;
            this._dynamicPropertiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dynamicPropertiesListView.ImeMode = System.Windows.Forms.ImeMode.On;
            this._dynamicPropertiesListView.Location = new System.Drawing.Point(0, 0);
            this._dynamicPropertiesListView.Name = "_dynamicPropertiesListView";
            this._dynamicPropertiesListView.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dynamicPropertiesListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._dynamicPropertiesListView.RowHeadersVisible = false;
            this._dynamicPropertiesListView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._dynamicPropertiesListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dynamicPropertiesListView.Size = new System.Drawing.Size(705, 328);
            this._dynamicPropertiesListView.TabIndex = 12;
            // 
            // DynamicPropertyDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 328);
            this.Controls.Add(this._dynamicPropertiesListView);
            this.Name = "DynamicPropertyDisplayer";
            this.Text = "DynamicPropertyDisplayer";
            ((System.ComponentModel.ISupportInitialize)(this._dynamicPropertiesListView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _dynamicPropertiesListView;
	}
}