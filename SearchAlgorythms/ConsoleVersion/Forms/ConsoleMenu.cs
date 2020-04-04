using ConsoleApp1.GraphSaver;
using ConsoleVersion.GraphLoader;
using ConsoleVersion.InputClass;
using ConsoleVersion.PathFindAlgorithmMenu;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;
using SearchAlgorythms.PauseMaker;
using SearchAlgorythms.RoleChanger;
using System;

namespace ConsoleVersion.Forms
{
    public class ConsoleMenu
    {
        private PathFindMenu pathFindMenu;
        private ConsoleGraph graph = null;
        private ConsoleGraphTopRoleChanger changer;
        private RandomValuedConsoleGraphFactory factory;
        private string statistics;

        public ConsoleMenu()
        {
            factory = new RandomValuedConsoleGraphFactory(percentOfObstacles: 40,
                width: 20, height: 20);
            graph = new ConsoleGraph(factory.GetGraph());
            changer = new ConsoleGraphTopRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }

        public void CreateGraph()
        {
            int obstacles = Input.InputNumber("Enter number of obstacles: ", 100);
            int height = Input.InputNumber("Enter width of graph: ", 25, 10);
            int width = Input.InputNumber("Enter height of graph: ", 25, 10);
            factory = new RandomValuedConsoleGraphFactory(obstacles, width, height);
            graph = new ConsoleGraph(factory.GetGraph());
            changer = new ConsoleGraphTopRoleChanger(graph);
            pathFindMenu = new PathFindMenu(graph);
        }
       
        public void ShowGraph()
        {
            GraphShower.ShowGraph(ref graph);
        }

        public void ShowStat()
        {
            Console.WriteLine(statistics);
        }

        public void Refresh()
        {
            graph?.Refresh();
        }

        public void ChooseStart()
        {
            pathFindMenu.ChooseStart();
        }

        public void Save()
        {
            ConsoleGraphSaver save = new ConsoleGraphSaver();
            save.SaveGraph(graph);
        }

        public void Load()
        {
            ConsoleGraphLoader loader = new ConsoleGraphLoader();
            IGraph temp = loader.GetGraph();
            if (temp != null)
            {
                graph = new ConsoleGraph(temp.GetArray());              
                changer = new ConsoleGraphTopRoleChanger(graph);
                pathFindMenu = new PathFindMenu(graph);
            }

        }

        public void ChooseEnd()
        {
            pathFindMenu.ChooseEnd();
        }

        public void Find()
        {
            var search = pathFindMenu.ChoosePathFindAlgorithm();
            search.Pause = PauseMaker.ConsolePause;
            if (search.FindDestionation())
                search.DrawPath();
            statistics = search.GetStatistics();
            graph.End = null;
            graph.Start = null;
        }
    }
}
