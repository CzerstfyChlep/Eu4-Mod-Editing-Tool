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
            this.ReadOnlyNewFiles = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NewFilesForNewObjects = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameDirectoryBox
            // 
            this.GameDirectoryBox.Location = new System.Drawing.Point(20, 41);
            this.GameDirectoryBox.Name = "GameDirectoryBox";
            this.GameDirectoryBox.Size = new System.Drawing.Size(465, 20);
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
            this.ModDirectoryBox.Size = new System.Drawing.Size(465, 20);
            this.ModDirectoryBox.TabIndex = 2;
            // 
            // GameDirectoryButton
            // 
            this.GameDirectoryButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GameDirectoryButton.Location = new System.Drawing.Point(491, 41);
            this.GameDirectoryButton.Name = "GameDirectoryButton";
            this.GameDirectoryButton.Size = new System.Drawing.Size(37, 20);
            this.GameDirectoryButton.TabIndex = 4;
            this.GameDirectoryButton.Text = "...";
            this.GameDirectoryButton.UseVisualStyleBackColor = true;
            this.GameDirectoryButton.Click += new System.EventHandler(this.GameDirectoryButton_Click);
            // 
            // ModDirectoryButton
            // 
            this.ModDirectoryButton.Location = new System.Drawing.Point(491, 89);
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
            this.CheckFilesButton.Size = new System.Drawing.Size(508, 23);
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
            this.groupBox1.Size = new System.Drawing.Size(538, 152);
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
            this.groupBox2.Location = new System.Drawing.Point(12, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(775, 610);
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
            this.LoadingPanel.Location = new System.Drawing.Point(6, 53);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(763, 522);
            this.LoadingPanel.TabIndex = 12;
            // 
            // ConfirmFileUsage
            // 
            this.ConfirmFileUsage.Location = new System.Drawing.Point(6, 581);
            this.ConfirmFileUsage.Name = "ConfirmFileUsage";
            this.ConfirmFileUsage.Size = new System.Drawing.Size(749, 23);
            this.ConfirmFileUsage.TabIndex = 11;
            this.ConfirmFileUsage.Text = "Confirm file usage";
            this.ConfirmFileUsage.UseVisualStyleBackColor = true;
            this.ConfirmFileUsage.Click += new System.EventHandler(this.ConfirmFileUsage_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.NewFilesForNewObjects);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.ReadOnlyNewFiles);
            this.groupBox3.Location = new System.Drawing.Point(552, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(235, 152);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Special options";
            // 
            // ReadOnlyNewFiles
            // 
            this.ReadOnlyNewFiles.AutoSize = true;
            this.ReadOnlyNewFiles.Location = new System.Drawing.Point(16, 21);
            this.ReadOnlyNewFiles.Name = "ReadOnlyNewFiles";
            this.ReadOnlyNewFiles.Size = new System.Drawing.Size(208, 17);
            this.ReadOnlyNewFiles.TabIndex = 0;
            this.ReadOnlyNewFiles.Text = "Create new files when Read Only is on";
            this.ReadOnlyNewFiles.UseVisualStyleBackColor = true;
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
            // NewFilesForNewObjects
            // 
            this.NewFilesForNewObjects.Checked = true;
            this.NewFilesForNewObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NewFilesForNewObjects.Location = new System.Drawing.Point(16, 89);
            this.NewFilesForNewObjects.Name = "NewFilesForNewObjects";
            this.NewFilesForNewObjects.Size = new System.Drawing.Size(208, 57);
            this.NewFilesForNewObjects.TabIndex = 2;
            this.NewFilesForNewObjects.Text = "Create new files for new Objects (new Trade nodes, Areas etc.)\r\nIf unchecked it w" +
    "ill look for first not ReadOnly file";
            this.NewFilesForNewObjects.UseVisualStyleBackColor = true;
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 792);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoadingScreen";
            this.Tag = "game";
            this.Text = "Loading screen";
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
    }
}