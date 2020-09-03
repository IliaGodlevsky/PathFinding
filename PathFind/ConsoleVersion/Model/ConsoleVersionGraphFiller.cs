using ConsoleVersion.StatusSetter;
using GraphLibrary.Extensions;
using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;

namespace ConsoleVersion.Model
{
    internal class ConsoleVersionGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            
        }

        protected override IVertexStatusSetter GetStatusSetter(AbstractGraph graph)
        {
            return new ConsoleVertexStatusSetter(graph);
        }

        protected override void RefreshVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            if (!vertex.IsObstacle)
                vertex.SetToDefault();
        }
    }
}
