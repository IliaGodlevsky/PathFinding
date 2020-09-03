using GraphLibrary.Extensions;
using GraphLibrary.Model;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using WinFormsVersion.StatusSetter;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Model
{
    public class WinFormsVertexEventSetter : AbstractVertexEventSetter
    {
        protected override void ChargeVertex(IVertex vertex, IVertexEventHolder changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WinFormsVertex).MouseClick += changer.SetStartVertex;
            (vertex as WinFormsVertex).MouseClick += changer.ReversePolarity;
           (vertex as WinFormsVertex).MouseWheel += changer.ChangeVertexValue;
        }

        protected override IVertexEventHolder GetStatusSetter(GraphLibrary.Collection.Graph graph)
        {
            return new WinFormsVertexStatusSetter(graph);
        }

        protected override void RefreshVertex(IVertex vertex, IVertexEventHolder changer)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
                (vertex as WinFormsVertex).MouseClick -= changer.SetStartVertex;
                (vertex as WinFormsVertex).MouseClick -= changer.SetDestinationVertex;
                (vertex as WinFormsVertex).MouseClick += changer.SetStartVertex;
            }

        }
    }
}
