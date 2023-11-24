using System.ComponentModel.DataAnnotations;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class AlgorithmEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int GraphId { get; set; }

        [Required]
        public string Statistics { get; set; }

        public string VisitedVertices { get; set; }

        public string Path { get; set; }

        [Required]
        public string Obstacles { get; set; }

        [Required]
        public string Costs { get; set; }

        [Required]
        public string Range { get; set; }
    }
}
