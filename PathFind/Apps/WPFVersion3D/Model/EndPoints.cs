using GraphLib.Base.EndPoints;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints<Vertex3D>
    {
        protected override void SubscribeVertex(Vertex3D vertex)
        {
            vertex.MouseLeftButtonDown += SetEndPoints;
            vertex.MouseUp += MarkIntermediateToReplace;
        }

        protected override void UnsubscribeVertex(Vertex3D vertex)
        {
            vertex.MouseLeftButtonDown -= SetEndPoints;
            vertex.MouseUp -= MarkIntermediateToReplace;
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
