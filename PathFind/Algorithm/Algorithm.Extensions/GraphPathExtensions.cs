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
        /// <summary>
        /// Highlights all vertices that implement an <see cref="IVisualizable"/> interface 
        /// in <paramref name="self"/>, except <paramref name="endPoints"/>, as path
        /// </summary>
        /// <param name="self"></param>
        /// <param name="endPoints"></param>
        /// <returns>The same instance of <see cref="IGraphPath"/></returns>
        /// <remarks>Vertex should implement an <see cref="IVisualizable"/> interface
        /// to be highlighted as path</remarks>
        public static IGraphPath Highlight(this IGraphPath self, IEndPoints endPoints)
        {
            self.Path
                .Without(endPoints)
                .OfType<IVisualizable>()
                .Reverse()
                .ForEach(vertex => vertex.VisualizeAsPath());
            return self;
        }

        public static async Task<IGraphPath> HighlightAsync(this IGraphPath self, IEndPoints endPoints)
        {
            return await Task.Run(() => self.Highlight(endPoints)).ConfigureAwait(false);
        }
    }
}