using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface ISubAlgorithmRepository
    {
        bool DeleteByAlgorithmId(int algorithmId);

        IEnumerable<SubAlgorithmEntity> GetByAlgorithmId(int algorithmId);

        IEnumerable<SubAlgorithmEntity> Insert(IEnumerable<SubAlgorithmEntity> entities);
    }
}
