using GraphLib.Base.EndPoints;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("Source - {Source}, Target - {Target}")]
    internal sealed class PathfindingRange : BasePathfindingRange
    {
        protected override void SubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vert)
            {
                vert.MouseLeftButtonDown += SetPathfindingRange;
                vert.MouseUp += MarkIntermediateToReplace;
            }
        }

        protected override void UnsubscribeVertex(IVertex vertex)
        {
            if (vertex is Vertex3D vert)
            {
                vert.MouseLeftButtonDown -= SetPathfindingRange;
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
