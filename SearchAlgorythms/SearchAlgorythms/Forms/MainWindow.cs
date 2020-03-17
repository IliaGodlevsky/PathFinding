using SearchAlgorythms.Algorythms;
using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form
    {
        private ISearchAlgorythm searchAlgo = null;
        private ICreateAlgorythm createAlgo = null;
        private Button[,] buttons = null;
        private GraphTop start = null;
        private GraphTop end = null;
        private const int BUTTON_SIZE = 25;
        private const int BUTTON_POSITION = 27;
        private int obstaclePercent = 25;
        private int width = 25;
        private int height = 25;
        private GraphInfoCollector collector = new GraphInfoCollector();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {
            widthNumber.Text = width.ToString();
            heightNumber.Text = height.ToString();
            percent.Value = obstaclePercent;
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
        }

        private bool IsRightDestination(GraphTop top)
        {
            return top != null && top.GetNeighbours().Count > 0 
                && !top.IsStart && !top.IsEnd;
        }

        private void AddButtonsToControls(Button[,] buttons)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buttons[i, j].Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                    buttons[i, j].Location = new Point((i + 1) *
                        BUTTON_POSITION + 150, (j + 1) * BUTTON_POSITION);
                    if (!GraphInfoCollector.IsObstacle(buttons[i, j]))
                        buttons[i, j].Click += ChooseStart;
                    buttons[i, j].MouseDown += ChangeColor;
                    Controls.Add(buttons[i, j]);
                }
            }
            Size = new Size(new Point((width + 2) *
                BUTTON_POSITION + 140, (height + 3) * BUTTON_POSITION + 100));
            DesktopLocation = new Point();
        }

        // class GraphTopShifter
        private void ChangeColor(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                GraphTopShifter shifter = new GraphTopShifter();
                Button top = sender as Button;
                Button prev = sender as Button;
                Controls.Remove((Control)sender);
                if (GraphInfoCollector.IsObstacle(top))
                {
                    top = new GraphTop();
                    top.Location = prev.Location;
                    if (start == null)
                        top.Click += ChooseStart;
                    else if (start != null && end == null)
                        top.Click += ChooseEnd;
                    shifter.InsertGraphTop(top, buttons, width, height);
                    shifter.SetBoundsBetweenNeighbours(top);
                }
                else if (!GraphInfoCollector.IsObstacle(top) &&
                    (top as GraphTop)?.IsEnd == false 
                    && (top as GraphTop)?.IsStart == false) 
                {
                    shifter.BreakBoundsBetweenNeighbours(top);
                    top = new Button();
                    top.Location = prev.Location;
                    top.BackColor = Color.FromName("Black");
                    shifter.InsertGraphTop(top, buttons, width, height);
                }
                top.Size = prev.Size;
                top.MouseDown += ChangeColor;
                Controls.Add(top);
            }
        }

        private void ChooseStart(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top)) 
                return;
            top.IsStart = true;
            foreach (var but in buttons)
            {
                but.Click -= ChooseStart;
                but.Click += ChooseEnd;
            }
            top.BackColor = Color.FromName("Green");
            start = top;
        }

        private void ChooseEnd(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.BackColor = Color.FromName("Red");
            foreach (var but in buttons)
                but.Click -= ChooseEnd;
            end = top;
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (buttons is null)
                return;
            foreach(var button in buttons)
                Controls.Remove(button);
        }

        private void WideSearchToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (CanStartSearch())
            {
                searchAlgo = new WideSearch();
                SearchPath(end);
            }
        }

        private bool CanStartSearch()
        {
            if (start == null || end == null)
            {
                MessageBox.Show("Start or end buttons wasn't chosen");
                return false;
            }
            return true;
        }

        private void SearchPath(GraphTop startTopOfDrawingPath)
        {
            searchAlgo.FindDestionation(start);
            if (searchAlgo.DestinationFound)
            {
                searchAlgo.DrawPath(startTopOfDrawingPath);
                MessageBox.Show("Destination is found");
            }
            else
                MessageBox.Show("Couldn't find path");
            searchAlgo = null;
            start = null;
            end = null;
        }

        private void BestfirstWideSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanStartSearch())
            {
                searchAlgo = new BestFirstSearch(end);
                SearchPath(start);
            }            
        }

        private void Percent_Scroll(object sender, EventArgs e)
        {
            obstaclePercent = percent.Value;
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
        }

        private void SizeTextBoxTextChanged(TextBox textBox, ref int size)
        {
            if (textBox.Text.Length == 0)
                size = 0;
            else if (!int.TryParse(textBox.Text, out size))
                MessageBox.Show("Wrong size parametres");
            textBox.Text = size.ToString();
        }

        // make method to prevent doubling
        private void WidthNumber_TextChanged(object sender, EventArgs e)
        {
            SizeTextBoxTextChanged((TextBox)sender,ref width);      
        }

        private void HeightNumber_TextChanged(object sender, EventArgs e)
        {
            SizeTextBoxTextChanged((TextBox)sender, ref height);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            RestartToolStripMenuItem_Click(sender, e);
            createAlgo = new RandomCreate(obstaclePercent);
            buttons = createAlgo.GetGraph(width, height);
            AddButtonsToControls(buttons);
            createAlgo = null;
            start = null;
            end = null;
        }

        // class GraphInfoCollector
        private void InitializeGraphWithInfo(GraphTopInfo[,] info)
        {
            if (info == null)
                return;
            RestartToolStripMenuItem_Click(this, new EventArgs());
            width = info.GetLength(0);
            height = info.Length / info.GetLength(0);
            buttons = new Button[width, height];
            collector.InitializeWithInfo(buttons, info);
            NeighbourSetter setter = new NeighbourSetter(width, height, buttons);
            setter.SetNeighbours();
            AddButtonsToControls(buttons);
            widthNumber.Text = width.ToString();
            heightNumber.Text = height.ToString();
            widthNumber.Update();
            heightNumber.Update();
        }

        // class GraphSerializer
        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            GraphTopInfo[,] info = collector.CollectInfo(buttons);
            BinaryFormatter f = new BinaryFormatter();
            if (save.ShowDialog() == DialogResult.OK)
                using (var stream = new FileStream(save.FileName, FileMode.Create))
                    f.Serialize(stream, info);
        }

        private void LoadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphTopInfo[,] info = null;
            OpenFileDialog open = new OpenFileDialog();
            BinaryFormatter f = new BinaryFormatter();
            if (open.ShowDialog() == DialogResult.OK)
                using (var stream = new FileStream(open.FileName, FileMode.Open))
                    info = (GraphTopInfo[,])f.Deserialize(stream);
            InitializeGraphWithInfo(info);
        }

        private void PercentTextBox_TextChanged(object sender, EventArgs e)
        {
            const int MAX_PERCENT_VALUE = 100;
            int currentValue;
            if (!int.TryParse(percentTextBox.Text, out currentValue)
                || percent.Value < 0)
                percent.Value = 0;
            else if (currentValue > MAX_PERCENT_VALUE)
            {
                percent.Value = MAX_PERCENT_VALUE;
                obstaclePercent = currentValue;
            }
            else
            {
                percent.Value = currentValue;
                obstaclePercent = currentValue;
            }


        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            if (buttons == null)
                return;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GraphTop top = buttons[i, j] as GraphTop;
                    if (top != null)
                    {
                        top.SetToDefault();
                        buttons[i, j].Click -= ChooseStart;
                        buttons[i, j].Click -= ChooseEnd;
                        buttons[i, j].Click += ChooseStart;
                    }
                    buttons[i, j].MouseDown -= ChangeColor;
                    buttons[i, j].MouseDown += ChangeColor;
                }
            }
            start = null;
            end = null;
        }
    }
}
