using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Models.Undefined;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class RunCreatedMessaged(RunStatisticsModel Model,
        IReadOnlyCollection<SubAlgorithmModel> SubAlgorithms);
}
