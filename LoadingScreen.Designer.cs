namespace Eu4ModEditor
{
    partial class LoadingScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingScreen));
            this.GameDirectoryBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ModDirectoryBox = new System.Windows.Forms.TextBox();
            this.GameDirectoryButton = new System.Windows.Forms.Button();
            this.ModDirectoryButton = new System.Windows.Forms.Button();
            this.CheckFilesButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.AllNotRead = new System.Windows.Forms.Button();
            this.AllRead = new System.Windows.Forms.Button();
            this.AllBoth = new System.Windows.Forms.Button();
            this.AllGame = new System.Windows.Forms.Button();
            this.AllMod = new System.Windows.Forms.Button();
            this.LoadingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfirmFileUsage = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.StartDateBox = new System.Windows.Forms.TextBox();
            this.DarkmodeOption = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.AppSizeBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LanguageBox = new System.Windows.Forms.ComboBox();
            this.NewFilesForNewObjects = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ReadOnlyNewFiles = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameDirectoryBox
            // 
            this.GameDirectoryBox.Location = new System.Drawing.Point(20, 41);
            this.GameDirectoryBox.Name = "GameDirectoryBox";
            this.GameDirectoryBox.Size = new System.Drawing.Size(701, 20);
            this.GameDirectoryBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Game directory (should be something like \"C:\\Steam\\steamapps\\common\\Europa Univer" +
    "salis IV\")";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(306, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mod directory (does *NOT* need to be a directory in mod folder)";
            // 
            // ModDirectoryBox
            // 
            this.ModDirectoryBox.Location = new System.Drawing.Point(20, 89);
            this.ModDirectoryBox.Name = "ModDirectoryBox";
            this.ModDirectoryBox.Size = new System.Drawing.Size(701, 20);
            this.ModDirectoryBox.TabIndex = 2;
            // 
            // GameDirectoryButton
            // 
            this.GameDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GameDirectoryButton.Location = new System.Drawing.Point(727, 41);
            this.GameDirectoryButton.Name = "GameDirectoryButton";
            this.GameDirectoryButton.Size = new System.Drawing.Size(37, 20);
            this.GameDirectoryButton.TabIndex = 4;
            this.GameDirectoryButton.Text = "...";
            this.GameDirectoryButton.UseVisualStyleBackColor = true;
            this.GameDirectoryButton.Click += new System.EventHandler(this.GameDirectoryButton_Click);
            // 
            // ModDirectoryButton
            // 
            this.ModDirectoryButton.Location = new System.Drawing.Point(727, 89);
            this.ModDirectoryButton.Name = "ModDirectoryButton";
            this.ModDirectoryButton.Size = new System.Drawing.Size(37, 20);
            this.ModDirectoryButton.TabIndex = 5;
            this.ModDirectoryButton.Text = "...";
            this.ModDirectoryButton.UseVisualStyleBackColor = true;
            this.ModDirectoryButton.Click += new System.EventHandler(this.ModDirectoryButton_Click);
            // 
            // CheckFilesButton
            // 
            this.CheckFilesButton.Location = new System.Drawing.Point(20, 115);
            this.CheckFilesButton.Name = "CheckFilesButton";
            this.CheckFilesButton.Size = new System.Drawing.Size(701, 23);
            this.CheckFilesButton.TabIndex = 6;
            this.CheckFilesButton.Text = "Check and set files";
            this.CheckFilesButton.UseVisualStyleBackColor = true;
            this.CheckFilesButton.Click += new System.EventHandler(this.CheckFilesButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CheckFilesButton);
            this.groupBox1.Controls.Add(this.GameDirectoryBox);
            this.groupBox1.Controls.Add(this.ModDirectoryButton);
            this.groupBox1.Controls.Add(this.ModDirectoryBox);
            this.groupBox1.Controls.Add(this.GameDirectoryButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(775, 146);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.AllNotRead);
            this.groupBox2.Controls.Add(this.AllRead);
            this.groupBox2.Controls.Add(this.AllBoth);
            this.groupBox2.Controls.Add(this.AllGame);
            this.groupBox2.Controls.Add(this.AllMod);
            this.groupBox2.Controls.Add(this.LoadingPanel);
            this.groupBox2.Controls.Add(this.ConfirmFileUsage);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 502);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2";
            // 
            // AllNotRead
            // 
            this.AllNotRead.Location = new System.Drawing.Point(619, 19);
            this.AllNotRead.Name = "AllNotRead";
            this.AllNotRead.Size = new System.Drawing.Size(150, 23);
            this.AllNotRead.TabIndex = 17;
            this.AllNotRead.Text = "Remove all Read Only";
            this.AllNotRead.UseVisualStyleBackColor = true;
            this.AllNotRead.Click += new System.EventHandler(this.AllNotRead_Click);
            // 
            // AllRead
            // 
            this.AllRead.Location = new System.Drawing.Point(450, 19);
            this.AllRead.Name = "AllRead";
            this.AllRead.Size = new System.Drawing.Size(163, 23);
            this.AllRead.TabIndex = 16;
            this.AllRead.Text = "Set all to Read Only";
            this.AllRead.UseVisualStyleBackColor = true;
            this.AllRead.Click += new System.EventHandler(this.AllRead_Click);
            // 
            // AllBoth
            // 
            this.AllBoth.Location = new System.Drawing.Point(303, 19);
            this.AllBoth.Name = "AllBoth";
            this.AllBoth.Size = new System.Drawing.Size(141, 23);
            this.AllBoth.TabIndex = 15;
            this.AllBoth.Text = "Set all to Both";
            this.AllBoth.UseVisualStyleBackColor = true;
            this.AllBoth.Click += new System.EventHandler(this.AllBoth_Click);
            // 
            // AllGame
            // 
            this.AllGame.Location = new System.Drawing.Point(157, 19);
            this.AllGame.Name = "AllGame";
            this.AllGame.Size = new System.Drawing.Size(140, 23);
            this.AllGame.TabIndex = 14;
            this.AllGame.Text = "Set all to Game";
            this.AllGame.UseVisualStyleBackColor = true;
            this.AllGame.Click += new System.EventHandler(this.AllGame_Click);
            // 
            // AllMod
            // 
            this.AllMod.Location = new System.Drawing.Point(6, 19);
            this.AllMod.Name = "AllMod";
            this.AllMod.Size = new System.Drawing.Size(145, 23);
            this.AllMod.TabIndex = 13;
            this.AllMod.Text = "Set all to Mod";
            this.AllMod.UseVisualStyleBackColor = true;
            this.AllMod.Click += new System.EventHandler(this.AllMod_Click);
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.AutoScroll = true;
            this.LoadingPanel.Location = new System.Drawing.Point(6, 48);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(763, 414);
            this.LoadingPanel.TabIndex = 12;
            // 
            // ConfirmFileUsage
            // 
            this.ConfirmFileUsage.Location = new System.Drawing.Point(15, 468);
            this.ConfirmFileUsage.Name = "ConfirmFileUsage";
            this.ConfirmFileUsage.Size = new System.Drawing.Size(749, 23);
            this.ConfirmFileUsage.TabIndex = 11;
            this.ConfirmFileUsage.Text = "Confirm file usage";
            this.ConfirmFileUsage.UseVisualStyleBackColor = true;
            this.ConfirmFileUsage.Click += new System.EventHandler(this.ConfirmFileUsage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.StartDateBox);
            this.groupBox3.Controls.Add(this.DarkmodeOption);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.AppSizeBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.LanguageBox);
            this.groupBox3.Controls.Add(this.NewFilesForNewObjects);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.ReadOnlyNewFiles);
            this.groupBox3.Location = new System.Drawing.Point(796, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 348);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Special options";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Start date (YYYY.MM.DD)";
            // 
            // StartDateBox
            // 
            this.StartDateBox.Location = new System.Drawing.Point(16, 266);
            this.StartDateBox.Name = "StartDateBox";
            this.StartDateBox.Size = new System.Drawing.Size(100, 20);
            this.StartDateBox.TabIndex = 8;
            this.StartDateBox.Text = "1444.11.11";
            // 
            // DarkmodeOption
            // 
            this.DarkmodeOption.Enabled = false;
            this.DarkmodeOption.Location = new System.Drawing.Point(16, 319);
            this.DarkmodeOption.Name = "DarkmodeOption";
            this.DarkmodeOption.Size = new System.Drawing.Size(136, 23);
            this.DarkmodeOption.TabIndex = 7;
            this.DarkmodeOption.Text = "Toggle darkmode";
            this.DarkmodeOption.UseVisualStyleBackColor = true;
            this.DarkmodeOption.Click += new System.EventHandler(this.DarkmodeOption_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 197);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "App size";
            // 
            // AppSizeBox
            // 
            this.AppSizeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AppSizeBox.FormattingEnabled = true;
            this.AppSizeBox.Items.AddRange(new object[] {
            "1752 x 970",
            "1290 x 970"});
            this.AppSizeBox.Location = new System.Drawing.Point(16, 216);
            this.AppSizeBox.Name = "AppSizeBox";
            this.AppSizeBox.Size = new System.Drawing.Size(136, 21);
            this.AppSizeBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Language of localisation files";
            // 
            // LanguageBox
            // 
            this.LanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageBox.FormattingEnabled = true;
            this.LanguageBox.Items.AddRange(new object[] {
            "English",
            "French",
            "German",
            "Spanish"});
            this.LanguageBox.Location = new System.Drawing.Point(16, 168);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.Size = new System.Drawing.Size(136, 21);
            this.LanguageBox.TabIndex = 3;
            // 
            // NewFilesForNewObjects
            // 
            this.NewFilesForNewObjects.Checked = true;
            this.NewFilesForNewObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NewFilesForNewObjects.Enabled = false;
            this.NewFilesForNewObjects.Location = new System.Drawing.Point(16, 89);
            this.NewFilesForNewObjects.Name = "NewFilesForNewObjects";
            this.NewFilesForNewObjects.Size = new System.Drawing.Size(208, 57);
            this.NewFilesForNewObjects.TabIndex = 2;
            this.NewFilesForNewObjects.Text = "Create new files for new Objects (currently only new Trade nodes)\r\nIf unchecked i" +
    "t will look for first not ReadOnly file";
            this.NewFilesForNewObjects.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 68);
            this.label3.TabIndex = 1;
            this.label3.Text = "If you want to always have new files while using this editor: set all to Ready On" +
    "ly and check the option above";
            // 
            // ReadOnlyNewFiles
            // 
            this.ReadOnlyNewFiles.AutoSize = true;
            this.ReadOnlyNewFiles.Checked = true;
            this.ReadOnlyNewFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReadOnlyNewFiles.Location = new System.Drawing.Point(16, 21);
            this.ReadOnlyNewFiles.Name = "ReadOnlyNewFiles";
            this.ReadOnlyNewFiles.Size = new System.Drawing.Size(208, 17);
            this.ReadOnlyNewFiles.TabIndex = 0;
            this.ReadOnlyNewFiles.Text = "Create new files when Read Only is on";
            this.ReadOnlyNewFiles.UseVisualStyleBackColor = true;
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 674);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LoadingScreen";
            this.Tag = "game";
            this.Text = "Loading screen - 1.1.8";
            this.Load += new System.EventHandler(this.LoadingScreen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox GameDirectoryBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ModDirectoryBox;
        private System.Windows.Forms.Button GameDirectoryButton;
        private System.Windows.Forms.Button ModDirectoryButton;
        private System.Windows.Forms.Button CheckFilesButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button ConfirmFileUsage;
        private System.Windows.Forms.FlowLayoutPanel LoadingPanel;
        private System.Windows.Forms.Button AllNotRead;
        private System.Windows.Forms.Button AllRead;
        private System.Windows.Forms.Button AllBoth;
        private System.Windows.Forms.Button AllGame;
        private System.Windows.Forms.Button AllMod;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ReadOnlyNewFiles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox NewFilesForNewObjects;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox LanguageBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox AppSizeBox;
        private System.Windows.Forms.Button DarkmodeOption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox StartDateBox;
    }
}