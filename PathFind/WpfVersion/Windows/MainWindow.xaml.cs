using GraphLibrary.Algorithm;
using GraphLibrary.GraphFactory;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;
using System.Windows;
using System.Windows.Controls;
using WpfVersion.Graph;
using WpfVersion.Vertex;

namespace WpfVersion
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WpfGraph graph = null;
        private IGraphFactory createAlgorythm = null;
        private IVertexRoleChanger changer = null;
        private IPathFindAlgorithm pathFindAlgo = null;

        private void AddGraphToControls()
        {
            for (int i = 0; i < 25; i++)
            {
                Field.ColumnDefinitions.Add(new ColumnDefinition());
                for (int j = 0; j < 25; j++)
                {
                    Field.RowDefinitions.Add(new RowDefinition());
                    //if (!graph[i, j].IsObstacle)
                    //    (graph[i, j] as WpfVertex).MouseLeftButtonDown += changer.SetStartPoint;
                    //(graph[i, j] as WpfVertex).MouseLeftButtonDown += changer.ReversePolarity;
                    //(graph[i, j] as WpfVertex).MouseLeftButtonDown += changer.ChangeTopText;
                    //Grid.SetColumn(graph[i, j] as WpfVertex, i);
                    //Grid.SetRow(graph[i, j] as WpfVertex, j);
                    Field.Width = 25 * 25;
                    Field.Height = 25 * 25;
                    Button b = new Button();
                    b.MinHeight = 25;
                    b.MaxHeight = 25;
                    b.MaxWidth = 25;
                    b.MinWidth = 25;
                    b.Width = 25;
                    b.Height = 25;
                    Grid.SetRow(b, j);
                    Grid.SetColumn(b, i);
                    Field.Children.Add(b);
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();                       
            AddGraphToControls();
        }
    }
}
