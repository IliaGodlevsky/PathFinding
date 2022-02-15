using Algorithm.Сompanions.Interface;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;

namespace Algorithm.Extensions
{
    public static class IParentVerticesExtensions
    {
        public static IVertex GetParentOrNullVertex(this IParentVertices self, IVertex vertex)
        {
            return self.HasParent(vertex) ? self.GetParent(vertex) : NullVertex.Instance;
        }
    }
}
