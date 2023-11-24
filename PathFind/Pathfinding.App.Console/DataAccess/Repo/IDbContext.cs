using Pathfinding.App.Console.DataAccess.Entities;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal interface IDbContext
    {
        IRepo<CostRangeEntity> VerticesCosts { get; }

        IRepo<AlgorithmEntity> Algorithms { get; }

        IRepo<GraphEntity> Graphs { get; }

        IRepo<VertexEntity> Vertices { get; }

        IRepo<NeighboursEntity> Neighbours { get; }

        void SaveChanges();
    }
}
