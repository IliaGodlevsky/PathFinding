using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class RunCreatedMessaged(IReadOnlyCollection<RunStatisticsModel> Models);
}
