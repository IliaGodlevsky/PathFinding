using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Neighbors)]
    [BsonTable(DbTables.Neighbors)]
    internal class NeighborEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        public int Id { get; set; }

        [Required]
        public int NeighborId { get; set; }

        [Required]
        [IndexField]
        public int VertexId { get; set; }
    }
}
