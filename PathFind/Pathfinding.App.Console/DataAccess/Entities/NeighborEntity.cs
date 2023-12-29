using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    [Table(DbTables.Neighbors)]
    [BsonTable(DbTables.Neighbors)]
    internal class NeighborEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        [IndexField]
        public int Id { get; set; }

        [Required]
        public int NeighborId { get; set; }

        [Required]
        public int VertexId { get; set; }
    }
}
