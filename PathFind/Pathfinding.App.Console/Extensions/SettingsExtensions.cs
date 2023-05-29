using System.Configuration;

namespace Pathfinding.App.Console.Extensions
{
    internal static class SettingsExtensions
    {
        public static object GetValueOrDefault(this SettingsBase keys, string sourceName)
        {
            return keys.PropertyValues[sourceName].PropertyValue;
        }
    }
}
