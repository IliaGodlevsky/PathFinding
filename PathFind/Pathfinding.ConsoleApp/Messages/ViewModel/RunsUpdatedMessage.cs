using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal record class RunsUpdatedMessage(IReadOnlyCollection<RunStatisticsModel> Updated);
}
