using GraphLibrary.GraphField;
using GraphLibrary.Graphs.Interface;

namespace GraphLibrary.GraphFieldCreating
{
    public abstract class GraphFieldFactory
    {
        protected abstract IGraphField CreateField();
        public virtual IGraphField CreateGraphField(IGraph graph)
        {
            var graphField = CreateField();
            for (int i = 0; i < graph.Width; i++)
                for (int j = 0; j < graph.Height; j++)
                    graphField?.Add(graph[i, j], i, j);
            return graphField;
        }
    }
}
