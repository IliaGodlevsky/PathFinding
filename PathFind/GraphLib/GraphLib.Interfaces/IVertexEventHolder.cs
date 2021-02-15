using System;

namespace GraphLib.Interface
{
    public interface IVertexEventHolder
    {
        IGraph Graph { get; set; }

        void Reverse(object sender, EventArgs e);

        void ChangeVertexCost(object sender, EventArgs e);

        void SubscribeVertices();

        void UnsubscribeVertices();
    }
}
