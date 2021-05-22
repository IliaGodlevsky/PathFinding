using Common.Extensions;
using ConsoleVersion.Model;
using GraphLib.Interfaces;
using GraphLib.Realizations.Coordinates;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;

namespace ConsoleVersion.View
{
    internal sealed class GraphField : IGraphField
    {
        public int Width { get; set; }

        public int Length { get; set; }

        public IReadOnlyCollection<IVertex> Vertices => vertices;

        public GraphField()
        {
            vertices = new List<Vertex>();
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
            return Width * Constants.LateralDistanceBetweenVertices
                   + MainView.WidthOfOrdinateView - 1;
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
                    frame.Append(HorizontalFrameComponent);
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

        private readonly List<Vertex> vertices;

        private const string Space = " ";
        private const string BigSpace = "  ";
        private const string LargeSpace = "   ";
        private const string HorizontalFrameComponent = "+--";
        private const string VerticalFrameComponent = "|";
    }
}
