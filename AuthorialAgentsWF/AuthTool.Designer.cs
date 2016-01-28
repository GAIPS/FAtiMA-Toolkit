namespace AuthorialAgentsWF
{
    partial class AuthTool
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
            this.learningGoalsPage = new System.Windows.Forms.TabPage();
            this.learningGoalsLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.learningGoalsList = new System.Windows.Forms.ListBox();
            this.removeLearningGoals = new System.Windows.Forms.Button();
            this.editLearningGoals = new System.Windows.Forms.Button();
            this.addLearningGoal = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editActions = new System.Windows.Forms.Button();
            this.removeScenarios = new System.Windows.Forms.Button();
            this.editScenarios = new System.Windows.Forms.Button();
            this.addScenario = new System.Windows.Forms.Button();
            this.learningGoalsPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // learningGoalsPage
            // 
            this.learningGoalsPage.Controls.Add(this.learningGoalsLabel);
            this.learningGoalsPage.Controls.Add(this.panel1);
            this.learningGoalsPage.Controls.Add(this.removeLearningGoals);
            this.learningGoalsPage.Controls.Add(this.editLearningGoals);
            this.learningGoalsPage.Controls.Add(this.addLearningGoal);
            this.learningGoalsPage.Location = new System.Drawing.Point(4, 22);
            this.learningGoalsPage.Name = "learningGoalsPage";
            this.learningGoalsPage.Padding = new System.Windows.Forms.Padding(3);
            this.learningGoalsPage.Size = new System.Drawing.Size(600, 228);
            this.learningGoalsPage.TabIndex = 7;
            this.learningGoalsPage.Text = "Learning Goals";
            this.learningGoalsPage.UseVisualStyleBackColor = true;
            // 
            // learningGoalsLabel
            // 
            this.learningGoalsLabel.AutoSize = true;
            this.learningGoalsLabel.Location = new System.Drawing.Point(6, 16);
            this.learningGoalsLabel.Name = "learningGoalsLabel";
            this.learningGoalsLabel.Size = new System.Drawing.Size(78, 13);
            this.learningGoalsLabel.TabIndex = 4;
            this.learningGoalsLabel.Text = "Learning Goals";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.learningGoalsList);
            this.panel1.Location = new System.Drawing.Point(6, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(588, 142);
            this.panel1.TabIndex = 3;
            // 
            // learningGoalsList
            // 
            this.learningGoalsList.FormattingEnabled = true;
            this.learningGoalsList.Location = new System.Drawing.Point(3, 11);
            this.learningGoalsList.Name = "learningGoalsList";
            this.learningGoalsList.Size = new System.Drawing.Size(582, 121);
            this.learningGoalsList.TabIndex = 0;
            // 
            // removeLearningGoals
            // 
            this.removeLearningGoals.Location = new System.Drawing.Point(225, 180);
            this.removeLearningGoals.Name = "removeLearningGoals";
            this.removeLearningGoals.Size = new System.Drawing.Size(75, 23);
            this.removeLearningGoals.TabIndex = 2;
            this.removeLearningGoals.Text = "Remove";
            this.removeLearningGoals.UseVisualStyleBackColor = true;
            this.removeLearningGoals.Click += new System.EventHandler(this.removeLearningGoals_Click);
            // 
            // editLearningGoals
            // 
            this.editLearningGoals.Location = new System.Drawing.Point(115, 180);
            this.editLearningGoals.Name = "editLearningGoals";
            this.editLearningGoals.Size = new System.Drawing.Size(75, 23);
            this.editLearningGoals.TabIndex = 1;
            this.editLearningGoals.Text = "Edit";
            this.editLearningGoals.UseVisualStyleBackColor = true;
            this.editLearningGoals.Click += new System.EventHandler(this.editLearningGoals_Click);
            // 
            // addLearningGoal
            // 
            this.addLearningGoal.Location = new System.Drawing.Point(6, 180);
            this.addLearningGoal.Name = "addLearningGoal";
            this.addLearningGoal.Size = new System.Drawing.Size(75, 23);
            this.addLearningGoal.TabIndex = 0;
            this.addLearningGoal.Text = "Add";
            this.addLearningGoal.UseVisualStyleBackColor = true;
            this.addLearningGoal.Click += new System.EventHandler(this.addLearningGoal_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.learningGoalsPage);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(608, 254);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.editActions);
            this.tabPage2.Controls.Add(this.removeScenarios);
            this.tabPage2.Controls.Add(this.editScenarios);
            this.tabPage2.Controls.Add(this.addScenario);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(600, 228);
            this.tabPage2.TabIndex = 9;
            this.tabPage2.Text = "Scenarios";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBox1);
            this.panel2.Location = new System.Drawing.Point(26, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(554, 160);
            this.panel2.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(548, 147);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scenarios";
            // 
            // editActions
            // 
            this.editActions.Location = new System.Drawing.Point(360, 190);
            this.editActions.Name = "editActions";
            this.editActions.Size = new System.Drawing.Size(75, 23);
            this.editActions.TabIndex = 3;
            this.editActions.Text = "Edit Actions";
            this.editActions.UseVisualStyleBackColor = true;
            this.editActions.Click += new System.EventHandler(this.editActions_Click);
            // 
            // removeScenarios
            // 
            this.removeScenarios.Location = new System.Drawing.Point(249, 190);
            this.removeScenarios.Name = "removeScenarios";
            this.removeScenarios.Size = new System.Drawing.Size(75, 23);
            this.removeScenarios.TabIndex = 2;
            this.removeScenarios.Text = "Remove";
            this.removeScenarios.UseVisualStyleBackColor = true;
            this.removeScenarios.Click += new System.EventHandler(this.removeScenarios_Click);
            // 
            // editScenarios
            // 
            this.editScenarios.Location = new System.Drawing.Point(139, 190);
            this.editScenarios.Name = "editScenarios";
            this.editScenarios.Size = new System.Drawing.Size(75, 23);
            this.editScenarios.TabIndex = 1;
            this.editScenarios.Text = "Edit";
            this.editScenarios.UseVisualStyleBackColor = true;
            this.editScenarios.Click += new System.EventHandler(this.editScenarios_Click);
            // 
            // addScenario
            // 
            this.addScenario.Location = new System.Drawing.Point(26, 190);
            this.addScenario.Name = "addScenario";
            this.addScenario.Size = new System.Drawing.Size(75, 23);
            this.addScenario.TabIndex = 0;
            this.addScenario.Text = "Add";
            this.addScenario.UseVisualStyleBackColor = true;
            this.addScenario.Click += new System.EventHandler(this.addScenario_Click);
            // 
            // AuthTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 267);
            this.Controls.Add(this.tabControl1);
            this.Name = "AuthTool";
            this.Text = "Form1";
            this.learningGoalsPage.ResumeLayout(false);
            this.learningGoalsPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage learningGoalsPage;
        private System.Windows.Forms.Label learningGoalsLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox learningGoalsList;
        private System.Windows.Forms.Button removeLearningGoals;
        private System.Windows.Forms.Button editLearningGoals;
        private System.Windows.Forms.Button addLearningGoal;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button editActions;
        private System.Windows.Forms.Button removeScenarios;
        private System.Windows.Forms.Button editScenarios;
        private System.Windows.Forms.Button addScenario;
    }
}

