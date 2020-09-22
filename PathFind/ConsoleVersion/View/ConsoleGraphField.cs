using ConsoleVersion.Model.Vertex;
using GraphLibrary.GraphField;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class ConsoleGraphField : IGraphField
    {
        public ConsoleGraphField()
        {
            vertices = new Dictionary<Position, IVertex>();
        }

        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            vertices.Add(new Position(xCoordinate, yCoordinate), vertex);
            Width = xCoordinate + 1;
            Height = yCoordinate + 1;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public void ShowGraphWithFrames()
        {
            Console.ForegroundColor = Color.White;
            Console.Write(DrawAbscissa());
            Console.Write(WriteHorizontalFrame());
            ShowGraph();
            Console.Write(largeSpace);
            Console.Write(WriteHorizontalFrame());
            Console.Write(DrawAbscissa());
        }

        private readonly Dictionary<Position, IVertex> vertices;

        private enum TableSide
        {
            Right,
            Left
        }

        private const int OFFSET_ARRAY_INDEX = 10;
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "---";
        private const string verticalFrame = "|";

        private string DrawAbscissa()
        {
            var abscissa = new StringBuilder();
            abscissa.Append(largeSpace);
            for (int i = 0; i < Width; i++)
                abscissa.Append(i + (i < OFFSET_ARRAY_INDEX ? bigSpace : space));
            abscissa.Append("\n" + largeSpace);
            return abscissa.ToString();
        }

        private string WriteHorizontalFrame()
        {
            var frame = new StringBuilder();
            for (int i = 0; i < Width; i++)
                frame.Append(horizontalFrame);
            frame.Append("\n");
            return frame.ToString();
        }

        private string DrawOrdinate(int currentHeight,
            TableSide tableSide = TableSide.Left)
        {
            string line;
            if (tableSide == TableSide.Right)
                line = verticalFrame + currentHeight + "\n";
            else if (currentHeight < OFFSET_ARRAY_INDEX)
                line = currentHeight + space + verticalFrame;
            else
                line = currentHeight + verticalFrame;
            return line;
        }

        private void ShowVertex(IVertex vertex)
        {
            ConsoleVertex vert = vertex as ConsoleVertex;
            Console.Write(vert.Cost + bigSpace, vert.Colour);
        }

        private bool IsEndOfRow(int currentWidth)
        {
            return currentWidth == Width - 1;
        }

        private void ShowGraph()
        {
            for (int currentHeight = 0; currentHeight < Height; currentHeight++)
            {
                Console.Write(DrawOrdinate(currentHeight));
                for (int currentWidth = 0; currentWidth < Width; currentWidth++)
                {
                    var currentVertexPosition = new Position(currentWidth, currentHeight);
                    ShowVertex(vertices[currentVertexPosition]);
                    if (IsEndOfRow(currentWidth))
                        Console.Write(DrawOrdinate(currentHeight, TableSide.Right));
                }
            }
        }
    }
}
