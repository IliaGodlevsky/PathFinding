using System.ComponentModel.DataAnnotations;

namespace Pathfinding.App.Console.DataAccess.Entities
{
    internal class CostRangeEntity : IEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int VertexId { get; set; }

        [Required]
        public int UpperValueOfRange { get; set; }

        [Required]
        public int LowerValueOfRange { get; set; }
    }
}
