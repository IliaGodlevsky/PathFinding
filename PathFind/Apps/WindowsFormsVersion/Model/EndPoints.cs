using GraphLib.Base.EndPoints;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints<Vertex>
    {
        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.MouseClick += SetEndPoints;
            vertex.MouseClick += MarkIntermediateToReplace;
        }

        protected override void SetEndPoints(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Left)
            {
                base.SetEndPoints(sender, e);
            }
        }

        protected override void UnsubscribeVertex(Vertex vertex)
        {
            vertex.MouseClick -= SetEndPoints;
            vertex.MouseClick -= MarkIntermediateToReplace;
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
