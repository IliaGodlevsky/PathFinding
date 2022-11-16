using Pathfinding.App.WPF._2D.Attributes;
using System;

namespace Pathfinding.App.WPF._2D.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsAppWindow(this Type window)
        {
            return Attribute.IsDefined(window, typeof(AppWindowAttribute));
        }
    }
}
