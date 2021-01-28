using GraphLib.Vertex.Interface;

namespace GraphLib.Test.Extensions
{
    public static class IVertexExtensions
    {
        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            bool hasEqualCost = (int)self.Cost == (int)vertex.Cost;
            bool hasEqualPosition = self.Position.Equals(vertex.Position);
            bool hasEqualState = self.IsObstacle == vertex.IsObstacle;
            return hasEqualCost && hasEqualPosition && hasEqualState;
        }
    }
}
