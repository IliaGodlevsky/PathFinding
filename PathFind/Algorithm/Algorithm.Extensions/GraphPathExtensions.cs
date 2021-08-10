using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class GraphPathExtensions
    {
        public static void Highlight(this IGraphPath self, IEndPoints endpoints)
        {
            self.Path
                .Except(endpoints.Source, endpoints.Target)
                .OfType<IMarkable>()
                .ForEach(vertex => vertex.MarkAsPath());
        }

        public static async Task HighlightAsync(this IGraphPath self, IEndPoints endPoints)
        {
            await Task.Run(() => self.Highlight(endPoints)).ConfigureAwait(false);
        }
    }
}