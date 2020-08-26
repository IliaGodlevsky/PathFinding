using ConsoleVersion.InputClass;
using GraphLibrary.AlgorithmEnum;
using ConsoleVersion.Graph;
using ConsoleVersion.RoleChanger;
using System;
using System.Drawing;
using GraphLibrary.Extensions;
using System.Text;

namespace ConsoleVersion.PathFindAlgorithmMenu
{
    public class PathFindMenu
    {
        private readonly ConsoleGraph graph;
        private readonly ConsoleVertexRoleChanger changer;
        private readonly string algorithmList;

        public PathFindMenu(ConsoleGraph graph)
        {
            this.graph = graph;
            changer = new ConsoleVertexRoleChanger(this.graph);
            algorithmList = GetShowAlgorithms();
        }

        public void ChooseStart()
        {
            ChooseRange("\n" + Res.StartPoint, changer.SetStartPoint);
        }

        public void ChooseEnd()
        {
            ChooseRange(Res.DestinationPoint, changer.SetDestinationPoint);
        }

        private void ChooseRange(string message, EventHandler method)
        {
            Console.WriteLine(message);
            Point point = ChoosePoint();
            method(graph[point.X, point.Y], new EventArgs());
        }

        private Point ChoosePoint()
        {
            Point point = Input.InputPoint(graph.Width, graph.Height);
            while (!graph[point.X, point.Y].IsValidToBeRange())
                point = Input.InputPoint(graph.Width, graph.Height);
            return point;
        }

        private string GetShowAlgorithms()
        {
            var stringBuilder = new StringBuilder("\n");

            foreach (var item in ((Algorithms)default).GetDescriptions<Algorithms>())
            {
                int numberOf = ((Algorithms)default).GetDescriptions<Algorithms>().IndexOf(item) + 1;
                stringBuilder.Append(string.Format(Res.ShowFormat, numberOf, item) + "\n");
            }

            return stringBuilder.ToString();
        }

        public Algorithms GetAlgorithmEnum()
        {
            return (Algorithms)Input.InputNumber(algorithmList + Res.ChooseAlrorithm,
                (int)Algorithms.ValueGreedyAlgorithm, (int)Algorithms.WidePathFind);
        }
    }
}
