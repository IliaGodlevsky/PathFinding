using Algorithm.Interfaces;
using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class GraphPathExtensions
    {
        public static IGraphPath Highlight(this IGraphPath self)
        {
            self.Path
                .Select(vertex => vertex.AsVisualizable())
                .Where(vertex => !vertex.IsVisualizedAsEndPoint)
                .Reverse()
                .ForEach(vertex => vertex.VisualizeAsPath());
            return self;
        }

        public static async Task<IGraphPath> HighlightAsync(this IGraphPath self)
        {
            return await Task.Run(() => self.Highlight()).ConfigureAwait(false);
        }

        public static string ToStatistics(this IGraphPath path, Stopwatch timer, int visited, string algorithm, string format)
        {
            string pathfindingInfo = string.Format(format, path.Length, path.Cost, visited);
            return string.Join("\t", algorithm, timer.ToFormattedString(), pathfindingInfo);
        }
    }
}