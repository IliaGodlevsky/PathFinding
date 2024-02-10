using Pathfinding.App.Console.DAL.Models.Entities;
using System.Collections.Generic;

namespace Pathfinding.App.Console.DAL.Interface
{
    internal interface IAlgorithmsRepository
    {
        IEnumerable<AlgorithmEntity> GetAll();
    }
}
