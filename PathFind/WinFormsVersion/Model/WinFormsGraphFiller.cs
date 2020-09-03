using GraphLibrary.Extensions;
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
        protected override void ChargeVertex(IVertex vertex, IVertexStatusSetter changer)
        {
            if (!vertex.IsObstacle)
                (vertex as WinFormsVertex).MouseClick += changer.SetStartVertex;
            (vertex as WinFormsVertex).MouseClick += changer.ReversePolarity;
           (vertex as WinFormsVertex).MouseWheel += changer.ChangeVertexValue;
        }

        protected override IVertexStatusSetter GetStatusSetter(AbstractGraph graph)
        {
            return new WinFormsVertexStatusSetter(graph);
        }

        protected override void RefreshVertex(IVertex vertex, IVertexStatusSetter changer)
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
