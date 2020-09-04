using System;

namespace GraphLibrary.VertexEventHolder
{
    public interface IVertexEventHolder
    {
        void SetStartVertex(object sender, EventArgs e);
        void SetDestinationVertex(object sender, EventArgs e);
        void ReversePolarity(object sender, EventArgs e);
        void ChangeVertexValue(object sender, EventArgs e);
        void ChargeGraph();
        void RefreshGraph();
    }
}
