using System;

namespace GraphLib.Interfaces
{
    public interface IVertexEventHolder
    {
        void Reverse(object sender, EventArgs e);

        void ChangeVertexCost(object sender, EventArgs e);

        void SubscribeVertices(IGraph graph);

        void UnsubscribeVertices(IGraph graph);
    }
}
