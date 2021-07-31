﻿namespace WindowsFormsVersion.View
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
            this.visualizeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.delaySlider)).BeginInit();
            this.SuspendLayout();
            // 
            // algorithmListBox
            // 
            this.algorithmListBox.FormattingEnabled = true;
            this.algorithmListBox.ItemHeight = 16;
            this.algorithmListBox.Location = new System.Drawing.Point(28, 10);
            this.algorithmListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.algorithmListBox.Name = "algorithmListBox";
            this.algorithmListBox.Size = new System.Drawing.Size(220, 132);
            this.algorithmListBox.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(28, 265);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(67, 26);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(181, 265);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(67, 26);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // delaySlider
            // 
            this.delaySlider.Location = new System.Drawing.Point(28, 177);
            this.delaySlider.Margin = new System.Windows.Forms.Padding(4);
            this.delaySlider.Name = "delaySlider";
            this.delaySlider.Size = new System.Drawing.Size(156, 56);
            this.delaySlider.TabIndex = 3;
            // 
            // delayTextBox
            // 
            this.delayTextBox.Location = new System.Drawing.Point(192, 177);
            this.delayTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.delayTextBox.Name = "delayTextBox";
            this.delayTextBox.Size = new System.Drawing.Size(56, 22);
            this.delayTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Algorithm speed";
            // 
            // visualizeCheckBox
            // 
            this.visualizeCheckBox.AutoSize = true;
            this.visualizeCheckBox.Location = new System.Drawing.Point(38, 221);
            this.visualizeCheckBox.Name = "visualizeCheckBox";
            this.visualizeCheckBox.Size = new System.Drawing.Size(146, 21);
            this.visualizeCheckBox.TabIndex = 6;
            this.visualizeCheckBox.Text = "Apply visualization";
            this.visualizeCheckBox.UseVisualStyleBackColor = true;
            // 
            // PathFindingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 300);
            this.Controls.Add(this.visualizeCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delayTextBox);
            this.Controls.Add(this.delaySlider);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.algorithmListBox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "PathFindingWindow";
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
        private System.Windows.Forms.CheckBox visualizeCheckBox;
    }
}