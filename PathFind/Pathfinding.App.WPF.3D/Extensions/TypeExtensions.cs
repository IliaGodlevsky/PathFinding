using Pathfinding.App.WPF._3D.Attributes;
using System;

namespace Pathfinding.App.WPF._3D.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAppWindow(this Type window)
        {
            return Attribute.IsDefined(window, typeof(AppWindowAttribute));
        }
    }
}
