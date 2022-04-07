namespace EmotionalDecisionMakingWF
{
    partial class AddOrEditReactionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddOrEditReactionForm));
            this.emotionalAppraisalAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.addOrEditButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBeliefErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxAction = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxTarget = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxPriority = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.textBoxLayer = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(50, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 14;
            this.label1.Text = "Name:";
            this.toolTip1.SetToolTip(this.label1, "Name of the action, is a well-formed name that can be either a symbol (e.g. Smile" +
        ", Eat) ");
            // 
            // addOrEditButton
            // 
            this.addOrEditButton.Location = new System.Drawing.Point(93, 319);
            this.addOrEditButton.Margin = new System.Windows.Forms.Padding(4);
            this.addOrEditButton.Name = "addOrEditButton";
            this.addOrEditButton.Size = new System.Drawing.Size(134, 36);
            this.addOrEditButton.TabIndex = 25;
            this.addOrEditButton.Text = "Add Action Rule";
            this.addOrEditButton.UseVisualStyleBackColor = true;
            this.addOrEditButton.Click += new System.EventHandler(this.addOrEditButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 16;
            this.label3.Text = "Priority:";
            this.toolTip1.SetToolTip(this.label3, "The |priority| is a numerical value that represents how important it is for the a" +
        "gent to execute the action.");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 110);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Target:";
            this.toolTip1.SetToolTip(this.label2, "The Target of the action is the name of the agent or object that the action is ai" +
        "med at");
            // 
            // addBeliefErrorProvider
            // 
            this.addBeliefErrorProvider.ContainerControl = this;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 242);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 26;
            this.label4.Text = "Layer:";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // textBoxAction
            // 
            this.textBoxAction.AllowComposedName = true;
            this.textBoxAction.AllowLiteral = true;
            this.textBoxAction.AllowNil = true;
            this.textBoxAction.AllowUniversal = true;
            this.textBoxAction.AllowUniversalLiteral = true;
            this.textBoxAction.AllowVariable = true;
            this.textBoxAction.Location = new System.Drawing.Point(53, 68);
            this.textBoxAction.Name = "textBoxAction";
            this.textBoxAction.OnlyIntOrVariable = false;
            this.textBoxAction.Size = new System.Drawing.Size(223, 22);
            this.textBoxAction.TabIndex = 27;
            this.toolTip1.SetToolTip(this.textBoxAction, "Name of the action, is a well-formed name that can be either a symbol (e.g. Smile" +
        ", Eat) \r\nor a composed name (e.g. Offer(Gift)) in which the inner symbols are tr" +
        "eated as the action\'s parameters");
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.AllowComposedName = true;
            this.textBoxTarget.AllowLiteral = true;
            this.textBoxTarget.AllowNil = true;
            this.textBoxTarget.AllowUniversal = true;
            this.textBoxTarget.AllowUniversalLiteral = true;
            this.textBoxTarget.AllowVariable = true;
            this.textBoxTarget.Location = new System.Drawing.Point(53, 129);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.OnlyIntOrVariable = false;
            this.textBoxTarget.Size = new System.Drawing.Size(223, 22);
            this.textBoxTarget.TabIndex = 28;
            this.toolTip1.SetToolTip(this.textBoxTarget, "The Target of the action is the name of the agent or object that the action is ai" +
        "med at");
            // 
            // textBoxPriority
            // 
            this.textBoxPriority.AllowComposedName = true;
            this.textBoxPriority.AllowLiteral = true;
            this.textBoxPriority.AllowNil = true;
            this.textBoxPriority.AllowUniversal = true;
            this.textBoxPriority.AllowUniversalLiteral = true;
            this.textBoxPriority.AllowVariable = true;
            this.textBoxPriority.Location = new System.Drawing.Point(53, 193);
            this.textBoxPriority.Name = "textBoxPriority";
            this.textBoxPriority.OnlyIntOrVariable = false;
            this.textBoxPriority.Size = new System.Drawing.Size(223, 22);
            this.textBoxPriority.TabIndex = 29;
            this.toolTip1.SetToolTip(this.textBoxPriority, "The |priority| is a numerical value that represents how important it is for the a" +
        "gent to execute the action.\r\nWe reccomend using a 0-10 range");
            // 
            // textBoxLayer
            // 
            this.textBoxLayer.AllowComposedName = true;
            this.textBoxLayer.AllowLiteral = true;
            this.textBoxLayer.AllowNil = true;
            this.textBoxLayer.AllowUniversal = true;
            this.textBoxLayer.AllowUniversalLiteral = true;
            this.textBoxLayer.AllowVariable = true;
            this.textBoxLayer.Location = new System.Drawing.Point(53, 261);
            this.textBoxLayer.Name = "textBoxLayer";
            this.textBoxLayer.OnlyIntOrVariable = false;
            this.textBoxLayer.Size = new System.Drawing.Size(223, 22);
            this.textBoxLayer.TabIndex = 30;
            this.toolTip1.SetToolTip(this.textBoxLayer, resources.GetString("textBoxLayer.ToolTip"));
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label5.Location = new System.Drawing.Point(139, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Walk, Speak, Eat([food])";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label6.Location = new System.Drawing.Point(159, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "John, beach, [target]";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label7.Location = new System.Drawing.Point(238, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "[0-10]";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.label8.Location = new System.Drawing.Point(182, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 15);
            this.label8.TabIndex = 34;
            this.label8.Text = "Reactive, Social";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Action Rule";
            // 
            // AddOrEditReactionForm
            // 
            this.AcceptButton = this.addOrEditButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(315, 368);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLayer);
            this.Controls.Add(this.textBoxPriority);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.textBoxAction);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addOrEditButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "AddOrEditReactionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Action Rule";
            this.Load += new System.EventHandler(this.AddOrEditReactionForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOrEditReactionForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.emotionalAppraisalAssetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addBeliefErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource emotionalAppraisalAssetBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addOrEditButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider addBeliefErrorProvider;
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxLayer;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxPriority;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxTarget;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox textBoxAction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}