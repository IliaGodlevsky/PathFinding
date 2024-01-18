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
        public int Id { get; set; }

        [NotNull]
        [Reference(DbTables.Vertices, nameof(VertexEntity.Id))]
        public int NeighborId { get; set; }

        [NotNull]
        [Index]
        [Reference(DbTables.Vertices, nameof(VertexEntity.Id), 
            ReferenceAttribute.OnDeleteCascade)]
        public int VertexId { get; set; }
    }
}
