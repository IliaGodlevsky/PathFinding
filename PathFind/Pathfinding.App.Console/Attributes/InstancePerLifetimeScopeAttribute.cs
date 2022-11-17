using System;

namespace Pathfinding.App.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class InstancePerLifetimeScopeAttribute : Attribute
    {
    }
}
