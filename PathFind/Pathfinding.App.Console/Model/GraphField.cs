using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model.FramedAxes;
using Pathfinding.GraphLib.Core.Interface;
using Shared.Extensions;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphField : IDisplayable
    {
        public static readonly GraphField Empty 
            = new(Array.Empty<IDisplayable>());

        private IReadOnlyCollection<IDisplayable> Displayables { get; }

        public GraphField(IGraph<Vertex> graph)
            : this(new List<IDisplayable>(graph)
            {
                new FramedOverAbscissa(graph),
                new FramedUnderAbscissa(graph),
                new FramedToRightOrdinate(graph),
                new FramedToLeftOrdinate(graph)
            }.AsReadOnly())
        {
        }

        private GraphField(IReadOnlyCollection<IDisplayable> graph)
        {
            Displayables = graph;
        }

        public void Display()
        {
            Displayables.ForEach(element => element.Display());
        }
    }
}