using ConsoleVersion.StatusSetter;
using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;

namespace ConsoleVersion.Model
{
    public class ConsoleGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexStatusSetter changer)
        {
            
        }

        protected override void ChargeVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            
        }

        protected override IGraphField GetField()
        {
            return null;
        }

        protected override IVertexStatusSetter GetRoleChanger(AbstractGraph graph)
        {
            return new ConsoleVertexStatusSetter(graph);
        }
    }
}
