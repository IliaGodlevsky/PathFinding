using GraphLibrary.Extensions;
using GraphLibrary.Model;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using WpfVersion.Model.StatusSetter;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    internal class WpfVertexEventSetter : AbstractVertexEventSetter
    {        
        protected override void ChargeVertex(IVertex vertex, IVertexEventHolder changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WpfVertex).MouseLeftButtonDown += changer.SetStartVertex;            
            (vertex as WpfVertex).MouseRightButtonDown += changer.ReversePolarity;
            (vertex as WpfVertex).MouseWheel += changer.ChangeVertexValue;
        }

        protected override IVertexEventHolder GetStatusSetter(GraphLibrary.Collection.Graph graph)
        {
            return new WpfVertexStatusSetter(graph);
        }

        protected override void RefreshVertex(IVertex vertex, IVertexEventHolder changer)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
                (vertex as WpfVertex).MouseLeftButtonDown -= changer.SetStartVertex;
                (vertex as WpfVertex).MouseLeftButtonDown -= changer.SetDestinationVertex;
                (vertex as WpfVertex).MouseLeftButtonDown += changer.SetStartVertex;
            }
        }
    }
}
