namespace GAIPS.AssetEditorTools
{
	partial class ProgressForm
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
			this._bar = new System.Windows.Forms.ProgressBar();
			this._progressMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// _bar
			// 
			this._bar.Location = new System.Drawing.Point(12, 12);
			this._bar.Maximum = 10000;
			this._bar.Name = "_bar";
			this._bar.Size = new System.Drawing.Size(460, 23);
			this._bar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this._bar.TabIndex = 1;
			this._bar.UseWaitCursor = true;
			// 
			// _progressMessage
			// 
			this._progressMessage.Location = new System.Drawing.Point(12, 38);
			this._progressMessage.Name = "_progressMessage";
			this._progressMessage.Size = new System.Drawing.Size(460, 20);
			this._progressMessage.TabIndex = 2;
			this._progressMessage.Text = "Lorem ipsum dolor sit amet, cu est clita tractatos, ei probo altera meliore mea. " +
    "Ius wisi debet iudicabit te.";
			this._progressMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this._progressMessage.UseWaitCursor = true;
			// 
			// ProgressForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 70);
			this.ControlBox = false;
			this.Controls.Add(this._progressMessage);
			this.Controls.Add(this._bar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ProgressForm";
			this.Text = "ProgressForm";
			this.UseWaitCursor = true;
			this.Shown += new System.EventHandler(this.OnShown);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar _bar;
		private System.Windows.Forms.Label _progressMessage;
	}
}