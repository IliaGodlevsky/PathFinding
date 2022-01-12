using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.FramedAxes;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ValueRange;
using ValueRange.Extensions;

namespace ConsoleVersion.Model
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
            countRange = new InclusiveValueRange<int>(width * length + 3);
        }

        public void Add(IVertex vertex)
        {
            if (countRange.Contains(uiElements.Count))
            {
                if (vertex is Vertex vertex2D)
                {
                    uiElements.Add(vertex2D);
                }
            }
        }

        public void Display()
        {
            uiElements.ForEach(element => element.Display());
        }

        private readonly InclusiveValueRange<int> countRange;
        private readonly Collection<IDisplayable> uiElements;
    }
}