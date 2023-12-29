using System;

namespace Pathfinding.App.Console.DataAccess
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class BsonTableAttribute : Attribute
    {
        public string Name { get; }

        public BsonTableAttribute(string name)
        {
            Name = name;
        }
    }
}
