using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace WPFVersion3D.Model
{
    public class WpfGraphField3DFactory : GraphFieldFactory
    {
        public override IGraphField CreateGraphField(IGraph graph)
        {
            var graph3D = graph as Graph3D;

            int width = graph3D.Width;
            int length = graph3D.Length;
            int height = graph3D.Height;

            var field = GetField(width, length, height);

            foreach (var vertex in graph)
            {
                field.Add(vertex);
            }

            return field;
        }

        protected override IGraphField GetField()
        {
            return new WpfGraphField3D();
        }

        private WpfGraphField3D GetField(int width, int length, int height)
        {
            return new WpfGraphField3D(width, length, height);
        }
    }
}
