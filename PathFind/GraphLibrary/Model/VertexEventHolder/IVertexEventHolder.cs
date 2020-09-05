using System;

namespace GraphLibrary.VertexEventHolder
{
    public interface IVertexEventHolder
    {
        void ChooseExtremeVertices(object sender, EventArgs e);       
        void ReversePolarity(object sender, EventArgs e);
        void ChangeVertexValue(object sender, EventArgs e);
        void ChargeGraph();
    }
}
