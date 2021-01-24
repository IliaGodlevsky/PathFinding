using ConsoleVersion.Model;
using GraphLib.Coordinates;
using GraphLib.Extensions;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class GraphField : IGraphField
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public GraphField()
        {
            vertices = new List<Vertex>();
        }

        public void Add(IVertex vertex)
        {
            if (vertex.Position as Coordinate2D == null)
            {
                throw new ArgumentException("Must be 2D coordinates");
            }

            vertices.Add(vertex as Vertex);
        }

        public void ShowGraphWithFrames()
        {
            Console.ForegroundColor = Color.White;
            DrawAbscissa();
            DrawOrdinate();
            DrawGraph();
        }

        private string Abscissa
        {
            get
            {
                var abscissa = new StringBuilder();

                for (var i = 0; i < Width; i++)
                {
                    var offset = !IsOffsetIndex(i) ? BigSpace : Space;
                    abscissa.Append(i + offset);
                }
                return abscissa.ToString();
            }
        }

        private string HorizontalFrame
        {
            get
            {
                var frame = new StringBuilder();

                for (var i = 0; i < Width; i++)
                {
                    frame.Append(horizontalFrame);
                }

                return frame.ToString();
            }
        }

        private void DrawOrdinate()
        {
            for (int length = 0; length < Length; length++)
            {
                Console.SetCursorPosition(0, 3 + length);
                Console.Write(length.ToString().PadLeft(2) + verticalFrame);
                Console.SetCursorPosition(3 + Width * 3, 3 + length);
                Console.Write(verticalFrame + length.ToString());
            }
        }

        private void DrawAbscissa()
        {
            Console.SetCursorPosition(3, 1);
            Console.Write(Abscissa);
            Console.SetCursorPosition(3, 2);
            Console.Write(HorizontalFrame);
            Console.SetCursorPosition(3, Length + 3);
            Console.Write(HorizontalFrame);
            Console.SetCursorPosition(3, Length + 4);
            Console.Write(Abscissa);
        }

        private void DrawGraph()
        {
            for (int length = 0; length < Length; length++)
            {                
                for (int width = 0; width < Width; width++)
                {
                    var coordinate = new Coordinate2D(width, length);
                    int index = coordinate.ToIndex(Length);

                    var vertex = vertices[index];

                    vertex.Show();
                }
            }
        }

        private bool IsOffsetIndex(int currentIndex)
        {
            return currentIndex >= 10;
        }

        private readonly List<Vertex> vertices;

        private const string Space = " ";
        private const string BigSpace = "  ";
        private const string LargeSpace = "   ";
        private const string horizontalFrame = "+--";
        private const string verticalFrame = "|";
    }
}
