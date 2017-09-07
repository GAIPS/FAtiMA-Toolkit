namespace CommeillFautWF
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SocialExchangeBox = new System.Windows.Forms.ListBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.TriggerRulesBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.AddTriggerRule = new System.Windows.Forms.Button();
            this.EditTriggerRule = new System.Windows.Forms.Button();
            this.DeleteTriggerRule = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(318, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Location = new System.Drawing.Point(0, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(658, 196);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SocialExchange";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(379, 28);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "Refresh";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SocialExchangeBox
            // 
            this.SocialExchangeBox.FormattingEnabled = true;
            this.SocialExchangeBox.ItemHeight = 17;
            this.SocialExchangeBox.Location = new System.Drawing.Point(16, 120);
            this.SocialExchangeBox.Name = "SocialExchangeBox";
            this.SocialExchangeBox.Size = new System.Drawing.Size(499, 89);
            this.SocialExchangeBox.TabIndex = 6;
            this.SocialExchangeBox.SelectedIndexChanged += new System.EventHandler(this.SocialExchangeBox_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(262, 28);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 32);
            this.button3.TabIndex = 7;
            this.button3.Text = "Remove";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(146, 28);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 32);
            this.button4.TabIndex = 8;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TriggerRulesBox
            // 
            this.TriggerRulesBox.FormattingEnabled = true;
            this.TriggerRulesBox.ItemHeight = 17;
            this.TriggerRulesBox.Location = new System.Drawing.Point(16, 336);
            this.TriggerRulesBox.Name = "TriggerRulesBox";
            this.TriggerRulesBox.Size = new System.Drawing.Size(499, 89);
            this.TriggerRulesBox.TabIndex = 9;
            this.TriggerRulesBox.SelectedIndexChanged += new System.EventHandler(this.TriggerRulesBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "TriggerRules";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // AddTriggerRule
            // 
            this.AddTriggerRule.Location = new System.Drawing.Point(16, 279);
            this.AddTriggerRule.Name = "AddTriggerRule";
            this.AddTriggerRule.Size = new System.Drawing.Size(113, 37);
            this.AddTriggerRule.TabIndex = 11;
            this.AddTriggerRule.Text = "Add";
            this.AddTriggerRule.UseVisualStyleBackColor = true;
            this.AddTriggerRule.Click += new System.EventHandler(this.AddTriggerRule_Click);
            // 
            // EditTriggerRule
            // 
            this.EditTriggerRule.Location = new System.Drawing.Point(146, 279);
            this.EditTriggerRule.Name = "EditTriggerRule";
            this.EditTriggerRule.Size = new System.Drawing.Size(113, 37);
            this.EditTriggerRule.TabIndex = 12;
            this.EditTriggerRule.Text = "Edit";
            this.EditTriggerRule.UseVisualStyleBackColor = true;
            this.EditTriggerRule.Click += new System.EventHandler(this.EditTriggerRule_Click);
            // 
            // DeleteTriggerRule
            // 
            this.DeleteTriggerRule.Location = new System.Drawing.Point(290, 279);
            this.DeleteTriggerRule.Name = "DeleteTriggerRule";
            this.DeleteTriggerRule.Size = new System.Drawing.Size(113, 37);
            this.DeleteTriggerRule.TabIndex = 13;
            this.DeleteTriggerRule.Text = "Delete";
            this.DeleteTriggerRule.UseVisualStyleBackColor = true;
            this.DeleteTriggerRule.Click += new System.EventHandler(this.DeleteTriggerRule_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1385, 479);
            this.Controls.Add(this.DeleteTriggerRule);
            this.Controls.Add(this.EditTriggerRule);
            this.Controls.Add(this.AddTriggerRule);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TriggerRulesBox);
            this.Controls.Add(this.SocialExchangeBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Comme ill Faut";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Controls.SetChildIndex(this.groupBox1, 0);
            this.Controls.SetChildIndex(this.SocialExchangeBox, 0);
            this.Controls.SetChildIndex(this.TriggerRulesBox, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.AddTriggerRule, 0);
            this.Controls.SetChildIndex(this.EditTriggerRule, 0);
            this.Controls.SetChildIndex(this.DeleteTriggerRule, 0);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox SocialExchangeBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox TriggerRulesBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button AddTriggerRule;
        private System.Windows.Forms.Button EditTriggerRule;
        private System.Windows.Forms.Button DeleteTriggerRule;
    }
}

