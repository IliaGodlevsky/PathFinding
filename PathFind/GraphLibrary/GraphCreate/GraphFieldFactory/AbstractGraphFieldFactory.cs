using GraphLibrary.GraphField;
using GraphLibrary.Graphs.Interface;

namespace GraphLibrary.GraphCreate.GraphFieldFactory
{
    public abstract class AbstractGraphFieldFactory
    {
        protected abstract IGraphField GetField();
        public virtual IGraphField GetGraphField(IGraph graph)
        {
            var graphField = GetField();
            for (int i = 0; i < graph.Width; i++)
                for (int j = 0; j < graph.Height; j++)
                    graphField?.Add(graph[i, j], i, j);
            return graphField;
        }
    }
}
