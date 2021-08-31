using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        private static readonly Color ToReplaceMarkColor; 

        static EndPoints()
        {
            ToReplaceMarkColor = Color.FromArgb(alpha: 185, red: 255, green: 140, blue: 0);
        }

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
                if (sender is Vertex vertex && markedToReplaceIntermediates.Contains(vertex))
                {
                    vertex.BackColor = ToReplaceMarkColor;
                }
            }            
        }
    }
}
