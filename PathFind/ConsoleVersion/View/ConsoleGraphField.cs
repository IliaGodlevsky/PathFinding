using ConsoleVersion.Model;
using GraphLib.Coordinates;
using GraphLib.Coordinates.Infrastructure;
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
            if (vertex.Position as Coordinate2D == null)
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
            var framedAbscissaComponents = new List<string>() 
            { 
                Abscissa, 
                HorizontalFrame 
            };

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

        private string DrawOrdinate(int currentLength, TableSide tableSide)
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
            for (int length = 0; length < Length; length++)
            {
                string ordinate = DrawOrdinate(length, TableSide.Left);
                Console.Write(ordinate);

                for (int width = 0; width < Width; width++)
                {
                    var coordinate = new Coordinate2D(width, length);
                    int index = Index.ToIndex(coordinate, Length);

                    ShowVertex(vertices[index]);

                    if (IsEndOfRow(width))
                    {
                        ordinate = DrawOrdinate(length, TableSide.Right);
                        Console.Write(ordinate);
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

        private enum FramedAbscissaView 
        { 
            FrameOver, 
            FrameUnder 
        
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
        private const string horizontalFrame = "+--";
        private const string verticalFrame = "|";
    }
}
