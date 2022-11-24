using Pathfinding.GraphLib.Core.Realizations;
using Shared.Primitives.ValueRange;
using System.Diagnostics;

namespace Pathfinding.App.WPF._3D.Model
{
    [DebuggerDisplay("Cost = {CurrentCost}")]
    internal sealed class Vertex3DCost : VertexCost
    {
        public Vertex3DCost(int cost) : base(cost)
        {

        }

        static Vertex3DCost()
        {
            CostRange = new InclusiveValueRange<int>(4, 1);
        }
    }
}
