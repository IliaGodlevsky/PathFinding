using System.ComponentModel.DataAnnotations;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class PathfindingRangeEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int GraphId { get; set; }

        [Required]
        public int VertexId { get; set; }

        [Required]
        public int Order { get; set; }
    }
}
