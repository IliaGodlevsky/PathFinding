namespace Pathfinding.AlgorithmLib.Core.Interface.Extensions
{
    public static class IGraphPathExtensions
    {
        public static bool IsEmpty(this IGraphPath path)
        {
            return path.Count == 0;
        }
    }
}
