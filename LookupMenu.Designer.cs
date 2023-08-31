namespace Eu4ModEditor
{
    partial class LookupMenu
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
            this.FilterTextbox = new System.Windows.Forms.TextBox();
            this.MainListView = new System.Windows.Forms.ListView();
            this.Culture = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilterTextbox
            // 
            this.FilterTextbox.Location = new System.Drawing.Point(12, 12);
            this.FilterTextbox.Name = "FilterTextbox";
            this.FilterTextbox.Size = new System.Drawing.Size(325, 20);
            this.FilterTextbox.TabIndex = 0;
            this.FilterTextbox.TextChanged += new System.EventHandler(this.FilterTextbox_TextChanged);
            // 
            // MainListView
            // 
            this.MainListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.MainListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Culture});
            this.MainListView.HideSelection = false;
            this.MainListView.Location = new System.Drawing.Point(12, 38);
            this.MainListView.MultiSelect = false;
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(325, 400);
            this.MainListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.MainListView.TabIndex = 1;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.Details;
            // 
            // Culture
            // 
            this.Culture.Text = "Culture";
            this.Culture.Width = 300;
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.Location = new System.Drawing.Point(12, 444);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(325, 24);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LookupMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 473);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MainListView);
            this.Controls.Add(this.FilterTextbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupMenu";
            this.Text = "LookupMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilterTextbox;
        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ColumnHeader Culture;
    }
}