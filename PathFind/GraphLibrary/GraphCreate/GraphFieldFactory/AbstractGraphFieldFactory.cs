using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.GraphField;
using GraphLibrary.Graphs;

namespace GraphLibrary.GraphCreate.GraphFieldFactory
{
    public abstract class AbstractGraphFieldFactory
    {
        protected abstract IGraphField GetField();
        public virtual IGraphField GetGraphField(Graph graph)
        {
            var graphField = GetField();
            for (int i = 0; i < graph.Width; i++)
                for (int j = 0; j < graph.Height; j++)
                    graphField?.Add(graph[i, j], i, j);
            return graphField;
        }
    }
}
