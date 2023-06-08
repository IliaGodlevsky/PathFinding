using Pathfinding.App.Console.Dto;
using Pathfinding.App.Console.Repositories;
using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IUnitOfWork : IDisposable
    {
        IRepository<AlgorithmDto> AlgorithmRepository { get; }

        IRepository<GraphDto> GraphRepository { get; }

        IRepository<VerticesDto> VisitedRepository { get; }

        IRepository<VerticesDto> ObstaclesRepository { get; }

        IRepository<VerticesDto> PathfindingRangesRepository { get; }

        IRepository<VerticesDto> PathfindingRangeRepository { get; }

        IRepository<VerticesDto> PathRepository { get; }
    }
}
