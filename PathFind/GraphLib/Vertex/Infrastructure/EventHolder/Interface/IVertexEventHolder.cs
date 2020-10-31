using GraphLib.Graphs.Abstractions;
using System;

namespace GraphLib.EventHolder.Interface
{
    public interface IVertexEventHolder
    {
        IGraph Graph { get; set; }
        void ChooseExtremeVertices(object sender, EventArgs e);
        void ReversePolarity(object sender, EventArgs e);
        void ChangeVertexValue(object sender, EventArgs e);
        void SubscribeVertices();
    }
}
