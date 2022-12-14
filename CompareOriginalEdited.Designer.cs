namespace Eu4ModEditor
{
    partial class CompareOriginalEdited
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareOriginalEdited));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.OriginalText = new System.Windows.Forms.RichTextBox();
            this.EditedText = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(214, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Original";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(983, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Edited";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(769, 749);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(486, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(63, 749);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(486, 30);
            this.button2.TabIndex = 5;
            this.button2.Text = "Keep old";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // OriginalText
            // 
            this.OriginalText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.OriginalText.Location = new System.Drawing.Point(12, 45);
            this.OriginalText.Name = "OriginalText";
            this.OriginalText.ReadOnly = true;
            this.OriginalText.Size = new System.Drawing.Size(654, 698);
            this.OriginalText.TabIndex = 6;
            this.OriginalText.Text = "";
            // 
            // EditedText
            // 
            this.EditedText.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.EditedText.Location = new System.Drawing.Point(673, 45);
            this.EditedText.Name = "EditedText";
            this.EditedText.Size = new System.Drawing.Size(642, 698);
            this.EditedText.TabIndex = 7;
            this.EditedText.Text = "";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(358, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(528, 36);
            this.label3.TabIndex = 8;
            this.label3.Text = "If you see weird letters, DO NOT save the file here. Encoding is wrong, no idea h" +
    "ow to fix. If you save via the previous window it will work normally.\r\n";
            // 
            // CompareOriginalEdited
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1327, 791);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EditedText);
            this.Controls.Add(this.OriginalText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CompareOriginalEdited";
            this.Text = "Compare files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox OriginalText;
        private System.Windows.Forms.RichTextBox EditedText;
        private System.Windows.Forms.Label label3;
    }
}