using ConsoleVersion.View;
using GraphLib.GraphField;
using GraphLib.GraphFieldCreating;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;

namespace ConsoleVersion.Model
{
    internal class GraphFieldFactory : BaseGraphFieldFactory
    {
        protected override IGraphField GetField()
        {
            return new ConsoleGraphField();
        }

        /// <summary>
        /// Creates graph field from <paramref name="graph"/>
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>Graph field</returns>
        public override IGraphField CreateGraphField(IGraph graph)
        {
            var field = base.CreateGraphField(graph) as ConsoleGraphField;
            var graph2d = graph as Graph2D;

            field.Width = graph2d.Width;
            field.Length = graph2d.Length;

            return field;
        }
    }
}
