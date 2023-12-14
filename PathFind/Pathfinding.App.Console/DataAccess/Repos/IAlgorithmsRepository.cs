using Pathfinding.App.Console.DataAccess.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DataAccess.Repos
{
    internal interface IAlgorithmsRepository
    {
        IEnumerable<AlgorithmEntity> GetByGraphId(int graphId);

        void AddOne(AlgorithmEntity entity);

        int AddMany(IEnumerable<AlgorithmEntity> entity);

        bool DeleteByGraphId(int graphId);
    }
}
