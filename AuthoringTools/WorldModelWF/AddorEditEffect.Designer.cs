namespace WorldModelWF
{
    partial class AddorEditEffect
    {

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertyName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newValue = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.Oberver = new System.Windows.Forms.Label();
            this.responsibleAgent = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.label4 = new System.Windows.Forms.Label();
            this.observerName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.addEffect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // propertyName
            // 
            this.propertyName.AllowComposedName = true;
            this.propertyName.AllowLiteral = true;
            this.propertyName.AllowNil = true;
            this.propertyName.AllowUniversal = true;
            this.propertyName.AllowVariable = true;
            this.propertyName.Location = new System.Drawing.Point(183, 48);
            this.propertyName.Name = "propertyName";
            this.propertyName.OnlyIntOrVariable = false;
            this.propertyName.Size = new System.Drawing.Size(163, 22);
            this.propertyName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Property Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Value";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // newValue
            // 
            this.newValue.AllowComposedName = true;
            this.newValue.AllowLiteral = true;
            this.newValue.AllowNil = true;
            this.newValue.AllowUniversal = true;
            this.newValue.AllowVariable = true;
            this.newValue.Location = new System.Drawing.Point(183, 92);
            this.newValue.Name = "newValue";
            this.newValue.OnlyIntOrVariable = false;
            this.newValue.Size = new System.Drawing.Size(163, 22);
            this.newValue.TabIndex = 2;
            this.newValue.TextChanged += new System.EventHandler(this.wfNameFieldBox2_TextChanged);
            // 
            // Oberver
            // 
            this.Oberver.AutoSize = true;
            this.Oberver.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Oberver.Location = new System.Drawing.Point(12, 136);
            this.Oberver.Name = "Oberver";
            this.Oberver.Size = new System.Drawing.Size(149, 20);
            this.Oberver.TabIndex = 5;
            this.Oberver.Text = "Responsible Agent";
            // 
            // responsibleAgent
            // 
            this.responsibleAgent.AllowComposedName = true;
            this.responsibleAgent.AllowLiteral = true;
            this.responsibleAgent.AllowNil = true;
            this.responsibleAgent.AllowUniversal = true;
            this.responsibleAgent.AllowVariable = true;
            this.responsibleAgent.Location = new System.Drawing.Point(183, 136);
            this.responsibleAgent.Name = "responsibleAgent";
            this.responsibleAgent.OnlyIntOrVariable = false;
            this.responsibleAgent.Size = new System.Drawing.Size(163, 22);
            this.responsibleAgent.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Observer";
            // 
            // observerName
            // 
            this.observerName.AllowComposedName = true;
            this.observerName.AllowLiteral = true;
            this.observerName.AllowNil = true;
            this.observerName.AllowUniversal = true;
            this.observerName.AllowVariable = true;
            this.observerName.Location = new System.Drawing.Point(183, 179);
            this.observerName.Name = "observerName";
            this.observerName.OnlyIntOrVariable = false;
            this.observerName.Size = new System.Drawing.Size(163, 22);
            this.observerName.TabIndex = 6;
            // 
            // addEffect
            // 
            this.addEffect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.addEffect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addEffect.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.addEffect.Location = new System.Drawing.Point(113, 224);
            this.addEffect.Name = "addEffect";
            this.addEffect.Size = new System.Drawing.Size(126, 41);
            this.addEffect.TabIndex = 8;
            this.addEffect.Text = "Add";
            this.addEffect.UseVisualStyleBackColor = true;
            this.addEffect.Click += new System.EventHandler(this.addEffect_Click);
            // 
            // AddorEditEffect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 296);
            this.Controls.Add(this.addEffect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.observerName);
            this.Controls.Add(this.Oberver);
            this.Controls.Add(this.responsibleAgent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertyName);
            this.Name = "AddorEditEffect";
            this.Text = "AddorEditEffect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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



        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox propertyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox newValue;
        private System.Windows.Forms.Label Oberver;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox responsibleAgent;
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox observerName;
        private System.Windows.Forms.Button addEffect;
    }
}