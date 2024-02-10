using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IAlgorithmRunRepository
    {
        AlgorithmRunEntity Insert(AlgorithmRunEntity entity);

        IEnumerable<AlgorithmRunEntity> GetByGraphId(int graphId);

        bool DeleteByGraphId(int graphId);

        int GetCount(int graphId);
    }
}
