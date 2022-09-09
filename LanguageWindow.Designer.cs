namespace Eu4ModEditor
{
    partial class LanguageWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageWindow));
            this.PrimaryLanguage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SecondaryLanguage = new System.Windows.Forms.ComboBox();
            this.GenerateWord = new System.Windows.Forms.Button();
            this.LengthBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.NormalOption = new System.Windows.Forms.RadioButton();
            this.AreaOption = new System.Windows.Forms.RadioButton();
            this.RegionOption = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.LengthBar)).BeginInit();
            this.SuspendLayout();
            // 
            // PrimaryLanguage
            // 
            this.PrimaryLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryLanguage.FormattingEnabled = true;
            this.PrimaryLanguage.Items.AddRange(new object[] {
            "English",
            "Scandinavian languages",
            "Nahuatl",
            "Polish",
            "Mongolian",
            "French",
            "Italian",
            "Latin",
            "Spanish",
            "Russian",
            "Chinese",
            "German",
            "Dutch",
            "Greek",
            "Japanese",
            "Vyeshal"});
            this.PrimaryLanguage.Location = new System.Drawing.Point(12, 25);
            this.PrimaryLanguage.Name = "PrimaryLanguage";
            this.PrimaryLanguage.Size = new System.Drawing.Size(307, 21);
            this.PrimaryLanguage.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Primary language";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Secondary language (highly optional)";
            // 
            // SecondaryLanguage
            // 
            this.SecondaryLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecondaryLanguage.FormattingEnabled = true;
            this.SecondaryLanguage.Items.AddRange(new object[] {
            " ",
            "English",
            "Scandinavian languages ",
            "Nahuatl",
            "Polish",
            "Mongolian",
            "French",
            "Italian",
            "Latin",
            "Spanish",
            "Russian",
            "Chinese",
            "German",
            "Dutch",
            "Greek",
            "Japanese"});
            this.SecondaryLanguage.Location = new System.Drawing.Point(12, 65);
            this.SecondaryLanguage.Name = "SecondaryLanguage";
            this.SecondaryLanguage.Size = new System.Drawing.Size(307, 21);
            this.SecondaryLanguage.TabIndex = 3;
            // 
            // GenerateWord
            // 
            this.GenerateWord.Location = new System.Drawing.Point(12, 196);
            this.GenerateWord.Name = "GenerateWord";
            this.GenerateWord.Size = new System.Drawing.Size(307, 23);
            this.GenerateWord.TabIndex = 4;
            this.GenerateWord.Text = "Generate word";
            this.GenerateWord.UseVisualStyleBackColor = true;
            this.GenerateWord.Click += new System.EventHandler(this.GenerateWord_Click);
            // 
            // LengthBar
            // 
            this.LengthBar.LargeChange = 1;
            this.LengthBar.Location = new System.Drawing.Point(12, 105);
            this.LengthBar.Maximum = 2;
            this.LengthBar.Name = "LengthBar";
            this.LengthBar.Size = new System.Drawing.Size(307, 45);
            this.LengthBar.TabIndex = 5;
            this.LengthBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Length";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Short";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(142, 145);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Average";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(282, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Long";
            // 
            // OutputBox
            // 
            this.OutputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OutputBox.Location = new System.Drawing.Point(12, 225);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(307, 48);
            this.OutputBox.TabIndex = 10;
            // 
            // NormalOption
            // 
            this.NormalOption.AutoSize = true;
            this.NormalOption.Checked = true;
            this.NormalOption.Location = new System.Drawing.Point(12, 173);
            this.NormalOption.Name = "NormalOption";
            this.NormalOption.Size = new System.Drawing.Size(58, 17);
            this.NormalOption.TabIndex = 11;
            this.NormalOption.TabStop = true;
            this.NormalOption.Text = "Normal";
            this.NormalOption.UseVisualStyleBackColor = true;
            // 
            // AreaOption
            // 
            this.AreaOption.AutoSize = true;
            this.AreaOption.Location = new System.Drawing.Point(142, 173);
            this.AreaOption.Name = "AreaOption";
            this.AreaOption.Size = new System.Drawing.Size(47, 17);
            this.AreaOption.TabIndex = 12;
            this.AreaOption.Text = "Area";
            this.AreaOption.UseVisualStyleBackColor = true;
            // 
            // RegionOption
            // 
            this.RegionOption.AutoSize = true;
            this.RegionOption.Location = new System.Drawing.Point(254, 173);
            this.RegionOption.Name = "RegionOption";
            this.RegionOption.Size = new System.Drawing.Size(59, 17);
            this.RegionOption.TabIndex = 13;
            this.RegionOption.Text = "Region";
            this.RegionOption.UseVisualStyleBackColor = true;
            // 
            // LanguageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 285);
            this.Controls.Add(this.RegionOption);
            this.Controls.Add(this.AreaOption);
            this.Controls.Add(this.NormalOption);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LengthBar);
            this.Controls.Add(this.GenerateWord);
            this.Controls.Add(this.SecondaryLanguage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PrimaryLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LanguageWindow";
            this.Text = "Language Engine";
            ((System.ComponentModel.ISupportInitialize)(this.LengthBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PrimaryLanguage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox SecondaryLanguage;
        private System.Windows.Forms.Button GenerateWord;
        private System.Windows.Forms.TrackBar LengthBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.RadioButton NormalOption;
        private System.Windows.Forms.RadioButton AreaOption;
        private System.Windows.Forms.RadioButton RegionOption;
    }
}