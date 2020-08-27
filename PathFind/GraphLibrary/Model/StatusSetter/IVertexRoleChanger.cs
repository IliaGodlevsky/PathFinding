using System;

namespace GraphLibrary.StatusSetter
{
    public interface IVertexStatusSetter
    {
        void SetStartVertex(object sender, EventArgs e);
        void SetDestinationVertex(object sender, EventArgs e);
        void ReversePolarity(object sender, EventArgs e);
        void ChangeVertexValue(object sender, EventArgs e);
    }
}
