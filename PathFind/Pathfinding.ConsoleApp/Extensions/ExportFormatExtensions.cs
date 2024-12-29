using Pathfinding.ConsoleApp.Model;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class ExportFormatExtensions
    {
        public static string ToExtensionRepresentation(this StreamFormat exportFormat)
        {
            return exportFormat switch
            {
                StreamFormat.Json => ".json",
                StreamFormat.Binary => ".dat",
                StreamFormat.Xml => ".xml",
                _ => ""
            };
        }
    }
}
