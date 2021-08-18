using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithm.Extensions
{
    public static class GraphPathExtensions
    {
        public static void Highlight(this IGraphPath self, IIntermediateEndPoints endpoints)
        {
            self.Path
                .Except(endpoints.Source, endpoints.Target)
                .Except(endpoints.IntermediateVertices)
                .Reverse()
                .OfType<IVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsPath())
                .OfType<IVertex>()
                .Where(v => !self.Path.IsSingle(i => i.Equals(v)))
                .OfType<IVisualizable>()
                .ForEach(vertex => vertex.VisualizeAsPath());
        }

        public static async Task HighlightAsync(this IGraphPath self, IIntermediateEndPoints endPoints)
        {
            await Task.Run(() => self.Highlight(endPoints)).ConfigureAwait(false);
        }
    }
}