using JsonFlatFileDataStore;
using Pathfinding.App.Console.Settings;
using System;
using System.IO;

namespace Pathfinding.App.Console.DataAccess
{
    internal sealed class DataContext : IDisposable
    {
        public DataStore Graphs { get; }

        public DataStore Informations { get; }

        public DataStore Algorithms { get; }

        public DataStore Visited { get; }

        public DataStore Obstacles { get; }

        public DataStore Ranges { get; }

        public DataStore Paths { get; }

        public DataStore Costs { get; }

        public DataStore Statistics { get; }

        public DataContext()
        {
            Graphs = new DataStore(CreateConnection(Connections.Default.GraphsConnection));
            Informations = new DataStore(CreateConnection(Connections.Default.InformationConnection));
            Algorithms = new DataStore(CreateConnection(Connections.Default.AlgorithmsConnection));
            Visited = new DataStore(CreateConnection(Connections.Default.VisitedConnection));
            Obstacles = new DataStore(CreateConnection(Connections.Default.ObstaclesConnection));
            Ranges = new DataStore(CreateConnection(Connections.Default.RangesConnection));
            Paths = new DataStore(CreateConnection(Connections.Default.PathsConnection));
            Costs = new DataStore(CreateConnection(Connections.Default.CostsConnection));
            Statistics = new DataStore(CreateConnection(Connections.Default.StatisticsConnection));
        }

        public void Dispose()
        {
            Graphs.Dispose();
            Informations.Dispose();
            Algorithms.Dispose();
            Visited.Dispose();
            Obstacles.Dispose();
            Ranges.Dispose();
            Paths.Dispose();
            Costs.Dispose(); 
            Statistics.Dispose();
        }

        private static string CreateConnection(string connectionString)
        {
            return Path.Combine(Connections.Default.Folder, connectionString);
        }
    }
}
