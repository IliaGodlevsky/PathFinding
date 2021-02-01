using Common.Extensions;
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
            graph.ForEach(graphField.Add);

            return graphField;
        }
    }
}
