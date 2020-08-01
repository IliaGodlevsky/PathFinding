using ConsoleVersion.InputClass;
using GraphLibrary.Enums.AlgorithmEnum;
using GraphLibrary.PathFindAlgorithmSelector;
using ConsoleVersion.Graph;
using ConsoleVersion.RoleChanger;
using System;
using System.Drawing;
using System.Linq;
using GraphLibrary.Algorithm;
using GraphLibrary.Extensions;
using System.Text;

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

        public void ChooseStart() => ChooseRange("\n" + Res.StartPoint, changer.SetStartPoint);

        public void ChooseEnd() => ChooseRange(Res.DestinationPoint, changer.SetDestinationPoint);

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
            var stringBuilder = new StringBuilder("\n");
            Algorithms algo = default;
            var descriptions = algo.GetDescriptions<Algorithms>();
            foreach (var item in descriptions)
            {
                int numberOf = descriptions.IndexOf(item) + 1;
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item));
                stringBuilder.Append("\n");
            }

            return stringBuilder.ToString();
        }

        public IPathFindAlgorithm ChoosePathFindAlgorithm()
        {
            Algorithms algorithms = (Algorithms)Input.InputNumber(ShowAlgorithms() + Res.ChooseAlrorithm, 
                (int)Algorithms.ValueGreedyAlgorithm, (int)Algorithms.WidePathFind);
            return AlgorithmSelector.GetPathFindAlgorithm(algorithms, graph);
        }
    }
}
