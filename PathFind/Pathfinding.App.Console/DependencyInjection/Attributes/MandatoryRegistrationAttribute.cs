using System;

namespace Pathfinding.App.Console.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class MandatoryRegistrationAttribute : Attribute
    {
    }
}
