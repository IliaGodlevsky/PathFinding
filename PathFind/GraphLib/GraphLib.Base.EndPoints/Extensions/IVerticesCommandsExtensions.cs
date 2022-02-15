using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
        public static IReadOnlyList<IVertexCommand> GetAttachedCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self.GetType().Assembly
                .GetTypes()
                .Where(type => type.IsAttachedCommand(self.GetType()))
                .Select(type => Activator.CreateInstance(type, endPoints))
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
