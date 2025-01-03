using Pathfinding.ConsoleApp.Model;
using Pathfinding.Shared.Extensions;
using System.ComponentModel;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class ExportFormatExtensions
    {
        public static string ToExtensionRepresentation(this StreamFormat exportFormat)
        {
            return exportFormat.GetAttributeOrDefault<DescriptionAttribute>().Description;
        }
    }
}
