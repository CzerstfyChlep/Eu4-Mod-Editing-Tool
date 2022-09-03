namespace Eu4ModEditor
{
    partial class StatisticsForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Dec8 = new System.Windows.Forms.Label();
            this.Dec5 = new System.Windows.Forms.Label();
            this.Dec7 = new System.Windows.Forms.Label();
            this.Dec6 = new System.Windows.Forms.Label();
            this.TotalDev = new System.Windows.Forms.GroupBox();
            this.StatsProvTotalManpower = new System.Windows.Forms.Label();
            this.StatsProvTotalProduction = new System.Windows.Forms.Label();
            this.StatsProvTotalTax = new System.Windows.Forms.Label();
            this.StatsProvTotalDev = new System.Windows.Forms.Label();
            this.Dec4 = new System.Windows.Forms.Label();
            this.Dec1 = new System.Windows.Forms.Label();
            this.Dec3 = new System.Windows.Forms.Label();
            this.Dec2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.StatsProvAvgManpower = new System.Windows.Forms.Label();
            this.StatsProvAvgProduction = new System.Windows.Forms.Label();
            this.StatsProvAvgTax = new System.Windows.Forms.Label();
            this.StatsProvAvgDev = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.TotalDev.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 313);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Provinces";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.TotalDev);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(351, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Development";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.StatsProvAvgManpower);
            this.groupBox2.Controls.Add(this.Dec8);
            this.groupBox2.Controls.Add(this.StatsProvAvgProduction);
            this.groupBox2.Controls.Add(this.Dec5);
            this.groupBox2.Controls.Add(this.StatsProvAvgTax);
            this.groupBox2.Controls.Add(this.Dec7);
            this.groupBox2.Controls.Add(this.StatsProvAvgDev);
            this.groupBox2.Controls.Add(this.Dec6);
            this.groupBox2.Location = new System.Drawing.Point(173, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(172, 110);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Average";
            // 
            // Dec8
            // 
            this.Dec8.AutoSize = true;
            this.Dec8.Location = new System.Drawing.Point(6, 83);
            this.Dec8.Name = "Dec8";
            this.Dec8.Size = new System.Drawing.Size(102, 13);
            this.Dec8.TabIndex = 3;
            this.Dec8.Text = "Average manpower:";
            // 
            // Dec5
            // 
            this.Dec5.AutoSize = true;
            this.Dec5.Location = new System.Drawing.Point(6, 16);
            this.Dec5.Name = "Dec5";
            this.Dec5.Size = new System.Drawing.Size(114, 13);
            this.Dec5.TabIndex = 0;
            this.Dec5.Text = "Average development:";
            // 
            // Dec7
            // 
            this.Dec7.AutoSize = true;
            this.Dec7.Location = new System.Drawing.Point(6, 60);
            this.Dec7.Name = "Dec7";
            this.Dec7.Size = new System.Drawing.Size(103, 13);
            this.Dec7.TabIndex = 2;
            this.Dec7.Text = "Average production:";
            // 
            // Dec6
            // 
            this.Dec6.AutoSize = true;
            this.Dec6.Location = new System.Drawing.Point(6, 38);
            this.Dec6.Name = "Dec6";
            this.Dec6.Size = new System.Drawing.Size(67, 13);
            this.Dec6.TabIndex = 1;
            this.Dec6.Text = "Average tax:";
            // 
            // TotalDev
            // 
            this.TotalDev.Controls.Add(this.StatsProvTotalManpower);
            this.TotalDev.Controls.Add(this.StatsProvTotalProduction);
            this.TotalDev.Controls.Add(this.StatsProvTotalTax);
            this.TotalDev.Controls.Add(this.StatsProvTotalDev);
            this.TotalDev.Controls.Add(this.Dec4);
            this.TotalDev.Controls.Add(this.Dec1);
            this.TotalDev.Controls.Add(this.Dec3);
            this.TotalDev.Controls.Add(this.Dec2);
            this.TotalDev.Location = new System.Drawing.Point(6, 19);
            this.TotalDev.Name = "TotalDev";
            this.TotalDev.Size = new System.Drawing.Size(161, 110);
            this.TotalDev.TabIndex = 1;
            this.TotalDev.TabStop = false;
            this.TotalDev.Text = "Total";
            // 
            // StatsProvTotalManpower
            // 
            this.StatsProvTotalManpower.Location = new System.Drawing.Point(110, 83);
            this.StatsProvTotalManpower.Name = "StatsProvTotalManpower";
            this.StatsProvTotalManpower.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvTotalManpower.Size = new System.Drawing.Size(45, 13);
            this.StatsProvTotalManpower.TabIndex = 6;
            this.StatsProvTotalManpower.Text = "0";
            // 
            // StatsProvTotalProduction
            // 
            this.StatsProvTotalProduction.Location = new System.Drawing.Point(110, 60);
            this.StatsProvTotalProduction.Name = "StatsProvTotalProduction";
            this.StatsProvTotalProduction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvTotalProduction.Size = new System.Drawing.Size(45, 13);
            this.StatsProvTotalProduction.TabIndex = 5;
            this.StatsProvTotalProduction.Text = "0";
            // 
            // StatsProvTotalTax
            // 
            this.StatsProvTotalTax.Location = new System.Drawing.Point(110, 38);
            this.StatsProvTotalTax.Name = "StatsProvTotalTax";
            this.StatsProvTotalTax.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvTotalTax.Size = new System.Drawing.Size(45, 13);
            this.StatsProvTotalTax.TabIndex = 4;
            this.StatsProvTotalTax.Text = "0";
            // 
            // StatsProvTotalDev
            // 
            this.StatsProvTotalDev.Location = new System.Drawing.Point(110, 16);
            this.StatsProvTotalDev.Name = "StatsProvTotalDev";
            this.StatsProvTotalDev.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvTotalDev.Size = new System.Drawing.Size(45, 13);
            this.StatsProvTotalDev.TabIndex = 1;
            this.StatsProvTotalDev.Text = "0";
            // 
            // Dec4
            // 
            this.Dec4.AutoSize = true;
            this.Dec4.Location = new System.Drawing.Point(6, 83);
            this.Dec4.Name = "Dec4";
            this.Dec4.Size = new System.Drawing.Size(86, 13);
            this.Dec4.TabIndex = 3;
            this.Dec4.Text = "Total manpower:";
            // 
            // Dec1
            // 
            this.Dec1.AutoSize = true;
            this.Dec1.Location = new System.Drawing.Point(6, 16);
            this.Dec1.Name = "Dec1";
            this.Dec1.Size = new System.Drawing.Size(98, 13);
            this.Dec1.TabIndex = 0;
            this.Dec1.Text = "Total development:";
            // 
            // Dec3
            // 
            this.Dec3.AutoSize = true;
            this.Dec3.Location = new System.Drawing.Point(6, 60);
            this.Dec3.Name = "Dec3";
            this.Dec3.Size = new System.Drawing.Size(87, 13);
            this.Dec3.TabIndex = 2;
            this.Dec3.Text = "Total production:";
            // 
            // Dec2
            // 
            this.Dec2.AutoSize = true;
            this.Dec2.Location = new System.Drawing.Point(6, 38);
            this.Dec2.Name = "Dec2";
            this.Dec2.Size = new System.Drawing.Size(51, 13);
            this.Dec2.TabIndex = 1;
            this.Dec2.Text = "Total tax:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(768, 287);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Countries";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(768, 287);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Religions";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // StatsProvAvgManpower
            // 
            this.StatsProvAvgManpower.Location = new System.Drawing.Point(121, 83);
            this.StatsProvAvgManpower.Name = "StatsProvAvgManpower";
            this.StatsProvAvgManpower.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvAvgManpower.Size = new System.Drawing.Size(45, 13);
            this.StatsProvAvgManpower.TabIndex = 10;
            this.StatsProvAvgManpower.Text = "0";
            // 
            // StatsProvAvgProduction
            // 
            this.StatsProvAvgProduction.Location = new System.Drawing.Point(121, 60);
            this.StatsProvAvgProduction.Name = "StatsProvAvgProduction";
            this.StatsProvAvgProduction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvAvgProduction.Size = new System.Drawing.Size(45, 13);
            this.StatsProvAvgProduction.TabIndex = 9;
            this.StatsProvAvgProduction.Text = "0";
            // 
            // StatsProvAvgTax
            // 
            this.StatsProvAvgTax.Location = new System.Drawing.Point(121, 38);
            this.StatsProvAvgTax.Name = "StatsProvAvgTax";
            this.StatsProvAvgTax.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvAvgTax.Size = new System.Drawing.Size(45, 13);
            this.StatsProvAvgTax.TabIndex = 8;
            this.StatsProvAvgTax.Text = "0";
            // 
            // StatsProvAvgDev
            // 
            this.StatsProvAvgDev.Location = new System.Drawing.Point(121, 16);
            this.StatsProvAvgDev.Name = "StatsProvAvgDev";
            this.StatsProvAvgDev.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StatsProvAvgDev.Size = new System.Drawing.Size(45, 13);
            this.StatsProvAvgDev.TabIndex = 7;
            this.StatsProvAvgDev.Text = "0";
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 339);
            this.Controls.Add(this.tabControl1);
            this.Name = "StatisticsForm";
            this.Text = "Statistics";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.TotalDev.ResumeLayout(false);
            this.TotalDev.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox TotalDev;
        private System.Windows.Forms.Label Dec1;
        private System.Windows.Forms.Label Dec3;
        private System.Windows.Forms.Label Dec2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label Dec4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label Dec8;
        private System.Windows.Forms.Label Dec5;
        private System.Windows.Forms.Label Dec7;
        private System.Windows.Forms.Label Dec6;
        private System.Windows.Forms.Label StatsProvTotalDev;
        private System.Windows.Forms.Label StatsProvTotalManpower;
        private System.Windows.Forms.Label StatsProvTotalProduction;
        private System.Windows.Forms.Label StatsProvTotalTax;
        private System.Windows.Forms.Label StatsProvAvgManpower;
        private System.Windows.Forms.Label StatsProvAvgProduction;
        private System.Windows.Forms.Label StatsProvAvgTax;
        private System.Windows.Forms.Label StatsProvAvgDev;
    }
}