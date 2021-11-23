using System;
using WindowsFormsVersion.Attributes;

namespace WindowsFormsVersion.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAppWindow(this Type window)
        {
            return Attribute.IsDefined(window, typeof(AppWindowAttribute));
        }
    }
}
