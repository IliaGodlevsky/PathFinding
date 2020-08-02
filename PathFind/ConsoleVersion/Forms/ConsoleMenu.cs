using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.PathFindAlgorithmMenu;
using GraphLibrary.GraphFactory;
using GraphLibrary.View;
using ConsoleVersion.Graph;
using ConsoleVersion.GraphFactory;
using ConsoleVersion.RoleChanger;
using System;
using System.Drawing;
using GraphLibrary.RoleChanger;
using GraphLibrary.Graph;
using System.ComponentModel;
using System.Text;
using GraphLibrary.Extensions;

namespace ConsoleVersion.Forms
{
    public class ConsoleMenu : IView
    {
        private enum MenuOption
        {
            [Description("Quit")]
            Quit,
            [Description("Find path")]
            PathFind,
            [Description("Save")]
            Save,
            [Description("Load")]
            Load,
            [Description("Create")]
            Create,
            [Description("Refresh")]
            Refresh,
            [Description("Reverse")]
            Reverse
        };

        private PathFindMenu pathFindMenu;
        private ConsoleGraph graph = null;
        private IVertexRoleChanger changer;
        private IGraphFactory factory;
        private string statistics;

        public ConsoleMenu()
        {
            factory = new RandomValuedConsoleGraphFactory(percentOfObstacles: 40,
                width: 35, height: 23);
            graph = (ConsoleGraph)factory.GetGraph();
            changer = new ConsoleVertexRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }
        
        private void ShowMenu()
        {
            var stringBuilder = new StringBuilder("\n");
            MenuOption menu = default;
            var descriptions = menu.GetDescriptions<MenuOption>();

            foreach (var item in descriptions)
            {
                int numberOf = descriptions.IndexOf(item);
                if (numberOf.IsEven())
                    stringBuilder.Append("\n");
                else
                    stringBuilder.Append("  \t");
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item));
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        private MenuOption GetOption()
        {
            Console.Clear();
            GraphShower.ShowGraph(graph);
            ShowMenu();
            return (MenuOption)Input.InputNumber("Choose option: ",
                (int)MenuOption.Reverse, (int)MenuOption.Quit);
        }

        public void Run()
        {
            var option = GetOption();
            while (option != MenuOption.Quit)
            {
                switch (option)
                {
                    case MenuOption.PathFind: FindPath(); break;
                    case MenuOption.Save: SaveGraph(); break;
                    case MenuOption.Load: LoadGraph(); break;
                    case MenuOption.Create: CreateGraph(); break;
                    case MenuOption.Refresh: RefreshGraph(); break;
                    case MenuOption.Reverse: Reverse(); break;
                }
                option = GetOption();
            }
        }

        public void Reverse()
        {
            Console.WriteLine(Res.ReverseMsg);
            Point point = Input.InputPoint(graph.Width, graph.Height);
            changer.ReversePolarity(graph[point.X, point.Y], new EventArgs());
        }

        public void CreateGraph()
        {
            const int MAX_PERCENT_OF_OBSTACLES = 100;
            const int MAX_GRAPH_WIDTH = 100;
            const int MAX_GRAPH_HEIGHT = MAX_GRAPH_WIDTH;
            const int MIN_GRAPH_WIDTH = 10;
            const int MIN_GRAPH_HEIGHT = MIN_GRAPH_WIDTH;

            int obstacles = Input.InputNumber(Res.PercentMsg, MAX_PERCENT_OF_OBSTACLES);
            int width = Input.InputNumber(Res.WidthMsg, MAX_GRAPH_WIDTH, MIN_GRAPH_WIDTH);
            int height = Input.InputNumber(Res.HeightMsg, MAX_GRAPH_HEIGHT, MIN_GRAPH_HEIGHT);
            
            factory = new RandomValuedConsoleGraphFactory(obstacles, width, height);
            graph = (ConsoleGraph)factory.GetGraph();
            changer = new ConsoleVertexRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }

        public void SaveGraph() => new ConsoleGraphSaver().SaveGraph(graph);

        public void LoadGraph()
        {
            AbstractGraph temp = new ConsoleGraphLoader().GetGraph();
            if (temp != null)
            {
                graph = new ConsoleGraph(temp.GetArray());              
                changer = new ConsoleVertexRoleChanger(graph);
                pathFindMenu = new PathFindMenu(graph);
            }
        }

        public void FindPath()
        {
            graph.Refresh();
            Console.Clear();
            GraphShower.ShowGraph(graph);
            pathFindMenu.ChooseStart();
            pathFindMenu.ChooseEnd();
            Console.Clear();
            GraphShower.ShowGraph(graph);
            var search = pathFindMenu.ChoosePathFindAlgorithm();
            search.PauseEvent = () => { };
            if (search.FindDestionation())
            {
                search.DrawPath();
                statistics = search.StatCollector.GetStatistics().GetFormattedData();
                Console.Clear();
                GraphShower.ShowGraph(graph);
                Console.WriteLine("\n" + statistics);
                graph.End = null;
                graph.Start = null;
            }
            else
                Console.WriteLine(Res.BadResultMsg);
            Console.ReadKey();
        }

        public void RefreshGraph() => graph?.Refresh();
    }
}
