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
			this.button1 = new System.Windows.Forms.Button();
			this._voiceComboBox = new System.Windows.Forms.ComboBox();
			this._speachRateSlider = new System.Windows.Forms.TrackBar();
			this._rateValueLabel = new System.Windows.Forms.Label();
			this._pitchValueLabel = new System.Windows.Forms.Label();
			this._pitchSlider = new System.Windows.Forms.TrackBar();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._speachRateSlider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this._pitchSlider)).BeginInit();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Location = new System.Drawing.Point(13, 23);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(539, 119);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Hello, World!";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(335, 166);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(109, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Test Speech";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.OnTestButtonClick);
			// 
			// _voiceComboBox
			// 
			this._voiceComboBox.FormattingEnabled = true;
			this._voiceComboBox.Location = new System.Drawing.Point(13, 149);
			this._voiceComboBox.Name = "_voiceComboBox";
			this._voiceComboBox.Size = new System.Drawing.Size(290, 21);
			this._voiceComboBox.TabIndex = 2;
			// 
			// _speachRateSlider
			// 
			this._speachRateSlider.Location = new System.Drawing.Point(12, 176);
			this._speachRateSlider.Maximum = 100;
			this._speachRateSlider.Minimum = -100;
			this._speachRateSlider.Name = "_speachRateSlider";
			this._speachRateSlider.Size = new System.Drawing.Size(291, 45);
			this._speachRateSlider.TabIndex = 3;
			this._speachRateSlider.TickStyle = System.Windows.Forms.TickStyle.None;
			this._speachRateSlider.ValueChanged += new System.EventHandler(this.OnRateValueChanged);
			// 
			// _rateValueLabel
			// 
			this._rateValueLabel.AutoSize = true;
			this._rateValueLabel.Location = new System.Drawing.Point(24, 207);
			this._rateValueLabel.Name = "_rateValueLabel";
			this._rateValueLabel.Size = new System.Drawing.Size(84, 13);
			this._rateValueLabel.TabIndex = 4;
			this._rateValueLabel.Text = "_rateValueLabel";
			// 
			// _pitchValueLabel
			// 
			this._pitchValueLabel.AutoSize = true;
			this._pitchValueLabel.Location = new System.Drawing.Point(24, 258);
			this._pitchValueLabel.Name = "_pitchValueLabel";
			this._pitchValueLabel.Size = new System.Drawing.Size(89, 13);
			this._pitchValueLabel.TabIndex = 6;
			this._pitchValueLabel.Text = "_pitchValueLabel";
			// 
			// _pitchSlider
			// 
			this._pitchSlider.Location = new System.Drawing.Point(12, 227);
			this._pitchSlider.Minimum = -10;
			this._pitchSlider.Name = "_pitchSlider";
			this._pitchSlider.Size = new System.Drawing.Size(291, 45);
			this._pitchSlider.TabIndex = 5;
			this._pitchSlider.TickStyle = System.Windows.Forms.TickStyle.None;
			this._pitchSlider.ValueChanged += new System.EventHandler(this.OnPitchValueChanged);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(335, 217);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Generate";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.OnGenerateButtonClick);
			// 
			// TextToSpeechForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(564, 437);
			this.Controls.Add(this.button2);
			this.Controls.Add(this._pitchValueLabel);
			this.Controls.Add(this._pitchSlider);
			this.Controls.Add(this._rateValueLabel);
			this.Controls.Add(this._speachRateSlider);
			this.Controls.Add(this._voiceComboBox);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Name = "TextToSpeechForm";
			this.Text = "Form1";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
			((System.ComponentModel.ISupportInitialize)(this._speachRateSlider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this._pitchSlider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox _voiceComboBox;
		private System.Windows.Forms.TrackBar _speachRateSlider;
		private System.Windows.Forms.Label _rateValueLabel;
		private System.Windows.Forms.Label _pitchValueLabel;
		private System.Windows.Forms.TrackBar _pitchSlider;
		private System.Windows.Forms.Button button2;
	}
}