using SearchAlgorythms.Algorythms.GraphCreateAlgorythm;
using SearchAlgorythms.Algorythms.SearchAlgorythm;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
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
        private int width = 15;
        private int height = 15;
        private const int BUTTON_SIZE = 25;
        private const int BUTTON_POSITION = 27;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {

        }

        private bool IsRightDestination(GraphTop top)
        {
            return top != null && top.GetNeighbours().Count > 0;
        }

        private void AddButtonsToControls(Button[,] buttons)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    buttons[i, j].Size = new Size(BUTTON_SIZE, BUTTON_SIZE);
                    buttons[i, j].Location = new Point((i + 1) *
                        BUTTON_POSITION, (j + 1) * BUTTON_POSITION);
                    buttons[i, j].Click += ChooseStart;
                    Controls.Add(buttons[i, j]);
                }
            }
            Size = new Size(new Point((width + 2) * 
                BUTTON_POSITION, (height + 3) * BUTTON_POSITION));
            DesktopLocation = new Point();
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
            foreach(var b in buttons)
                Controls.Remove(b);
        }

        private void WideSearchToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if(start == null || end == null)
            {
                MessageBox.Show("Start or end buttons wasn't chosen");
                return;
            }
            searchAlgo = new WideSearch();
            searchAlgo.FindDestionation(start);
            if (searchAlgo.DestinationFound)
            {
                searchAlgo.DrawPath(end);
                MessageBox.Show("Destination is found");
            }
            else
                MessageBox.Show("Couldn't find path");
            searchAlgo = null;
            start = null;
            end = null;
        }

        private void RandomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestartToolStripMenuItem_Click(sender, e);
            createAlgo = new RandomCreate();
            buttons = createAlgo.GetGraph(width, height);
            AddButtonsToControls(buttons);
            createAlgo = null;
            start = null;
            end = null;
        }
    }
}
