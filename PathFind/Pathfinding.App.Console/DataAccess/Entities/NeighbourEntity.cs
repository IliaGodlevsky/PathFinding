using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    [Table("Neighbors")]
    internal class NeighbourEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(VertexEntity))]
        public int NeighbourId { get; set; }

        [Required]
        [ForeignKey(nameof(VertexEntity))]
        public int VertexId { get; set; }
    }
}
