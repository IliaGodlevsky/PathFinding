using Common.Extensions;
using ConsoleVersion.Model;
using ConsoleVersion.View.FramedAxes;
using ConsoleVersion.View.Interface;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConsoleVersion.View
{
    internal sealed class GraphField : IGraphField, IDisplayable
    {
        public GraphField(int width, int length)
        {
            uiElements = new Collection<IDisplayable>
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
                uiElements.Add(vertex2D);
            }
        }

        public void Display()
        {
            uiElements.ForEach(element => element.Display());
        }

        private readonly IList<IDisplayable> uiElements;
    }
}