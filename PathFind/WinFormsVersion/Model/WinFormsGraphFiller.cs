using GraphLibrary.Graph;
using GraphLibrary.Model;
using GraphLibrary.StatusSetter;
using GraphLibrary.Vertex;
using System.Drawing;
using WinFormsVersion.Graph;
using WinFormsVersion.StatusSetter;
using WinFormsVersion.Vertex;

namespace WinFormsVersion.Model
{
    public class WinFormsGraphFiller : AbstractGraphFiller
    {
        protected override void ChargeGraph(AbstractGraph graph, IVertexStatusSetter changer)
        {
            (graph as WinFormsGraph).SetStart += changer.SetStartVertex;
            (graph as WinFormsGraph).SetEnd += changer.SetDestinationVertex;
        }

        protected override void ChargeVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WinFormsVertex).MouseClick += changer.SetStartVertex;
            (vertex as WinFormsVertex).MouseClick += changer.ReversePolarity;
        }

        protected override IGraphField GetField()
        {
            return new WinFormsGraphField() { Location = new Point(4, 90) };
        }

        protected override IVertexStatusSetter GetRoleChanger(AbstractGraph graph)
        {
            return new WinFormsVertexStatusSetter(graph);
        }
    }
}
