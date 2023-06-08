using Pathfinding.App.Console.Dto;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.UnitOfWork
{
    internal sealed class MemoryUnitOfWork : IUnitOfWork
    {
        public IRepository<AlgorithmDto> AlgorithmRepository { get; } = new AlgorithmMemoryRepository();

        public IRepository<GraphDto> GraphRepository { get; } = new GraphMemoryRepository();

        public IRepository<VerticesDto> VisitedRepository { get; } = new VerticesMemoryRepository();

        public IRepository<VerticesDto> ObstaclesRepository { get; } = new VerticesMemoryRepository();

        public IRepository<VerticesDto> PathfindingRangesRepository { get; } = new VerticesMemoryRepository();

        public IRepository<VerticesDto> PathfindingRangeRepository { get; } = new VerticesMemoryRepository();

        public IRepository<VerticesDto> PathRepository { get; } = new VerticesMemoryRepository();

        public void Dispose()
        {
            
        }
    }
}
