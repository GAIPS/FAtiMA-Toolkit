namespace IntegratedAuthoringToolWF
{
	partial class TextToSpeechForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._testSpeechButton = new System.Windows.Forms.Button();
            this._voiceComboBox = new System.Windows.Forms.ComboBox();
            this._speachRateSlider = new System.Windows.Forms.TrackBar();
            this._pitchValueLabel = new System.Windows.Forms.Label();
            this._pitchSlider = new System.Windows.Forms.TrackBar();
            this._generateAllButton = new System.Windows.Forms.Button();
            this._rateTextBox = new System.Windows.Forms.TextBox();
            this._dialogOptions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this._visemeDisplay = new System.Windows.Forms.PictureBox();
            this._generateButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._speachRateSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pitchSlider)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._visemeDisplay)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(4, 45);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(623, 235);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Hello, World!";
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // _testSpeechButton
            // 
            this._testSpeechButton.AutoSize = true;
            this._testSpeechButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._testSpeechButton.Dock = System.Windows.Forms.DockStyle.Top;
            this._testSpeechButton.Location = new System.Drawing.Point(4, 4);
            this._testSpeechButton.Margin = new System.Windows.Forms.Padding(4);
            this._testSpeechButton.Name = "_testSpeechButton";
            this._testSpeechButton.Size = new System.Drawing.Size(109, 30);
            this._testSpeechButton.TabIndex = 1;
            this._testSpeechButton.Text = "Test Speech";
            this._testSpeechButton.UseVisualStyleBackColor = true;
            this._testSpeechButton.Click += new System.EventHandler(this.OnTestButtonClick);
            // 
            // _voiceComboBox
            // 
            this._voiceComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._voiceComboBox.FormattingEnabled = true;
            this._voiceComboBox.Location = new System.Drawing.Point(4, 4);
            this._voiceComboBox.Margin = new System.Windows.Forms.Padding(4);
            this._voiceComboBox.Name = "_voiceComboBox";
            this._voiceComboBox.Size = new System.Drawing.Size(482, 28);
            this._voiceComboBox.TabIndex = 2;
            this._voiceComboBox.SelectionChangeCommitted += new System.EventHandler(this.OnVoiceSelectionChange);
            // 
            // _speachRateSlider
            // 
            this._speachRateSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._speachRateSlider.Location = new System.Drawing.Point(4, 55);
            this._speachRateSlider.Margin = new System.Windows.Forms.Padding(4);
            this._speachRateSlider.Maximum = 3000;
            this._speachRateSlider.Minimum = 300;
            this._speachRateSlider.Name = "_speachRateSlider";
            this._speachRateSlider.Size = new System.Drawing.Size(482, 43);
            this._speachRateSlider.TabIndex = 3;
            this._speachRateSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this._speachRateSlider.Value = 1000;
            this._speachRateSlider.ValueChanged += new System.EventHandler(this.OnRateValueChanged);
            // 
            // _pitchValueLabel
            // 
            this._pitchValueLabel.AutoSize = true;
            this._pitchValueLabel.Location = new System.Drawing.Point(104, 0);
            this._pitchValueLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._pitchValueLabel.Name = "_pitchValueLabel";
            this._pitchValueLabel.Size = new System.Drawing.Size(115, 40);
            this._pitchValueLabel.TabIndex = 6;
            this._pitchValueLabel.Text = "_pitchValueLabel";
            // 
            // _pitchSlider
            // 
            this._pitchSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pitchSlider.Location = new System.Drawing.Point(4, 157);
            this._pitchSlider.Margin = new System.Windows.Forms.Padding(4);
            this._pitchSlider.Minimum = -10;
            this._pitchSlider.Name = "_pitchSlider";
            this._pitchSlider.Size = new System.Drawing.Size(482, 43);
            this._pitchSlider.TabIndex = 5;
            this._pitchSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this._pitchSlider.ValueChanged += new System.EventHandler(this.OnPitchValueChanged);
            // 
            // _generateAllButton
            // 
            this._generateAllButton.Dock = System.Windows.Forms.DockStyle.Top;
            this._generateAllButton.Location = new System.Drawing.Point(4, 209);
            this._generateAllButton.Margin = new System.Windows.Forms.Padding(4);
            this._generateAllButton.Name = "_generateAllButton";
            this._generateAllButton.Size = new System.Drawing.Size(109, 42);
            this._generateAllButton.TabIndex = 7;
            this._generateAllButton.Text = "Generate All Dialogs";
            this._generateAllButton.UseVisualStyleBackColor = true;
            this._generateAllButton.Click += new System.EventHandler(this.OnGenerateAllButtonClick);
            // 
            // _rateTextBox
            // 
            this._rateTextBox.Location = new System.Drawing.Point(104, 4);
            this._rateTextBox.Margin = new System.Windows.Forms.Padding(4);
            this._rateTextBox.Name = "_rateTextBox";
            this._rateTextBox.Size = new System.Drawing.Size(132, 26);
            this._rateTextBox.TabIndex = 8;
            this._rateTextBox.Validated += new System.EventHandler(this.OnValidatedRateTextBox);
            // 
            // _dialogOptions
            // 
            this._dialogOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dialogOptions.FormattingEnabled = true;
            this._dialogOptions.Location = new System.Drawing.Point(155, 4);
            this._dialogOptions.Margin = new System.Windows.Forms.Padding(4);
            this._dialogOptions.Name = "_dialogOptions";
            this._dialogOptions.Size = new System.Drawing.Size(464, 28);
            this._dialogOptions.TabIndex = 9;
            this._dialogOptions.SelectedIndexChanged += new System.EventHandler(this._dialogOptions_SelectedIndexChanged);
            this._dialogOptions.SelectionChangeCommitted += new System.EventHandler(this._dialogOptions_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 33);
            this.label1.TabIndex = 10;
            this.label1.Text = "Available Dialogs:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._dialogOptions, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(623, 33);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 6);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 271F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(631, 555);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(4, 288);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(623, 263);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this._testSpeechButton, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this._generateAllButton, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this._visemeDisplay, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this._generateButton, 0, 4);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(502, 4);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(117, 255);
            this.tableLayoutPanel5.TabIndex = 13;
            // 
            // _visemeDisplay
            // 
            this._visemeDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this._visemeDisplay.ErrorImage = global::IntegratedAuthoringToolWF.Properties.Resources._0_silence;
            this._visemeDisplay.Location = new System.Drawing.Point(5, 46);
            this._visemeDisplay.Margin = new System.Windows.Forms.Padding(4);
            this._visemeDisplay.Name = "_visemeDisplay";
            this._visemeDisplay.Size = new System.Drawing.Size(107, 101);
            this._visemeDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._visemeDisplay.TabIndex = 8;
            this._visemeDisplay.TabStop = false;
            // 
            // _generateButton
            // 
            this._generateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._generateButton.Dock = System.Windows.Forms.DockStyle.Top;
            this._generateButton.Location = new System.Drawing.Point(4, 159);
            this._generateButton.Margin = new System.Windows.Forms.Padding(4);
            this._generateButton.Name = "_generateButton";
            this._generateButton.Size = new System.Drawing.Size(109, 42);
            this._generateButton.TabIndex = 9;
            this._generateButton.Text = "Generate Single Text";
            this._generateButton.UseVisualStyleBackColor = true;
            this._generateButton.Click += new System.EventHandler(this.OnGenerateSingleButtonClick);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this._voiceComboBox, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this._pitchSlider, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this._speachRateSlider, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(490, 255);
            this.tableLayoutPanel4.TabIndex = 13;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel7.Controls.Add(this._pitchValueLabel, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 204);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel7.Size = new System.Drawing.Size(490, 51);
            this.tableLayoutPanel7.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Margin = new System.Windows.Forms.Padding(4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 43);
            this.label3.TabIndex = 7;
            this.label3.Text = "Pitch Shift:";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this._rateTextBox, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 102);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(490, 51);
            this.tableLayoutPanel6.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 40);
            this.label2.TabIndex = 9;
            this.label2.Text = "Speak Rate:";
            // 
            // TextToSpeechForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 567);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(661, 606);
            this.Name = "TextToSpeechForm";
            this.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Text = "Text-To-Speech";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            ((System.ComponentModel.ISupportInitialize)(this._speachRateSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pitchSlider)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._visemeDisplay)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button _testSpeechButton;
		private System.Windows.Forms.ComboBox _voiceComboBox;
		private System.Windows.Forms.TrackBar _speachRateSlider;
		private System.Windows.Forms.Label _pitchValueLabel;
		private System.Windows.Forms.TrackBar _pitchSlider;
		private System.Windows.Forms.Button _generateAllButton;
		private System.Windows.Forms.TextBox _rateTextBox;
		private System.Windows.Forms.ComboBox _dialogOptions;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.PictureBox _visemeDisplay;
		private System.Windows.Forms.Button _generateButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
		private System.Windows.Forms.Label label2;
	}
}