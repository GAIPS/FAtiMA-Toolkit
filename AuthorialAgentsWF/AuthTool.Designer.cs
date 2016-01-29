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
            this.learningGoalsView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.learningGoalsLabel = new System.Windows.Forms.Label();
            this.removeLearningGoals = new System.Windows.Forms.Button();
            this.editLearningGoals = new System.Windows.Forms.Button();
            this.addLearningGoal = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scenariosListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.editActions = new System.Windows.Forms.Button();
            this.removeScenarios = new System.Windows.Forms.Button();
            this.editScenarios = new System.Windows.Forms.Button();
            this.addScenario = new System.Windows.Forms.Button();
            this.learningGoalsPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // learningGoalsPage
            // 
            this.learningGoalsPage.Controls.Add(this.learningGoalsView);
            this.learningGoalsPage.Controls.Add(this.learningGoalsLabel);
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
            // learningGoalsView
            // 
            this.learningGoalsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.valueColumnHeader});
            this.learningGoalsView.FullRowSelect = true;
            this.learningGoalsView.Location = new System.Drawing.Point(9, 32);
            this.learningGoalsView.Name = "learningGoalsView";
            this.learningGoalsView.Size = new System.Drawing.Size(539, 161);
            this.learningGoalsView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.learningGoalsView.TabIndex = 5;
            this.learningGoalsView.UseCompatibleStateImageBehavior = false;
            this.learningGoalsView.View = System.Windows.Forms.View.Details;
            this.learningGoalsView.SelectedIndexChanged += new System.EventHandler(this.learningGoalsView_SelectedIndexChanged);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 150;
            // 
            // valueColumnHeader
            // 
            this.valueColumnHeader.Text = "Description";
            this.valueColumnHeader.Width = 146;
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
            // removeLearningGoals
            // 
            this.removeLearningGoals.Location = new System.Drawing.Point(218, 199);
            this.removeLearningGoals.Name = "removeLearningGoals";
            this.removeLearningGoals.Size = new System.Drawing.Size(75, 23);
            this.removeLearningGoals.TabIndex = 2;
            this.removeLearningGoals.Text = "Remove";
            this.removeLearningGoals.UseVisualStyleBackColor = true;
            this.removeLearningGoals.Click += new System.EventHandler(this.removeLearningGoals_Click);
            // 
            // editLearningGoals
            // 
            this.editLearningGoals.Location = new System.Drawing.Point(109, 199);
            this.editLearningGoals.Name = "editLearningGoals";
            this.editLearningGoals.Size = new System.Drawing.Size(75, 23);
            this.editLearningGoals.TabIndex = 1;
            this.editLearningGoals.Text = "Edit";
            this.editLearningGoals.UseVisualStyleBackColor = true;
            this.editLearningGoals.Click += new System.EventHandler(this.editLearningGoals_Click);
            // 
            // addLearningGoal
            // 
            this.addLearningGoal.Location = new System.Drawing.Point(9, 199);
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
            this.tabPage2.Controls.Add(this.scenariosListView);
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
            // scenariosListView
            // 
            this.scenariosListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.scenariosListView.FullRowSelect = true;
            this.scenariosListView.Location = new System.Drawing.Point(6, 23);
            this.scenariosListView.Name = "scenariosListView";
            this.scenariosListView.Size = new System.Drawing.Size(539, 161);
            this.scenariosListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.scenariosListView.TabIndex = 6;
            this.scenariosListView.UseCompatibleStateImageBehavior = false;
            this.scenariosListView.View = System.Windows.Forms.View.Details;
            this.scenariosListView.SelectedIndexChanged += new System.EventHandler(this.scenariosListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Description";
            this.columnHeader2.Width = 146;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scenarios";
            // 
            // editActions
            // 
            this.editActions.Location = new System.Drawing.Point(301, 190);
            this.editActions.Name = "editActions";
            this.editActions.Size = new System.Drawing.Size(75, 23);
            this.editActions.TabIndex = 3;
            this.editActions.Text = "Edit Actions";
            this.editActions.UseVisualStyleBackColor = true;
            this.editActions.Click += new System.EventHandler(this.editActions_Click);
            // 
            // removeScenarios
            // 
            this.removeScenarios.Location = new System.Drawing.Point(208, 190);
            this.removeScenarios.Name = "removeScenarios";
            this.removeScenarios.Size = new System.Drawing.Size(75, 23);
            this.removeScenarios.TabIndex = 2;
            this.removeScenarios.Text = "Remove";
            this.removeScenarios.UseVisualStyleBackColor = true;
            this.removeScenarios.Click += new System.EventHandler(this.removeScenarios_Click);
            // 
            // editScenarios
            // 
            this.editScenarios.Location = new System.Drawing.Point(108, 190);
            this.editScenarios.Name = "editScenarios";
            this.editScenarios.Size = new System.Drawing.Size(75, 23);
            this.editScenarios.TabIndex = 1;
            this.editScenarios.Text = "Edit";
            this.editScenarios.UseVisualStyleBackColor = true;
            this.editScenarios.Click += new System.EventHandler(this.editScenarios_Click);
            // 
            // addScenario
            // 
            this.addScenario.Location = new System.Drawing.Point(6, 190);
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
            this.ClientSize = new System.Drawing.Size(622, 284);
            this.Controls.Add(this.tabControl1);
            this.Name = "AuthTool";
            this.Text = "Form1";
            this.learningGoalsPage.ResumeLayout(false);
            this.learningGoalsPage.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage learningGoalsPage;
        private System.Windows.Forms.Label learningGoalsLabel;
        private System.Windows.Forms.Button removeLearningGoals;
        private System.Windows.Forms.Button editLearningGoals;
        private System.Windows.Forms.Button addLearningGoal;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button editActions;
        private System.Windows.Forms.Button removeScenarios;
        private System.Windows.Forms.Button editScenarios;
        private System.Windows.Forms.Button addScenario;
        private System.Windows.Forms.ListView scenariosListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView learningGoalsView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader valueColumnHeader;
    }
}

