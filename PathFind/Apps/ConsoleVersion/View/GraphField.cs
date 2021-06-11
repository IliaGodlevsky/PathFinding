using Common.Extensions;
using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal sealed class GraphField : IGraphField
    {
        private int Width { get; }

        private int Length { get; }

        public IReadOnlyCollection<IVertex> Vertices => vertices;

        public GraphField(int width, int length)
            : this()
        {
            Width = width;
            Length = length;
        }

        public GraphField()
        {
            vertices = new List<Vertex>();
            horizontalFrame = new Lazy<string>(GetHorizontalFrame);
            abscissa = new Lazy<string>(GetAbscissa);
            frameOverFramedAbscissa = new Lazy<string>(() => GetFramedAbscissa(FramedAbscissaPosition.FrameOver));
            framedUnderFramedAbscissa = new Lazy<string>(() => GetFramedAbscissa(FramedAbscissaPosition.FrameUnder));
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex vertex2D && vertex.Position is Coordinate2D)
            {
                vertices.Add(vertex2D);
            }
        }

        public void Clear()
        {
            vertices.Clear();
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
            Enumerable.Range(0, Length).ForEach(index =>
            {
                DrawLeftYCoodrinate(padding, index);
                DrawRightYCoordinate(padding, index);
            });
        }

        private void DrawAbscissaFrame(int topOffset, string framedAbscissa)
        {
            Console.SetCursorPosition(0, topOffset);
            Console.Write(framedAbscissa);
        }

        private void DrawAbscissaFrames()
        {
            int cursorTop = MainView.HeightOfGraphParametresView;
            DrawAbscissaFrame(cursorTop, framedUnderFramedAbscissa.Value);
            cursorTop += Length + MainView.HeightOfAbscissaView;
            DrawAbscissaFrame(cursorTop, frameOverFramedAbscissa.Value);
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
            return Width * Constants.LateralDistanceBetweenVertices
                   + MainView.WidthOfOrdinateView - 1;
        }

        private string GetAbscissa()
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

        private string GetHorizontalFrame()
        {
            var frame = new StringBuilder(LargeSpace);

            for (var i = 0; i < Width; i++)
            {
                frame.Append(HorizontalFrameComponent);
            }

            return frame.ToString();
        }

        private string GetFramedAbscissa(FramedAbscissaPosition framedAbscissaView)
        {
            var framedAbscissaComponents = new List<string>()
            {
                abscissa.Value,
                horizontalFrame.Value
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

        private readonly List<Vertex> vertices;

        private readonly Lazy<string> horizontalFrame;
        private readonly Lazy<string> abscissa;
        private readonly Lazy<string> frameOverFramedAbscissa;
        private readonly Lazy<string> framedUnderFramedAbscissa;

        private const string Space = " ";
        private const string BigSpace = "  ";
        private const string LargeSpace = "   ";
        private const string HorizontalFrameComponent = "+--";
        private const string VerticalFrameComponent = "|";
    }
}
