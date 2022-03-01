using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using ConsoleVersion.Views;
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
        public static Vertex InputVertex(this ConsoleValueInput<int> self,
            Graph2D graph, IEndPoints endPoints, string message)
        {
            Console.WriteLine(message);
            Vertex vertex;
            do
            {
                vertex = (Vertex)self.InputVertex(graph);
            } 
            while (!endPoints.CanBeEndPoint(vertex));

            return vertex;
        }

        public static void InputEndPoints(this ConsoleValueInput<int> self, Graph2D graph,
            IEndPoints endPoints, IEnumerable<string> messages)
        {
            ConsoleCursor.SaveCursorPosition();
            foreach (var message in messages)
            {
                ConsoleCursor.RestoreCursorPosition();
                var vertex = self.InputVertex(graph, endPoints, message);
                ConsoleCursor.SaveCursorPosition();
                vertex.OnEndPointChosen();
            }
        }

        public static void ChangeIntermediates(this ConsoleValueInput<int> self, Graph2D graph,
            IEndPoints endPoints)
        {
            Console.WriteLine(MessagesTexts.IntermediateToReplaceMsg);
            var toReplace = (Vertex)self.InputVertex((Graph2D)graph);
            ConsoleCursor.SaveCursorPosition();
            toReplace.OnMarkedToReplaceIntermediate();
            ConsoleCursor.RestoreCursorPosition();
            Console.WriteLine(MessagesTexts.PlaceToPutIntermediateMsg);
            var newIntermediate = (Vertex)self.InputVertex((Graph2D)graph);
            newIntermediate.OnEndPointChosen();
        }

        public static void ChooseIntermediates(this ConsoleValueInput<int> self, Graph2D graph,
            IEndPoints endPoints, int numberOfAvailableIntermediate,  int offset)
        {
            MainView.SetCursorPositionUnderMenu(offset);
            int numberOfIntermediates = self.InputValue(MessagesTexts.NumberOfIntermediateVerticesInputMsg, 
                numberOfAvailableIntermediate);
            var messages = Enumerable.Repeat(MessagesTexts.IntermediateVertexChoiceMsg, numberOfIntermediates);
            self.InputEndPoints((Graph2D)graph, endPoints, messages);
        }
    }
}
