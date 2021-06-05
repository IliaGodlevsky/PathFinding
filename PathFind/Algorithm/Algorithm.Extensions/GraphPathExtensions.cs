using Algorithm.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Linq;

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

        public static bool HasPath(this IGraphPath self)
        {
            return self.Path.Any();
        }
    }
}
