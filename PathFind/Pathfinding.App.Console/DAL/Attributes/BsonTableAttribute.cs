using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class BsonTableAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
