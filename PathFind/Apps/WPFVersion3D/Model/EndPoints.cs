using GraphLib.Base;
using GraphLib.Interfaces;

namespace WPFVersion3D.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseLeftButtonDown += SetEndPoints;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vertex3D)
            {
                vertex3D.MouseLeftButtonDown -= SetEndPoints;
            }
        }
    }
}
