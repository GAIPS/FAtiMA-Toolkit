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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.addEffect = new System.Windows.Forms.Button();
            this.observerName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.newValue = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.propertyName = new GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Property Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label2.Location = new System.Drawing.Point(12, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "New Value";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F);
            this.label4.Location = new System.Drawing.Point(12, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Observer Agent";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // addEffect
            // 
            this.addEffect.Font = new System.Drawing.Font("Arial", 9.75F);
            this.addEffect.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.addEffect.Location = new System.Drawing.Point(224, 186);
            this.addEffect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addEffect.Name = "addEffect";
            this.addEffect.Size = new System.Drawing.Size(125, 33);
            this.addEffect.TabIndex = 80;
            this.addEffect.Text = "Add";
            this.addEffect.UseVisualStyleBackColor = true;
            this.addEffect.Click += new System.EventHandler(this.addEffect_Click);
            // 
            // observerName
            // 
            this.observerName.AllowComposedName = true;
            this.observerName.AllowLiteral = true;
            this.observerName.AllowNil = true;
            this.observerName.AllowUniversal = true;
            this.observerName.AllowVariable = true;
            this.observerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.observerName.Font = new System.Drawing.Font("Arial", 9.75F);
            this.observerName.Location = new System.Drawing.Point(183, 134);
            this.observerName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.observerName.Name = "observerName";
            this.observerName.OnlyIntOrVariable = false;
            this.observerName.Size = new System.Drawing.Size(379, 26);
            this.observerName.TabIndex = 6;
            this.observerName.TextChanged += new System.EventHandler(this.observerName_TextChanged);
            // 
            // newValue
            // 
            this.newValue.AllowComposedName = true;
            this.newValue.AllowLiteral = true;
            this.newValue.AllowNil = true;
            this.newValue.AllowUniversal = true;
            this.newValue.AllowVariable = true;
            this.newValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newValue.Font = new System.Drawing.Font("Arial", 9.75F);
            this.newValue.Location = new System.Drawing.Point(183, 92);
            this.newValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newValue.Name = "newValue";
            this.newValue.OnlyIntOrVariable = false;
            this.newValue.Size = new System.Drawing.Size(379, 26);
            this.newValue.TabIndex = 2;
            this.newValue.TextChanged += new System.EventHandler(this.wfNameFieldBox2_TextChanged);
            // 
            // propertyName
            // 
            this.propertyName.AllowComposedName = true;
            this.propertyName.AllowLiteral = true;
            this.propertyName.AllowNil = true;
            this.propertyName.AllowUniversal = true;
            this.propertyName.AllowVariable = true;
            this.propertyName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.propertyName.Font = new System.Drawing.Font("Arial", 9.75F);
            this.propertyName.Location = new System.Drawing.Point(183, 48);
            this.propertyName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.propertyName.Name = "propertyName";
            this.propertyName.OnlyIntOrVariable = false;
            this.propertyName.Size = new System.Drawing.Size(379, 26);
            this.propertyName.TabIndex = 0;
            this.propertyName.TextChanged += new System.EventHandler(this.propertyName_TextChanged);
            // 
            // AddorEditEffect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 233);
            this.Controls.Add(this.addEffect);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.observerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.newValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.propertyName);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AddorEditEffect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddorEditEffect";
            this.Load += new System.EventHandler(this.AddorEditEffect_Load);
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
        private System.Windows.Forms.Label label4;
        private GAIPS.AssetEditorTools.TypedTextBoxes.WFNameFieldBox observerName;
        private System.Windows.Forms.Button addEffect;
    }
}