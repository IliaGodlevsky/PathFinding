using SearchAlgorythms.Algorythms;
using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form
    {
        private IGraph graph = null;
        private ISearchAlgorythm searchAlgorythm = null;
        private ICreateAlgorythm createAlgorythm = null;
        private const int BUTTON_SIZE = 25;
        private const int BUTTON_POSITION = 27;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PrepareWindow(int obstaclePercent,int graphWidth, int graphHeight)
        {
            percent.Value = obstaclePercent;
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
            widthNumber.Text = graphWidth.ToString();
            heightNumber.Text = graphHeight.ToString();
            widthNumber.Update();
            heightNumber.Update();
            time.Text = 0.ToString() + " seconds";
            time.Update();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {
            PrepareWindow(obstaclePercent: 20, graphWidth: 15, graphHeight: 15);
        }

        private bool IsRightDestination(GraphTop top)
        {
            return top != null && top.GetNeighbours().Count > 0 
                && !top.IsStart && !top.IsEnd;
        }

        private void AddButtonsToControls()
        {
            for (int i = 0; i < graph.GetWidth(); i++)
            {
                for (int j = 0; j < graph.GetHeight(); j++)
                {
                    if ((graph[i, j] as GraphTop) != null) 
                      graph[i, j].Click += ChooseStart;
                    graph[i, j].MouseDown += ChangeColor;
                    Controls.Add(graph[i, j]);
                }
            }
            Size = new Size(new Point((graph.GetWidth() + 2) *
                BUTTON_POSITION + 140, (graph.GetHeight() + 3) * BUTTON_POSITION + 100));
            DesktopLocation = new Point();
        }

        private void ChangeColor(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button top = sender as Button;
                Controls.Remove(top);
                graph.Reverse(ref top);
                Controls.Add(top);
                percent.Value = graph.ObstaclePercent();
                percentTextBox.Text = percent.Value.ToString();
                percentTextBox.Update();
            }
        }

        private void ChooseStart(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top)) 
                return;
            top.IsStart = true;
            for (int i = 0; i < graph.GetWidth(); i++)
                for (int j = 0; j < graph.GetHeight(); j++)
            {
                    graph[i, j].Click -= ChooseStart;
                    graph[i, j].Click += ChooseEnd;
            }
            top.MarkAsStart();
            graph.Start = top;
        }

        private void ChooseEnd(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            for (int i = 0; i < graph.GetWidth(); i++)
                for (int j = 0; j < graph.GetHeight(); j++)
                    graph[i, j].Click -= ChooseEnd;
            graph.End = top;
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graph is null)
                return;
            for (int i = 0; i < graph.GetWidth(); i++)
                for (int j = 0; j < graph.GetHeight(); j++)
                    Controls.Remove(graph[i, j]);
        }

        private void WideSearchToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (CanStartSearch())
            {
                searchAlgorythm = new WideSearch();
                SearchPath(graph.End);
            }
        }

        private bool CanStartSearch()
        {
            if (graph.Start == null || graph.End == null)
            {
                MessageBox.Show("Start or end buttons wasn't chosen");
                return false;
            }
            return true;
        }

        private void SearchPath(GraphTop startTopOfDrawingPath)
        {
            searchAlgorythm.FindDestionation(graph.Start);
            time.Text = searchAlgorythm.GetTime().ToString() + " seconds";
            time.Update();
            if (searchAlgorythm.DestinationFound)
            {
                searchAlgorythm.DrawPath(startTopOfDrawingPath);
                MessageBox.Show("Destination is found");
            }
            else
                MessageBox.Show("Couldn't find path");
            searchAlgorythm = null;
            graph.Start = null;
            graph.End = null;
        }

        private void BestfirstWideSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CanStartSearch())
            {
                searchAlgorythm = new BestFirstSearch(graph.End);
                SearchPath(graph.Start);
            }            
        }

        private void Percent_Scroll(object sender, EventArgs e)
        {
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
        }

        private bool SizeTextBoxTextChanged(TextBox textBox)
        {
            if (textBox.Text.Length == 0 || !int.TryParse(textBox.Text, out int size))
                return false;
            textBox.Text = size.ToString();
            return true;
        }

        private void WidthNumber_TextChanged(object sender, EventArgs e)
        {
            SizeTextBoxTextChanged((TextBox)sender);   
        }

        private void HeightNumber_TextChanged(object sender, EventArgs e)
        {
            SizeTextBoxTextChanged((TextBox)sender);
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (!SizeTextBoxTextChanged(widthNumber) || !SizeTextBoxTextChanged(heightNumber))
                return;
            RestartToolStripMenuItem_Click(sender, e);
            createAlgorythm = new RandomCreate(percent.Value, int.Parse(widthNumber.Text),
                int.Parse(heightNumber.Text), BUTTON_SIZE, BUTTON_SIZE, BUTTON_POSITION);
            graph = new UnweightedGraph(createAlgorythm.GetGraph());
            graph.SetStart += ChooseStart;
            graph.SetEnd += ChooseEnd;
            graph.SwitchRole += ChangeColor;
            AddButtonsToControls();
            createAlgorythm = null;
        }

        private void InitializeGraphWithInfo(GraphTopInfo[,] info)
        {
            if (info == null)
                return;
            RestartToolStripMenuItem_Click(this, new EventArgs());
            createAlgorythm = new OnInfoGraphCreater(info,
                BUTTON_SIZE, BUTTON_SIZE, BUTTON_POSITION);
            graph = new UnweightedGraph(createAlgorythm.GetGraph());
            NeigbourSetter setter = new NeigbourSetter(graph.GetArray());
            setter.SetNeighbours();
            AddButtonsToControls();
            PrepareWindow(graph.ObstaclePercent(), graph.GetWidth(), graph.GetHeight());
            createAlgorythm = null;
        }

        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (graph != null)
            {
                SaveFileDialog save = new SaveFileDialog();
                GraphTopInfo[,] info = graph.GetInfo();
                BinaryFormatter f = new BinaryFormatter();
                if (save.ShowDialog() == DialogResult.OK)
                    using (var stream = new FileStream(save.FileName, FileMode.Create))
                    {
                        try
                        {
                            f.Serialize(stream, info);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
            }
        }

        private void LoadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphTopInfo[,] info = null;
            OpenFileDialog open = new OpenFileDialog();
            BinaryFormatter f = new BinaryFormatter();
            if (open.ShowDialog() == DialogResult.OK)
                using (var stream = new FileStream(open.FileName, FileMode.Open))
                {
                    try
                    {
                        info = (GraphTopInfo[,])f.Deserialize(stream);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            InitializeGraphWithInfo(info);
            graph.SetStart += ChooseStart;
            graph.SetEnd += ChooseEnd;
            graph.SwitchRole += ChangeColor;
        }

        private void PercentTextBox_TextChanged(object sender, EventArgs e)
        {
            const int MAX_PERCENT_VALUE = 100;
            if (!int.TryParse(percentTextBox.Text, out int currentValue)
                || percent.Value < 0)
                percent.Value = 0;
            else if (currentValue > MAX_PERCENT_VALUE)
                percent.Value = MAX_PERCENT_VALUE;
            else
                percent.Value = currentValue;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            graph?.Refresh();
            time.Text = 0.ToString() + " seconds";
            time.Update();
        }
    }
}
