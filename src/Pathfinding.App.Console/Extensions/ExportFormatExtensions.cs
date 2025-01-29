using Pathfinding.App.Console.Model;
using System.ComponentModel;

namespace Pathfinding.App.Console.Extensions
{
    internal static class ExportFormatExtensions
    {
        public static string ToExtensionRepresentation(this StreamFormat exportFormat)
        {
            var field = exportFormat.GetType().GetField(exportFormat.ToString());
            var attribute = (DescriptionAttribute)Attribute
                .GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute?.Description ?? DescriptionAttribute.Default.Description;
        }
    }
}
