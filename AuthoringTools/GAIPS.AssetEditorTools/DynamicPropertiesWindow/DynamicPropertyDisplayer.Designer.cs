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
            this._dynamicPropertiesListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dynamicPropertiesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dynamicPropertiesListView.ImeMode = System.Windows.Forms.ImeMode.On;
            this._dynamicPropertiesListView.Location = new System.Drawing.Point(0, 0);
            this._dynamicPropertiesListView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this._dynamicPropertiesListView.Name = "_dynamicPropertiesListView";
            this._dynamicPropertiesListView.ReadOnly = true;
            this._dynamicPropertiesListView.RowHeadersVisible = false;
            this._dynamicPropertiesListView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this._dynamicPropertiesListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._dynamicPropertiesListView.Size = new System.Drawing.Size(1137, 404);
            this._dynamicPropertiesListView.TabIndex = 12;
            // 
            // DynamicPropertyDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 404);
            this.Controls.Add(this._dynamicPropertiesListView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DynamicPropertyDisplayer";
            this.Text = "Dynamic Properties";
            this.Load += new System.EventHandler(this.DynamicPropertyDisplayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dynamicPropertiesListView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _dynamicPropertiesListView;
	}
}