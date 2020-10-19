using ConsoleVersion.View;
using GraphLibrary.GraphField;
using GraphLibrary.GraphFieldCreating;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;

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
            (field as ConsoleGraphField).Width = (graph as Graph).Width;
            (field as ConsoleGraphField).Length = (graph as Graph).Length;
            return field;
        }
    }
}
