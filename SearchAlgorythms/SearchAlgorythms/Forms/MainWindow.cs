using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
using SearchAlgorythms.GraphSaver;
using SearchAlgorythms.Top;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form
    {
        private IGraph graph = null;
        private ISearchAlgorythm searchAlgorythm = null;
        private ICreateAlgorythm createAlgorythm = null;
        private const int BUTTON_SIZE = 30;
        private const int BUTTON_POSITION = 32;

        private void Pause(int milliseconds)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < milliseconds)
                Application.DoEvents();
        }

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
            PrepareWindow(obstaclePercent: 40, graphWidth: 25, graphHeight: 18);
            Create(sender, e);
        }

        private bool IsRightDestination(GraphTop top)
        {
            return !top.Neighbours.IsEmpty() && top.IsSimpleTop;
        }

        private void AddGraphToControls()
        {
            foreach (var top in graph)
            {
                if (!(top as GraphTop).IsObstacle)
                    (top as GraphTop).Click += SetStartPoint;
                (top as GraphTop).MouseDown += ReversePolarity;
                Controls.Add(top as GraphTop);
            }
            Size = new Size(new Point((graph.GetWidth() + 2) *
                BUTTON_POSITION + 140, (graph.GetHeight() + 3) * BUTTON_POSITION));
            DesktopLocation = new Point();
        }

        private void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                IGraphTop top = sender as GraphTop;
                Controls.Remove(top as GraphTop);
                graph.Reverse(ref top);
                Controls.Add(top as GraphTop);
                percent.Value = graph.GetObstaclePercent();
                percentTextBox.Text = percent.Value.ToString();
                percentTextBox.Update();
            }
        }

        private void SetStartPoint(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top)) 
                return;
            top.IsStart = true;
            foreach (var butt in graph)
            {
                (butt as GraphTop).Click -= SetStartPoint;
                (butt as GraphTop).Click += SetDestinationPoint;
            }
            top.MarkAsStart();
            graph.Start = top;
        }

        private void SetDestinationPoint(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top))
                return;
            top.IsEnd = true;
            top.MarkAsEnd();
            foreach(var butt in graph)
                (butt as GraphTop).Click -= SetDestinationPoint;
            graph.End = top;
        }

        private void RemoveGraphFromControl()
        {
            if (graph is null)
                return;
            foreach(var top in graph)
                Controls.Remove(top as GraphTop);
        }

        private void WideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new WideSearch(graph.End);
            SearchPath(graph.End);
        }

        private void DijkstraAlgorythmToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new DijkstraAlgorythm(graph.End, graph);
            SearchPath(graph.End);
        }

        private void GreedySearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new GreedySearch(graph.End);
            SearchPath(graph.End);
        }

        private void BestfirstWideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new BestFirstSearch(graph.End);
            SearchPath(graph.Start);
        }

        private void SearchPath(IGraphTop startTopOfDrawingPath)
        {
            searchAlgorythm.Pause = Pause;
            if (searchAlgorythm.FindDestionation(graph.Start))
            {
                time.Text = searchAlgorythm.Time.ToString() + " seconds";
                time.Update();
                searchAlgorythm.DrawPath(startTopOfDrawingPath);
                stat.Text = searchAlgorythm.GetStatistics();
                stat.Update();
            }
            else
                MessageBox.Show("Couldn't find path");
            searchAlgorythm = null;
            graph.Start = null;
            graph.End = null;
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

        private void Create(object sender, EventArgs e)
        {
            if (!SizeTextBoxTextChanged(widthNumber) 
                || !SizeTextBoxTextChanged(heightNumber))
                return;
            RemoveGraphFromControl();
            createAlgorythm = new RandomValuedButtonGraphCreate(percent.Value,
                int.Parse(widthNumber.Text), int.Parse(heightNumber.Text),
                BUTTON_SIZE, BUTTON_SIZE, BUTTON_POSITION);
            graph = new ButtonGraph(createAlgorythm.GetGraph());
            graph.SetStart += SetStartPoint;
            graph.SetEnd += SetDestinationPoint;
            graph.SwitchRole += ReversePolarity;
            AddGraphToControls();
            createAlgorythm = null;
        }

        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonGraphSaver saver = new ButtonGraphSaver();
            saver.SaveGraph(graph);
        }

        private void LoadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonGraphLoader loader = new ButtonGraphLoader(BUTTON_SIZE, 
                BUTTON_SIZE, BUTTON_POSITION);
            IGraph temp = loader.GetGraph();                      
            if (temp != null)
            {
                RemoveGraphFromControl();
                graph = temp;
                graph.SetStart += SetStartPoint;
                graph.SetEnd += SetDestinationPoint;
                graph.SwitchRole += ReversePolarity;
                AddGraphToControls();
                PrepareWindow(graph.GetObstaclePercent(), 
                    graph.GetWidth(), graph.GetHeight());
                createAlgorythm = null;
            }
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
            stat.Text = "";
            stat.Update();
        }

        private void ASearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchAlgorythm = new AStarSearch(graph.End, graph);
            var algo = (searchAlgorythm as AStarSearch);
            algo.Heuristic = GetChebyshevDistance;
            SearchPath(graph.End);
        }

        private double GetEuclideanDistance(IGraphTop top1, IGraphTop top2)
        {
            double a = Math.Pow(top1.Location.X - top2.Location.X, 2);
            double b = Math.Pow(top1.Location.Y - top2.Location.Y, 2);
            return Math.Sqrt(a + b);
        }

        private double GetChebyshevDistance(IGraphTop top1, IGraphTop top2)
        {
            int a = Math.Abs(top1.Location.X - top2.Location.X);
            int b = Math.Abs(top1.Location.Y - top2.Location.Y);
            return Math.Max(a, b);
        }
    }
}
