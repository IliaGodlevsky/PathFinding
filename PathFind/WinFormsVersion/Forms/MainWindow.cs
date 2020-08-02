using System;
using System.Drawing;
using System.Windows.Forms;
using GraphLibrary.GraphFactory;
using GraphLibrary.Constants;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using GraphLibrary.View;
using GraphLibrary.RoleChanger;
using GraphLibrary.Algorithm;
using WinFormsVersion.Graph;
using WinFormsVersion.Vertex;
using WinFormsVersion.RoleChanger;
using WinFormsVersion.GraphSaver;
using GraphLibrary.GraphLoader;
using WinFormsVersion.GraphLoader;
using WinFormsVersion.GraphFactory;
using System.Linq;
using GraphLibrary.Extensions;

namespace WinFormsVersion.Forms
{
    public partial class MainWindow : Form, IView
    {
        private Algorithms algorithm;
        private WinFormsGraph graph = null;
        private IGraphFactory createAlgorythm = null;
        private IVertexRoleChanger changer = null;
        private IPathFindAlgorithm pathFindAlgo = null;

        public MainWindow()
        {
            InitializeComponent();
            var dataSource = Enum.GetValues(typeof(Algorithms)).Cast<Algorithms>().
                Select(algo => new { Name = algo.GetDescription(), Value = algo }).ToList();
            algoComboBox.DataSource = dataSource;
            var type = dataSource.First().GetType();
            var properties = type.GetProperties();

            algoComboBox.DisplayMember = properties.First().Name;
            algoComboBox.ValueMember = properties.Last().Name;
            algorithm = Algorithms.WidePathFind;
        }

        private void AlgoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            algorithm = (Algorithms)algoComboBox.SelectedValue;
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
            time.Text = string.Empty;
            time.Update();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {
            PrepareWindow(obstaclePercent: 40, graphWidth: 35, graphHeight: 23);
            Create(sender, e);
        }

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

        private void Percent_Scroll(object sender, EventArgs e) => PreparePercentBar();

        private bool SizeTextBoxTextChanged(TextBox textBox)
        {
            if (!textBox.Text.Any() || !int.TryParse(textBox.Text, out int size))
                return false;
            if (size <= 5)
                return false;
            textBox.Text = size.ToString();
            return true;
        }

        private void WidthNumber_TextChanged(object sender, EventArgs e) => SizeTextBoxTextChanged((TextBox)sender);

        private void HeightNumber_TextChanged(object sender, EventArgs e) => SizeTextBoxTextChanged((TextBox)sender);

        private void StartButton_StartPathFind(object sender, EventArgs e)
        {

            pathFindAlgo = AlgorithmSelector.GetPathFindAlgorithm(algorithm, graph);
            FindPath();
        }

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
            string format = "Time: {0}:{1}.{2}\nSteps: {3}\nPath length: {4}\nVisited vertices: {5}";
            if (pathFindAlgo != null)
            {
                pathFindAlgo.PauseEvent = () => Application.DoEvents();
                if (pathFindAlgo.FindDestionation())
                {
                    pathFindAlgo.DrawPath();
                    time.Text = pathFindAlgo.StatCollector.GetStatistics().
                        GetFormattedData(format);
                    time.Update();
                    graph.Start = null;
                    graph.End = null;
                }
                else
                    MessageBox.Show(WinFormsResources.BadResultMsg);
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
            time.Text = string.Empty;
            time.Update();
            percent.Value = graph.ObstaclePercent;
            PreparePercentBar();
        }

        
    }
}
