using GraphLib.Base.EndPoints;
using System.Diagnostics;

namespace ConsoleVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints<Vertex>
    {
        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.EndPointChosen += SetEndPoints;
            vertex.MarkedToReplaceIntermediate += MarkIntermediateToReplace;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.EndPointChosen -= SetEndPoints;
            vertex.MarkedToReplaceIntermediate -= MarkIntermediateToReplace;
        }
    }
}
