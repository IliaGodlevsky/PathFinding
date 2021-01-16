using GraphLib.GraphField;
using GraphLib.Graphs.Abstractions;

namespace GraphLib.GraphFieldCreating
{
    public abstract class BaseGraphFieldFactory
    {
        protected abstract IGraphField GetField();

        public virtual IGraphField CreateGraphField(IGraph graph)
        {
            var graphField = GetField();

            foreach (var vertex in graph)
            {
                graphField.Add(vertex);
            }

            return graphField;
        }
    }
}
