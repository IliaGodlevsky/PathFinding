using GraphLibrary.Graph;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;

namespace GraphLibrary.Model
{
    public abstract class AbstractGraphFiller
    {
        protected abstract IVertexRoleChanger GetRoleChanger(AbstractGraph graph);
        protected abstract IGraphField GetField();
        protected abstract void ChargeVertex(IVertex vertex, IVertexRoleChanger changer);
        protected abstract void ChargeGraph(AbstractGraph graph, IVertexRoleChanger changer);

        public IGraphField FillGraphField(AbstractGraph graph)
        {
            var changer = GetRoleChanger(graph);
            var graphField = GetField();
            for (int i = 0; i < graph.Width; i++)
            {
                for (int j = 0; j < graph.Height; j++)
                {
                    graphField.Add(graph[i, j], i, j);
                    ChargeVertex(graph[i, j], changer);
                }
            }
            ChargeGraph(graph, changer);
            return graphField;
        }
    }
}
