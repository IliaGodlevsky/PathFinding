using ConsoleVersion.RoleChanger;
using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;

namespace ConsoleVersion.Model
{
    public class ConsoleGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexRoleChanger changer)
        {
            
        }

        protected override void ChargeVertex(IVertex vertex, IVertexRoleChanger changer)
        {
            
        }

        protected override IGraphField GetField()
        {
            return null;
        }

        protected override IVertexRoleChanger GetRoleChanger(AbstractGraph graph)
        {
            return new ConsoleVertexRoleChanger(graph);
        }
    }
}
