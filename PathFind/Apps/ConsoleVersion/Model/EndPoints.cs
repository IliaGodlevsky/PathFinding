using GraphLib.Base;
using GraphLib.Interfaces;

namespace ConsoleVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnExtremeVertexChosen += SetEndPoints;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnExtremeVertexChosen -= SetEndPoints;
            }
        }
    }
}
