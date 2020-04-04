using ConsoleVersion.InputClass;
using SearchAlgorythms.Algorithm;
using SearchAlgorythms.DistanceCalculator;
using SearchAlgorythms.Graph;
using SearchAlgorythms.RoleChanger;
using System;
using System.Drawing;

namespace ConsoleVersion.PathFindAlgorithmMenu
{
    public class PathFindMenu
    {
        private ConsoleGraph graph = null;
        private readonly ConsoleGraphTopRoleChanger changer;

        public enum Algorithms
        {
            WidePathFind = 1,
            BestFirstPathFind,
            DijkstraAlgorithm,
            AStarAlgorithm,
            GreedyAlgorithm
        };

        public PathFindMenu(ConsoleGraph graph)
        {
            this.graph = graph;
            changer = new ConsoleGraphTopRoleChanger(this.graph);
        }

        public void ChooseStart()
        {
            Console.WriteLine("Start point: ");
            Point point = ChoosePoint();
            changer.SetStartPoint(graph[point.X, point.Y], new EventArgs());
        }

        public void ChooseEnd()
        {
            Console.WriteLine("Destination point: ");
            Point point = ChoosePoint();
            changer.SetDestinationPoint(graph[point.X, point.Y], new EventArgs());
        }

        public Point ChoosePoint()
        {
            int x = Input.InputNumber("Enter x coordinate of point: ", graph.GetWidth());
            int y = Input.InputNumber("Enter y coordinate of point: ", graph.GetHeight());           
            while (graph[x, y].IsObstacle || !graph[x, y].IsSimpleTop)
            {
                x = Input.InputNumber("Enter x coordinate of point: ", graph.GetWidth());
                y = Input.InputNumber("Enter y coordinate of point: ", graph.GetHeight());
            }
            return new Point(x, y);
        }

        private string ShowAlgorithms()
        {
            return "1. Wide path find\n" +
                    "2. Best first path find\n" +
                    "3. Dijkstra algorithm\n" +
                    "4. A* algorithm\n" +
                    "5. Greedy algorithm\n";
        }

        public IPathFindAlgorithm ChoosePathFindAlgorithm()
        {
            Algorithms algorithms = (Algorithms)Input.InputNumber(
                ShowAlgorithms() + "Choose algorithm: ", 5, 1);
            switch (algorithms)
            {
                case Algorithms.WidePathFind:
                    return new WidePathFindAlgorithm(graph);
                case Algorithms.BestFirstPathFind:
                    return new BestFirstAlgorithm(graph);
                case Algorithms.DijkstraAlgorithm:
                    return new DijkstraAlgorithm(graph);
                case Algorithms.AStarAlgorithm:
                    {
                        AStarAlgorithm algo = new AStarAlgorithm(graph)
                        {
                            HeuristicFunction = DistanceCalculator.GetChebyshevDistance
                        };
                        return algo;
                    }
                case Algorithms.GreedyAlgorithm:
                    return new GreedyAlgorithm(graph);
                default:
                    return null;
            }
        }
    }
}
