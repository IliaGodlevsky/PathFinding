using Pathfinding.App.Console.Attributes;
using System;

namespace Pathfinding.App.Console.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsInstancePerLifetimeScope(this Type type)
        {
            return Attribute.IsDefined(type, typeof(InstancePerLifetimeScopeAttribute));
        }
    }
}
