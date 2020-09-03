using ConsoleVersion.StatusSetter;
using GraphLibrary.Extensions;
using GraphLibrary.Collection;
using GraphLibrary.Model;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;

namespace ConsoleVersion.Model
{
    internal class ConsoleVersionVertexEventSetter : AbstractVertexEventSetter
    {
        protected override void ChargeVertex(IVertex vertex, IVertexEventHolder changer)
        {
            
        }

        protected override IVertexEventHolder GetStatusSetter(GraphLibrary.Collection.Graph graph)
        {
            return new ConsoleVertexStatusSetter(graph);
        }

        protected override void RefreshVertex(IVertex vertex, IVertexEventHolder changer)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
