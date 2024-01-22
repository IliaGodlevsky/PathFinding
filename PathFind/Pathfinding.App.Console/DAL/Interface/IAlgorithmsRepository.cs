using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IAlgorithmsRepository
    {
        IEnumerable<AlgorithmEntity> GetByGraphId(int graphId);

        AlgorithmEntity Insert(AlgorithmEntity entity);

        IEnumerable<AlgorithmEntity> Insert(IEnumerable<AlgorithmEntity> entity);

        bool DeleteByGraphId(int graphId);
    }
}
