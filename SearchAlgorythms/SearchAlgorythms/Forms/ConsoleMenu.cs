using SearchAlgorythms.Algorithm;
using SearchAlgorythms.Graph;
using SearchAlgorythms.GraphFactory;
using SearchAlgorythms.RoleChanger;
using SearchAlgorythms.Top;
using System;
using System.Diagnostics;
using System.Drawing;
using Console = Colorful.Console;

namespace SearchAlgorythms.Forms
{
    public class ConsoleMenu
    {
        public ConsoleMenu()
        {
            RandomValuedConsoleGraphFactory factory 
                = new RandomValuedConsoleGraphFactory(percentOfObstacles: 40, width: 20, height: 20);
            graph = new ConsoleGraph(factory.GetGraph());
            changer = new ConsoleGraphTopRoleChanger(graph);
        }

        public void ConsolePause(int milliseconds)
        {
            var sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < milliseconds)
                continue;
            ShowGraph();
            Console.Clear();
            sw.Stop();
        }

        private IGraph graph = null;
        ConsoleGraphTopRoleChanger changer;
        public void ShowGraph()
        {
            for (int width = 0; width < graph.GetWidth(); width++)
            {
                for (int height = 0; height < graph.GetHeight(); height++) 
                {
                    ConsoleGraphTop top = graph[width, height] as ConsoleGraphTop;
                    Console.Write(top.Text + " ", top.Colour);
                    if (height != 0 && height % 19 == 0) 
                        Console.Write("\n");
                }
            }
        }

        public void ChooseStart()
        {
            Point point = ChoosePoint();
            changer.SetStartPoint(graph[point.X, point.Y], new EventArgs());
        }

        public void ChooseEnd()
        {
            Point point = ChoosePoint();
            changer.SetDestinationPoint(graph[point.X, point.Y], new EventArgs());
        }

        private Point ChoosePoint()
        {
            int x, y;
            x = int.Parse(Console.ReadLine());
            y = int.Parse(Console.ReadLine());
            return new Point(x, y);
        }

        public void Find()
        {
            var search = new AStarAlgorithm(graph)
            {
                HeuristicFunction = DistanceCalculator.DistanceCalculator.GetChebyshevDistance,
                Pause = ConsolePause
            };
            if (search.FindDestionation())
                search.DrawPath();
        }
    }
}
