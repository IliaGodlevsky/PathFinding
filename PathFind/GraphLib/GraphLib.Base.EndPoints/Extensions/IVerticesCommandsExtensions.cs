using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using static System.Activator;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
        public static IReadOnlyList<IVertexCommand> GetAttachedCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            var selfType = self.GetType();
            return selfType
                .Assembly
                .GetTypes()
                .Where(type => type.IsAttachedTo(selfType))
                .Select(type => CreateInstance(type, endPoints))
                .Cast<IVertexCommand>()
                .OrderByOrderAttribute()
                .ToArray();
        }

        public static void ExecuteForEach(this IVerticesCommands self, IEnumerable<IVertex> range)
        {
            range.ForEach(self.Execute);
        }
    }
}
