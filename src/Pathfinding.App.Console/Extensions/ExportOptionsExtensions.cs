using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Resources;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ExportOptionsExtensions
    {
        public static string ToStringRepresentation(this ExportOptions options)
        {
            return options switch
            {
                ExportOptions.GraphOnly => Resource.GraphOnly,
                ExportOptions.WithRange => Resource.WithRange,
                ExportOptions.WithRuns => Resource.WithRuns,
                _ => string.Empty
            };
        }
    }
}
