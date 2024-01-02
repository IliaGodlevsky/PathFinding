using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Vertices)]
    [BsonTable(DbTables.Vertices)]
    internal class VertexEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        [IndexField]
        public int Id { get; set; }

        [Required]
        [IndexField]
        [ForeignKey(nameof(GraphEntity))]
        public int GraphId { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public int UpperValueRange { get; set; }

        [Required]
        public int LowerValueRange { get; set; }

        [Required]
        public bool IsObstacle { get; set; }
    }
}
