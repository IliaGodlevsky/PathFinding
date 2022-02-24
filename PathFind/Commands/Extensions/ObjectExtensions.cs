using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Commands.Extensions
{
    public static class ObjectExtensions
    {
        public static IReadOnlyCollection<T> GetAttachedCommands<T>(this object self, params object[] ctorParams)
        {
            return self
                .GetType()
                .Assembly
                .GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type))
                .Where(type => type.IsAttachedTo(self))
                .Select(type => Activator.CreateInstance(type, ctorParams))
                .Cast<T>()
                .OrderByOrderAttribute()
                .ToArray();
        }
    }
}
