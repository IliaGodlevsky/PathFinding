using GraphLib.Base;
using GraphLib.Interfaces;
using System;
using System.Windows.Input;

namespace WPFVersion.Model
{
    internal sealed class EndPoints : BaseEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown += SetEndPoints;
                vert.MouseUp += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex vert)
            {
                vert.MouseLeftButtonDown -= SetEndPoints;
                vert.MouseUp -= MarkIntermediateToReplace;
            }
        }

        protected override void MarkIntermediateToReplace(object sender, EventArgs e)
        {
            if (e is MouseButtonEventArgs args && args.ChangedButton == MouseButton.Middle)
            {
                base.MarkIntermediateToReplace(sender, e);
            }
        }
    }
}
