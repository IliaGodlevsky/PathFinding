using System;

namespace Pathfinding.App.Console.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class SettingsPropertyAttribute : Attribute
    {
        public string SettingsProperty { get; }

        public SettingsPropertyAttribute(string property)
        {
            SettingsProperty = property;
        }
    }
}