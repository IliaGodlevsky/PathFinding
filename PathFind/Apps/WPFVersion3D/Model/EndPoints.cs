using GraphLib.Base.EndPoints;
using GraphLib.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            (vertex as Vertex3D).MouseLeftButtonDown += SetEndPoints;
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            (vertex as Vertex3D).MouseLeftButtonDown -= SetEndPoints;
        }
    }
}
