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
        private int width;
        public int Width
        {
            get => width;
            private set
            {
                if (value > width)
                    width = value;
            }
        }

        private int height;
        public int Height 
        {
            get => height;
            private set
            {
                if (value > height)
                    height = value;
            }
        }

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
            Console.Write(GetFramedAbscissa(FramedAbscissaView.FrameUnder));
            ShowGraph();
            Console.Write(GetFramedAbscissa(FramedAbscissaView.FrameOver));
        }
       
        private string Abscissa
        {
            get
            {
                var abscissa = new StringBuilder(largeSpace);
                for (var i = 0; i < Width; i++)
                {
                    var offset = !IsOffsetIndex(i) ? bigSpace : space;
                    abscissa.Append(i + offset);
                }
                abscissa.Append(largeSpace);
                return abscissa.ToString();
            }
        }

        private string HorizontalFrame
        {
            get
            {
                var frame = new StringBuilder(largeSpace);
                for (var i = 0; i < Width; i++)
                    frame.Append(horizontalFrame);
                return frame.ToString();
            }
        }

        private string GetFramedAbscissa(FramedAbscissaView framedAbscissaView)
        {
            var framedAbscissaComponents = new List<string>() { Abscissa, HorizontalFrame };
            var framedAbscissa = new StringBuilder();
            if (framedAbscissaView == FramedAbscissaView.FrameOver)
                framedAbscissaComponents.Reverse();
            foreach (var component in framedAbscissaComponents)
                framedAbscissa.AppendLine(component);
            return framedAbscissa.ToString();
        }

        private string DrawOrdinate(int currentHeight,
            TableSide tableSide)
        {
            string ordinate;
            if (tableSide == TableSide.Right)
                ordinate = verticalFrame + currentHeight + newLine;
            else if (!IsOffsetIndex(currentHeight))
                ordinate = currentHeight + space + verticalFrame;
            else
                ordinate = currentHeight + verticalFrame;
            return ordinate;
        }

        private void ShowGraph()
        {
            for (var currentHeight = 0; currentHeight < Height; currentHeight++)
            {
                Console.Write(DrawOrdinate(currentHeight, TableSide.Left));
                for (var currentWidth = 0; currentWidth < Width; currentWidth++)
                {
                    var currentVertexIndex = currentWidth * Height + currentHeight;
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

        private enum FramedAbscissaView { FrameOver, FrameUnder }

        private enum TableSide { Right, Left }

        private readonly List<ConsoleVertex> vertices;

        private const string newLine = "\n";
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "+--";
        private const string verticalFrame = "|";
    }
}
