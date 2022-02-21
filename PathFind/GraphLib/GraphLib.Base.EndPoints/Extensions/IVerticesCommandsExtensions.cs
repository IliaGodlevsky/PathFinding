using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

using static System.Activator;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
        public static IReadOnlyCollection<T> GetAttachedCommands<T>(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self
                .GetType()
                .Assembly
                .GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type))
                .Where(type => type.IsAttachedTo(self))
                .Select(type => CreateInstance(type, endPoints))
                .Cast<T>()
                .OrderByOrderAttribute()
                .ToArray();
        }

        public static IReadOnlyCollection<IUndoCommand> GetAttachedUndoCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self.GetAttachedCommands<IUndoCommand>(endPoints);
        }

        public static IReadOnlyCollection<IVertexCommand> GetAttachedExecuteCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self.GetAttachedCommands<IVertexCommand>(endPoints);
        }
    }
}
