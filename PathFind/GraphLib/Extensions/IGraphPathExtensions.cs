using Common.Extensions;
using GraphLib.Interface;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IGraphPathExtensions
    {
        public static void HighlightPath(this IGraphPath self, IEndPoints endpoints)
        {
            self.Path.Where(vertex => !endpoints.IsEndPoint(vertex) && vertex is IMarkableVertex)
                .ForEach(vertex => (vertex as IMarkableVertex).MarkAsPath());
        }

        public static int GetPathCost(this IGraphPath self)
        {
            return self.Path.Sum(vertex => vertex.Cost.CurrentCost);
            
        }

        public static int GetPathLength(this IGraphPath self)
        {
            return self.Path.Count();
        }

        public static bool IsExtracted(this IGraphPath self)
        {
            return self.Path.Any();
        }
    }
}
