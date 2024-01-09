using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Neighbors)]
    [BsonTable(DbTables.Neighbors)]
    internal class NeighborEntity : IEntity
    {
        [BsonId]
        [NotNull]
        [Identity]
        [Autoincrement]
        public int Id { get; set; }

        [NotNull]
        public int NeighborId { get; set; }

        [NotNull]
        [IndexField]
        [OnDeleteCascade]
        [Reference(DbTables.Vertices, nameof(VertexEntity.Id))]
        public int VertexId { get; set; }
    }
}
