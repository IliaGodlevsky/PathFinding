namespace Pathfinding.Service.Interface.Requests.Create
{
    public class CreateAlgorithmRunRequest
    {
        public int Id { get; set; }

        public int GraphId { get; set; }

        public string AlgorithmId { get; set; }
    }
}
