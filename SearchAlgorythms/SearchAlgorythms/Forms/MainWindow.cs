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
        private Button[,] buttons;
        private GraphTop start = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchAlgorythms_Load(object sender, EventArgs e)
        {
            int x = 27, y = 15;
            int buttonSize = 25;
            int buttonPosition = 27;
            createAlgo = new RandomCreate();            
            buttons = createAlgo.GetGraph(x, y);            
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    buttons[i,j].Size = new Size(buttonSize, buttonSize);
                    buttons[i, j].Location = new Point(i * buttonPosition, j * buttonPosition);
                    buttons[i, j].Click += ChooseStart;
                    Controls.Add(buttons[i, j]);
                }
        }

        private void ChooseStart(object sender, EventArgs e)
        {
            GraphTop top = sender as GraphTop;
            top.IsStart = true;
            foreach(var but in buttons)
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
            top.IsEnd = true;
            top.BackColor = Color.FromName("Red");
            foreach (var but in buttons)
                but.Click -= ChooseEnd;     
        }

        private void FindPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (searchAlgo == null) 
                MessageBox.Show("Algorithm is not chosen");
            else
            {
                searchAlgo.FindDestionation(start);
                searchAlgo = null;
            }           
        }

        private void WideSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchAlgo = new WideSearch();
        }
    }
}
