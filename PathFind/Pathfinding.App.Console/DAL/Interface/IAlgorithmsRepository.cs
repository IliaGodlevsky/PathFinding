using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IAlgorithmsRepository
    {
        IEnumerable<AlgorithmEntity> GetByGraphId(int graphId);

        void AddOne(AlgorithmEntity entity);

        int AddMany(IEnumerable<AlgorithmEntity> entity);

        bool DeleteByGraphId(int graphId);
    }
}
