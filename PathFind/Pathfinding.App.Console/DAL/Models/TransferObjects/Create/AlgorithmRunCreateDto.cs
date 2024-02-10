using System;

namespace Pathfinding.App.Console.DAL.Models.TransferObjects.Create
{
    internal class AlgorithmRunCreateDto
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public string AlgorithmId { get; set; }
    }
}
