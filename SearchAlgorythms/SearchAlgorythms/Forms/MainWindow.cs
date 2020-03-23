using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
using SearchAlgorythms.Extensions;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphLoader;
using SearchAlgorythms.GraphSaver;
using SearchAlgorythms.Top;
using System;
using SearchAlgorythms.DelegatedMethods;
using System.Drawing;
using System.Windows.Forms;
using SearchAlgorythms.RoleChanger;

namespace SearchAlgorythms
{
    public partial class MainWindow : Form
    {
        private IGraph graph = null;
        private ISearchAlgorythm searchAlgorythm = null;
        private ICreateAlgorythm createAlgorythm = null;
        private const int BUTTON_SIZE = 27;
        private const int BUTTON_POSITION = 29;
        private ButtonGraphTopRoleChanger changer = null;

        public MainWindow()
        {                        
            InitializeComponent();
        }

        private void PrepareWindow(int obstaclePercent,int graphWidth, int graphHeight)
        {
            Field.BorderStyle = BorderStyle.FixedSingle;
            percent.Value = obstaclePercent;
            percentTextBox.Text = percent.Value.ToString();
            percentTextBox.Update();
            widthNumber.Text = graphWidth.ToString();
            heightNumber.Text = graphHeight.ToString();
            widthNumber.Update();
            heightNumber.Update();
            time.Text = "";
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
            changer.SwitchRole += ReversePolarity;
            foreach (var top in graph)
            {
                if (!(top as GraphTop).IsObstacle)
                    (top as GraphTop).Click += changer.SetStartPoint;
                (top as GraphTop).MouseDown += ReversePolarity;
                Field.Controls.Add(top as GraphTop);
            }
            Field.Size = new Size(new Point(graph.GetWidth() *
                BUTTON_POSITION, (graph.GetHeight()) * BUTTON_POSITION));
            Field.Location = new Point(177, 35);
            Size = new Size(Field.Size.Width + FieldParams.Width + 50,
                Field.Size.Height + 80);
            DesktopLocation = new Point();
        }

        private void ReversePolarity(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                IGraphTop top = sender as GraphTop;
                Field.Controls.Remove(top as GraphTop);
                changer.Reverse(ref top);
                Field.Controls.Add(top as GraphTop);
                percent.Value = graph.GetObstaclePercent();
                percentTextBox.Text = percent.Value.ToString();
                percentTextBox.Update();
            }
        }

        private void RemoveGraphFromControl()
        {
            if (graph is null)
                return;
            Field.Controls.Clear();
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
            searchAlgorythm = new GreedyAlgorithm(graph.End);
            SearchPath(graph.End);
        }

        private void BestfirstWideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new BestFirstSearch(graph.End);
            SearchPath(graph.Start);
        }

        private void SearchPath(IGraphTop startTopOfDrawingPath)
        {
            searchAlgorythm.Pause = DelegatedMethod.Pause;
            if (searchAlgorythm.FindDestionation(graph.Start))
            {
                searchAlgorythm.DrawPath(startTopOfDrawingPath);
                time.Text = searchAlgorythm.GetStatistics();
                time.Update();                
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
                BUTTON_POSITION);
            graph = new ButtonGraph(createAlgorythm.GetGraph());
            changer = new ButtonGraphTopRoleChanger(graph);
            (graph as ButtonGraph).SetStart += changer.SetStartPoint;
            (graph as ButtonGraph).SetEnd += changer.SetDestinationPoint;
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
            ButtonGraphLoader loader = new ButtonGraphLoader(BUTTON_POSITION);
            IGraph temp = loader.GetGraph();                      
            if (temp != null)
            {
                RemoveGraphFromControl();
                graph = temp;
                changer = new ButtonGraphTopRoleChanger(graph);
                (graph as ButtonGraph).SetStart += changer.SetStartPoint;
                (graph as ButtonGraph).SetEnd += changer.SetDestinationPoint;
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
            time.Text = "";
            time.Update();
        }

        private void ASearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchAlgorythm = new AStarAlgorithm(graph.End, graph);
            (searchAlgorythm as AStarAlgorithm).Heuristic = DelegatedMethod.GetChebyshevDistance;
            SearchPath(graph.End);
        }
    }
}
