using System;

namespace Pathfinding.App.Console.Model.Notes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class DisplayNameSourceAttribute : Attribute
    {
        public string ResourceName { get; }

        public DisplayNameSourceAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}
