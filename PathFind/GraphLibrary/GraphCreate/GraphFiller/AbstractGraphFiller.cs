using GraphLibrary.Graph;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;

namespace GraphLibrary.Model
{
    public abstract class AbstractGraphFiller
    {
        protected abstract IVertexStatusSetter GetStatusSetter(AbstractGraph graph);
        protected abstract IGraphField GetField();
        protected abstract void ChargeVertex(IVertex vertex, IVertexStatusSetter changer);
        protected abstract void ChargeGraph(AbstractGraph graph, IVertexStatusSetter changer);

        public IGraphField FillGraphField(AbstractGraph graph)
        {
            var changer = GetStatusSetter(graph);
            var graphField = GetField();

            for (int i = 0; i < graph.Width; i++)
            {
                for (int j = 0; j < graph.Height; j++)
                {
                    graphField?.Add(graph[i, j], i, j);
                    ChargeVertex(graph[i, j], changer);
                }
            }

            ChargeGraph(graph, changer);
            return graphField;
        }
    }
}
