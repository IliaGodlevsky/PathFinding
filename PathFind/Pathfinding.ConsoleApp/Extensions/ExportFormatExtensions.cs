using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class ExportFormatExtensions
    {
        public static string ToExtensionRepresentation(this ExportFormat exportFormat)
        {
            return exportFormat switch
            {
                ExportFormat.Json => ".json",
                ExportFormat.Binary => ".dat",
                ExportFormat.Xml => ".xml",
                _ => ""
            };
        }
    }
}
