using Common.Extensions;
using GraphLib.Base;
using GraphLib.Graphs;
using GraphLib.Interface;

namespace WPFVersion3D.Model
{
    public class GraphField3DFactory : BaseGraphFieldFactory
    {
        public override IGraphField CreateGraphField(IGraph graph)
        {
            var graph3D = graph as Graph3D;

            var field = GetField(graph3D.Width, graph3D.Length, graph3D.Height);
            graph.Vertices.ForEach(field.Add);

            return field;
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
