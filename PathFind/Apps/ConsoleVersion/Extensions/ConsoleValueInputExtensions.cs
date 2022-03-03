using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Extensions
{
    internal static class ConsoleValueInputExtensions
    {
        public static void ChooseEndPoints(this ConsoleValueInput<int> self, Graph2D graph, 
            IEndPoints endPoints, IEnumerable<string> messages)
        {
            endPoints.ChooseEndPoints(graph, messages, self.InputEndPoint, OnEndPointChosen);
        }

        public static void ChooseIntermediates(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints,
            int numberOfAvailableIntermediate)
        {
            int numberOfIntermediates = self.InputValue(MessagesTexts.NumberOfIntermediateVerticesInputMsg, 
                numberOfAvailableIntermediate);
            var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
            self.ChooseEndPoints(graph, endPoints, messages);
        }

        public static void ChangeIntermediates(this ConsoleValueInput<int> self, Graph2D graph,
            IEndPoints endPoints, int numberOfIntermediates)
        {
            int numberToReplace = self.InputValue(MessagesTexts.NumberOfIntermediateVerticesInputMsg, numberOfIntermediates);
            var messages = Enumerable.Repeat(MessagesTexts.IntermediateToReplaceMsg, numberToReplace);
            self.ChooseExistingIntermediates(graph, endPoints, messages);
            messages = Enumerable.Repeat(MessagesTexts.PlaceToPutIntermediateMsg, numberToReplace);
            self.ChooseEndPoints(graph, endPoints, messages);
        }

        public static void ChangeSource(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            var source = (Vertex)endPoints.Source;
            self.ChangeSourceOrTarget(graph, endPoints, source, MessagesTexts.SourceVertexChoiceMsg);
        }

        public static void ChangeTarget(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints)
        {
            var target = (Vertex)endPoints.Target;
            self.ChangeSourceOrTarget(graph, endPoints, target, MessagesTexts.TargetVertexChoiceMsg);
        }

        private static void ChooseExistingIntermediates(this ConsoleValueInput<int> self, 
            Graph2D graph, IEndPoints endPoints, IEnumerable<string> messages)
        {
            endPoints.ChooseEndPoints(graph, messages, self.ChooseExistingIntermediate, MarkToReplace);
        }

        private static Vertex InputVertex(this ConsoleValueInput<int> self, 
            Graph2D graph, Predicate<IVertex> predicate, string message)
        {
            Console.WriteLine(message);
            Vertex vertex;
            do
            {
                vertex = (Vertex)self.InputVertex(graph);
            }
            while (!predicate(vertex));

            return vertex;
        }

        private static void ChangeSourceOrTarget(this ConsoleValueInput<int> self,
            Graph2D graph, IEndPoints endPoints, Vertex vertex, string message)
        {
            vertex.OnEndPointChosen();
            vertex = self.InputEndPoint(graph, endPoints, message);
            vertex.OnEndPointChosen();
        }

        private static Vertex InputEndPoint(this ConsoleValueInput<int> self, Graph2D graph, IEndPoints endPoints, string message)
        {
            return self.InputVertex(graph, endPoints.CanBeEndPoint, message);
        }

        private static Vertex ChooseExistingIntermediate(this ConsoleValueInput<int> self, 
            Graph2D graph, IEndPoints endPoints, string message)
        {
            var intermediates = endPoints.GetIntermediates().ToList();
            return self.InputVertex(graph, intermediates.Contains, message);
        }

        private static void MarkToReplace(Vertex vertex) => vertex.OnMarkedToReplaceIntermediate();
        private static void OnEndPointChosen(Vertex vertex) => vertex.OnEndPointChosen();
    }
}