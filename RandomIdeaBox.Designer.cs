namespace Eu4ModEditor
{
    partial class RandomIdeaBox
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
            this.RandomIdeaButton = new System.Windows.Forms.Button();
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RandomIdeaButton
            // 
            this.RandomIdeaButton.Location = new System.Drawing.Point(12, 12);
            this.RandomIdeaButton.Name = "RandomIdeaButton";
            this.RandomIdeaButton.Size = new System.Drawing.Size(174, 23);
            this.RandomIdeaButton.TabIndex = 0;
            this.RandomIdeaButton.Text = "Get Random Idea";
            this.RandomIdeaButton.UseVisualStyleBackColor = true;
            this.RandomIdeaButton.Click += new System.EventHandler(this.RandomIdeaButton_Click);
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(12, 41);
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(174, 20);
            this.OutputBox.TabIndex = 1;
            // 
            // RandomIdeaBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(201, 77);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.RandomIdeaButton);
            this.MaximizeBox = false;
            this.Name = "RandomIdeaBox";
            this.Text = "Random Idea";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RandomIdeaButton;
        private System.Windows.Forms.TextBox OutputBox;
    }
}