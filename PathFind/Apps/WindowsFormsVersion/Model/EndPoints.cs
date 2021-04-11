using GraphLib.Base;
using GraphLib.Interface;
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
            }
        }

        protected override void SetEndPoints(object sender, EventArgs e)
        {
            if ((e as MouseEventArgs)?.Button == MouseButtons.Left)
            {
                base.SetEndPoints(sender, e);
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseClick -= SetEndPoints;
            }
        }
    }
}
