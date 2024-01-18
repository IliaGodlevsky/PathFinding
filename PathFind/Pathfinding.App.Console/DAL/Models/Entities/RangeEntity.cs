using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Ranges)]
    [BsonTable(DbTables.Ranges)]
    internal class RangeEntity : IEntity
    {
        [BsonId]
        [NotNull]
        [Identity]
        public int Id { get; set; }

        [NotNull]
        [Index]
        [Reference(DbTables.Graphs, nameof(GraphEntity.Id),
            ReferenceAttribute.OnDeleteCascade)]
        public int GraphId { get; set; }

        [Unique]
        [NotNull]
        [Reference(DbTables.Vertices, nameof(VertexEntity.Id))]
        public int VertexId { get; set; }

        [NotNull]
        public int Position { get; set; }
    }
}
