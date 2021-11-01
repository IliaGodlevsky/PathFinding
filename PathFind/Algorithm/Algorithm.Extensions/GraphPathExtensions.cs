using Algorithm.Interfaces;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class GraphPathExtensions
    {
        public static IGraphPath Highlight(this IGraphPath self, IIntermediateEndPoints endPoints)
        {
            self.Path
                .Without(endPoints)
                .OfType<IVisualizable>()
                .Reverse()
                .ForEach(vertex => vertex.VisualizeAsPath());
            return self;
        }

        public static async Task<IGraphPath> HighlightAsync(this IGraphPath self, IIntermediateEndPoints endPoints)
        {
            return await Task.Run(() => self.Highlight(endPoints)).ConfigureAwait(false);
        }
    }
}