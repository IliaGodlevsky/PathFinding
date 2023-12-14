using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    [Table("Graphs")]
    internal class GraphEntity : IEntity
    {
        [BsonId]
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int ObstaclesCount { get; set; }
    }
}
