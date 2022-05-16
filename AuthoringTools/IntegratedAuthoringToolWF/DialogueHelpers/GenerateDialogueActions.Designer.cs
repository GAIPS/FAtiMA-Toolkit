
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
            this.label4 = new System.Windows.Forms.Label();
            this.optionsPerStateBox = new GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Initial State:";
            this.toolTip1.SetToolTip(this.label1, "The Name of the state the dialogue is supposed to start");
            // 
            // initialStateBox
            // 
            this.initialStateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialStateBox.Location = new System.Drawing.Point(126, 22);
            this.initialStateBox.Name = "initialStateBox";
            this.initialStateBox.Size = new System.Drawing.Size(159, 22);
            this.initialStateBox.TabIndex = 1;
            this.initialStateBox.Text = "Start";
            this.initialStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // endStateBox
            // 
            this.endStateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endStateBox.Location = new System.Drawing.Point(126, 61);
            this.endStateBox.Name = "endStateBox";
            this.endStateBox.Size = new System.Drawing.Size(159, 22);
            this.endStateBox.TabIndex = 3;
            this.endStateBox.Text = "End";
            this.endStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.endStateBox, "The name of the state where the dialogue is supposed to end");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "End State:";
            this.toolTip1.SetToolTip(this.label2, "The name of the state where the dialogue is supposed to end");
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(75, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "Generate Dialogue Actions";
            this.toolTip1.SetToolTip(this.button1, "Automatically generate the number of states defined above");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // stateNumberBox
            // 
            this.stateNumberBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateNumberBox.Location = new System.Drawing.Point(215, 98);
            this.stateNumberBox.Name = "stateNumberBox";
            this.stateNumberBox.Size = new System.Drawing.Size(70, 22);
            this.stateNumberBox.TabIndex = 5;
            this.stateNumberBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.stateNumberBox, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(45, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Number of States:";
            this.toolTip1.SetToolTip(this.label3, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Dialogues per State";
            this.toolTip1.SetToolTip(this.label4, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // optionsPerStateBox
            // 
            this.optionsPerStateBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optionsPerStateBox.Location = new System.Drawing.Point(215, 126);
            this.optionsPerStateBox.Name = "optionsPerStateBox";
            this.optionsPerStateBox.Size = new System.Drawing.Size(70, 22);
            this.optionsPerStateBox.TabIndex = 7;
            this.optionsPerStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.optionsPerStateBox, "Number of states to be automatically generated by this tool. You can edit them la" +
        "ter.");
            // 
            // GenerateDialogueActions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 217);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.optionsPerStateBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stateNumberBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.endStateBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.initialStateBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.Int32FieldBox optionsPerStateBox;
    }
}