﻿using Pathfinding.Service.Interface.Models.Undefined;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal record class RunsUpdatedMessage(IReadOnlyCollection<RunStatisticsModel> Updated);
}
