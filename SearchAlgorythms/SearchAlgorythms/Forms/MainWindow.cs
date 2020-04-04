using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
using SearchAlgorythms.GraphSaver;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;
using SearchAlgorythms.RoleChanger;
using SearchAlgorythms.Algorithm;
using SearchAlgorythms.GraphFactory;
using System.Linq;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form
    {
        private AbstractGraph graph = null;
        private IPathFindAlgorithm pathFindAlgorithm = null;
        private IGraphFactory createAlgorythm = null;
        private const int BUTTON_POSITION = 25;
        private ButtonGraphTopRoleChanger changer = null;

        public MainWindow()
        {                        
            InitializeComponent();
        }

        private void PrepareWindow(int obstaclePercent,int graphWidth, int graphHeight)
        {
            Field.BorderStyle = BorderStyle.FixedSingle;
            percent.Value = obstaclePercent;
            PreparePercentBar();
            widthNumber.Text = graphWidth.ToString();
            heightNumber.Text = graphHeight.ToString();
            widthNumber.Update();
            heightNumber.Update();
            time.Text = "";
            time.Update();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {
            PrepareWindow(obstaclePercent: 40, graphWidth: 35, graphHeight: 23);
            Create(sender, e);
        }

        private bool IsRightDestination(GraphTop top)
        {
            return !top.Neighbours.Any() && top.IsSimpleTop;
        }

        private void AddGraphToControls()
        { 
            foreach (var top in graph)
            {
                if (!(top as GraphTop).IsObstacle)
                    (top as GraphTop).MouseClick += changer.SetStartPoint;
                (top as GraphTop).MouseClick += changer.ReversePolarity;
                (top as GraphTop).MouseClick += changer.ChangeTopText;
                Field.Controls.Add(top as GraphTop);
            }
            Field.Size = new Size(new Point(graph.Width*
                BUTTON_POSITION, graph.Height* BUTTON_POSITION));
            Field.Location = new Point(177, 35);
            Size = new Size(Field.Size.Width + FieldParams.Width + 50,
                Field.Size.Height + 80);
            DesktopLocation = new Point();
        }

        private void RemoveGraphFromControl()
        {
            if (graph is null)
                return;
            Field.Controls.Clear();
        }

        private void WideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            pathFindAlgorithm = new WidePathFindAlgorithm(graph);
            StartSearchPath();
        }

        private void DijkstraAlgorythmToolStripMenuItem(object sender, EventArgs e)
        {
            pathFindAlgorithm = new DijkstraAlgorithm(graph);
            StartSearchPath();
        }

        private void GreedySearchToolStripMenuItem(object sender, EventArgs e)
        {
            pathFindAlgorithm = new GreedyAlgorithm(graph);
            StartSearchPath();
        }

        private void BestfirstWideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            pathFindAlgorithm = new BestFirstAlgorithm(graph);
            StartSearchPath();
        }

        private void StartSearchPath()
        {
            pathFindAlgorithm.Pause = PauseMaker.PauseMaker.WinFormsPause;
            if (pathFindAlgorithm.FindDestionation())
            {
                pathFindAlgorithm.DrawPath();
                time.Text = pathFindAlgorithm.GetStatistics();
                time.Update();
                pathFindAlgorithm = null;
                graph.Start = null;
                graph.End = null;
            }
            else
                MessageBox.Show("Couldn't find path");            
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
            createAlgorythm = new RandomValuedButtonGraphFactory(percent.Value,
                int.Parse(widthNumber.Text), int.Parse(heightNumber.Text),
                BUTTON_POSITION);
            PrepareGraph(createAlgorythm.GetGraph());           
        }

        private void PrepareGraph(IGraphTop[,] tops)
        {
            RemoveGraphFromControl();
            graph = new ButtonGraph(tops);
            changer = new ButtonGraphTopRoleChanger(graph);
            (graph as ButtonGraph).SetStart += changer.SetStartPoint;
            (graph as ButtonGraph).SetEnd += changer.SetDestinationPoint;
            AddGraphToControls();
            createAlgorythm = null;
        }

        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saver = new ButtonGraphSaver();
            saver.SaveGraph(graph);
        }

        private void LoadMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var loader = new ButtonGraphLoader(BUTTON_POSITION);
            AbstractGraph temp = loader.GetGraph();                      
            if (temp != null)
            {
                PrepareGraph(temp.GetArray());                
                PrepareWindow(graph.GetObstaclePercent(), graph.Width, graph.Height);
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
            time.Text = "";
            time.Update();
            percent.Value = graph.GetObstaclePercent();
            PreparePercentBar();
        }

        private void ASearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pathFindAlgorithm = new AStarAlgorithm(graph);
            (pathFindAlgorithm as AStarAlgorithm).HeuristicFunction = 
                DistanceCalculator.DistanceCalculator.GetChebyshevDistance;
            StartSearchPath();
        }

        private void PreparePercentBar()
        {
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
        }
    }
}
