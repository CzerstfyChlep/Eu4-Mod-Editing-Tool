namespace Eu4ModEditor
{
    partial class LoadingProgress
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
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.LoadingProgressLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 104);
            this.progressBar2.Maximum = 110;
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(353, 30);
            this.progressBar2.TabIndex = 1;
            // 
            // LoadingProgressLabel
            // 
            this.LoadingProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoadingProgressLabel.Location = new System.Drawing.Point(12, 9);
            this.LoadingProgressLabel.Name = "LoadingProgressLabel";
            this.LoadingProgressLabel.Size = new System.Drawing.Size(353, 92);
            this.LoadingProgressLabel.TabIndex = 2;
            this.LoadingProgressLabel.Text = "Loading Progress";
            this.LoadingProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 147);
            this.ControlBox = false;
            this.Controls.Add(this.LoadingProgressLabel);
            this.Controls.Add(this.progressBar2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "LoadingProgress";
            this.Text = "Loading";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label LoadingProgressLabel;
    }
}