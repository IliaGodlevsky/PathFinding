using System;

namespace Pathfinding.App.Console.DAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal sealed class DefaultAttribute(object value)
        : SqliteBuildAttribute($"DEFAULT {value}", 5)
    {
    }
}
