using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class ExportOptionsExtensions
    {
        public static string ToStringRepresentation(this ExportOptions options)
        {
            return options switch
            {
                ExportOptions.GraphOnly => "Graph only",
                ExportOptions.WithRange => "With range",
                ExportOptions.WithRuns => "With runs",
                _ => ""
            };
        }
    }
}
