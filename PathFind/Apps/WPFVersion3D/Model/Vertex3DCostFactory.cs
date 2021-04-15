using GraphLib.Interfaces;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DCostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new Vertex3DCost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new Vertex3DCost(cost);
        }
    }
}
