using GraphLibrary.Graphs.Interface;
using System;

namespace GraphLibrary.EventHolder.Interface
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
