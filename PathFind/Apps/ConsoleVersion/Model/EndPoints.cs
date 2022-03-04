using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using System.Diagnostics;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.EndPointChosen += SetEndPoints;
                vert.MarkedToReplaceIntermediate += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.EndPointChosen -= SetEndPoints;
                vert.MarkedToReplaceIntermediate -= MarkIntermediateToReplace;
            }
        }
    }
}
