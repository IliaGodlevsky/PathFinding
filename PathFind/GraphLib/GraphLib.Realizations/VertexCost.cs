using GraphLib.Base;
using System.Diagnostics;

namespace GraphLib.Realizations
{
    [DebuggerDisplay("Cost = {CurrentCost}")]
    public sealed class VertexCost : BaseVertexCost
    {
        public VertexCost(int startCost)
            : base(startCost)
        {

        }
    }
}