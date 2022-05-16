
namespace IntegratedAuthoringToolWF.DialogueHelpers
{
    partial class GenerateDialogueActions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateDialogueActions));
            this.label1 = new System.Windows.Forms.Label();
            this.initialStateBox = new System.Windows.Forms.TextBox();
            this.endStateBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.stateNumberBox = new GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Initial State:";
            this.toolTip1.SetToolTip(this.label1, "The Name of the state the dialogue is supposed to start");
            // 
            // initialStateBox
            // 
            this.initialStateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialStateBox.Location = new System.Drawing.Point(168, 27);
            this.initialStateBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.initialStateBox.Name = "initialStateBox";
            this.initialStateBox.Size = new System.Drawing.Size(211, 26);
            this.initialStateBox.TabIndex = 1;
            this.initialStateBox.Text = "Start";
            this.initialStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endStateBox
            // 
            this.endStateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endStateBox.Location = new System.Drawing.Point(168, 79);
            this.endStateBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.endStateBox.Name = "endStateBox";
            this.endStateBox.Size = new System.Drawing.Size(211, 26);
            this.endStateBox.TabIndex = 3;
            this.endStateBox.Text = "End";
            this.endStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.endStateBox, "The name of the state where the dialogue is supposed to end");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "End State:";
            this.toolTip1.SetToolTip(this.label2, "The name of the state where the dialogue is supposed to end");
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(100, 207);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(241, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate Dialogue Actions";
            this.toolTip1.SetToolTip(this.button1, "Automatically generate the number of states defined above");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stateNumberBox
            // 
            this.stateNumberBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateNumberBox.Location = new System.Drawing.Point(220, 139);
            this.stateNumberBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.stateNumberBox.Name = "stateNumberBox";
            this.stateNumberBox.Size = new System.Drawing.Size(92, 26);
            this.stateNumberBox.TabIndex = 5;
            this.stateNumberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.stateNumberBox, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 143);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Number of States:";
            this.toolTip1.SetToolTip(this.label3, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // GenerateDialogueActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 267);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stateNumberBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.endStateBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.initialStateBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GenerateDialogueActions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenerateDialogueActions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox initialStateBox;
        private System.Windows.Forms.TextBox endStateBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox stateNumberBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}