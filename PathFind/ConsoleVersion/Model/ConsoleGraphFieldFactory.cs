using ConsoleVersion.View;
using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace ConsoleVersion.Model
{
    internal class ConsoleGraphFieldFactory : GraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new ConsoleGraphField();
        }

        public override IGraphField CreateGraphField(IGraph graph)
        {
            var field = base.CreateGraphField(graph);
            (field as ConsoleGraphField).Width = (graph as Graph2d).Width;
            (field as ConsoleGraphField).Length = (graph as Graph2d).Length;
            return field;
        }
    }
}
