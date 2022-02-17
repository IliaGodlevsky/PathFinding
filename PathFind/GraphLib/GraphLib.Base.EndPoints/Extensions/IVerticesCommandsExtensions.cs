using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

using static System.Activator;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
        public static IReadOnlyList<IVertexCommand> GetAttachedCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self
                .GetType()
                .Assembly
                .GetTypes()
                .Where(type => type.IsAttachedTo(self))
                .Select(type => CreateInstance(type, endPoints))
                .Cast<IVertexCommand>()
                .OrderByOrderAttribute()
                .ToArray()
                .ToReadOnlyCollection();
        }
    }
}
