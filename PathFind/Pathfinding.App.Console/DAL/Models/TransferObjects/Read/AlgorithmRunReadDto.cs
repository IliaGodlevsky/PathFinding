using System;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Read
{
    internal class AlgorithmRunReadDto
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public string AlgorithmId { get; set; }
    }
}
