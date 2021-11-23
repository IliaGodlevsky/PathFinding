using System;
using WPFVersion.Attributes;

namespace WPFVersion.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAppWindow(this Type window)
        {
            return Attribute.IsDefined(window, typeof(AppWindowAttribute));
        }
    }
}
