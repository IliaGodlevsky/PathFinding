using Pathfinding.App.Console.Model.Notes;
using System;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class StatisticEntity : IEntity<int>
    {
        public int Id { get; set; }

        public Guid AlgorithmId { get; set; }

        public Statistics Statistics { get; set; }
    }
}
