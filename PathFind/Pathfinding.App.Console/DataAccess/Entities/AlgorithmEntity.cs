using LiteDB;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    [Table("Algorithms")]
    internal class AlgorithmEntity : IEntity
    {
        [Key]
        [BsonId]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(GraphEntity))]
        public int GraphId { get; set; }

        public string Statistics { get; set; }

        public string Path { get; set; }

        public string Obstacles { get; set; }

        public string Visited { get; set; }

        public string Range { get; set; }

        public string Costs { get; set; }
    }
}
