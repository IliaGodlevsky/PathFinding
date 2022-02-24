using Commands.Extensions;
using Commands.Interfaces;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
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
