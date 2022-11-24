namespace Eu4ModEditor
{
    partial class DateWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BookmarksBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CreateBookmark = new System.Windows.Forms.Button();
            this.BookmarkNameBox = new System.Windows.Forms.TextBox();
            this.DayInput = new System.Windows.Forms.NumericUpDown();
            this.MonthInput = new System.Windows.Forms.NumericUpDown();
            this.YearInput = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.DayInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearInput)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Day";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Month";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Year";
            // 
            // BookmarksBox
            // 
            this.BookmarksBox.Enabled = false;
            this.BookmarksBox.FormattingEnabled = true;
            this.BookmarksBox.Location = new System.Drawing.Point(197, 27);
            this.BookmarksBox.Name = "BookmarksBox";
            this.BookmarksBox.Size = new System.Drawing.Size(121, 21);
            this.BookmarksBox.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(194, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Bookmarks";
            // 
            // CreateBookmark
            // 
            this.CreateBookmark.Enabled = false;
            this.CreateBookmark.Location = new System.Drawing.Point(279, 48);
            this.CreateBookmark.Name = "CreateBookmark";
            this.CreateBookmark.Size = new System.Drawing.Size(39, 23);
            this.CreateBookmark.TabIndex = 14;
            this.CreateBookmark.Text = "Add";
            this.CreateBookmark.UseVisualStyleBackColor = true;
            // 
            // BookmarkNameBox
            // 
            this.BookmarkNameBox.Enabled = false;
            this.BookmarkNameBox.Location = new System.Drawing.Point(197, 51);
            this.BookmarkNameBox.Name = "BookmarkNameBox";
            this.BookmarkNameBox.Size = new System.Drawing.Size(76, 20);
            this.BookmarkNameBox.TabIndex = 15;
            // 
            // DayInput
            // 
            this.DayInput.InterceptArrowKeys = false;
            this.DayInput.Location = new System.Drawing.Point(12, 28);
            this.DayInput.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.DayInput.Name = "DayInput";
            this.DayInput.Size = new System.Drawing.Size(35, 20);
            this.DayInput.TabIndex = 16;
            this.DayInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DayInput.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.DayInput.ValueChanged += new System.EventHandler(this.DayInput_ValueChanged);
            // 
            // MonthInput
            // 
            this.MonthInput.InterceptArrowKeys = false;
            this.MonthInput.Location = new System.Drawing.Point(52, 28);
            this.MonthInput.Maximum = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.MonthInput.Name = "MonthInput";
            this.MonthInput.Size = new System.Drawing.Size(35, 20);
            this.MonthInput.TabIndex = 17;
            this.MonthInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MonthInput.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.MonthInput.ValueChanged += new System.EventHandler(this.MonthInput_ValueChanged);
            // 
            // YearInput
            // 
            this.YearInput.InterceptArrowKeys = false;
            this.YearInput.Location = new System.Drawing.Point(93, 28);
            this.YearInput.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.YearInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.YearInput.Name = "YearInput";
            this.YearInput.Size = new System.Drawing.Size(68, 20);
            this.YearInput.TabIndex = 18;
            this.YearInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.YearInput.Value = new decimal(new int[] {
            1444,
            0,
            0,
            0});
            this.YearInput.ValueChanged += new System.EventHandler(this.YearInput_ValueChanged);
            // 
            // DateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 76);
            this.Controls.Add(this.YearInput);
            this.Controls.Add(this.MonthInput);
            this.Controls.Add(this.DayInput);
            this.Controls.Add(this.BookmarkNameBox);
            this.Controls.Add(this.CreateBookmark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BookmarksBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DateWindow";
            this.Text = "DateWindow";
            ((System.ComponentModel.ISupportInitialize)(this.DayInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MonthInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YearInput)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox BookmarksBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CreateBookmark;
        private System.Windows.Forms.TextBox BookmarkNameBox;
        private System.Windows.Forms.NumericUpDown DayInput;
        private System.Windows.Forms.NumericUpDown MonthInput;
        private System.Windows.Forms.NumericUpDown YearInput;
    }
}