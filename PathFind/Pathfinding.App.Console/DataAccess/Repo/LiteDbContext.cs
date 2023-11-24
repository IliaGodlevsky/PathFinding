using LiteDB;
using Pathfinding.App.Console.DataAccess.Entities;
using System;
using System.Net.Http.Headers;

namespace Pathfinding.App.Console.DataAccess.Repo
{
    internal sealed class LiteDbContext : IDbContext, IDisposable
    {
        private readonly LiteDatabase database;

        public IRepo<AlgorithmEntity> Algorithms { get; }

        public IRepo<GraphEntity> Graphs { get; }

        public IRepo<VertexEntity> Vertices { get; }

        public IRepo<NeighboursEntity> Neighbours { get; }

        public IRepo<CostRangeEntity> VerticesCosts { get; }

        public LiteDbContext(LiteDatabase database)
        {
            this.database = database;
            Algorithms = new LiteDbRepo<AlgorithmEntity>(database, "Algorithms");
            Graphs = new LiteDbRepo<GraphEntity>(database, "Graphs");
            Vertices = new LiteDbRepo<VertexEntity>(database, "Vertices");
            Neighbours = new LiteDbRepo<NeighboursEntity>(database, "Neighbours");
            VerticesCosts = new LiteDbRepo<CostRangeEntity>(database, "Costs");
        }

        public void SaveChanges()
        {
            
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
