using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
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
        private const int BUTTON_SIZE = 25;
        private const int BUTTON_POSITION = 27;

        private void Pause(int value)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < value)
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
            PrepareWindow(obstaclePercent: 20, graphWidth: 15, graphHeight: 15);
        }

        private bool IsRightDestination(GraphTop top)
        {
            return top != null && top.Neighbours.Count > 0 
                && !top.IsStart && !top.IsEnd;
        }

        private void AddGraphToControls()
        {
            foreach (var top in graph)
            {
                if (!(top as GraphTop).IsObstacle)
                    (top as GraphTop).Click += ChooseStart;
                (top as GraphTop).MouseDown += ChangeColor;
                Controls.Add(top as GraphTop);
            }
            Size = new Size(new Point((graph.GetWidth() + 2) *
                BUTTON_POSITION + 140, (graph.GetHeight() + 3) * BUTTON_POSITION + 100));
            DesktopLocation = new Point();
        }

        private void ChangeColor(object sender, EventArgs e)
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

        private void ChooseStart(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            if (!IsRightDestination(top)) 
                return;
            top.IsStart = true;
            foreach (var butt in graph)
            {
                (butt as GraphTop).Click -= ChooseStart;
                (butt as GraphTop).Click += ChooseEnd;
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
            foreach(var butt in graph)
                (butt as GraphTop).Click -= ChooseEnd;
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
            if (searchAlgorythm.CanStartSearch())
                SearchPath(graph.End);
        }

        private void DijkstraAlgorythmToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new DijkstraAlgorythm(graph.End, graph);
            if (searchAlgorythm.CanStartSearch())
                SearchPath(graph.End);
        }

        private void GreedySearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new GreedySearch(graph.End);
            if (searchAlgorythm.CanStartSearch())
                SearchPath(graph.End);
        }

        private void BestfirstWideSearchToolStripMenuItem(object sender, EventArgs e)
        {
            searchAlgorythm = new BestFirstSearch(graph.End);
            if (searchAlgorythm.CanStartSearch())
                SearchPath(graph.Start);
        }

        private void SearchPath(IGraphTop startTopOfDrawingPath)
        {
            searchAlgorythm.Pause = Pause;
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
            if (!SizeTextBoxTextChanged(widthNumber) || !SizeTextBoxTextChanged(heightNumber))
                return;
            RemoveGraphFromControl();
            createAlgorythm = new RandomValuedButtonGraphCreate(percent.Value, 
                int.Parse(widthNumber.Text),
                int.Parse(heightNumber.Text), BUTTON_SIZE, BUTTON_SIZE, BUTTON_POSITION);
            graph = new ButtonGraph(createAlgorythm.GetGraph());
            graph.SetStart += ChooseStart;
            graph.SetEnd += ChooseEnd;
            graph.SwitchRole += ChangeColor;
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
                graph.SetStart += ChooseStart;
                graph.SetEnd += ChooseEnd;
                graph.SwitchRole += ChangeColor;
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
        }
    }
}
