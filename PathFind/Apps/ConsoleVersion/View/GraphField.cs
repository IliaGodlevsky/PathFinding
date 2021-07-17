using Common.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.View.FramedAxes;
using ConsoleVersion.View.Interface;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace ConsoleVersion.View
{
    internal sealed class GraphField : IGraphField
    {
        public IReadOnlyCollection<IVertex> Vertices => vertices;

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex vertex2D)
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
            axesView.ForEach(view => view.Show());
            vertices.ForEach(vertex => vertex.ColorizeVertex());
        }

        public GraphField(int width, int length)
        {
            vertices = new List<Vertex>();
            int abscissaCursorTop = MainView.HeightOfGraphParametresView;
            int ordinateCursorTop = MainView.HeightOfAbscissaView + 1;
            axesView = new AxisView[]
            {
                new AxisView(new FramedOverAbscissa(width, length), abscissaCursorTop),
                new AxisView(new FramedUnderAbscissa(width), abscissaCursorTop),
                new AxisView(new FramedToRightOrdinate(width, length), ordinateCursorTop),
                new AxisView(new FramedToLeftOrdinate(length), ordinateCursorTop)
            };
        }

        private readonly List<Vertex> vertices;
        private readonly IAxisView[] axesView;
    }
}