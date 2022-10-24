using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class PathfindingRange : BasePathfindingRange
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick += SetPathfindingRange;
                vert.MouseClick += MarkIntermediateToReplace;
            }
        }

        protected override void SetPathfindingRange(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Left)
            {
                base.SetPathfindingRange(sender, e);
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick -= SetPathfindingRange;
                vert.MouseClick -= MarkIntermediateToReplace;
            }
        }

        protected override void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Middle)
            {
                base.MarkIntermediateToReplace(sender, e);
            }
        }
    }
}
