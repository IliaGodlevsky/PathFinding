using Colorful;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Graphs;
using System.Linq;

namespace ConsoleVersion.Model
{
    internal sealed class EndPointsSelection : IRequireInt32Input
    {
        public IValueInput<int> Int32Input { get; set; }

        public EndPointsSelection(BaseEndPoints endPoints, IGraph graph,
            int numberOfAvailiableVertices)
        {
            this.graph = graph;
            this.endPoints = endPoints;
            this.numberOfAvailiableVertices = numberOfAvailiableVertices;
        }

        public void ChooseEndPoints()
        {
            int cursorLeft = Console.CursorLeft;
            int cursorRight = Console.CursorTop;
            int numberOfIntermediates = Int32Input.InputValue(MessagesTexts.NumberOfIntermediateVerticesInputMsg, numberOfAvailiableVertices);
            var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
            var chooseMessages = new[] { "\n" + MessagesTexts.SourceVertexChoiceMsg, MessagesTexts.TargetVertexChoiceMsg }.Concat(messages);
            endPoints.Reset();
            foreach (var message in chooseMessages)
            {
                Console.SetCursorPosition(cursorLeft, cursorRight);
                var vertex = ChooseVertex(message);
                cursorLeft = Console.CursorLeft;
                cursorRight = Console.CursorTop;
                (vertex as Vertex)?.SetAsExtremeVertex();
            }
        }

        private IVertex ChooseVertex(string message)
        {
            if (graph is Graph2D graph2D)
            {
                Console.WriteLine(message);
                IVertex vertex;
                do
                {
                    vertex = Int32Input.InputVertex(graph2D);
                } while (!endPoints.CanBeEndPoint(vertex));

                return vertex;
            }
            return new NullVertex();
        }

        private readonly IGraph graph;
        private readonly BaseEndPoints endPoints;
        private readonly int numberOfAvailiableVertices;
    }
}
