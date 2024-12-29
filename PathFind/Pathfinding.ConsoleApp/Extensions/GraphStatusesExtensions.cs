using Pathfinding.ConsoleApp.Resources;
using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class GraphStatusesExtensions
    {
        public static string ToStringRepresentation(this GraphStatuses status)
        {
            return status switch
            {
                GraphStatuses.Editable => Resource.Editable,
                GraphStatuses.Readonly => Resource.Readonly,
                _ => string.Empty
            };
        }
    }
}
