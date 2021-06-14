using ConsoleVersion.View;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;

namespace ConsoleVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory
    {
        /// <summary>
        /// Creates graph field from <paramref name="graph"/>
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>Graph field</returns>
        public IGraphField CreateGraphField(IGraph graph)
        {
            if (graph is Graph2D graph2D)
            {
                var graphField = new GraphField(graph2D.Width, graph2D.Length);
                graph.ForEach(graphField.Add);

                return graphField;
            }

            throw new ArgumentException(nameof(graph));
        }
    }
}
