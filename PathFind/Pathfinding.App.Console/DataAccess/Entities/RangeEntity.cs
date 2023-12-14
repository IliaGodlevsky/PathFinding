using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    [Table("Ranges")]
    internal class RangeEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(GraphEntity))]
        public int GraphId { get; set; }

        [Required]
        [ForeignKey(nameof(VertexEntity))]
        public int VertexId { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
