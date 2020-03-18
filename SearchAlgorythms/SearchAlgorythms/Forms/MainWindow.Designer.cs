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
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wideSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bestfirstWideSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percent = new System.Windows.Forms.TrackBar();
            this.widthNumber = new System.Windows.Forms.TextBox();
            this.heightNumber = new System.Windows.Forms.TextBox();
            this.create = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FieldParams = new System.Windows.Forms.GroupBox();
            this.width = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.percentTextBox = new System.Windows.Forms.TextBox();
            this.fieldSize = new System.Windows.Forms.Label();
            this.percentOfObstacles = new System.Windows.Forms.Label();
            this.timeOfSearch = new System.Windows.Forms.GroupBox();
            this.time = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percent)).BeginInit();
            this.FieldParams.SuspendLayout();
            this.timeOfSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMapToolStripMenuItem,
            this.loadMapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveMapToolStripMenuItem.Text = "Save map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadMapToolStripMenuItem.Text = "Load map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.LoadMapToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findPathToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.optionsToolStripMenuItem.Text = "Algorythm";
            // 
            // findPathToolStripMenuItem
            // 
            this.findPathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wideSearchToolStripMenuItem,
            this.bestfirstWideSearchToolStripMenuItem});
            this.findPathToolStripMenuItem.Name = "findPathToolStripMenuItem";
            this.findPathToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.findPathToolStripMenuItem.Text = "Find path";
            // 
            // wideSearchToolStripMenuItem
            // 
            this.wideSearchToolStripMenuItem.Name = "wideSearchToolStripMenuItem";
            this.wideSearchToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.wideSearchToolStripMenuItem.Text = "Wide search";
            this.wideSearchToolStripMenuItem.Click += new System.EventHandler(this.WideSearchToolStripMenuItem_Click_1);
            // 
            // bestfirstWideSearchToolStripMenuItem
            // 
            this.bestfirstWideSearchToolStripMenuItem.Name = "bestfirstWideSearchToolStripMenuItem";
            this.bestfirstWideSearchToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.bestfirstWideSearchToolStripMenuItem.Text = "Best-first wide search";
            this.bestfirstWideSearchToolStripMenuItem.Click += new System.EventHandler(this.BestfirstWideSearchToolStripMenuItem_Click);
            // 
            // percent
            // 
            this.percent.Location = new System.Drawing.Point(6, 45);
            this.percent.Maximum = 100;
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(104, 45);
            this.percent.TabIndex = 1;
            this.percent.Scroll += new System.EventHandler(this.Percent_Scroll);
            // 
            // widthNumber
            // 
            this.widthNumber.Location = new System.Drawing.Point(84, 111);
            this.widthNumber.Name = "widthNumber";
            this.widthNumber.Size = new System.Drawing.Size(64, 20);
            this.widthNumber.TabIndex = 2;
            this.widthNumber.TextChanged += new System.EventHandler(this.WidthNumber_TextChanged);
            // 
            // heightNumber
            // 
            this.heightNumber.Location = new System.Drawing.Point(84, 136);
            this.heightNumber.Name = "heightNumber";
            this.heightNumber.Size = new System.Drawing.Size(64, 20);
            this.heightNumber.TabIndex = 3;
            this.heightNumber.TextChanged += new System.EventHandler(this.HeightNumber_TextChanged);
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(17, 171);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(131, 23);
            this.create.TabIndex = 6;
            this.create.Text = "Create graph";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.Create_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Graph height";
            // 
            // FieldParams
            // 
            this.FieldParams.Controls.Add(this.width);
            this.FieldParams.Controls.Add(this.refresh);
            this.FieldParams.Controls.Add(this.percentTextBox);
            this.FieldParams.Controls.Add(this.fieldSize);
            this.FieldParams.Controls.Add(this.percentOfObstacles);
            this.FieldParams.Controls.Add(this.percent);
            this.FieldParams.Controls.Add(this.label2);
            this.FieldParams.Controls.Add(this.create);
            this.FieldParams.Controls.Add(this.widthNumber);
            this.FieldParams.Controls.Add(this.heightNumber);
            this.FieldParams.Location = new System.Drawing.Point(12, 27);
            this.FieldParams.Name = "FieldParams";
            this.FieldParams.Size = new System.Drawing.Size(159, 229);
            this.FieldParams.TabIndex = 10;
            this.FieldParams.TabStop = false;
            this.FieldParams.Text = "Field parametres";
            // 
            // width
            // 
            this.width.AutoSize = true;
            this.width.Location = new System.Drawing.Point(14, 114);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(64, 13);
            this.width.TabIndex = 14;
            this.width.Text = "Graph width";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(17, 200);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(131, 23);
            this.refresh.TabIndex = 13;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // percentTextBox
            // 
            this.percentTextBox.Location = new System.Drawing.Point(116, 54);
            this.percentTextBox.Name = "percentTextBox";
            this.percentTextBox.Size = new System.Drawing.Size(32, 20);
            this.percentTextBox.TabIndex = 12;
            this.percentTextBox.TextChanged += new System.EventHandler(this.PercentTextBox_TextChanged);
            // 
            // fieldSize
            // 
            this.fieldSize.AutoSize = true;
            this.fieldSize.Location = new System.Drawing.Point(44, 93);
            this.fieldSize.Name = "fieldSize";
            this.fieldSize.Size = new System.Drawing.Size(57, 13);
            this.fieldSize.TabIndex = 11;
            this.fieldSize.Text = "Graph size";
            // 
            // percentOfObstacles
            // 
            this.percentOfObstacles.AutoSize = true;
            this.percentOfObstacles.Location = new System.Drawing.Point(26, 29);
            this.percentOfObstacles.Name = "percentOfObstacles";
            this.percentOfObstacles.Size = new System.Drawing.Size(104, 13);
            this.percentOfObstacles.TabIndex = 10;
            this.percentOfObstacles.Text = "Percent of obstacles";
            // 
            // timeOfSearch
            // 
            this.timeOfSearch.Controls.Add(this.time);
            this.timeOfSearch.Location = new System.Drawing.Point(18, 272);
            this.timeOfSearch.Name = "timeOfSearch";
            this.timeOfSearch.Size = new System.Drawing.Size(153, 42);
            this.timeOfSearch.TabIndex = 11;
            this.timeOfSearch.TabStop = false;
            this.timeOfSearch.Text = "Time of search";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(41, 20);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(35, 13);
            this.time.TabIndex = 0;
            this.time.Text = "label1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.timeOfSearch);
            this.Controls.Add(this.FieldParams);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "SearchAlgorythms";
            this.Load += new System.EventHandler(this.SearchAlgorythms_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percent)).EndInit();
            this.FieldParams.ResumeLayout(false);
            this.FieldParams.PerformLayout();
            this.timeOfSearch.ResumeLayout(false);
            this.timeOfSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wideSearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bestfirstWideSearchToolStripMenuItem;
        private System.Windows.Forms.TrackBar percent;
        private System.Windows.Forms.TextBox widthNumber;
        private System.Windows.Forms.TextBox heightNumber;
        private System.Windows.Forms.Button create;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox FieldParams;
        private System.Windows.Forms.Label fieldSize;
        private System.Windows.Forms.Label percentOfObstacles;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.TextBox percentTextBox;
        private System.Windows.Forms.Button refresh;
        private System.Windows.Forms.Label width;
        private System.Windows.Forms.GroupBox timeOfSearch;
        private System.Windows.Forms.Label time;
    }
}

