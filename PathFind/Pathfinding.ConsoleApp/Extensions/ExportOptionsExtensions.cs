using Pathfinding.ConsoleApp.Model;
using Pathfinding.ConsoleApp.Resources;

namespace Pathfinding.ConsoleApp.Extensions
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
