﻿using Pathfinding.Domain.Core;

namespace Pathfinding.ConsoleApp.Extensions
{
    internal static class RunStatusesExtensions
    {
        public static string ToStringRepresentation(this RunStatuses statuses)
        {
            return statuses switch
            {
                RunStatuses.Success => "Success",
                RunStatuses.Failure => "Failure",
                _ => "",
            };
        }
    }
}
