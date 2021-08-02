using WindowsFormsVersion.View;

namespace WindowsFormsVersion.Forms
{
    partial class MainWindow
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
            this.statistics = new System.Windows.Forms.Label();
            this.parametres = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.unweightedButton = new Syncfusion.Windows.Forms.Tools.ToolStripRadioButton();
            this.weightedButton = new Syncfusion.Windows.Forms.Tools.ToolStripRadioButton();
            this.graphField = new WindowsFormsVersion.View.WinFormsGraphField();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statistics
            // 
            this.statistics.AutoSize = true;
            this.statistics.Location = new System.Drawing.Point(11, 70);
            this.statistics.Name = "statistics";
            this.statistics.Size = new System.Drawing.Size(46, 17);
            this.statistics.TabIndex = 17;
            this.statistics.Text = "label1";
            // 
            // parametres
            // 
            this.parametres.AutoSize = true;
            this.parametres.Location = new System.Drawing.Point(11, 41);
            this.parametres.Name = "parametres";
            this.parametres.Size = new System.Drawing.Size(46, 17);
            this.parametres.TabIndex = 18;
            this.parametres.Text = "label2";
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.unweightedButton,
            this.weightedButton});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(542, 31);
            this.menu.TabIndex = 19;
            this.menu.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::WindowsFormsVersion.Properties.Resources.save;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::WindowsFormsVersion.Properties.Resources.open;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::WindowsFormsVersion.Properties.Resources.refresh;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::WindowsFormsVersion.Properties.Resources.clear;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::WindowsFormsVersion.Properties.Resources.find;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(29, 28);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // unweightedButton
            // 
            this.unweightedButton.Checked = true;
            this.unweightedButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.unweightedButton.Name = "unweightedButton";
            this.unweightedButton.Size = new System.Drawing.Size(22, 28);
            // 
            // weightedButton
            // 
            this.weightedButton.Name = "weightedButton";
            this.weightedButton.Size = new System.Drawing.Size(22, 28);
            // 
            // graphField
            // 
            this.graphField.AutoSize = true;
            this.graphField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.graphField.Location = new System.Drawing.Point(4, 95);
            this.graphField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.graphField.Name = "graphField";
            this.graphField.Size = new System.Drawing.Size(493, 462);
            this.graphField.TabIndex = 15;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 562);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 24);
            this.flowLayoutPanel1.TabIndex = 20;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(542, 622);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.parametres);
            this.Controls.Add(this.statistics);
            this.Controls.Add(this.graphField);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainWindow";
            this.Text = "PathFind";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WinFormsGraphField graphField;
        private System.Windows.Forms.Label statistics;
        private System.Windows.Forms.Label parametres;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStrip menu;
        private Syncfusion.Windows.Forms.Tools.ToolStripRadioButton unweightedButton;
        private Syncfusion.Windows.Forms.Tools.ToolStripRadioButton weightedButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

