using Commands.Interfaces;
using Common.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Base.EndPoints.Extensions
{
    internal static class IVerticesCommandsExtensions
    {
        public static IReadOnlyCollection<IUndoCommand> GetAttachedUndoCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self.GetAttached<IUndoCommand>(endPoints);
        }

        public static IReadOnlyCollection<IVertexCommand> GetAttachedExecuteCommands(this IVerticesCommands self, BaseEndPoints endPoints)
        {
            return self.GetAttached<IVertexCommand>(endPoints);
        }
    }
}
