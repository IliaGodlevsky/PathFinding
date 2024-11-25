using Pathfinding.Domain.Core;
using System;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class GraphStatusesExtensions
    {
        public static string ToStringRepresentation(this GraphStatuses status)
        {
            return status switch
            {
                GraphStatuses.Editable => "Editable",
                GraphStatuses.Readonly => "Readonly",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Invalid GraphStatus")
            };
        }
    }
}
