using Pathfinding.Service.Interface.Models.Read;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.ConsoleApp.Messages.ViewModel
{
    internal sealed record class RunCreatedMessaged(AlgorithmRunHistoryModel Model,
        IReadOnlyCollection<SubAlgorithmModel> SubAlgorithms);
}
