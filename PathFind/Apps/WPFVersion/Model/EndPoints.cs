using GraphLib.Base;
using GraphLib.Interfaces;

namespace WPFVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown += SetEndPoints;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown -= SetEndPoints;
            }
        }
    }
}
