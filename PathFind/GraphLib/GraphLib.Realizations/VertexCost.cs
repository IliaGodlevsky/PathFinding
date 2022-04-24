using GraphLib.Base;
using GraphLib.Interfaces;
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

        public override IVertexCost Clone()
        {
            return new VertexCost(CurrentCost);
        }
    }
}