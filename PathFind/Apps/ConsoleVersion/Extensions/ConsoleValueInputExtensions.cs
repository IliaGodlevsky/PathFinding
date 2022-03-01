using ConsoleVersion.Model;
using ConsoleVersion.ValueInput;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;
using System.Collections.Generic;

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
    }
}
