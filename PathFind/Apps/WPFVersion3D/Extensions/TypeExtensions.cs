using System;
using WPFVersion3D.Attributes;

namespace WPFVersion3D.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAppWindow(this Type window)
        {
            return Attribute.IsDefined(window, typeof(AppWindowAttribute));
        }
    }
}
