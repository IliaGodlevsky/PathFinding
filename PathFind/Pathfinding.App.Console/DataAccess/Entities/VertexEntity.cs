using System.ComponentModel.DataAnnotations;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class VertexEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int GraphId { get; set; }

        [Required]
        public int Cost { get; set; }

        [Required]
        public bool IsObstacle { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }
    }
}
