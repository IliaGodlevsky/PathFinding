using System.ComponentModel.DataAnnotations;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class GraphEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Length { get; set; }
    }
}
