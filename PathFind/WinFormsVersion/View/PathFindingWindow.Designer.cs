namespace WinFormsVersion.View
{
    partial class PathFindingWindow
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
            this.algorithmListBox = new System.Windows.Forms.ListBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.delaySlider = new System.Windows.Forms.TrackBar();
            this.delayTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.delaySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // algorithmListBox
            // 
            this.algorithmListBox.FormattingEnabled = true;
            this.algorithmListBox.Location = new System.Drawing.Point(21, 8);
            this.algorithmListBox.Margin = new System.Windows.Forms.Padding(2);
            this.algorithmListBox.Name = "algorithmListBox";
            this.algorithmListBox.Size = new System.Drawing.Size(166, 108);
            this.algorithmListBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(21, 194);
            this.okButton.Margin = new System.Windows.Forms.Padding(2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 21);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(137, 194);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(50, 21);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // delaySlider
            // 
            this.delaySlider.Location = new System.Drawing.Point(21, 144);
            this.delaySlider.Name = "delaySlider";
            this.delaySlider.Size = new System.Drawing.Size(117, 45);
            this.delaySlider.TabIndex = 3;
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(144, 144);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(43, 20);
            this.delayTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Algorithm speed";
            // 
            // PathFindWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 226);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.delaySlider);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.algorithmListBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PathFindWindow";
            this.Text = "PathFindWindow";
            ((System.ComponentModel.ISupportInitialize)(this.delaySlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox algorithmListBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TrackBar delaySlider;
        private System.Windows.Forms.TextBox delayTextBox;
        private System.Windows.Forms.Label label1;
    }
}