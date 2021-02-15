using GraphLib.Base;
using GraphLib.Interface;

namespace ConsoleVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            var vert = vertex as Vertex;
            vert.OnExtremeVertexChosen += SetEndPoints;
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            var vert = vertex as Vertex;
            vert.OnExtremeVertexChosen -= SetEndPoints;
        }
    }
}
