namespace Pathfinding.Service.Interface.Requests.Read
{
    public class ReadGraphRequest
    {
        public int GraphId { get; }

        public ReadGraphRequest(int graphId)
        {
            GraphId = graphId;
        }
    }
}
