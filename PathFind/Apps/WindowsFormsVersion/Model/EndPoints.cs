using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick += SetEndPoints;
                vert.MouseClick += MarkIntermediateToReplace;
            }
        }

        protected override void SetEndPoints(object sender, EventArgs e)
        {
            if (e is MouseEventArgs args && args.Button == MouseButtons.Left)
            {
                base.SetEndPoints(sender, e);
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick -= SetEndPoints;
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
