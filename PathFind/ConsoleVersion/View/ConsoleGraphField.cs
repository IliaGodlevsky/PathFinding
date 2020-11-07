using ConsoleVersion.Model.Vertex;
using GraphLib.Coordinates;
using GraphLib.GraphField;
using GraphLib.Vertex.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal class ConsoleGraphField : IGraphField
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public ConsoleGraphField()
        {
            vertices = new List<ConsoleVertex>();
        }

        public void Add(IVertex vertex)
        {
            var coordinate = vertex.Position as Coordinate2D;

            if (coordinate == null)
            {
                throw new ArgumentException("Must be 2D coordinates");
            }

            vertices.Add(vertex as ConsoleVertex);
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
                {
                    frame.Append(horizontalFrame);
                }

                return frame.ToString();
            }
        }

        private string GetFramedAbscissa(FramedAbscissaView framedAbscissaView)
        {
            var framedAbscissaComponents = new List<string>() { Abscissa, HorizontalFrame };
            var framedAbscissa = new StringBuilder();

            if (framedAbscissaView == FramedAbscissaView.FrameOver)
            {
                framedAbscissaComponents.Reverse();
            }

            foreach (var component in framedAbscissaComponents)
            {
                framedAbscissa.AppendLine(component);
            }

            return framedAbscissa.ToString();
        }

        private string DrawOrdinate(int currentLength,
            TableSide tableSide)
        {
            string ordinate;
            if (tableSide == TableSide.Right)
            {
                ordinate = verticalFrame + currentLength + newLine;
            }
            else if (!IsOffsetIndex(currentLength))
            {
                ordinate = currentLength + space + verticalFrame;
            }
            else
            {
                ordinate = currentLength + verticalFrame;
            }

            return ordinate;
        }

        private void ShowGraph()
        {
            for (var currentLength = 0; currentLength < Length; currentLength++)
            {
                Console.Write(DrawOrdinate(currentLength, TableSide.Left));

                for (var currentWidth = 0; currentWidth < Width; currentWidth++)
                {
                    int index = Index.ToIndex(new Coordinate2D(currentWidth, currentLength), Length);
                    ShowVertex(vertices[index]);

                    if (IsEndOfRow(currentWidth))
                    {
                        Console.Write(DrawOrdinate(currentLength, TableSide.Right));
                    }
                }
            }
        }

        private void ShowVertex(ConsoleVertex vertex)
        {
            Console.Write(vertex.Text + bigSpace, vertex.Colour);
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

        private readonly IList<ConsoleVertex> vertices;

        private const string newLine = "\n";
        private const string space = " ";
        private const string bigSpace = "  ";
        private const string largeSpace = "   ";
        private const string horizontalFrame = "+--";
        private const string verticalFrame = "|";
    }
}
