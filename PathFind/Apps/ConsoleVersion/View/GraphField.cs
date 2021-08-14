using Common.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.View.FramedAxes;
using ConsoleVersion.View.Interface;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace ConsoleVersion.View
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        public GraphField(int width, int length)
        {
            elements = new List<IDisplayable>
            {
                new FramedOverAbscissa(width, length),
                new FramedUnderAbscissa(width),
                new FramedToRightOrdinate(width, length),
                new FramedToLeftOrdinate(length)
            };
        }

        public void Add(IVertex vertex)
        {
            if (vertex is Vertex vertex2D)
            {
                elements.Add(vertex2D);
            }
        }

        public void Display()
        {
            elements.ForEach(element => element.Display());
        }

        private readonly IList<IDisplayable> elements;
    }
}