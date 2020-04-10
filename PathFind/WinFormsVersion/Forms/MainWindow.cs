using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
using SearchAlgorythms.GraphSaver;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;
using SearchAlgorythms.RoleChanger;
using SearchAlgorythms.GraphFactory;
using System.Linq;
using GraphLibrary.GraphFactory;
using GraphLibrary.Constants;
using SearchAlgorythms.PauseMaker;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using GraphLibrary.View;
using SearchAlgorythms.Algorithm;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form, IView
    {
        private WinFormsGraph graph = null;
        private IGraphFactory createAlgorythm = null;
        private IVertexRoleChanger changer = null;
        private IPathFindAlgorithm pathFindAlgo = null;

        public MainWindow() => InitializeComponent();

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

        private bool IsRightDestination(WinFormsVertex vertex) => !vertex.Neighbours.Any() && vertex.IsSimpleVertex;

        private void AddGraphToControls()
        { 
            foreach (var top in graph)
            {
                if (!(top as WinFormsVertex).IsObstacle)
                    (top as WinFormsVertex).MouseClick += changer.SetStartPoint;
                (top as WinFormsVertex).MouseClick += changer.ReversePolarity;
                (top as WinFormsVertex).MouseClick += changer.ChangeTopText;
                Field.Controls.Add(top as WinFormsVertex);
            }
            Field.Size = new Size(new Point(graph.Width * Const.SIZE_BETWEEN_VERTICES,
                graph.Height * Const.SIZE_BETWEEN_VERTICES));
            Field.Location = new Point(177, 35);
            Size = new Size(Field.Size.Width + FieldParams.Width + 50,
                Field.Size.Height + 80);
            DesktopLocation = new Point();
        }

        private void WideSearchToolStripMenuItem(object sender, EventArgs e) => StartSearchPath(Algorithms.WidePathFind);

        private void DijkstraAlgorythmToolStripMenuItem(object sender, EventArgs e) => StartSearchPath(Algorithms.DijkstraAlgorithm);

        private void GreedyForDistanceAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
            => StartSearchPath(Algorithms.DistanceGreedyAlgorithm);

        private void GreedyForValueAlgorithmToolStripMenuItem_Click(object sender, EventArgs e)
            => StartSearchPath(Algorithms.ValueGreedyAlgorithm);

        private void AStarAlgorithmToolStripMenuItem(object sender, EventArgs e) => StartSearchPath(Algorithms.AStarAlgorithm);

        private void StartSearchPath(Algorithms algorithm)
        {
            pathFindAlgo = AlgorithmSelector.GetPathFindAlgorithm(algorithm, graph);
            FindPath();
        }

        private void Percent_Scroll(object sender, EventArgs e) => PreparePercentBar();

        private bool SizeTextBoxTextChanged(TextBox textBox)
        {
            if (textBox.Text.Length == 0 || !int.TryParse(textBox.Text, out int size))
                return false;
            if (size <= 5)
                return false;
            textBox.Text = size.ToString();
            return true;
        }

        private void WidthNumber_TextChanged(object sender, EventArgs e) => SizeTextBoxTextChanged((TextBox)sender);

        private void HeightNumber_TextChanged(object sender, EventArgs e) => SizeTextBoxTextChanged((TextBox)sender);

        private void Create(object sender, EventArgs e) => CreateGraph();

        private void PrepareGraph(WinFormsGraph graph)
        {
            Field.Controls.Clear();
            this.graph = graph;
            changer = new WinFormsVertexRoleChanger(graph);
            this.graph.SetStart += changer.SetStartPoint;
            this.graph.SetEnd += changer.SetDestinationPoint;
            AddGraphToControls();
            createAlgorythm = null;
        }

        private void SaveMapToolStripMenuItem_Click(object sender, EventArgs e) => SaveGraph();

        private void LoadMapToolStripMenuItem_Click(object sender, EventArgs e) => LoadGraph();

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

        private void Refresh_Click(object sender, EventArgs e) => RefreshGraph();

        private void PreparePercentBar()
        {
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
        }

        public void SaveGraph() => new WinFormsGraphSaver().SaveGraph(graph);

        public void LoadGraph()
        {
            IGraphLoader loader = new WinFormsGraphLoader(Const.SIZE_BETWEEN_VERTICES);
            WinFormsGraph temp = (WinFormsGraph)loader.GetGraph();
            if (temp != null)
            {
                PrepareGraph(temp);
                PrepareWindow(graph.ObstaclePercent, graph.Width, graph.Height);
            }
        }

        public void FindPath()
        {
            if (pathFindAlgo != null)
            {
                pathFindAlgo.Pause = new WinFormsPauseMaker().Pause;
                if (pathFindAlgo.FindDestionation())
                {
                    pathFindAlgo.DrawPath();
                    time.Text = pathFindAlgo.GetStatistics();
                    time.Update();
                    graph.Start = null;
                    graph.End = null;
                }
                else
                    MessageBox.Show("Couldn't find path");
            }
        }

        public void CreateGraph()
        {
            if (!SizeTextBoxTextChanged(widthNumber) || !SizeTextBoxTextChanged(heightNumber)) 
                return;
            createAlgorythm = new RandomValuedWinFormsGraphFactory(percent.Value,
                int.Parse(widthNumber.Text), int.Parse(heightNumber.Text),
                Const.SIZE_BETWEEN_VERTICES);
            PrepareGraph((WinFormsGraph)createAlgorythm.GetGraph());
        }

        public void RefreshGraph()
        {
            graph?.Refresh();
            time.Text = "";
            time.Update();
            percent.Value = graph.ObstaclePercent;
            PreparePercentBar();
        }
    }
}
