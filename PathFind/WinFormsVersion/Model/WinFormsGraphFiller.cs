using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.RoleChanger;
using GraphLibrary.Vertex;
using System.Drawing;
using WinFormsVersion.Graph;
using WinFormsVersion.RoleChanger;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Model
{
    public class WinFormsGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexRoleChanger changer)
        {
            (graph as WinFormsGraph).SetStart += changer.SetStartPoint;
            (graph as WinFormsGraph).SetEnd += changer.SetDestinationPoint;
        }

        protected override void ChargeVertex(IVertex vertex, IVertexRoleChanger changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WinFormsVertex).MouseClick += changer.SetStartPoint;
            (vertex as WinFormsVertex).MouseClick += changer.ReversePolarity;
        }

        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }

        protected override IVertexRoleChanger GetRoleChanger(AbstractGraph graph)
        {
            return new WinFormsVertexRoleChanger(graph);
        }
    }
}
