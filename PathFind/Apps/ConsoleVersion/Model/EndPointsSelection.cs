using Colorful;
using ConsoleVersion.Enums;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Messages;
using GalaSoft.MvvmLight.Messaging;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Graphs;
using Logging.Interface;
using System;
using System.Linq;

using Console = Colorful.Console;

namespace ConsoleVersion.Model
{
    internal sealed class EndPointsSelection : IRequireInt32Input, IDisposable
    {
        public IValueInput<int> Int32Input { get; set; }

        public EndPointsSelection(BaseEndPoints endPoints, ILog log)
        {
            graph = NullGraph.Instance;
            this.endPoints = endPoints;
            Messenger.Default.Register<GraphCreatedMessage>(this, MessageTokens.EndPointsSelection, SetGraph);
            var claimMessage = new ClaimGraphMessage(MessageTokens.EndPointsSelection);
            Messenger.Default.Forward(claimMessage, MessageTokens.Everyone);
        }

        public void ChooseEndPoints()
        {
            if (HasAnyVerticesToChooseAsEndPoints)
            {
                int cursorLeft = Console.CursorLeft;
                int cursorRight = Console.CursorTop;
                int numberOfIntermediates = Int32Input.InputValue(MessagesTexts.NumberOfIntermediateVerticesInputMsg, 
                    numberOfAvailiableVertices);
                var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
                var chooseMessages = new[] 
                { 
                    "\n" + MessagesTexts.SourceVertexChoiceMsg, 
                    MessagesTexts.TargetVertexChoiceMsg 
                }.Concat(messages);
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
            else
            {
                log.Warn(MessagesTexts.NoVerticesAsEndPointsMsg);
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
            return NullVertex.Instance;
        }

        private void SetGraph(GraphCreatedMessage message)
        {
            graph = message.Graph;
        }

        public void Dispose()
        {
            Messenger.Default.Unregister(this);
        }

        private int NumberOfAvailableIntermediate => graph.Size - graph.GetIsolatedCount() - 2;
        private bool HasAnyVerticesToChooseAsEndPoints => NumberOfAvailableIntermediate >= 0;

        private readonly ILog log;
        private IGraph graph;
        private readonly BaseEndPoints endPoints;
        private readonly int numberOfAvailiableVertices;
    }
}
