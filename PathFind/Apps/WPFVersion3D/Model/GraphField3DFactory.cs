using Common.Extensions;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;
using System;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3DFactory : BaseGraphFieldFactory
    {
        public override IGraphField CreateGraphField(IGraph graph)
        {
            if (graph is Graph3D graph3D)
            {
                var field = GetField(graph3D);
                graph.ForEach(field.Add);

                return field;
            }

            throw new ArgumentException(nameof(graph));
        }

        protected override IGraphField GetField()
        {
            return new GraphField3D(0, 0, 0);
        }

        private GraphField3D GetField(Graph3D graph3D)
        {
            return new GraphField3D(graph3D.Width,
                graph3D.Length, graph3D.Height);
        }
    }
}
