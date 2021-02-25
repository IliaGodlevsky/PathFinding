using System;

namespace GraphLib.Interface
{
    public interface IVertexEventHolder
    {
        void Reverse(object sender, EventArgs e);

        void ChangeVertexCost(object sender, EventArgs e);

        void SubscribeVertices(IGraph graph);

        void UnsubscribeVertices(IGraph graph);
    }
}
