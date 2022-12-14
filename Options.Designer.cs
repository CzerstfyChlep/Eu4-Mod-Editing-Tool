namespace Eu4ModEditor
{
    partial class Options
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.OptionTabs = new System.Windows.Forms.TabControl();
            this.GeneralOptionsPage = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SavingOnCrashEnabled = new System.Windows.Forms.CheckBox();
            this.CrashSavingPathGoto = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.CrashSavingPathSet = new System.Windows.Forms.Button();
            this.CrashSavingPath = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.GotoPathAutosaving = new System.Windows.Forms.Button();
            this.SetPathAutosaving = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.AutoSavingEnabled = new System.Windows.Forms.CheckBox();
            this.AutosavingPath = new System.Windows.Forms.TextBox();
            this.AutosavingInterval = new System.Windows.Forms.NumericUpDown();
            this.RandomnessOptionsPage = new System.Windows.Forms.TabPage();
            this.ProvinceDev = new System.Windows.Forms.GroupBox();
            this.CustomRandomValues = new System.Windows.Forms.CheckBox();
            this.SetTheSameForAll = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HighMaximum = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.HighAverage = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.HighMinimum = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MediumMaximum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.MediumAverage = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.MediumMinimum = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.LowMaximum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.LowAverage = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.LowMinimum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.OnExitAutoSave = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.OptionTabs.SuspendLayout();
            this.GeneralOptionsPage.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutosavingInterval)).BeginInit();
            this.RandomnessOptionsPage.SuspendLayout();
            this.ProvinceDev.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HighMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighMinimum)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MediumMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediumAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediumMinimum)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowMinimum)).BeginInit();
            this.SuspendLayout();
            // 
            // OptionTabs
            // 
            this.OptionTabs.Controls.Add(this.GeneralOptionsPage);
            this.OptionTabs.Controls.Add(this.RandomnessOptionsPage);
            this.OptionTabs.Location = new System.Drawing.Point(3, 3);
            this.OptionTabs.Name = "OptionTabs";
            this.OptionTabs.SelectedIndex = 0;
            this.OptionTabs.Size = new System.Drawing.Size(382, 299);
            this.OptionTabs.TabIndex = 0;
            // 
            // GeneralOptionsPage
            // 
            this.GeneralOptionsPage.Controls.Add(this.groupBox5);
            this.GeneralOptionsPage.Controls.Add(this.groupBox4);
            this.GeneralOptionsPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralOptionsPage.Name = "GeneralOptionsPage";
            this.GeneralOptionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralOptionsPage.Size = new System.Drawing.Size(374, 273);
            this.GeneralOptionsPage.TabIndex = 0;
            this.GeneralOptionsPage.Text = "General";
            this.GeneralOptionsPage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.SavingOnCrashEnabled);
            this.groupBox5.Controls.Add(this.CrashSavingPathGoto);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.CrashSavingPathSet);
            this.groupBox5.Controls.Add(this.CrashSavingPath);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Location = new System.Drawing.Point(6, 141);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(361, 126);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Saving on crash";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(258, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Tip: Path can be the same as the one for autosaving.";
            // 
            // SavingOnCrashEnabled
            // 
            this.SavingOnCrashEnabled.AutoSize = true;
            this.SavingOnCrashEnabled.Location = new System.Drawing.Point(9, 19);
            this.SavingOnCrashEnabled.Name = "SavingOnCrashEnabled";
            this.SavingOnCrashEnabled.Size = new System.Drawing.Size(65, 17);
            this.SavingOnCrashEnabled.TabIndex = 8;
            this.SavingOnCrashEnabled.Text = "Enabled";
            this.SavingOnCrashEnabled.UseVisualStyleBackColor = true;
            this.SavingOnCrashEnabled.CheckedChanged += new System.EventHandler(this.SavingOnCrashEnabled_CheckedChanged);
            // 
            // CrashSavingPathGoto
            // 
            this.CrashSavingPathGoto.Location = new System.Drawing.Point(279, 95);
            this.CrashSavingPathGoto.Name = "CrashSavingPathGoto";
            this.CrashSavingPathGoto.Size = new System.Drawing.Size(75, 23);
            this.CrashSavingPathGoto.TabIndex = 11;
            this.CrashSavingPathGoto.Text = "Go to";
            this.CrashSavingPathGoto.UseVisualStyleBackColor = true;
            this.CrashSavingPathGoto.Click += new System.EventHandler(this.CrashSavingPathGoto_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 40);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(336, 26);
            this.label13.TabIndex = 0;
            this.label13.Text = "It will work only on so called handled crashes, meaning that if the tool\r\ncloses " +
    "because of third party software/OS problems etc. it won\'t work!";
            // 
            // CrashSavingPathSet
            // 
            this.CrashSavingPathSet.Location = new System.Drawing.Point(318, 68);
            this.CrashSavingPathSet.Name = "CrashSavingPathSet";
            this.CrashSavingPathSet.Size = new System.Drawing.Size(36, 21);
            this.CrashSavingPathSet.TabIndex = 10;
            this.CrashSavingPathSet.Text = "...";
            this.CrashSavingPathSet.UseVisualStyleBackColor = true;
            this.CrashSavingPathSet.Click += new System.EventHandler(this.CrashSavingPathSet_Click);
            // 
            // CrashSavingPath
            // 
            this.CrashSavingPath.Location = new System.Drawing.Point(48, 69);
            this.CrashSavingPath.Name = "CrashSavingPath";
            this.CrashSavingPath.Size = new System.Drawing.Size(264, 20);
            this.CrashSavingPath.TabIndex = 8;
            this.CrashSavingPath.TextChanged += new System.EventHandler(this.CrashSavingPath_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 72);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 9;
            this.label14.Text = "Path:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.OnExitAutoSave);
            this.groupBox4.Controls.Add(this.GotoPathAutosaving);
            this.groupBox4.Controls.Add(this.SetPathAutosaving);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.AutoSavingEnabled);
            this.groupBox4.Controls.Add(this.AutosavingPath);
            this.groupBox4.Controls.Add(this.AutosavingInterval);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(361, 129);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Autosaving";
            // 
            // GotoPathAutosaving
            // 
            this.GotoPathAutosaving.Location = new System.Drawing.Point(280, 96);
            this.GotoPathAutosaving.Name = "GotoPathAutosaving";
            this.GotoPathAutosaving.Size = new System.Drawing.Size(75, 23);
            this.GotoPathAutosaving.TabIndex = 7;
            this.GotoPathAutosaving.Text = "Go to";
            this.GotoPathAutosaving.UseVisualStyleBackColor = true;
            this.GotoPathAutosaving.Click += new System.EventHandler(this.GotoPathAutosaving_Click);
            // 
            // SetPathAutosaving
            // 
            this.SetPathAutosaving.Location = new System.Drawing.Point(319, 69);
            this.SetPathAutosaving.Name = "SetPathAutosaving";
            this.SetPathAutosaving.Size = new System.Drawing.Size(36, 21);
            this.SetPathAutosaving.TabIndex = 6;
            this.SetPathAutosaving.Text = "...";
            this.SetPathAutosaving.UseVisualStyleBackColor = true;
            this.SetPathAutosaving.Click += new System.EventHandler(this.SetPathAutosaving_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 73);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "Path:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(108, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "minutes";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 44);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Interval:";
            // 
            // AutoSavingEnabled
            // 
            this.AutoSavingEnabled.AutoSize = true;
            this.AutoSavingEnabled.Location = new System.Drawing.Point(6, 19);
            this.AutoSavingEnabled.Name = "AutoSavingEnabled";
            this.AutoSavingEnabled.Size = new System.Drawing.Size(65, 17);
            this.AutoSavingEnabled.TabIndex = 1;
            this.AutoSavingEnabled.Text = "Enabled";
            this.AutoSavingEnabled.UseVisualStyleBackColor = true;
            this.AutoSavingEnabled.CheckedChanged += new System.EventHandler(this.AutoSavingEnabled_CheckedChanged);
            // 
            // AutosavingPath
            // 
            this.AutosavingPath.Location = new System.Drawing.Point(49, 70);
            this.AutosavingPath.Name = "AutosavingPath";
            this.AutosavingPath.Size = new System.Drawing.Size(264, 20);
            this.AutosavingPath.TabIndex = 0;
            this.AutosavingPath.TextChanged += new System.EventHandler(this.AutosavingPath_TextChanged);
            // 
            // AutosavingInterval
            // 
            this.AutosavingInterval.Location = new System.Drawing.Point(49, 42);
            this.AutosavingInterval.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.AutosavingInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AutosavingInterval.Name = "AutosavingInterval";
            this.AutosavingInterval.Size = new System.Drawing.Size(53, 20);
            this.AutosavingInterval.TabIndex = 2;
            this.AutosavingInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AutosavingInterval.ValueChanged += new System.EventHandler(this.AutosavingInterval_ValueChanged);
            // 
            // RandomnessOptionsPage
            // 
            this.RandomnessOptionsPage.Controls.Add(this.ProvinceDev);
            this.RandomnessOptionsPage.Location = new System.Drawing.Point(4, 22);
            this.RandomnessOptionsPage.Name = "RandomnessOptionsPage";
            this.RandomnessOptionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.RandomnessOptionsPage.Size = new System.Drawing.Size(374, 273);
            this.RandomnessOptionsPage.TabIndex = 1;
            this.RandomnessOptionsPage.Text = "Randomness";
            this.RandomnessOptionsPage.UseVisualStyleBackColor = true;
            // 
            // ProvinceDev
            // 
            this.ProvinceDev.Controls.Add(this.CustomRandomValues);
            this.ProvinceDev.Controls.Add(this.SetTheSameForAll);
            this.ProvinceDev.Controls.Add(this.groupBox3);
            this.ProvinceDev.Controls.Add(this.groupBox2);
            this.ProvinceDev.Controls.Add(this.groupBox1);
            this.ProvinceDev.Location = new System.Drawing.Point(6, 6);
            this.ProvinceDev.Name = "ProvinceDev";
            this.ProvinceDev.Size = new System.Drawing.Size(361, 171);
            this.ProvinceDev.TabIndex = 0;
            this.ProvinceDev.TabStop = false;
            this.ProvinceDev.Text = "Province development";
            // 
            // CustomRandomValues
            // 
            this.CustomRandomValues.AutoSize = true;
            this.CustomRandomValues.Location = new System.Drawing.Point(6, 20);
            this.CustomRandomValues.Name = "CustomRandomValues";
            this.CustomRandomValues.Size = new System.Drawing.Size(61, 17);
            this.CustomRandomValues.TabIndex = 8;
            this.CustomRandomValues.Text = "Custom";
            this.CustomRandomValues.UseVisualStyleBackColor = true;
            this.CustomRandomValues.CheckedChanged += new System.EventHandler(this.CustomRandomValues_CheckedChanged);
            // 
            // SetTheSameForAll
            // 
            this.SetTheSameForAll.AutoSize = true;
            this.SetTheSameForAll.Location = new System.Drawing.Point(6, 143);
            this.SetTheSameForAll.Name = "SetTheSameForAll";
            this.SetTheSameForAll.Size = new System.Drawing.Size(202, 17);
            this.SetTheSameForAll.TabIndex = 7;
            this.SetTheSameForAll.Text = "Same value for all selected provinces";
            this.SetTheSameForAll.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.HighMaximum);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.HighAverage);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.HighMinimum);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(240, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(111, 94);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "High";
            // 
            // HighMaximum
            // 
            this.HighMaximum.Location = new System.Drawing.Point(39, 66);
            this.HighMaximum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.HighMaximum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighMaximum.Name = "HighMaximum";
            this.HighMaximum.Size = new System.Drawing.Size(66, 20);
            this.HighMaximum.TabIndex = 5;
            this.HighMaximum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighMaximum.ValueChanged += new System.EventHandler(this.HighMaximum_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Max.";
            // 
            // HighAverage
            // 
            this.HighAverage.Location = new System.Drawing.Point(39, 40);
            this.HighAverage.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.HighAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighAverage.Name = "HighAverage";
            this.HighAverage.Size = new System.Drawing.Size(66, 20);
            this.HighAverage.TabIndex = 3;
            this.HighAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighAverage.ValueChanged += new System.EventHandler(this.HighAverage_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Avg.";
            // 
            // HighMinimum
            // 
            this.HighMinimum.Location = new System.Drawing.Point(39, 14);
            this.HighMinimum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.HighMinimum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighMinimum.Name = "HighMinimum";
            this.HighMinimum.Size = new System.Drawing.Size(66, 20);
            this.HighMinimum.TabIndex = 1;
            this.HighMinimum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HighMinimum.ValueChanged += new System.EventHandler(this.HighMinimum_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Min.";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MediumMaximum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.MediumAverage);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.MediumMinimum);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(123, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(111, 94);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Medium";
            // 
            // MediumMaximum
            // 
            this.MediumMaximum.Location = new System.Drawing.Point(39, 66);
            this.MediumMaximum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.MediumMaximum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumMaximum.Name = "MediumMaximum";
            this.MediumMaximum.Size = new System.Drawing.Size(66, 20);
            this.MediumMaximum.TabIndex = 5;
            this.MediumMaximum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumMaximum.ValueChanged += new System.EventHandler(this.MediumMaximum_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Max.";
            // 
            // MediumAverage
            // 
            this.MediumAverage.Location = new System.Drawing.Point(39, 40);
            this.MediumAverage.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.MediumAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumAverage.Name = "MediumAverage";
            this.MediumAverage.Size = new System.Drawing.Size(66, 20);
            this.MediumAverage.TabIndex = 3;
            this.MediumAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumAverage.ValueChanged += new System.EventHandler(this.MediumAverage_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Avg.";
            // 
            // MediumMinimum
            // 
            this.MediumMinimum.Location = new System.Drawing.Point(39, 14);
            this.MediumMinimum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.MediumMinimum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumMinimum.Name = "MediumMinimum";
            this.MediumMinimum.Size = new System.Drawing.Size(66, 20);
            this.MediumMinimum.TabIndex = 1;
            this.MediumMinimum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MediumMinimum.ValueChanged += new System.EventHandler(this.MediumMinimum_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Min.";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LowMaximum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.LowAverage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LowMinimum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(111, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Low";
            // 
            // LowMaximum
            // 
            this.LowMaximum.Location = new System.Drawing.Point(39, 66);
            this.LowMaximum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.LowMaximum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowMaximum.Name = "LowMaximum";
            this.LowMaximum.Size = new System.Drawing.Size(66, 20);
            this.LowMaximum.TabIndex = 5;
            this.LowMaximum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowMaximum.ValueChanged += new System.EventHandler(this.LowMaximum_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Max.";
            // 
            // LowAverage
            // 
            this.LowAverage.Location = new System.Drawing.Point(39, 40);
            this.LowAverage.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.LowAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowAverage.Name = "LowAverage";
            this.LowAverage.Size = new System.Drawing.Size(66, 20);
            this.LowAverage.TabIndex = 3;
            this.LowAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowAverage.ValueChanged += new System.EventHandler(this.LowAverage_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Avg.";
            // 
            // LowMinimum
            // 
            this.LowMinimum.Location = new System.Drawing.Point(39, 14);
            this.LowMinimum.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.LowMinimum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowMinimum.Name = "LowMinimum";
            this.LowMinimum.Size = new System.Drawing.Size(66, 20);
            this.LowMinimum.TabIndex = 1;
            this.LowMinimum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LowMinimum.ValueChanged += new System.EventHandler(this.LowMinimum_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Min.";
            // 
            // OnExitAutoSave
            // 
            this.OnExitAutoSave.AutoSize = true;
            this.OnExitAutoSave.Location = new System.Drawing.Point(77, 19);
            this.OnExitAutoSave.Name = "OnExitAutoSave";
            this.OnExitAutoSave.Size = new System.Drawing.Size(59, 17);
            this.OnExitAutoSave.TabIndex = 8;
            this.OnExitAutoSave.Text = "On exit";
            this.OnExitAutoSave.UseVisualStyleBackColor = true;
            this.OnExitAutoSave.CheckedChanged += new System.EventHandler(this.OnExitAutoSave_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 101);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(260, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "Leavy empty to save in the same directory as the tool.";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 306);
            this.Controls.Add(this.OptionTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.OptionTabs.ResumeLayout(false);
            this.GeneralOptionsPage.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutosavingInterval)).EndInit();
            this.RandomnessOptionsPage.ResumeLayout(false);
            this.ProvinceDev.ResumeLayout(false);
            this.ProvinceDev.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HighMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighMinimum)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MediumMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediumAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MediumMinimum)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LowMinimum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl OptionTabs;
        private System.Windows.Forms.TabPage GeneralOptionsPage;
        private System.Windows.Forms.TabPage RandomnessOptionsPage;
        private System.Windows.Forms.GroupBox ProvinceDev;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown HighMaximum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown HighAverage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown HighMinimum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown MediumMaximum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown MediumAverage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown MediumMinimum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown LowMaximum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown LowAverage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown LowMinimum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox SavingOnCrashEnabled;
        private System.Windows.Forms.Button CrashSavingPathGoto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button CrashSavingPathSet;
        private System.Windows.Forms.TextBox CrashSavingPath;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button GotoPathAutosaving;
        private System.Windows.Forms.Button SetPathAutosaving;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox AutoSavingEnabled;
        private System.Windows.Forms.TextBox AutosavingPath;
        private System.Windows.Forms.NumericUpDown AutosavingInterval;
        private System.Windows.Forms.CheckBox SetTheSameForAll;
        private System.Windows.Forms.CheckBox CustomRandomValues;
        private System.Windows.Forms.CheckBox OnExitAutoSave;
        private System.Windows.Forms.Label label16;
    }
}