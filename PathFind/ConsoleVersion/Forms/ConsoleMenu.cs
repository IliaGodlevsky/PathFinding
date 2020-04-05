using ConsoleVersion.GraphLoader;
using ConsoleVersion.GraphSaver;
using ConsoleVersion.InputClass;
using ConsoleVersion.PathFindAlgorithmMenu;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;
using SearchAlgorythms.PauseMaker;
using SearchAlgorythms.RoleChanger;
using System;
using System.Drawing;

namespace ConsoleVersion.Forms
{
    public class ConsoleMenu
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
        private ConsoleGraphTopRoleChanger changer;
        private RandomValuedConsoleGraphFactory factory;
        private string statistics;

        public ConsoleMenu()
        {
            factory = new RandomValuedConsoleGraphFactory(percentOfObstacles: 40,
                width: 35, height: 23);
            graph = new ConsoleGraph(factory.GetGraph());
            changer = new ConsoleGraphTopRoleChanger(graph);
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
                    case MenuOption.PathFind:   Find();               break;
                    case MenuOption.Save:       Save();               break;
                    case MenuOption.Load:       Load();               break;
                    case MenuOption.Create:     CreateGraph();        break;
                    case MenuOption.Refresh:    graph?.Refresh();     break;
                    case MenuOption.Reverse:    Reverse();            break;
                }
                Console.Clear();
                option = GetOption();
            }
        }

        private void Reverse()
        {
            Console.WriteLine("Reverse top choice: ");
            Point point = pathFindMenu.InputPoint();
            changer.ReversePolarity(graph[point.X, point.Y], new EventArgs());
        }

        private void CreateGraph()
        {
            int obstacles = Input.InputNumber("Enter number of obstacles: ", 100);
            int height = Input.InputNumber("Enter width of graph: ", 100, 10);
            int width = Input.InputNumber("Enter height of graph: ", 100, 10);
            factory = new RandomValuedConsoleGraphFactory(obstacles, width, height);
            graph = new ConsoleGraph(factory.GetGraph());
            changer = new ConsoleGraphTopRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }

        private void Save()
        {
            ConsoleGraphSaver save = new ConsoleGraphSaver();
            save.SaveGraph(graph);
        }

        private void Load()
        {
            ConsoleGraphLoader loader = new ConsoleGraphLoader();
            AbstractGraph temp = loader.GetGraph();
            if (temp != null)
            {
                graph = new ConsoleGraph(temp.GetArray());              
                changer = new ConsoleGraphTopRoleChanger(graph);
                pathFindMenu = new PathFindMenu(graph);
            }

        }

        private void Find()
        {
            graph.Refresh();
            Console.Clear();
            GraphShower.ShowGraph(graph);
            pathFindMenu.ChooseStart();
            pathFindMenu.ChooseEnd();
            Console.Clear();
            GraphShower.ShowGraph(graph);
            var search = pathFindMenu.ChoosePathFindAlgorithm();
            search.Pause = PauseMaker.ConsolePause;
            if (search.FindDestionation())
                search.DrawPath();
            statistics = search.GetStatistics();
            Console.Clear();
            GraphShower.ShowGraph(graph);
            Console.WriteLine("\n" + statistics);
            Console.ReadKey();
            graph.End = null;
            graph.Start = null;
        }
    }
}
