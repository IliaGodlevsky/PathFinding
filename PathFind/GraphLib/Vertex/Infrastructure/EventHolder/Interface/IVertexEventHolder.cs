using GraphLib.Graphs.Abstractions;
using System;

namespace GraphLib.EventHolder.Interface
{
    public interface IVertexEventHolder
    {
        IGraph Graph { get; set; }

        void ChooseExtremeVertices(object sender, EventArgs e);

        void Reverse(object sender, EventArgs e);

        void ChangeVertexCost(object sender, EventArgs e);

        void SubscribeVertices();
    }
}
