using LiteDB;
using Pathfinding.App.Console.DAL.Attributes;
using Pathfinding.App.Console.DAL.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DAL.Models.Entities
{
    [Table(DbTables.Vertices)]
    [BsonTable(DbTables.Vertices)]
    internal class VertexEntity : IEntity
    {
        [BsonId]
        [NotNull]
        [Identity]
        [IndexField]
        [Autoincrement]
        public int Id { get; set; }

        [NotNull]
        [IndexField]
        [OnDeleteCascade]
        [Reference(DbTables.Graphs, nameof(GraphEntity.Id))]
        public int GraphId { get; set; }

        [NotNull]
        public int X { get; set; }

        [NotNull]
        public int Y { get; set; }

        [NotNull]
        public int Cost { get; set; }

        [NotNull]
        public int UpperValueRange { get; set; }

        [NotNull]
        public int LowerValueRange { get; set; }

        [NotNull]
        public bool IsObstacle { get; set; }
    }
}
