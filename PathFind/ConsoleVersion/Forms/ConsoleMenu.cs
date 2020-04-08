using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.PathFindAlgorithmMenu;
using ConsoleVersion.PauseMaker;
using GraphLibrary.GraphFactory;
using GraphLibrary.View;
using SearchAlgorythms.Algorithm;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;
using SearchAlgorythms.GraphLoader;
using SearchAlgorythms.RoleChanger;
using System;
using System.Drawing;

namespace ConsoleVersion.Forms
{
    public class ConsoleMenu : IView
    {
        private enum MenuOption
        {
            Quit,
            PathFind,
            Save,
            Load,
            Create,
            Refresh,
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
            Console.WriteLine("\n0. Quit   1. Find path");
            Console.WriteLine("2. Save   3. Load");
            Console.WriteLine("4. Create 5. Refresh");
            Console.WriteLine("6. Reverse");
        }

        private MenuOption GetOption()
        {
            GraphShower.ShowGraph(graph);
            ShowMenu();
            return (MenuOption)Input.InputNumber("Choose option: ",
                (int)MenuOption.Reverse, (int)MenuOption.Quit);
        }

        public void Run()
        {
            MenuOption option = GetOption();
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
                Console.Clear();
                option = GetOption();
            }
        }

        public void Reverse()
        {
            Console.WriteLine("Reverse top choice: ");
            Point point = Input.InputPoint(graph.Width, graph.Height);
            changer.ReversePolarity(graph[point.X, point.Y], new EventArgs());
        }

        public void CreateGraph()
        {
            int obstacles = Input.InputNumber("Enter number of obstacles: ", 100);
            int height = Input.InputNumber("Enter width of graph: ", 100, 10);
            int width = Input.InputNumber("Enter height of graph: ", 100, 10);
            factory = new RandomValuedConsoleGraphFactory(obstacles, width, height);
            graph = (ConsoleGraph)factory.GetGraph();
            changer = new ConsoleVertexRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }

        public void SaveGraph() => new ConsoleGraphSaver().SaveGraph(graph);

        public void LoadGraph()
        {
            IGraphLoader loader = new ConsoleGraphLoader();
            AbstractGraph temp = loader.GetGraph();
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
            IPathFindAlgorithm search = pathFindMenu.ChoosePathFindAlgorithm();
            search.Pause = new ConsolePauseMaker().Pause;
            if (search.FindDestionation())
            {
                search.DrawPath();
                statistics = search.GetStatistics();
                Console.Clear();
                GraphShower.ShowGraph(graph);
                Console.WriteLine("\n" + statistics);
                graph.End = null;
                graph.Start = null;
            }
            else
                Console.WriteLine("Couldn't find path");
            Console.ReadKey();
        }

        public void RefreshGraph() => graph?.Refresh();
    }
}
