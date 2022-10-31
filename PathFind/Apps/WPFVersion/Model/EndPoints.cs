using GraphLib.Base.EndPoints;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WPFVersion.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class EndPoints : BaseEndPoints<Vertex>
    {
        protected override void SubscribeVertex(Vertex vertex)
        {
            vertex.MouseLeftButtonDown += SetEndPoints;
            vertex.MouseUp += MarkIntermediateToReplace;
        }

        protected override void UnsubscribeVertex(Vertex vertex)
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
