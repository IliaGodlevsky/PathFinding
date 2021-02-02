using ConsoleVersion.Model;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            Console.CursorVisible = false;
            DrawAbscissaFrames();
            DrawOrdinateFrames();
            DrawGraph();
        }

        public void DrawGraph()
        {
            vertices.ForEach(vertex => vertex.ColorizeVertex());
        }

        private void DrawOrdinateFrames()
        {
            int padding = MainView.YCoordinatePadding;
            for (int length = 0; length < Length; length++)
            {
                DrawLeftYCoodrinate(padding, length);
                DrawRightYCoordinate(padding, length);
            }
        }

        private void DrawAbscissaFrame(int topOffset, FramedAbscissaPosition view)
        {
            Console.SetCursorPosition(0, topOffset);
            Console.Write(GetFramedAbscissa(view));
        }

        private void DrawAbscissaFrames()
        {
            int cursorTop = MainView.HeightOfGraphParametresView;
            DrawAbscissaFrame(cursorTop, FramedAbscissaPosition.FrameUnder);
            cursorTop += Length + MainView.HeightOfAbscissaView;
            DrawAbscissaFrame(cursorTop, FramedAbscissaPosition.FrameOver);
        }

        private void DrawLeftYCoodrinate(int yCoodrinatePadding, int currentLength)
        {
            var cursorTop = MainView.HeightOfAbscissaView + 1 + currentLength;
            Console.SetCursorPosition(0, cursorTop);
            var yCoordinate = currentLength.ToString().PadLeft(yCoodrinatePadding);
            Console.Write(yCoordinate + VerticalFrameComponent);
        }

        private void DrawRightYCoordinate(int yCoodrinatePadding, int currentLength)
        {
            var cursorTop = MainView.HeightOfAbscissaView + 1 + currentLength;
            var cursorLeft = GetCursorLeftPositionCloseToRigthVerticalFrame();
            Console.SetCursorPosition(cursorLeft, cursorTop);
            var yCoordinate = currentLength.ToString().PadRight(yCoodrinatePadding);
            Console.Write(VerticalFrameComponent + yCoordinate);
        }

        private int GetCursorLeftPositionCloseToRigthVerticalFrame()
        {
            int lateralDistanceBetweenVertices
                = Convert.ToInt32(ConfigurationManager.AppSettings["distanceBetweenVertices"]);

            return Width * lateralDistanceBetweenVertices
                   + MainView.WidthOfOrdinateView - 2;
        }

        private string Abscissa
        {
            get
            {
                var abscissa = new StringBuilder(LargeSpace);

                for (var i = 0; i < Width; i++)
                {
                    var offset = !IsOffsetIndex(i) ? BigSpace : Space;
                    abscissa.Append(i + offset);
                }

                abscissa.Append(LargeSpace);
                return abscissa.ToString();
            }
        }

        private string HorizontalFrame
        {
            get
            {
                var frame = new StringBuilder(LargeSpace);

                for (var i = 0; i < Width; i++)
                {
                    if (i == Width - 1)
                    {
                        frame.Append("+");
                    }
                    else
                    {
                        frame.Append(HorizontalFrameComponent);
                    }
                }

                return frame.ToString();
            }
        }

        private string GetFramedAbscissa(FramedAbscissaPosition framedAbscissaView)
        {
            var framedAbscissaComponents = new List<string>()
            {
                Abscissa,
                HorizontalFrame
            };

            var framedAbscissa = new StringBuilder();

            if (framedAbscissaView == FramedAbscissaPosition.FrameOver)
            {
                framedAbscissaComponents.Reverse();
            }

            foreach (var component in framedAbscissaComponents)
            {
                framedAbscissa.AppendLine(component);
            }

            return framedAbscissa.ToString();
        }

        private bool IsOffsetIndex(int currentIndex)
        {
            return currentIndex >= 10;
        }

        private enum FramedAbscissaPosition
        {
            FrameOver,
            FrameUnder
        }

        private enum TableSide
        {
            Right,
            Left
        }

        private readonly List<Vertex> vertices;

        private const string Space = " ";
        private const string BigSpace = "  ";
        private const string LargeSpace = "   ";
        private const string HorizontalFrameComponent = "+--";
        private const string VerticalFrameComponent = "|";
    }
}
