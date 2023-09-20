using System;

namespace Pathfinding.App.Console.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class OptionalAttribute : Attribute
    {
        public string Name { get; }

        public OptionalAttribute(string name)
        {
            Name = name;
        }
    }
}