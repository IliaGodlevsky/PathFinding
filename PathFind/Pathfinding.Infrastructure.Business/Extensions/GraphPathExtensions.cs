using Pathfinding.Service.Interface;

namespace Pathfinding.Infrastructure.Business.Extensions
{
    public static class GraphPathExtensions
    {
        public static bool IsEmpty(this IGraphPath path)
        {
            return path.Count == 0;
        }
    }
}
