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
        public static IGraphPath Highlight(this IGraphPath self, IIntermediateEndPoints endpoints)
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
            return self;
        }

        public static async Task<IGraphPath> HighlightAsync(this IGraphPath self, IIntermediateEndPoints endPoints)
        {
            return await Task.Run(() => self.Highlight(endPoints)).ConfigureAwait(false);
        }
    }
}