using Common.Extensions;
using GraphLib.Base;
using GraphLib.Interfaces;
using GraphLib.Realizations;
using System;

namespace WPFVersion3D.Model
{
    internal sealed class GraphField3DFactory : BaseGraphFieldFactory
    {
        public override IGraphField CreateGraphField(IGraph graph)
        {
            if (graph is Graph3D graph3D)
            {
                var field = GetField(graph3D.Width, graph3D.Length, graph3D.Height);
                graph.Vertices.ForEach(field.Add);

                return field;
            }

            throw new ArgumentException(nameof(graph));
        }

        protected override IGraphField GetField()
        {
            return new GraphField3D();
        }

        private GraphField3D GetField(int width, int length, int height)
        {
            return new GraphField3D(width, length, height);
        }
    }
}
