using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
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
