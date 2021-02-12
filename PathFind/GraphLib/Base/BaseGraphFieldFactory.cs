using Common.Extensions;
using GraphLib.Interface;

namespace GraphLib.Base
{
    public abstract class BaseGraphFieldFactory
    {
        protected abstract IGraphField GetField();

        public virtual IGraphField CreateGraphField(IGraph graph)
        {
            var graphField = GetField();
            graph.Vertices.ForEach(graphField.Add);

            return graphField;
        }
    }
}
