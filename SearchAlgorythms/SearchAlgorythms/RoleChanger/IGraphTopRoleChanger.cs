using SearchAlgorythms.Top;
using System;

namespace SearchAlgorythms.RoleChanger
{
    public interface IGraphTopRoleChanger
    {
        void SetStartPoint(object sender, EventArgs e);
        void SetDestinationPoint(object sender, EventArgs e);
        void ReversePolarity(object sender, EventArgs e);
    }
}
