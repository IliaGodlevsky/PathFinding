using GraphLibrary.GraphField;
using GraphLibrary.Graphs.Interface;

namespace GraphLibrary.GraphFieldCreating
{
    public abstract class GraphFieldFactory
    {
        protected abstract IGraphField GetField();
        public virtual IGraphField CreateGraphField(IGraph graph)
        {
            var graphField = GetField();
            foreach (var vertex in graph)
                graphField?.Add(vertex);
            return graphField;
        }
    }
}
