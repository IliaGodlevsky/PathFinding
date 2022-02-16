using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints, IEndPoints
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vert)
            {
                vert.MouseLeftButtonDown += SetEndPoints;
                vert.MouseUp += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vert)
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
