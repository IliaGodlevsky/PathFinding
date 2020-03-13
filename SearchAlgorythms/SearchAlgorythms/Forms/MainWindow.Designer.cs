namespace SearchAlgorythms
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseSearchAlgorythmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wideSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findPathToolStripMenuItem,
            this.chooseSearchAlgorythmToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // findPathToolStripMenuItem
            // 
            this.findPathToolStripMenuItem.Name = "findPathToolStripMenuItem";
            this.findPathToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.findPathToolStripMenuItem.Text = "Find path";
            this.findPathToolStripMenuItem.Click += new System.EventHandler(this.findPathToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // chooseSearchAlgorythmToolStripMenuItem
            // 
            this.chooseSearchAlgorythmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wideSearchToolStripMenuItem});
            this.chooseSearchAlgorythmToolStripMenuItem.Name = "chooseSearchAlgorythmToolStripMenuItem";
            this.chooseSearchAlgorythmToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.chooseSearchAlgorythmToolStripMenuItem.Text = "Choose search algorythm";
            // 
            // wideSearchToolStripMenuItem
            // 
            this.wideSearchToolStripMenuItem.Name = "wideSearchToolStripMenuItem";
            this.wideSearchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wideSearchToolStripMenuItem.Text = "Wide search";
            this.wideSearchToolStripMenuItem.Click += new System.EventHandler(this.wideSearchToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "SearchAlgorythms";
            this.Load += new System.EventHandler(this.SearchAlgorythms_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseSearchAlgorythmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wideSearchToolStripMenuItem;
    }
}

