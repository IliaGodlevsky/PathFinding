using GraphLib.Base;
using System.Diagnostics;

namespace WPFVersion3D.Model
{
    [DebuggerDisplay("Cost = {CurrentCost}")]
    internal sealed class Vertex3DCost : BaseVertexCost
    {
        public Vertex3DCost(int cost) : base(cost)
        {

        }
    }
}
