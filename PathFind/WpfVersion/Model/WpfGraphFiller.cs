using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.RoleChanger;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    public class WpfGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexRoleChanger changer)
        {
            (graph as WpfGraph).SetStart += changer.SetStartPoint;
            (graph as WpfGraph).SetEnd += changer.SetDestinationPoint;
        }

        protected override void ChargeVertex(IVertex vertex, IVertexRoleChanger changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WpfVertex).MouseLeftButtonDown += changer.SetStartPoint;
            (vertex as WpfVertex).MouseRightButtonDown += changer.ReversePolarity;
        }

        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }

        protected override IVertexRoleChanger GetRoleChanger(AbstractGraph graph)
        {
            return new WpfRoleChanger(graph);
        }
    }
}
