using ConsoleVersion.InputClass;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using SearchAlgorythms.Algorithm;
using SearchAlgorythms.Graph;
using SearchAlgorythms.RoleChanger;
using System;
using System.Drawing;
using System.Linq;

namespace ConsoleVersion.PathFindAlgorithmMenu
{
    public class PathFindMenu
    {
        private ConsoleGraph graph = null;
        private readonly ConsoleVertexRoleChanger changer;

        public PathFindMenu(ConsoleGraph graph)
        {
            this.graph = graph;
            changer = new ConsoleVertexRoleChanger(this.graph);
        }

        public void ChooseStart() => ChooseRange("\nStart point: ", changer.SetStartPoint);

        public void ChooseEnd() => ChooseRange("Destination point: ", changer.SetDestinationPoint);

        private void ChooseRange(string message, EventHandler method)
        {
            Console.WriteLine(message);
            Point point = ChoosePoint();
            method(graph[point.X, point.Y], new EventArgs());
        }

        private Point ChoosePoint()
        {
            Point point = Input.InputPoint(graph.Width, graph.Height);
            while (graph[point.X, point.Y].IsObstacle || !graph[point.X, point.Y].IsSimpleVertex ||
                !graph[point.X, point.Y].Neighbours.Any())          
                point = Input.InputPoint(graph.Width, graph.Height);
            return point;
        }

        private string ShowAlgorithms()
        {
            return "\n1. Wide path find\n" +
                "2. Deep path find\n" +
                    "3. Dijkstra algorithm\n" +
                    "4. A* algorithm\n" +
                    "5. Greedy for distance algorithm\n" +
                    "6. Greedy for value algorithm\n";
        }

        public IPathFindAlgorithm ChoosePathFindAlgorithm()
        {
            Algorithms algorithms = (Algorithms)Input.InputNumber( ShowAlgorithms() + "Choose algorithm: ", 
                (int)Algorithms.ValueGreedyAlgorithm, (int)Algorithms.WidePathFind);
            return AlgorithmSelector.GetPathFindAlgorithm(algorithms, graph);
        }
    }
}
