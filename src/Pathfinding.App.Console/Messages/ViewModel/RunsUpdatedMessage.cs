using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Messages.ViewModel
{
    internal record class RunsUpdatedMessage(IReadOnlyCollection<RunStatisticsModel> Updated);
}
