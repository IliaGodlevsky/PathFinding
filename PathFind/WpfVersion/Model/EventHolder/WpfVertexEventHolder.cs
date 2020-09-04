using GraphLibrary.Extensions;
using GraphLibrary.VertexEventHolder;
using GraphLibrary.Vertex;
using System;
using System.Windows.Input;
using WpfVersion.Model.Vertex;

namespace WpfVersion.Model.EventHolder
{
    internal class WpfVertexEventHolder : AbstractVertexEventHolder
    {
        protected override int GetWheelDelta(EventArgs e)
        {
            return (e as MouseWheelEventArgs).Delta;
        }

        public override void SetDestinationVertex(object sender, EventArgs e)
        {
            base.SetDestinationVertex(sender, e);
            if ((sender as IVertex).IsIsolated())
                return;
            foreach (WpfVertex vertex in Graph)
                vertex.MouseLeftButtonDown -= SetDestinationVertex;
        }

        public override void SetStartVertex(object sender, EventArgs e)
        {
            base.SetStartVertex(sender, e);
            if ((sender as IVertex).IsIsolated())
                return;
            foreach (WpfVertex vertex in Graph)
            {
                vertex.MouseLeftButtonDown -= SetStartVertex;
                vertex.MouseLeftButtonDown += SetDestinationVertex;
            }
        }

        protected override void ChargeVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
                (vertex as WpfVertex).MouseLeftButtonDown += SetStartVertex;
            (vertex as WpfVertex).MouseRightButtonDown += ReversePolarity;
            (vertex as WpfVertex).MouseWheel += ChangeVertexValue;
        }

        protected override void RefreshVertex(IVertex vertex)
        {
            if (!vertex.IsObstacle)
            {
                vertex.SetToDefault();
                (vertex as WpfVertex).MouseLeftButtonDown -= SetStartVertex;
                (vertex as WpfVertex).MouseLeftButtonDown -= SetDestinationVertex;
                (vertex as WpfVertex).MouseLeftButtonDown += SetStartVertex;
            }
        }
    }
}
