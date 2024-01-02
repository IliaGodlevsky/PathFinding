using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Graphs)]
    [BsonTable(DbTables.Graphs)]
    internal class GraphEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        [IndexField]
        public int Id { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int ObstaclesCount { get; set; }
    }
}
