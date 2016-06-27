namespace SocialImportanceWF
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._attRulesDataView = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this._attRuleConditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this._claimDataView = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this._conferralsDataView = new GAIPS.AssetEditorTools.GenericPropertyDataGridControler();
			this._conferralsConditionSetEditor = new GAIPS.AssetEditorTools.ConditionSetEditorControl();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
			this.tabControl.CausesValidation = false;
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPage3);
			this.tabControl.Cursor = System.Windows.Forms.Cursors.Default;
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(3, 3);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(718, 513);
			this.tabControl.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(710, 484);
			this.tabPage1.TabIndex = 3;
			this.tabPage1.Text = "Attribution Rules";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(704, 478);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this._attRulesDataView);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(698, 233);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Rules";
			// 
			// _attRulesDataView
			// 
			this._attRulesDataView.AllowMuliSelect = false;
			this._attRulesDataView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._attRulesDataView.Location = new System.Drawing.Point(3, 16);
			this._attRulesDataView.Margin = new System.Windows.Forms.Padding(0);
			this._attRulesDataView.Name = "_attRulesDataView";
			this._attRulesDataView.Size = new System.Drawing.Size(692, 214);
			this._attRulesDataView.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._attRuleConditionSetEditor);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(3, 242);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(698, 233);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Conditions";
			// 
			// _attRuleConditionSetEditor
			// 
			this._attRuleConditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this._attRuleConditionSetEditor.Location = new System.Drawing.Point(3, 16);
			this._attRuleConditionSetEditor.Name = "_attRuleConditionSetEditor";
			this._attRuleConditionSetEditor.Size = new System.Drawing.Size(692, 214);
			this._attRuleConditionSetEditor.TabIndex = 0;
			this._attRuleConditionSetEditor.View = null;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this._claimDataView);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(710, 484);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Claims";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// _claimDataView
			// 
			this._claimDataView.AllowMuliSelect = false;
			this._claimDataView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._claimDataView.Location = new System.Drawing.Point(3, 3);
			this._claimDataView.Margin = new System.Windows.Forms.Padding(0);
			this._claimDataView.Name = "_claimDataView";
			this._claimDataView.Size = new System.Drawing.Size(704, 478);
			this._claimDataView.TabIndex = 0;
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.tableLayoutPanel2);
			this.tabPage3.Location = new System.Drawing.Point(4, 25);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(710, 484);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Conferrals";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tabControl);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3);
			this.panel1.Size = new System.Drawing.Size(724, 519);
			this.panel1.TabIndex = 2;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 1;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.groupBox4, 0, 1);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(710, 484);
			this.tableLayoutPanel2.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this._conferralsDataView);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(710, 242);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Conferrals";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this._conferralsConditionSetEditor);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox4.Location = new System.Drawing.Point(0, 242);
			this.groupBox4.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(710, 242);
			this.groupBox4.TabIndex = 1;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Conditions";
			// 
			// _conferralsDataView
			// 
			this._conferralsDataView.AllowMuliSelect = false;
			this._conferralsDataView.Dock = System.Windows.Forms.DockStyle.Fill;
			this._conferralsDataView.Location = new System.Drawing.Point(3, 16);
			this._conferralsDataView.Margin = new System.Windows.Forms.Padding(0);
			this._conferralsDataView.Name = "_conferralsDataView";
			this._conferralsDataView.Size = new System.Drawing.Size(704, 223);
			this._conferralsDataView.TabIndex = 0;
			// 
			// _conferralsConditionSetEditor
			// 
			this._conferralsConditionSetEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this._conferralsConditionSetEditor.Location = new System.Drawing.Point(3, 16);
			this._conferralsConditionSetEditor.Name = "_conferralsConditionSetEditor";
			this._conferralsConditionSetEditor.Size = new System.Drawing.Size(704, 223);
			this._conferralsConditionSetEditor.TabIndex = 0;
			this._conferralsConditionSetEditor.View = null;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(724, 543);
			this.Controls.Add(this.panel1);
			this.EditorName = "Social Importance Editor";
			this.Name = "MainForm";
			this.Text = "";
			this.Controls.SetChildIndex(this.panel1, 0);
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl _attRuleConditionSetEditor;
		private GAIPS.AssetEditorTools.GenericPropertyDataGridControler _claimDataView;
		private GAIPS.AssetEditorTools.GenericPropertyDataGridControler _attRulesDataView;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox4;
		private GAIPS.AssetEditorTools.GenericPropertyDataGridControler _conferralsDataView;
		private GAIPS.AssetEditorTools.ConditionSetEditorControl _conferralsConditionSetEditor;
	}
}