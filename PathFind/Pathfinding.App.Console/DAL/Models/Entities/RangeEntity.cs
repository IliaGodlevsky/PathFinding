using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Ranges)]
    [BsonTable(DbTables.Ranges)]
    internal class RangeEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        public int Id { get; set; }

        [Required]
        [IndexField]
        [ForeignKey(nameof(GraphEntity))]
        public int GraphId { get; set; }

        [Required]
        [ForeignKey(nameof(VertexEntity))]
        public int VertexId { get; set; }

        [Required]
        public int Position { get; set; }
    }
}
