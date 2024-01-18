using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Graphs)]
    [BsonTable(DbTables.Graphs)]
    internal class GraphEntity : IEntity
    {
        [BsonId]
        [NotNull]
        [Index]
        [Identity]
        public int Id { get; set; }

        [NotNull]
        public int Width { get; set; }

        [NotNull]
        public int Length { get; set; }

        [NotNull]
        public int ObstaclesCount { get; set; }
    }
}
