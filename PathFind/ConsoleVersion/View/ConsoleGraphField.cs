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
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ConsoleGraphField()
        {
            vertices = new List<ConsoleVertex>();
        }

        public void Add(IVertex vertex, int xCoordinate, int yCoordinate)
        {
            vertices.Add(vertex as ConsoleVertex);
            Width = xCoordinate + 1;
            Height = yCoordinate + 1;
        }

        public void ShowGraphWithFrames()
        {
            Console.ForegroundColor = Color.White;
            Console.Write(Abscissa);
            Console.Write(HorizontalFrame);
            ShowGraph();
            Console.Write(largeSpace);
            Console.Write(HorizontalFrame);
            Console.Write(Abscissa);
        }
        
        private string Abscissa
        {
            get
            {
                var abscissa = new StringBuilder();
                abscissa.Append(largeSpace);
                for (int i = 0; i < Width; i++)
                {
                    var offset = !IsOffsetIndex(i) ? bigSpace : space;
                    abscissa.Append(i + offset);
                }
                abscissa.Append(newLine + largeSpace);
                return abscissa.ToString();
            }
        }

        private string HorizontalFrame
        {
            get
            {
                var frame = new StringBuilder();
                for (int i = 0; i < Width; i++)
                    frame.Append(horizontalFrame);
                frame.Append(newLine);
                return frame.ToString();
            }
        }

        private string DrawOrdinate(int currentHeight,
            TableSide tableSide = TableSide.Left)
        {
            string line;
            if (tableSide is TableSide.Right)
                line = verticalFrame + currentHeight + newLine;
            else if (!IsOffsetIndex(currentHeight))
                line = currentHeight + space + verticalFrame;
            else
                line = currentHeight + verticalFrame;
            return line;
        }

        private void ShowGraph()
        {
            for (int currentHeight = 0; currentHeight < Height; currentHeight++)
            {
                Console.Write(DrawOrdinate(currentHeight));
                for (int currentWidth = 0; currentWidth < Width; currentWidth++)
                {
                    int currentVertexIndex = currentWidth * Height + currentHeight;
                    ShowVertex(vertices[currentVertexIndex]);
                    if (IsEndOfRow(currentWidth))
                        Console.Write(DrawOrdinate(currentHeight, TableSide.Right));
                }
            }
        }

        private void ShowVertex(ConsoleVertex vertex)
        {
            Console.Write(vertex.Cost + bigSpace, vertex.Colour);
        }

        private bool IsEndOfRow(int currentWidth)
        {
            return currentWidth == Width - 1;
        }

        private bool IsOffsetIndex(int currentIndex)
        {
            return currentIndex >= 10;
        }

        private enum TableSide
        {
            Right,
            Left
        }

        private readonly List<ConsoleVertex> vertices;

        private const string newLine = "\n";
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "---";
        private const string verticalFrame = "|";
    }
}
