namespace Eu4ModEditor
{
    partial class IdeaEditor
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
            this.IdeaSetBox = new System.Windows.Forms.ComboBox();
            this.IdeaTypeBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ModifiersBox = new System.Windows.Forms.GroupBox();
            this.ModifiersFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.TraditionLabel = new System.Windows.Forms.Label();
            this.Idea1Label = new System.Windows.Forms.Label();
            this.Idea2Label = new System.Windows.Forms.Label();
            this.Idea3Label = new System.Windows.Forms.Label();
            this.Idea4Label = new System.Windows.Forms.Label();
            this.Idea5Label = new System.Windows.Forms.Label();
            this.Idea6Label = new System.Windows.Forms.Label();
            this.Idea7Label = new System.Windows.Forms.Label();
            this.AmbitionLabel = new System.Windows.Forms.Label();
            this.NewIdeaSetBox = new System.Windows.Forms.TextBox();
            this.AddNewIdeaSet = new System.Windows.Forms.Button();
            this.CategoryBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveIdeaSet = new System.Windows.Forms.Button();
            this.TriggerBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AddCountryButton = new System.Windows.Forms.Button();
            this.ShowMatchingCountriesButton = new System.Windows.Forms.Button();
            this.ModifiersBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // IdeaSetBox
            // 
            this.IdeaSetBox.FormattingEnabled = true;
            this.IdeaSetBox.Location = new System.Drawing.Point(49, 32);
            this.IdeaSetBox.Name = "IdeaSetBox";
            this.IdeaSetBox.Size = new System.Drawing.Size(121, 21);
            this.IdeaSetBox.TabIndex = 0;
            this.IdeaSetBox.SelectedIndexChanged += new System.EventHandler(this.IdeaSetBox_SelectedIndexChanged);
            // 
            // IdeaTypeBox
            // 
            this.IdeaTypeBox.FormattingEnabled = true;
            this.IdeaTypeBox.Location = new System.Drawing.Point(49, 5);
            this.IdeaTypeBox.Name = "IdeaTypeBox";
            this.IdeaTypeBox.Size = new System.Drawing.Size(121, 21);
            this.IdeaTypeBox.TabIndex = 1;
            this.IdeaTypeBox.SelectedIndexChanged += new System.EventHandler(this.IdeaTypeBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Set";
            // 
            // ModifiersBox
            // 
            this.ModifiersBox.Controls.Add(this.ModifiersFlowPanel);
            this.ModifiersBox.Location = new System.Drawing.Point(12, 123);
            this.ModifiersBox.Name = "ModifiersBox";
            this.ModifiersBox.Size = new System.Drawing.Size(498, 169);
            this.ModifiersBox.TabIndex = 4;
            this.ModifiersBox.TabStop = false;
            this.ModifiersBox.Text = "Modifiers";
            // 
            // ModifiersFlowPanel
            // 
            this.ModifiersFlowPanel.Location = new System.Drawing.Point(6, 19);
            this.ModifiersFlowPanel.Name = "ModifiersFlowPanel";
            this.ModifiersFlowPanel.Size = new System.Drawing.Size(486, 144);
            this.ModifiersFlowPanel.TabIndex = 0;
            // 
            // TraditionLabel
            // 
            this.TraditionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TraditionLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TraditionLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.TraditionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TraditionLabel.Location = new System.Drawing.Point(12, 70);
            this.TraditionLabel.Name = "TraditionLabel";
            this.TraditionLabel.Size = new System.Drawing.Size(50, 50);
            this.TraditionLabel.TabIndex = 5;
            this.TraditionLabel.Text = "T";
            this.TraditionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TraditionLabel.Click += new System.EventHandler(this.TraditionLabel_Click);
            // 
            // Idea1Label
            // 
            this.Idea1Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea1Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea1Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea1Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea1Label.Location = new System.Drawing.Point(68, 70);
            this.Idea1Label.Name = "Idea1Label";
            this.Idea1Label.Size = new System.Drawing.Size(50, 50);
            this.Idea1Label.TabIndex = 6;
            this.Idea1Label.Text = "1";
            this.Idea1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea1Label.Click += new System.EventHandler(this.Idea1Label_Click);
            // 
            // Idea2Label
            // 
            this.Idea2Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea2Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea2Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea2Label.Location = new System.Drawing.Point(124, 70);
            this.Idea2Label.Name = "Idea2Label";
            this.Idea2Label.Size = new System.Drawing.Size(50, 50);
            this.Idea2Label.TabIndex = 7;
            this.Idea2Label.Text = "2";
            this.Idea2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea2Label.Click += new System.EventHandler(this.Idea2Label_Click);
            // 
            // Idea3Label
            // 
            this.Idea3Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea3Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea3Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea3Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea3Label.Location = new System.Drawing.Point(180, 70);
            this.Idea3Label.Name = "Idea3Label";
            this.Idea3Label.Size = new System.Drawing.Size(50, 50);
            this.Idea3Label.TabIndex = 8;
            this.Idea3Label.Text = "3";
            this.Idea3Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea3Label.Click += new System.EventHandler(this.Idea3Label_Click);
            // 
            // Idea4Label
            // 
            this.Idea4Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea4Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea4Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea4Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea4Label.Location = new System.Drawing.Point(236, 70);
            this.Idea4Label.Name = "Idea4Label";
            this.Idea4Label.Size = new System.Drawing.Size(50, 50);
            this.Idea4Label.TabIndex = 9;
            this.Idea4Label.Text = "4";
            this.Idea4Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea4Label.Click += new System.EventHandler(this.Idea4Label_Click);
            // 
            // Idea5Label
            // 
            this.Idea5Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea5Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea5Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea5Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea5Label.Location = new System.Drawing.Point(292, 70);
            this.Idea5Label.Name = "Idea5Label";
            this.Idea5Label.Size = new System.Drawing.Size(50, 50);
            this.Idea5Label.TabIndex = 10;
            this.Idea5Label.Text = "5";
            this.Idea5Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea5Label.Click += new System.EventHandler(this.Idea5Label_Click);
            // 
            // Idea6Label
            // 
            this.Idea6Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea6Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea6Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea6Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea6Label.Location = new System.Drawing.Point(348, 70);
            this.Idea6Label.Name = "Idea6Label";
            this.Idea6Label.Size = new System.Drawing.Size(50, 50);
            this.Idea6Label.TabIndex = 11;
            this.Idea6Label.Text = "6";
            this.Idea6Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea6Label.Click += new System.EventHandler(this.Idea6Label_Click);
            // 
            // Idea7Label
            // 
            this.Idea7Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Idea7Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Idea7Label.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Idea7Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Idea7Label.Location = new System.Drawing.Point(404, 70);
            this.Idea7Label.Name = "Idea7Label";
            this.Idea7Label.Size = new System.Drawing.Size(50, 50);
            this.Idea7Label.TabIndex = 12;
            this.Idea7Label.Text = "7";
            this.Idea7Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Idea7Label.Click += new System.EventHandler(this.Idea7Label_Click);
            // 
            // AmbitionLabel
            // 
            this.AmbitionLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.AmbitionLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AmbitionLabel.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.AmbitionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.AmbitionLabel.Location = new System.Drawing.Point(460, 70);
            this.AmbitionLabel.Name = "AmbitionLabel";
            this.AmbitionLabel.Size = new System.Drawing.Size(50, 50);
            this.AmbitionLabel.TabIndex = 13;
            this.AmbitionLabel.Text = "A";
            this.AmbitionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AmbitionLabel.Click += new System.EventHandler(this.AmbitionLabel_Click);
            // 
            // NewIdeaSetBox
            // 
            this.NewIdeaSetBox.Location = new System.Drawing.Point(348, 6);
            this.NewIdeaSetBox.Name = "NewIdeaSetBox";
            this.NewIdeaSetBox.Size = new System.Drawing.Size(78, 20);
            this.NewIdeaSetBox.TabIndex = 14;
            // 
            // AddNewIdeaSet
            // 
            this.AddNewIdeaSet.Location = new System.Drawing.Point(432, 32);
            this.AddNewIdeaSet.Name = "AddNewIdeaSet";
            this.AddNewIdeaSet.Size = new System.Drawing.Size(75, 20);
            this.AddNewIdeaSet.TabIndex = 15;
            this.AddNewIdeaSet.Text = "Add new";
            this.AddNewIdeaSet.UseVisualStyleBackColor = true;
            this.AddNewIdeaSet.Click += new System.EventHandler(this.AddNewIdeaSet_Click);
            // 
            // CategoryBox
            // 
            this.CategoryBox.FormattingEnabled = true;
            this.CategoryBox.Location = new System.Drawing.Point(348, 32);
            this.CategoryBox.Name = "CategoryBox";
            this.CategoryBox.Size = new System.Drawing.Size(78, 21);
            this.CategoryBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(307, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Category";
            // 
            // RemoveIdeaSet
            // 
            this.RemoveIdeaSet.Location = new System.Drawing.Point(12, 298);
            this.RemoveIdeaSet.Name = "RemoveIdeaSet";
            this.RemoveIdeaSet.Size = new System.Drawing.Size(81, 23);
            this.RemoveIdeaSet.TabIndex = 19;
            this.RemoveIdeaSet.Text = "Remove idea set";
            this.RemoveIdeaSet.UseVisualStyleBackColor = true;
            // 
            // TriggerBox
            // 
            this.TriggerBox.Location = new System.Drawing.Point(12, 350);
            this.TriggerBox.Multiline = true;
            this.TriggerBox.Name = "TriggerBox";
            this.TriggerBox.Size = new System.Drawing.Size(498, 73);
            this.TriggerBox.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 334);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Trigger";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(212, 323);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(209, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Add country";
            // 
            // AddCountryButton
            // 
            this.AddCountryButton.Location = new System.Drawing.Point(339, 323);
            this.AddCountryButton.Name = "AddCountryButton";
            this.AddCountryButton.Size = new System.Drawing.Size(50, 21);
            this.AddCountryButton.TabIndex = 24;
            this.AddCountryButton.Text = "Add";
            this.AddCountryButton.UseVisualStyleBackColor = true;
            this.AddCountryButton.Click += new System.EventHandler(this.AddCountryButton_Click);
            // 
            // ShowMatchingCountriesButton
            // 
            this.ShowMatchingCountriesButton.Location = new System.Drawing.Point(421, 303);
            this.ShowMatchingCountriesButton.Name = "ShowMatchingCountriesButton";
            this.ShowMatchingCountriesButton.Size = new System.Drawing.Size(89, 41);
            this.ShowMatchingCountriesButton.TabIndex = 25;
            this.ShowMatchingCountriesButton.Text = "Show matching countries";
            this.ShowMatchingCountriesButton.UseVisualStyleBackColor = true;
            this.ShowMatchingCountriesButton.Click += new System.EventHandler(this.ShowMatchingCountriesButton_Click);
            // 
            // IdeaEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 435);
            this.Controls.Add(this.ShowMatchingCountriesButton);
            this.Controls.Add(this.AddCountryButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TriggerBox);
            this.Controls.Add(this.RemoveIdeaSet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CategoryBox);
            this.Controls.Add(this.AddNewIdeaSet);
            this.Controls.Add(this.NewIdeaSetBox);
            this.Controls.Add(this.AmbitionLabel);
            this.Controls.Add(this.Idea7Label);
            this.Controls.Add(this.Idea6Label);
            this.Controls.Add(this.Idea5Label);
            this.Controls.Add(this.Idea4Label);
            this.Controls.Add(this.Idea3Label);
            this.Controls.Add(this.Idea2Label);
            this.Controls.Add(this.Idea1Label);
            this.Controls.Add(this.TraditionLabel);
            this.Controls.Add(this.ModifiersBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IdeaTypeBox);
            this.Controls.Add(this.IdeaSetBox);
            this.Name = "IdeaEditor";
            this.Text = "IdeaEditor";
            this.ModifiersBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox IdeaSetBox;
        private System.Windows.Forms.ComboBox IdeaTypeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox ModifiersBox;
        private System.Windows.Forms.Label TraditionLabel;
        private System.Windows.Forms.Label Idea1Label;
        private System.Windows.Forms.Label Idea2Label;
        private System.Windows.Forms.Label Idea3Label;
        private System.Windows.Forms.Label Idea4Label;
        private System.Windows.Forms.Label Idea5Label;
        private System.Windows.Forms.Label Idea6Label;
        private System.Windows.Forms.Label Idea7Label;
        private System.Windows.Forms.Label AmbitionLabel;
        private System.Windows.Forms.TextBox NewIdeaSetBox;
        private System.Windows.Forms.Button AddNewIdeaSet;
        private System.Windows.Forms.FlowLayoutPanel ModifiersFlowPanel;
        private System.Windows.Forms.ComboBox CategoryBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveIdeaSet;
        private System.Windows.Forms.TextBox TriggerBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button AddCountryButton;
        private System.Windows.Forms.Button ShowMatchingCountriesButton;
    }
}