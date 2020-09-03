using GraphLibrary.Collection;
using GraphLibrary.Model;

namespace GraphLibrary.GraphCreate.GraphFieldFiller
{
    public abstract class AbstractGraphFieldFiller
    {
        protected abstract IGraphField GetField();
        public virtual IGraphField FileGraphField(Graph graph)
        {
            var graphField = GetField();
            for (int i = 0; i < graph.Width; i++)           
                for (int j = 0; j < graph.Height; j++)               
                    graphField?.Add(graph[i, j], i, j);
            return graphField;
        }
    }
}
