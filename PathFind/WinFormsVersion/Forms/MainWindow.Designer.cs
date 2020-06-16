using WinFormsVerstion.Forms;

namespace WinFormsVersion.Forms
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
            this.dijkstraAlgorythmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greedySearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greedyForDistanceAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greedyForValueAlgorithmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deepPathFindToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Field = new WinFormsVerstion.Forms.FieldControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percent)).BeginInit();
            this.FieldParams.SuspendLayout();
            this.timeOfSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMapToolStripMenuItem,
            this.loadMapToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(194, 34);
            this.saveMapToolStripMenuItem.Text = "Save map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.SaveMapToolStripMenuItem_Click);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(194, 34);
            this.loadMapToolStripMenuItem.Text = "Load map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.LoadMapToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findPathToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(108, 29);
            this.optionsToolStripMenuItem.Text = "Algorithm";
            // 
            // findPathToolStripMenuItem
            // 
            this.findPathToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wideSearchToolStripMenuItem,
            this.dijkstraAlgorythmToolStripMenuItem,
            this.aSearchToolStripMenuItem,
            this.greedySearchToolStripMenuItem,
            this.deepPathFindToolStripMenuItem});
            this.findPathToolStripMenuItem.Name = "findPathToolStripMenuItem";
            this.findPathToolStripMenuItem.Size = new System.Drawing.Size(189, 34);
            this.findPathToolStripMenuItem.Text = "Find path";
            // 
            // wideSearchToolStripMenuItem
            // 
            this.wideSearchToolStripMenuItem.Name = "wideSearchToolStripMenuItem";
            this.wideSearchToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.wideSearchToolStripMenuItem.Text = "Wide path find";
            this.wideSearchToolStripMenuItem.Click += new System.EventHandler(this.WideSearchToolStripMenuItem);
            // 
            // dijkstraAlgorythmToolStripMenuItem
            // 
            this.dijkstraAlgorythmToolStripMenuItem.Name = "dijkstraAlgorythmToolStripMenuItem";
            this.dijkstraAlgorythmToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.dijkstraAlgorythmToolStripMenuItem.Text = "Dijkstra algorithm";
            this.dijkstraAlgorythmToolStripMenuItem.Click += new System.EventHandler(this.DijkstraAlgorythmToolStripMenuItem);
            // 
            // aSearchToolStripMenuItem
            // 
            this.aSearchToolStripMenuItem.Name = "aSearchToolStripMenuItem";
            this.aSearchToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.aSearchToolStripMenuItem.Text = "A* algorithm";
            this.aSearchToolStripMenuItem.Click += new System.EventHandler(this.AStarAlgorithmToolStripMenuItem);
            // 
            // greedySearchToolStripMenuItem
            // 
            this.greedySearchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.greedyForDistanceAlgorithmToolStripMenuItem,
            this.greedyForValueAlgorithmToolStripMenuItem});
            this.greedySearchToolStripMenuItem.Name = "greedySearchToolStripMenuItem";
            this.greedySearchToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.greedySearchToolStripMenuItem.Text = "Greedy algorithm";
            // 
            // greedyForDistanceAlgorithmToolStripMenuItem
            // 
            this.greedyForDistanceAlgorithmToolStripMenuItem.Name = "greedyForDistanceAlgorithmToolStripMenuItem";
            this.greedyForDistanceAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(350, 34);
            this.greedyForDistanceAlgorithmToolStripMenuItem.Text = "Greedy for distance algorithm";
            this.greedyForDistanceAlgorithmToolStripMenuItem.Click += new System.EventHandler(this.GreedyForDistanceAlgorithmToolStripMenuItem_Click);
            // 
            // greedyForValueAlgorithmToolStripMenuItem
            // 
            this.greedyForValueAlgorithmToolStripMenuItem.Name = "greedyForValueAlgorithmToolStripMenuItem";
            this.greedyForValueAlgorithmToolStripMenuItem.Size = new System.Drawing.Size(350, 34);
            this.greedyForValueAlgorithmToolStripMenuItem.Text = "Greedy for value algorithm";
            this.greedyForValueAlgorithmToolStripMenuItem.Click += new System.EventHandler(this.GreedyForValueAlgorithmToolStripMenuItem_Click);
            // 
            // deepPathFindToolStripMenuItem
            // 
            this.deepPathFindToolStripMenuItem.Name = "deepPathFindToolStripMenuItem";
            this.deepPathFindToolStripMenuItem.Size = new System.Drawing.Size(255, 34);
            this.deepPathFindToolStripMenuItem.Text = "Deep path find";
            this.deepPathFindToolStripMenuItem.Click += new System.EventHandler(this.DeepPathFindToolStripMenuItem_Click);
            // 
            // percent
            // 
            this.percent.Location = new System.Drawing.Point(9, 69);
            this.percent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.percent.Maximum = 100;
            this.percent.Name = "percent";
            this.percent.Size = new System.Drawing.Size(156, 69);
            this.percent.TabIndex = 1;
            this.percent.Scroll += new System.EventHandler(this.Percent_Scroll);
            // 
            // widthNumber
            // 
            this.widthNumber.Location = new System.Drawing.Point(126, 171);
            this.widthNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.widthNumber.Name = "widthNumber";
            this.widthNumber.Size = new System.Drawing.Size(94, 26);
            this.widthNumber.TabIndex = 2;
            this.widthNumber.TextChanged += new System.EventHandler(this.WidthNumber_TextChanged);
            // 
            // heightNumber
            // 
            this.heightNumber.Location = new System.Drawing.Point(126, 209);
            this.heightNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.heightNumber.Name = "heightNumber";
            this.heightNumber.Size = new System.Drawing.Size(94, 26);
            this.heightNumber.TabIndex = 3;
            this.heightNumber.TextChanged += new System.EventHandler(this.HeightNumber_TextChanged);
            // 
            // create
            // 
            this.create.Location = new System.Drawing.Point(26, 263);
            this.create.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(196, 35);
            this.create.TabIndex = 6;
            this.create.Text = "Create graph";
            this.create.UseVisualStyleBackColor = true;
            this.create.Click += new System.EventHandler(this.Create);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 214);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
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
            this.FieldParams.Location = new System.Drawing.Point(18, 42);
            this.FieldParams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FieldParams.Name = "FieldParams";
            this.FieldParams.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FieldParams.Size = new System.Drawing.Size(238, 352);
            this.FieldParams.TabIndex = 10;
            this.FieldParams.TabStop = false;
            this.FieldParams.Text = "Field parametres";
            // 
            // width
            // 
            this.width.AutoSize = true;
            this.width.Location = new System.Drawing.Point(21, 175);
            this.width.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(95, 20);
            this.width.TabIndex = 14;
            this.width.Text = "Graph width";
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(26, 308);
            this.refresh.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(196, 35);
            this.refresh.TabIndex = 13;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // percentTextBox
            // 
            this.percentTextBox.Location = new System.Drawing.Point(174, 83);
            this.percentTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.percentTextBox.Name = "percentTextBox";
            this.percentTextBox.Size = new System.Drawing.Size(46, 26);
            this.percentTextBox.TabIndex = 12;
            this.percentTextBox.TextChanged += new System.EventHandler(this.PercentTextBox_TextChanged);
            // 
            // fieldSize
            // 
            this.fieldSize.AutoSize = true;
            this.fieldSize.Location = new System.Drawing.Point(66, 143);
            this.fieldSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fieldSize.Name = "fieldSize";
            this.fieldSize.Size = new System.Drawing.Size(86, 20);
            this.fieldSize.TabIndex = 11;
            this.fieldSize.Text = "Graph size";
            // 
            // percentOfObstacles
            // 
            this.percentOfObstacles.AutoSize = true;
            this.percentOfObstacles.Location = new System.Drawing.Point(39, 45);
            this.percentOfObstacles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.percentOfObstacles.Name = "percentOfObstacles";
            this.percentOfObstacles.Size = new System.Drawing.Size(154, 20);
            this.percentOfObstacles.TabIndex = 10;
            this.percentOfObstacles.Text = "Percent of obstacles";
            // 
            // timeOfSearch
            // 
            this.timeOfSearch.Controls.Add(this.time);
            this.timeOfSearch.Location = new System.Drawing.Point(27, 418);
            this.timeOfSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeOfSearch.Name = "timeOfSearch";
            this.timeOfSearch.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.timeOfSearch.Size = new System.Drawing.Size(230, 102);
            this.timeOfSearch.TabIndex = 11;
            this.timeOfSearch.TabStop = false;
            this.timeOfSearch.Text = "Statistics";
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(12, 25);
            this.time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(51, 20);
            this.time.TabIndex = 0;
            this.time.Text = "label1";
            // 
            // Field
            // 
            this.Field.Location = new System.Drawing.Point(266, 55);
            this.Field.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Field.Name = "Field";
            this.Field.Size = new System.Drawing.Size(916, 545);
            this.Field.TabIndex = 13;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1200, 754);
            this.Controls.Add(this.Field);
            this.Controls.Add(this.timeOfSearch);
            this.Controls.Add(this.FieldParams);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "PathFind";
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
        private System.Windows.Forms.ToolStripMenuItem dijkstraAlgorythmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greedySearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSearchToolStripMenuItem;
        private FieldControl Field;
        private System.Windows.Forms.ToolStripMenuItem greedyForDistanceAlgorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greedyForValueAlgorithmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deepPathFindToolStripMenuItem;
    }
}

