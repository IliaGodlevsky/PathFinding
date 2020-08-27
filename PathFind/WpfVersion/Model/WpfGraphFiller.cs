using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using WpfVersion.Model.Graph;
using WpfVersion.Model.StatusSetter;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model
{
    public class WpfGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexStatusSetter changer)
        {
            (graph as WpfGraph).SetStart += changer.SetStartVertex;
            (graph as WpfGraph).SetEnd += changer.SetDestinationVertex;
        }

        protected override void ChargeVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WpfVertex).MouseLeftButtonDown += changer.SetStartVertex;
            (vertex as WpfVertex).MouseRightButtonDown += changer.ReversePolarity;
        }

        protected override IGraphField GetField()
        {
            return new WpfGraphField();
        }

        protected override IVertexStatusSetter GetRoleChanger(AbstractGraph graph)
        {
            return new WpfVertexStatusSetter(graph);
        }
    }
}
