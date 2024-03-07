using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface ISubAlgorithmRepository
    {
        IEnumerable<SubAlgorithmEntity> GetByAlgorithmRunId(int runId);

        IEnumerable<SubAlgorithmEntity> Insert(IEnumerable<SubAlgorithmEntity> entities);
    }
}
