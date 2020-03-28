using SearchAlgorithms.Top;
using System;

namespace SearchAlgorithms.RoleChanger
{
    public interface IGraphTopRoleChanger
    {
        void MakeObstacle(ref IGraphTop top);
        void MakeTop(ref IGraphTop top);
        void SetStartPoint(object sender, EventArgs e);
        void SetDestinationPoint(object sender, EventArgs e);
        void Reverse(ref IGraphTop top);
        void ReversePolarity(object sender, EventArgs e);
    }
}
