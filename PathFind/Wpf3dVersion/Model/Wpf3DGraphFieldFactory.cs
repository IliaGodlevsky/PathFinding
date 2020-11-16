using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace Wpf3dVersion.Model
{
    public class Wpf3DGraphFieldFactory : GraphFieldFactory
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
            return new Wpf3dGraphField();
        }

        private Wpf3dGraphField GetField(int width, int length, int height)
        {
            return new Wpf3dGraphField(width, length, height);
        }
    }
}
