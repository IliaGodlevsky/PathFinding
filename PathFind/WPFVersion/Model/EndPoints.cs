using GraphLib.Base;
using GraphLib.Interface;

namespace WPFVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            (vertex as Vertex).MouseLeftButtonDown += SetEndPoints;
        }
    }
}
