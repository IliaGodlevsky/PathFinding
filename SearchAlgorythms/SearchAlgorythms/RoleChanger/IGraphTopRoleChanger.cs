using SearchAlgorythms.Top;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorythms.RoleChanger
{
    public interface IGraphTopRoleChanger
    {
        void MakeObstacle(ref IGraphTop top);
        void MakeTop(ref IGraphTop top);
        void SetStartPoint(object sender, EventArgs e);
        void SetDestinationPoint(object sender, EventArgs e);
        void Reverse(ref IGraphTop top);
    }
}
