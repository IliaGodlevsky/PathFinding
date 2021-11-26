using GraphLib.Base;
using GraphLib.Interfaces;
using System.Diagnostics;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints, IEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnEndPointChosen += SetEndPoints;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.OnEndPointChosen -= SetEndPoints;
            }
        }
    }
}
